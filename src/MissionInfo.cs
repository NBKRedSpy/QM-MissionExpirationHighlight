using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MissionExpirationHighlight
{
    public enum MissionInfo
    {
        None,
        Victim,
        Benefit,
        Both = Victim | Benefit
    }
}
