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
using KSP.UI.Screens;
using System;
using UnityEngine;
using ToolbarControl_NS;
using ClickThroughFix;

namespace StageRecovery
{
    [KSPAddon(KSPAddon.Startup.MainMenu, true)]
    public class RegisterToolbar : MonoBehaviour
    {
        void Start()
        {
            ToolbarControl.RegisterMod(SettingsGUI.MODID, SettingsGUI.MODNAME);
        }
    }
}
