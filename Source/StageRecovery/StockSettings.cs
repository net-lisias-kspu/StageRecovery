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
/*
 * Contains code licensed under the MIT
 * © 2018-2020 LinuxGuruGamer
*/
using System;
using System.IO;
using System.Collections;
using System.Reflection;


namespace StageRecovery
{
    // http://forum.kerbalspaceprogram.com/index.php?/topic/147576-modders-notes-for-ksp-12/#comment-2754813
    // search for "Mod integration into Stock Settings

    public class SR1 : GameParameters.CustomParameterNode
    {
        public override string Title { get { return ""; } }
        public override GameParameters.GameMode GameMode { get { return GameParameters.GameMode.ANY; } }
        public override string Section { get { return "Stage Recovery"; } }
        public override string DisplaySection { get { return "Stage Recovery"; } }
        public override int SectionOrder { get { return 1; } }
        public override bool HasPresets { get { return false; } }

        internal static bool settingsFlatRateModel = false;
        internal static bool settingsUseDREVelocity = true;
        internal static bool settingsUseDistanceOverride = false;

        [GameParameters.CustomParameterUI("Mod Enabled")]
        public bool SREnabled = true;


        [GameParameters.CustomParameterUI("Use Distance Override",
            toolTip = "Enable Distance Override to use a specified value for distance modification rather than calculating it")]
        public bool UseDistanceOverride = false;

        [GameParameters.CustomParameterUI("Enable Flat Rate Model",
            toolTip = "Disabled this to use a Variable Rate Model")]
        public bool FlatRateModel = false;

        [GameParameters.CustomParameterUI("Use the DRE Velocity")]
        public bool UseDREVelocity = true;


        [GameParameters.CustomParameterUI("Pre-Recover Vessels",
            toolTip = "Recover Kerbals before a ship is deleted")]
        public bool PreRecover = true;

        [GameParameters.CustomParameterUI("Failure Messages")]
        public bool ShowFailureMessages = true;

        [GameParameters.CustomParameterUI("Success Messages")]
        public bool ShowSuccessMessages = true;

        [GameParameters.CustomParameterUI("Try Powered Recovery")]
        public bool PoweredRecovery = true;

        [GameParameters.CustomParameterUI("Recover Clamps")]
        public bool RecoverClamps = true;

        [GameParameters.CustomParameterUI("Tie Into Upgrades")]
        public bool UseUpgrades = true;

        [GameParameters.CustomParameterUI("Hide the SpaceCenter button",
            toolTip = "The button merely opens a window directing you to these settings pages")]
        public bool hideSpaceCenterButton = false;




        public override bool Interactible(MemberInfo member, GameParameters parameters)
        {

            settingsFlatRateModel = FlatRateModel;
            settingsUseDREVelocity = UseDREVelocity;
            settingsUseDistanceOverride = UseDistanceOverride;

            return true;
        }
        public override IList ValidValues(MemberInfo member)
        {
            return null;
        }
    }
    /// <summary>
    ///
    /// </summary>

    public class SR2 : GameParameters.CustomParameterNode
    {
        public override string Title { get { return "Rate Model Settings"; } }
        public override GameParameters.GameMode GameMode { get { return GameParameters.GameMode.ANY; } }
        public override string Section { get { return "Stage Recovery"; } }
        public override string DisplaySection { get { return "Stage Recovery"; } }
        public override int SectionOrder { get { return 2; } }
        public override bool HasPresets { get { return false; } }

        /// ///////////////
        // RecoveryModifier
        /// ///////////////
        public float recoveryModifier = 0.75f;
        [GameParameters.CustomFloatParameterUI("Flat Rate: Recovery Modifier (%)", minValue = 0.0f, maxValue = 100.0f,
         toolTip = "Modifies recovery payout by this percentage")]
        public float RecoveryMod
        {
            get { return recoveryModifier * 100; }
            set { recoveryModifier = value / 100.0f; }
        }
        public float RecoveryModifier
        {
            get { return recoveryModifier; }
            set { recoveryModifier = value; }
        }
        /// ///////////////




        [GameParameters.CustomFloatParameterUI("Flat Rate: Cutoff Velocity", minValue = 2.0f, maxValue = 12.0f, displayFormat = "F1",
         toolTip = "Maximum velocity for recovery")]
        public double CutoffVelocity = 10f;


        [GameParameters.CustomFloatParameterUI("Variable Rate: High Cutoff Velocity", minValue = 2.0f, maxValue = 12.0f, displayFormat = "F1",
         toolTip = "Maximum velocity for recovery")]
        public double HighCut = 12f;

        [GameParameters.CustomFloatParameterUI("Variable Rate: Low Cutoff Velocity", minValue = 2.0f, maxValue = 12.0f, displayFormat = "F1",
         toolTip = "Maximum velocity for total recovery")]
        public double LowCut = 6f;



        public override bool Enabled(MemberInfo member, GameParameters parameters)
        {
            HighCut = (float)Math.Max(Math.Round(HighCut, 1), LowCut + 0.1);

            return true; //otherwise return true
        }

        public override bool Interactible(MemberInfo member, GameParameters parameters)
        {
            //if (HighLogic.CurrentGame == null || HighLogic.CurrentGame.Parameters == null)
            //    return true;

            if (SR1.settingsFlatRateModel)
            {
                if (member.Name == "HighCut" ||
                    member.Name == "LowCut")
                    return false;
            }
            else
            {
                if (member.Name == "RecoveryMod" ||
                    member.Name == "CutoffVelocity")
                    return false;

            }
            return true;
        }
        public override IList ValidValues(MemberInfo member)
        {
            return null;
        }
    }

    /////
    /////

    public class SR3 : GameParameters.CustomParameterNode
    {
        public override string Title { get { return ""; } } // column heading
        public override GameParameters.GameMode GameMode { get { return GameParameters.GameMode.ANY; } }
        public override string Section { get { return "Stage Recovery"; } }
        public override string DisplaySection { get { return "Stage Recovery"; } }
        public override int SectionOrder { get { return 3; } }
        public override bool HasPresets { get { return false; } }

        /// /////////////
        // GlobalModifier
        /// /////////////
        public float globalModifier = 1.0f;
        [GameParameters.CustomFloatParameterUI("Global Modifier (%)", minValue = 0.0f, maxValue = 100.0f,
         toolTip = "Modifies final payout by this percentage")]
        public float GlobalMod
        {
            get { return globalModifier * 100; }
            set { globalModifier = value / 100.0f; }
        }
        public float GlobalModifier
        {
            get { return globalModifier; }
            set { globalModifier = value; }
        }

        /// ///////////////
        // DistanceOverride
        /// ///////////////
        public float distanceOverride = 0.01f;
        [GameParameters.CustomFloatParameterUI("Distance Override (%)", minValue = 1f, maxValue = 100.0f,
                 toolTip = "If >= 0, will use this as a distance modifier instead of calculating it")]
        public float DistanceOver
        {
            get { return distanceOverride * 100; }
            set { distanceOverride = value / 100.0f; }
        }
        public float DistanceOverride
        {
            get { return distanceOverride; }
            set { distanceOverride = value; }
        }


        /// ///////////////////////
        // DeadlyReentrymaxVelocity
        /// ///////////////////////
        public float DeadlyReentryMaxVelocity = 2000f;
        [GameParameters.CustomIntParameterUI("DRE Velocity 2", minValue = 0, maxValue = 6000, stepSize = 200,
                 toolTip = "If >= 0, will use this as a distance modifier instead of calculating it")]
        public int DreVelocity
        {
            get { return (int)DeadlyReentryMaxVelocity; }
            set { DeadlyReentryMaxVelocity = (float)value; }
        }


        [GameParameters.CustomFloatParameterUI("Powered TWR", minValue = 1.0f, maxValue = 12.0f, stepCount = 111, displayFormat = "F1",
        toolTip = "Minimum TWR needed for a powered recovery")]
        public double MinTWR = 1.0f;


        public override bool Enabled(MemberInfo member, GameParameters parameters)
        {
            return true; //otherwise return true
        }

        public override bool Interactible(MemberInfo member, GameParameters parameters)
        {
            //if (HighLogic.CurrentGame == null || HighLogic.CurrentGame.Parameters == null)
            //    return true;

            if (member.Name == "DreVelocity")
                return SR1.settingsUseDREVelocity;
            if (member.Name == "DistanceOver")
                return SR1.settingsUseDistanceOverride;
            return true;
        }

        public override IList ValidValues(MemberInfo member)
        {
            return null;
        }
    }




    public sealed class Settings1
    {
        public static SR1 Instance
        {
            get
            {
                return HighLogic.CurrentGame.Parameters.CustomParams<SR1>();
            }
        }
    }

    public sealed class Settings2
    {
        public static SR2 Instance
        {
            get
            {
                return HighLogic.CurrentGame.Parameters.CustomParams<SR2>();
            }
        }
    }

    public sealed class Settings3
    {
        public static SR3 Instance
        {
            get
            {
                return HighLogic.CurrentGame.Parameters.CustomParams<SR3>();
            }
        }
    }
}
