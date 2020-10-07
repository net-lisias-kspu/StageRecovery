/*
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
using KSP.UI.Screens;
using System;
using UnityEngine;
using ToolbarControl_NS;
using ClickThroughFix;

namespace StageRecovery
{

    //This class controls all the GUI elements for the in-game settings menu
    public class SettingsGUI
    {
        public FlightGUI flightGUI = new FlightGUI();

        public EditorGUI editorGUI = new EditorGUI();

        //The window is only shown when this is true
        private bool showWindow;
        private bool showBlacklist;

        //The width of the window, for easy changing later if need be
        private static int windowWidth = 300;
        //The main Rect object that the window occupies
        public Rect mainWindowRect = new Rect((Screen.width - windowWidth)/2, Screen.height / 2, windowWidth, 1);
        public Rect blacklistRect = new Rect(0, 0, 360, 1);


        private Vector2 scrollPos;

        static internal ToolbarControl toolbarControl;
        internal const string MODID = "StageRecovery_NS";
        internal const string MODNAME = "Stage Recovery";

        const string ButtonLoc = "StageRecovery/PluginData/icon";
        internal void InitializeToolbar(GameObject go)
        {
            ApplicationLauncher.AppScenes spaceCenter = 0;

               Log.Info("[SR]  InitializeToolbar");
            if (toolbarControl == null)
            {
                if (!Settings1.Instance.hideSpaceCenterButton)
                    spaceCenter = ApplicationLauncher.AppScenes.SPACECENTER;
                toolbarControl = go.AddComponent<ToolbarControl>();
                toolbarControl.AddToAllToolbars(
                    ShowWindow,
                    hideAll,
                    OnHoverOn,
                    OnHoverOff,
                    null,
                    null,
                    (
                    spaceCenter | 
                    ApplicationLauncher.AppScenes.FLIGHT | ApplicationLauncher.AppScenes.SPH | ApplicationLauncher.AppScenes.VAB | ApplicationLauncher.AppScenes.MAPVIEW),
                    MODID,
                    "stageControlButton",
                    ButtonLoc + "-38",
                    ButtonLoc + "-24",
                    MODNAME
                );

            }
        }
        internal void DoOnDestroy()
        {
            Log.Info("[SR] StageRecovery.SettingsGUI.OnDestroy");
            if (toolbarControl != null)
            {
                toolbarControl.OnDestroy();
                GameObject.Destroy(toolbarControl);
                toolbarControl = null;
            }
        }

        //When the button is hovered over, show the flight GUI if in flight
        public void OnHoverOn()
        {
            if (HighLogic.LoadedSceneIsFlight)
            {
                flightGUI.showFlightGUI = true;
            }
        }

        //When the button is no longer hovered over, hide the flight GUI if it wasn't clicked
        public void OnHoverOff()
        {
            if (HighLogic.LoadedSceneIsFlight && !Settings.Instance.Clicked)
            {
                flightGUI.showFlightGUI = false;
            }
        }

        //This shows the correct window depending on the current scene
        public void ShowWindow()
        {
            Settings.Instance.Clicked = true;
            switch (HighLogic.LoadedScene)
            {
                case GameScenes.FLIGHT:
                    flightGUI.showFlightGUI = true;
                    break;
                case GameScenes.EDITOR:
                    EditorCalc();
                    break;
                case GameScenes.SPACECENTER:
                    showWindow = true;
                    break;
            }
            if (HighLogic.LoadedSceneIsFlight)
            {
                flightGUI.showFlightGUI = true;
            }
            else if (HighLogic.LoadedSceneIsEditor)
            {
                EditorCalc();
            }
        }

        //Does stuff to draw the window.
        public void SetGUIPositions()
        {
            if (flightGUI.showFlightGUI)
            {
                flightGUI.flightWindowRect = ClickThruBlocker.GUILayoutWindow(8940, flightGUI.flightWindowRect, flightGUI.DrawFlightGUI, "StageRecovery", HighLogic.Skin.window);
            }

            if (showBlacklist)
            {
                blacklistRect = ClickThruBlocker.GUILayoutWindow(8941, blacklistRect, DrawBlacklistGUI, "Ignore List", HighLogic.Skin.window);
            }
            if (showWindow)
            {
                mainWindowRect = ClickThruBlocker.GUILayoutWindow(8940, mainWindowRect, DrawSettingsGUI, "StageRecovery", HighLogic.Skin.window);
            }
            if (editorGUI.showEditorGUI)
            {
                editorGUI.EditorGUIRect = ClickThruBlocker.GUILayoutWindow(8940, editorGUI.EditorGUIRect, editorGUI.DrawEditorGUI, "StageRecovery", HighLogic.Skin.window);
            }
        }

        //Hide all the windows. We only have one so this isn't super helpful, but alas.
        public void hideAll()
        {
            showWindow = false;
            flightGUI.showFlightGUI = false;
            editorGUI.showEditorGUI = false;
            showBlacklist = false;
            Settings.Instance.Clicked = false;
            editorGUI.UnHighlightAll();
        }

#if false
        //Resets the windows. Hides them and resets the Rect object. Not really needed, but it's here
        public void reset()
        {
            hideAll();
            mainWindowRect = new Rect(0, 0, windowWidth, 1);
            flightGUI.flightWindowRect = new Rect((Screen.width - 768) / 2, (Screen.height - 540) / 2, 768, 540);
            editorGUI.EditorGUIRect = new Rect(Screen.width / 3, Screen.height / 3, 200, 1);
            blacklistRect = new Rect(0, 0, 360, 1);
        }
#endif
        private void DrawSettingsGUI(int windowID)
        {
            GUILayout.BeginVertical();
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label("Settings are now in the stock settings");
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            GUILayout.Label("(old settings, if any, were NOT migrated)");
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
            GUILayout.Space(10);
            GUILayout.BeginHorizontal();
            GUILayout.FlexibleSpace();
            if (GUILayout.Button("Close", GUILayout.Width(60)))
            {
                showWindow = false;
            }
            GUILayout.FlexibleSpace();
            GUILayout.EndHorizontal();
          
            GUILayout.EndVertical();
            GUI.DragWindow();
        }

        private string tempListItem = "";
        private void DrawBlacklistGUI(int windowID)
        {
            GUILayout.BeginVertical();
            scrollPos = GUILayout.BeginScrollView(scrollPos, HighLogic.Skin.textArea, GUILayout.Height(Screen.height / 4));
            foreach (string s in Settings.Instance.BlackList.ignore)
            {
                GUILayout.BeginHorizontal();
                GUILayout.Label(s);
                if (GUILayout.Button("Remove", GUILayout.ExpandWidth(false)))
                {
                    Settings.Instance.BlackList.Remove(s);
                    break;
                }
                GUILayout.EndHorizontal();
            }
            GUILayout.EndScrollView();
            GUILayout.BeginHorizontal();
            tempListItem = GUILayout.TextField(tempListItem);
            if (GUILayout.Button("Add", GUILayout.ExpandWidth(false)))
            {
                Settings.Instance.BlackList.Add(tempListItem);
                tempListItem = "";
            }
            GUILayout.EndHorizontal();
            GUILayout.BeginHorizontal();
            if (GUILayout.Button("Save"))
            {
                Settings.Instance.BlackList.Save();
                showBlacklist = false;
            }
            if (GUILayout.Button("Cancel"))
            {
                Settings.Instance.BlackList.Load();
                showBlacklist = false;
            }
            GUILayout.EndHorizontal();
            GUILayout.EndVertical();

            if (!Input.GetMouseButtonDown(1) && !Input.GetMouseButtonDown(2))
            {
                GUI.DragWindow();
            }
        }


        public void EditorCalc()
        {
            if (EditorLogic.fetch.ship.parts.Count > 0)
            {
                editorGUI.BreakShipIntoStages();
                editorGUI.HighlightAll();
                editorGUI.showEditorGUI = true;
                editorGUI.EditorGUIRect.height = 1; //reset the height
            }
        }

    }
}
