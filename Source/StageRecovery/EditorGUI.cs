﻿/*
    This file is part of Stage Recovery /L
    © 2020 LisiasT
    © 2014-2018 magico13

    Stage Recovery /L licensed as follows:

    * GPL 3.0 : https://www.gnu.org/licenses/gpl-3.0.txt

    And you are allowed to choose the License that better suit your needs.

    Stage Recovery /L is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.

    You should have received a copy of the GNU General Public License 3.0
    along with Stage Recovery /L. If not, see <https://www.gnu.org/licenses/>.
*/
/*
 * Contains code licensed under the MIT 
 * © 2018-2020 LinuxGuruGamer
*/
using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using UnityEngine;


namespace StageRecovery
{
    public class EditorGUI
    {
        static internal EditorGUI Instance;

        public List<EditorStatItem> stages = new List<EditorStatItem>();
        public bool showEditorGUI = false;
        bool highLight = false, tanksDry = true;
        public Rect EditorGUIRect = new Rect(Screen.width / 3, Screen.height / 3, 250, 1);

        public EditorGUI()
        {
            Instance = this;
        }

        public void DrawEditorGUI(int windowID)
        {
            GUILayout.BeginVertical();
            //provide toggles to turn highlighting on/off
            if (GUILayout.Button("Toggle Vessel Highlighting"))
            {
                highLight = !highLight;
                if (highLight)
                {
                    HighlightAll();
                }
                else
                {
                    UnHighlightAll();
                }
            }

            if (GUILayout.Button("Tanks: " + (tanksDry ? "Empty" : "Full")))
            {
                tanksDry = !tanksDry;
                if (highLight)
                {
                    HighlightAll();
                }
            }

            //list each stage, with info for each
            for (int i = 0; i < stages.Count; i++)
            //foreach (EditorStatItem stage in stages)
            {
                EditorStatItem stage = stages[i];

                GUILayout.BeginHorizontal();
                GUILayout.Label("Stage " + stage.stageNumber);
                double vel = tanksDry ? stage.EmptyVelocity : stage.FullVelocity;
                GUILayout.Label(vel.ToString("N1") + " m/s");
                GUILayout.Label(stage.GetRecoveryPercent(tanksDry) + "%");
                //    GUILayout.Label("("+stage.FullVelocity.ToString("N1") + ")");
                if (GUILayout.Button("Highlight"))
                {
                    //highlight this stage and unhighlight all others
                    bool status = stage.Highlighted;
                    if (highLight)
                    {
                        status = false;
                    }

                    UnHighlightAll();
                    stage.SetHighlight(!status, tanksDry);
                }
                GUILayout.EndHorizontal();
            }


            if (GUILayout.Button("Recalculate"))
            {
                Recalculate();
#if false
                BreakShipIntoStages();
                if (highLight)
                {
                    HighlightAll();
                }

                EditorGUIRect.height = 1; //reset the height so it is the smallest size it needs to be 
#endif
            }
            GUILayout.EndVertical();

            /* if (GUI.Button(new Rect(EditorGUIRect.xMax-10, EditorGUIRect.yMin, 10, 10), "X"))
             {
                 UnHighlightAll();
                 showEditorGUI = false;
             }*/

            //Make it draggable
            if (!Input.GetMouseButtonDown(1) && !Input.GetMouseButtonDown(2))
            {
                GUI.DragWindow();
            }
        }

        internal void Recalculate()
        {
            BreakShipIntoStages();
            if (highLight)
            {
                HighlightAll();
            }

            EditorGUIRect.height = 1; //reset the height so it is the smallest size it needs to be 
        }

        public void UnHighlightAll()
        {
            highLight = false;
            for (int i = 0; i < stages.Count; i++)
            //foreach (EditorStatItem stage in stages)
            {
                EditorStatItem stage = stages[i];
                stage.UnHighlight();
            }
        }

        public void HighlightAll()
        {
            highLight = true;
            for (int i = 0; i < stages.Count; i++)
            //foreach (EditorStatItem stage in stages)
            {
                EditorStatItem stage = stages[i];
                stage.Highlight(tanksDry);
            }
        }

        public void BreakShipIntoStages()
        {
            stages.Clear();
            //loop through the part tree and try to break it into stages
            List<Part> parts = EditorLogic.fetch.ship.parts;
            EditorStatItem current = new EditorStatItem();
            int stageNum = 0;

            StageParts stage = new StageParts();
            List<Part> RemainingDecouplers = null; // = new List<Part>() { parts[0] };

            for (int i = 0; i < parts.Count; i++)
            //foreach (var p in parts)
            {
                Part p = parts[i];
                if (p.parent == null)
                {
                    RemainingDecouplers = new List<Part>() { p };
                    break;
                }
            }
            if (RemainingDecouplers == null)
            {
                Log.Error("No parent part found");
                return;
            }
            while (RemainingDecouplers.Count > 0)
            {
                //determine stages from the decouplers
                Part parent = RemainingDecouplers[0];
                RemainingDecouplers.RemoveAt(0);
                stage = DetermineStage(parent);
                current = new EditorStatItem
                {
                    stageNumber = stageNum++,
                    parts = stage.parts
                };
                Log.Info("Parent part: " + parent.partInfo.title);
                foreach (var d in stage.decouplers)
                    Log.Info("Child decouplers: " + d.partInfo.title);
                RemainingDecouplers.AddRange(stage.decouplers);

                //compute properties
                double dryMass = 0;
                double wetMass = 0;

                stage.parts.ForEach(p => { dryMass += p.mass; wetMass += p.mass + p.GetResourceMass(); });
                current.dryMass = dryMass;
                current.mass = wetMass;
                current.chuteArea = StageRecovery.GetChuteArea(stage.parts);

                stages.Add(current);
            }

            ConsolidateStages();
            Log.Info("[SR] Found " + stages.Count + " stages!");
        }
        PartModule pm = null;
        bool CheckForEnginePlate(Part parent, Part checking)
        {
            // The following code is designed to deal with the new engine plates in MakingHistory, which have embedded
            // decouplers which work on the stage below
            ModuleDecouple md = null;

            // First, if this is an engineplate, do not treat it as a decoupler
            if (checking.Modules.Contains("ModuleDynamicNodes") && checking.Modules.Contains("ModuleDecouple"))
            {
                md = checking.Modules.GetModule<ModuleDecouple>();

                if (md.isEnginePlate)
                {
                    // If the parent IS an engineplate, get the module and use it later
                    if (parent.Modules.Contains("ModuleDynamicNodes") && parent.Modules.Contains("ModuleDecouple"))
                        pm = parent.Modules.GetModule<ModuleDecouple>();
                    else
                        pm = null;
                }

                return md.isEnginePlate;
            }
            return false;
        }

        bool CheckForParentEnginePlate(Part part)
        {
            // If the parent IS an engineplate, and this part is NOT a decoupler, then treat it as if it WAS 
            // a decoupler and add it to the list
            if (pm != null && ((ModuleDecouple)pm).isEnginePlate && part.FindModulesImplementing<IStageSeparator>().Count == 0)
            {
                return true;
            }
            return false;
        }
        StageParts DetermineStage(Part parent)
        {
            Log.Info("DetermineStage 1 parent: " + parent.partInfo.title);
            StageParts stage = new StageParts();
            List<Part> toCheck = new List<Part>() { parent };
            while (toCheck.Count > 0) //should instead search through the children, stopping when finding a decoupler, then switch to it's children
            {
                Part checking = toCheck[0];
                toCheck.RemoveAt(0);
                bool isEnginePlate = false;
                if (Versioning.version_major == 1 && Versioning.version_minor >= 7)
                {
                    isEnginePlate = CheckForEnginePlate(parent, checking);
                }

                stage.parts.Add(checking);

                for (int i = 0; i < checking.children.Count; i++)
                //foreach (Part part in checking.children)
                {
                    Part part = checking.children[i];

                    //search for decouplers
                    //if (part.Modules.Contains("ModuleDecouple") || part.Modules.Contains("ModuleAnchoredDecoupler"))

                    if (Versioning.version_major == 1 && Versioning.version_minor >= 7 && isEnginePlate)
                    {
                        if (CheckForParentEnginePlate(part))
                        {
                            stage.decouplers.Add(part);
                            continue;
                        }
                    }

                    // If this part is a decoupler, add it to the list
                    if (part.FindModulesImplementing<IStageSeparator>().Count > 0)
                    {
                        stage.decouplers.Add(part);
                    }
                    else
                    {
                        toCheck.Add(part);
                    }
                }
            }
            return stage;
        }

        public void ConsolidateStages()
        {
            //finds identical (and adjacent) stages in the list and merges them into a single master stage
            //must find all identical stages first, then merge

            EditorStatItem compareItem = null;

            for (int i = 0; i < stages.Count; i++)
            {
                EditorStatItem stage = stages[i];
                //  if (compareItem == null)
                compareItem = stage;

                int j = i + 1;
                while (j < stages.Count)
                {
                    if (stages[j].parts.Count != compareItem.parts.Count || stages[j].mass != compareItem.mass || stages[j].chuteArea != compareItem.chuteArea)
                    {
                        //probably not the same stage setup
                        break;
                    }
                    j++;
                }

                if (j > i + 1)
                {
                    Log.Info("[SR] Found " + (j - i) + " identical stages");
                    //some stages are the same (up to j)
                    //merge the stages
                    for (int k = j - 1; k > i; k--)
                    {
                        //add the parts from k to i
                        stages[i].parts.AddRange(stages[k].parts);
                        stages.RemoveAt(k);
                    }
                    stages[i].ForceRecalculate();
                }
            }
        }
    }

    public class StageParts
    {
        public List<Part> parts = new List<Part>();
        public List<Part> decouplers = new List<Part>();
    }

    public class EditorStatItem
    {
        public int stageNumber = 0;
        public double dryMass = 0, mass = 0, chuteArea = 0;
        private double _FullVelocity = -1, _DryVelocity = -1;
        private bool _highlighted = false;

        public bool Highlighted
        {
            get
            {
                return _highlighted;
            }
        }
        public double FullVelocity
        {
            get
            {
                if (_FullVelocity < 0)
                {
                    _FullVelocity = GetVelocity(false);
                }

                return _FullVelocity;
            }
        }

        public double EmptyVelocity
        {
            get
            {
                if (_DryVelocity < 0)
                {
                    _DryVelocity = GetVelocity(true);
                }

                return _DryVelocity;
            }
        }


        public List<Part> parts = new List<Part>();

        public void Set(List<Part> StageParts, int StageNum, double DryMass, double Mass, double ChuteArea)
        {
            parts = StageParts;
            stageNumber = StageNum;
            dryMass = DryMass;
            mass = Mass;
            chuteArea = ChuteArea;
        }

        private double GetVelocity(bool dry = true)
        {
            if (dry)
            {
                return StageRecovery.VelocityEstimate(dryMass, chuteArea);
            }
            else
            {
                return StageRecovery.VelocityEstimate(mass, chuteArea);
            }
        }

        public double GetRecoveryPercent(bool dry = true)
        {
            double Vt = GetVelocity(dry);
            bool recovered = false;
            if (Settings1.Instance.FlatRateModel)
            {
                recovered = Vt < Settings2.Instance.CutoffVelocity;
            }
            else
            {
                recovered = Vt < Settings2.Instance.HighCut;
            }

            if (!recovered)
            {
                return 0;
            }

            double recoveryPercent = 0;
            if (recovered && Settings1.Instance.FlatRateModel)
            {
                recoveryPercent = 1;
            }
            else if (recovered && !Settings1.Instance.FlatRateModel)
            {
                recoveryPercent = RecoveryItem.GetVariableRecoveryValue(Vt);
            }

            return Math.Round(100 * recoveryPercent, 2);
        }

        public void Highlight(bool dry = true)
        {
            double vel = dry ? EmptyVelocity : FullVelocity;
            UnityEngine.Color stageColor = UnityEngine.Color.red;
            if (vel < Settings2.Instance.HighCut)
            {
                stageColor = UnityEngine.Color.yellow;
            }

            if (vel < Settings2.Instance.LowCut)
            {
                stageColor = UnityEngine.Color.green;
            }
            //Part p = parts[0];
            foreach (Part p in parts)
            {
                p.SetHighlight(true, false);
                p.SetHighlightColor(stageColor);
                p.SetHighlightType(Part.HighlightType.AlwaysOn);
            }
            _highlighted = true;
        }

        public void UnHighlight()
        {
            foreach (Part p in parts)
            {
                //p.SetHighlightColor(UnityEngine.Color.green);
                //p.SetHighlight(false, false);
                //p.SetHighlightType();
                p.SetHighlightDefault();
            }
            _highlighted = false;
        }

        public void SetHighlight(bool status, bool dry = true)
        {
            if (status)
            {
                Highlight(dry);
            }
            else
            {
                UnHighlight();
            }
        }

        public bool ToggleHighlight()
        {
            if (_highlighted)
            {
                UnHighlight();
            }
            else
            {
                Highlight();
            }

            return _highlighted;
        }

        public void ForceRecalculate()
        {
            _FullVelocity = GetVelocity(false);
            _DryVelocity = GetVelocity(true);
        }
    }
}
