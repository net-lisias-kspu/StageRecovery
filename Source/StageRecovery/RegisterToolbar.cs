/*
	This file is part of Stage Recovery /L Unleashed
		© 2020-2021 LisiasT
		© 2014-2018 magico13

	Stage Recovery /L licensed as follows:
		* GPL 3.0 : https://www.gnu.org/licenses/gpl-3.0.txt

	Stage Recovery /L Unleashedis distributed in the hope that it will be useful,
	but WITHOUT ANY WARRANTY; without even the implied warranty of
	MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.

	You should have received a copy of the GNU General Public License 3.0
	along with Stage Recovery /L Unleashed.
	If not, see <https://www.gnu.org/licenses/>.
*/
/*
 * Contains code licensed under the MIT
 * © 2018-2020 LinuxGuruGamer
*/
using UnityEngine;

using ToolbarControl_NS;


namespace StageRecovery
{
    [KSPAddon(KSPAddon.Startup.MainMenu, true)]
    public class RegisterToolbar : MonoBehaviour
    {
        internal const string MODID = "StageRecovery_NS";
        internal const string MODNAME = "Stage Recovery";
        void Start()
        {
            ToolbarControl.RegisterMod(MODID, MODNAME);
        }
    }
}
