﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace StageRecovery
{
    public class EditorGUI
    {
        public List<EditorStatItem> stages = new List<EditorStatItem>();
        public bool showEditorGUI = false;
        bool highLight = false;
        public Rect EditorGUIRect = new Rect(Screen.width / 3, Screen.height / 3, 200, 1);
        public void DrawEditorGUI(int windowID)
        {
            GUILayout.BeginVertical();
            //provide toggles to turn highlighting on/off
            if (GUILayout.Button("Toggle Vessel Highlighting"))
            {
                highLight = !highLight;
                if (highLight)
                    HighlightAll();
                else
                    UnHighlightAll();
            }

            //list each stage, with info for each
            foreach (EditorStatItem stage in stages)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label("Stage " + stage.stageNumber);
                GUILayout.Label(stage.Velocity.ToString("N1")+" m/s");
                if (GUILayout.Button("Highlight"))
                {
                    //highlight this stage and unhighlight all others
                    UnHighlightAll();
                    stage.Highlight();
                }
                GUILayout.EndHorizontal();
            }

            GUILayout.EndVertical();

            //Make it draggable
            if (!Input.GetMouseButtonDown(1) && !Input.GetMouseButtonDown(2))
                GUI.DragWindow();
        }

        public void UnHighlightAll()
        {
            highLight = false;
            foreach (EditorStatItem stage in stages)
                stage.UnHighlight();
        }

        public void HighlightAll()
        {
            highLight = true;
            foreach (EditorStatItem stage in stages)
                stage.Highlight();
        }

        public void BreakShipIntoStages()
        {
            stages.Clear();
            //loop through the part tree and try to break it into stages
            List<Part> parts = EditorLogic.fetch.ship.parts;
            EditorStatItem current = new EditorStatItem();
            int stageNum = 0;
            bool realChuteInUse = false;

            StageParts stage = new StageParts();
            List<Part> RemainingDecouplers = new List<Part>() { parts[0] };
            while (RemainingDecouplers.Count > 0)
            {
                //determine stages from the decouplers
                Part parent = RemainingDecouplers[0];
                RemainingDecouplers.RemoveAt(0);
                stage = DetermineStage(parent);
                current = new EditorStatItem();
                current.stageNumber = stageNum++;
                current.parts = stage.parts;
                RemainingDecouplers.AddRange(stage.decouplers);

                //compute properties
                foreach (Part part in stage.parts)
                {
                    current.dryMass += part.mass;
                    current.mass += part.mass + part.GetResourceMass();

                    double pChutes = 0;
                    if (part.Modules.Contains("RealChuteModule"))
                    {
                        PartModule realChute = part.Modules["RealChuteModule"];
                        ConfigNode rcNode = new ConfigNode();
                        realChute.Save(rcNode);

                        //This is where the Reflection starts. We need to access the material library that RealChute has, so we first grab it's Type
                        Type matLibraryType = AssemblyLoader.loadedAssemblies
                            .SelectMany(a => a.assembly.GetExportedTypes())
                            .SingleOrDefault(t => t.FullName == "RealChute.Libraries.MaterialsLibrary");

                        //We make a list of ConfigNodes containing the parachutes (usually 1, but now there can be any number of them)
                        //We get that from the PPMS 
                        ConfigNode[] parachutes = rcNode.GetNodes("PARACHUTE");
                        //We then act on each individual parachute in the module
                        foreach (ConfigNode chute in parachutes)
                        {
                            //First off, the diameter of the parachute. From that we can (later) determine the Vt, assuming a circular chute
                            float diameter = float.Parse(chute.GetValue("deployedDiameter"));
                            //The name of the material the chute is made of. We need this to get the actual material object and then the drag coefficient
                            string mat = chute.GetValue("material");
                            //This grabs the method that RealChute uses to get the material. We will invoke that with the name of the material from before.
                            System.Reflection.MethodInfo matMethod = matLibraryType.GetMethod("GetMaterial", new Type[] { mat.GetType() });
                            //In order to invoke the method, we need to grab the active instance of the material library
                            object MatLibraryInstance = matLibraryType.GetProperty("instance").GetValue(null, null);
                            //With the library instance we can invoke the GetMaterial method (passing the name of the material as a parameter) to receive an object that is the material
                            object materialObject = matMethod.Invoke(MatLibraryInstance, new object[] { mat });
                            //With that material object we can extract the dragCoefficient using the helper function above.
                            float dragC = (float)StageRecovery.GetMemberInfoValue(materialObject.GetType().GetMember("dragCoefficient")[0], materialObject);
                            //Now we calculate the RCParameter. Simple addition of this doesn't result in perfect results for Vt with parachutes with different diameter or drag coefficients
                            //But it works perfectly for mutiple identical parachutes (the normal case)
                            pChutes += dragC * (float)Math.Pow(diameter, 2);
                        }
                        realChuteInUse = true;
                    }
                    else if (part.Modules.Contains("RealChuteFAR")) //RealChute Lite for FAR
                    {
                        PartModule realChute = part.Modules["RealChuteFAR"];
                        float diameter = (float)realChute.Fields.GetValue("deployedDiameter");
                        // = float.Parse(realChute.moduleValues.GetValue("deployedDiameter"));
                        float dragC = 1.0f; //float.Parse(realChute.moduleValues.GetValue("staticCd"));
                        pChutes += dragC * (float)Math.Pow(diameter, 2);

                        realChuteInUse = true;
                    }
                    else if (!realChuteInUse && part.Modules.Contains("ModuleParachute"))
                    {
                        double scale = 1.0;
                        //check for Tweakscale and modify the area appropriately
                        if (part.Modules.Contains("TweakScale"))
                        {
                            PartModule tweakScale = part.Modules["TweakScale"];
                            double currentScale = 100, defaultScale = 100;
                            double.TryParse(tweakScale.Fields.GetValue("currentScale").ToString(), out currentScale);
                            double.TryParse(tweakScale.Fields.GetValue("defaultScale").ToString(), out defaultScale);
                            scale = currentScale / defaultScale;
                        }

                        ModuleParachute mp = (ModuleParachute)part.Modules["ModuleParachute"];
                        //dragCoeff += part.mass * mp.fullyDeployedDrag;
                        pChutes += mp.areaDeployed * Math.Pow(scale, 2);
                    }

                    current.chuteArea += pChutes;
                }

                stages.Add(current);
            }

            Debug.Log("[SR] Found " + stageNum + " stages!");

           /* while (toCheck.Count > 0) //should instead search through the children, stopping when finding a decoupler, then switch to it's children
            {
                parent = toCheck[0];
                toCheck.RemoveAt(0);
                current.parts.Add(parent);
                current.dryMass += parent.mass;
                current.mass += parent.mass + parent.GetResourceMass();

                foreach (Part part in parent.children) //does this include the parent? Hopefully not //Is this recursive? If not, this will be easier but we need to account for that
                {
                    if (!part.Modules.Contains("ModuleDecouple"))
                    {
                        //get parachutes
                        double pChutes = 0;
                        if (part.Modules.Contains("RealChuteModule"))
                        {
                            PartModule realChute = part.Modules["RealChuteModule"];
                            ConfigNode rcNode = new ConfigNode();
                            realChute.Save(rcNode);

                            //This is where the Reflection starts. We need to access the material library that RealChute has, so we first grab it's Type
                            Type matLibraryType = AssemblyLoader.loadedAssemblies
                                .SelectMany(a => a.assembly.GetExportedTypes())
                                .SingleOrDefault(t => t.FullName == "RealChute.Libraries.MaterialsLibrary");

                            //We make a list of ConfigNodes containing the parachutes (usually 1, but now there can be any number of them)
                            //We get that from the PPMS 
                            ConfigNode[] parachutes = rcNode.GetNodes("PARACHUTE");
                            //We then act on each individual parachute in the module
                            foreach (ConfigNode chute in parachutes)
                            {
                                //First off, the diameter of the parachute. From that we can (later) determine the Vt, assuming a circular chute
                                float diameter = float.Parse(chute.GetValue("deployedDiameter"));
                                //The name of the material the chute is made of. We need this to get the actual material object and then the drag coefficient
                                string mat = chute.GetValue("material");
                                //This grabs the method that RealChute uses to get the material. We will invoke that with the name of the material from before.
                                System.Reflection.MethodInfo matMethod = matLibraryType.GetMethod("GetMaterial", new Type[] { mat.GetType() });
                                //In order to invoke the method, we need to grab the active instance of the material library
                                object MatLibraryInstance = matLibraryType.GetProperty("instance").GetValue(null, null);
                                //With the library instance we can invoke the GetMaterial method (passing the name of the material as a parameter) to receive an object that is the material
                                object materialObject = matMethod.Invoke(MatLibraryInstance, new object[] { mat });
                                //With that material object we can extract the dragCoefficient using the helper function above.
                                float dragC = (float)StageRecovery.GetMemberInfoValue(materialObject.GetType().GetMember("dragCoefficient")[0], materialObject);
                                //Now we calculate the RCParameter. Simple addition of this doesn't result in perfect results for Vt with parachutes with different diameter or drag coefficients
                                //But it works perfectly for mutiple identical parachutes (the normal case)
                                pChutes += dragC * (float)Math.Pow(diameter, 2);
                            }
                            realChuteInUse = true;
                        }
                        else if (part.Modules.Contains("RealChuteFAR")) //RealChute Lite for FAR
                        {
                            PartModule realChute = part.Modules["RealChuteFAR"];
                            float diameter = (float)realChute.Fields.GetValue("deployedDiameter");
                            // = float.Parse(realChute.moduleValues.GetValue("deployedDiameter"));
                            float dragC = 1.0f; //float.Parse(realChute.moduleValues.GetValue("staticCd"));
                            pChutes += dragC * (float)Math.Pow(diameter, 2);

                            realChuteInUse = true;
                        }
                        else if (!realChuteInUse && part.Modules.Contains("ModuleParachute"))
                        {
                            double scale = 1.0;
                            //check for Tweakscale and modify the area appropriately
                            if (part.Modules.Contains("TweakScale"))
                            {
                                PartModule tweakScale = part.Modules["TweakScale"];
                                double currentScale = 100, defaultScale = 100;
                                double.TryParse(tweakScale.Fields.GetValue("currentScale").ToString(), out currentScale);
                                double.TryParse(tweakScale.Fields.GetValue("defaultScale").ToString(), out defaultScale);
                                scale = currentScale / defaultScale;
                            }

                            ModuleParachute mp = (ModuleParachute)part.Modules["ModuleParachute"];
                            //dragCoeff += part.mass * mp.fullyDeployedDrag;
                            pChutes += mp.areaDeployed * Math.Pow(scale, 2);
                        }

                        current.chuteArea += pChutes;
                        current.parts.Add(part);
                        toCheck.Add(part);
                    }
                    else
                    {
                        //stages.Add(current);
                        //start a new stage
                        //current = new EditorStatItem();
                        //stageNum++;
                        //current.stageNumber = stageNum;
                        //parent = part;
                       // break;
                        //If children isn't recursive, then reparent here and finish the foreach loop (perhaps exclude the decoupler too)
                        DecouplersToCheck.Add(part);
                    }

                    current.dryMass += part.mass;
                    current.mass += part.mass + part.GetResourceMass();
                }
                stages.Add(current);
                if (parent.children.Count == 0)
                    break;
                else
                {
                    //start a new stage
                    current = new EditorStatItem();
                    stageNum++;
                    current.stageNumber = stageNum;
                }
            }*/
        }

        StageParts DetermineStage(Part parent)
        {
            StageParts stage = new StageParts();
            List<Part> toCheck = new List<Part>() { parent };
            while (toCheck.Count > 0) //should instead search through the children, stopping when finding a decoupler, then switch to it's children
            {
                Part checking = toCheck[0];
                toCheck.RemoveAt(0);
                stage.parts.Add(checking);

                foreach (Part part in checking.children)
                {
                    //search for decouplers
                    if (part.Modules.Contains("ModuleDecouple") || part.Modules.Contains("ModuleAnchoredDecoupler"))
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
    }

    public class StageParts
    {
        public List<Part> parts = new List<Part>();
        public List<Part> decouplers = new List<Part>();
    }

    public class EditorStatItem
    {
        public int stageNumber=0;
        public double dryMass=0, mass=0, chuteArea=0;
        private double _Velocity = -1;
        private bool highlighted = false;
        public double Velocity
        {
            get
            {
                if (_Velocity < 0)
                    _Velocity = GetVelocity();
                return _Velocity;
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

        private double GetVelocity()
        {
            return StageRecovery.VelocityEstimate(mass, chuteArea);
        }

        public void Highlight()
        {
            UnityEngine.Color stageColor = UnityEngine.Color.red;
            if (Velocity < Settings.instance.HighCut)
                stageColor = UnityEngine.Color.yellow;
            if (Velocity < Settings.instance.LowCut)
                stageColor = UnityEngine.Color.green;
            foreach (Part p in parts)
            {
                p.SetHighlight(true, false);
                p.SetHighlightColor(stageColor);
                p.SetHighlightType(Part.HighlightType.AlwaysOn);
            }
            highlighted = true;
        }

        public void UnHighlight()
        {
            foreach (Part p in parts)
            {
                p.SetHighlightColor(UnityEngine.Color.green);
                p.SetHighlight(false, true);
            }
            highlighted = false;
        }

        public bool ToggleHighlight()
        {
            if (highlighted)
                UnHighlight();
            else
                Highlight();

            return highlighted;
        }
    }
}
