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
using UnityEngine;

using Asset = KSPe.IO.Asset<StageRecovery.Startup>;

namespace StageRecovery
{
	internal static class UI
	{
		internal static class Icon
		{
			private static Texture2D icon38 = null;
			internal static Texture2D Icon38 => icon38 ?? (icon38 = Asset.Texture2D.LoadFromFile("Icons", "icon-38"));

			private static Texture2D icon24 = null;
			internal static Texture2D Icon24 => icon24 ?? (icon24 = Asset.Texture2D.LoadFromFile("Icons", "icon-24"));
		}
	}
}
