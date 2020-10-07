/*
    This file is part of StageRecovery
    © 2014-2018 magico13

    StageRecovery licensed as follows:

    * GPL 3.0 : https://www.gnu.org/licenses/gpl-3.0.txt

    And you are allowed to choose the License that better suit your needs.

    StageRecovery is distributed in the hope that it will be useful,
    but WITHOUT ANY WARRANTY; without even the implied warranty of
    MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.

    You should have received a copy of the GNU General Public License 3.0
    along with StageRecovery If not, see <https://www.gnu.org/licenses/>.
*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace StageRecovery
{
    public class CrewWithSeat
    {
        public ProtoCrewMember CrewMember { get; private set; }
        public ProtoPartSnapshot PartSnapshot { get; private set; }

        public CrewWithSeat(ProtoCrewMember crew, ProtoPartSnapshot partSnapshot)
        {
            CrewMember = crew;
            PartSnapshot = partSnapshot;
        }

        public CrewWithSeat(ProtoCrewMember crew)
        {
            CrewMember = crew;
            PartSnapshot = crew?.seat?.part?.protoPartSnapshot;
        }

        public bool Restore(ProtoVessel vessel)
        {
            if (PartSnapshot == null)
            {
                return false;
            }
            ProtoPartSnapshot restoredPart = vessel.protoPartSnapshots.FirstOrDefault(p => p.craftID == PartSnapshot.craftID);
            if (restoredPart != null)
            {
                restoredPart.protoModuleCrew.Add(CrewMember);
                CrewMember.seat?.SpawnCrew();
                return true;
            }
            return false;
        }
    }
}
