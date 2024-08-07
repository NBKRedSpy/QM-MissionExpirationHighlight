using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QM_MissionExpirationHighlight
{
    /// <summary>
    /// The type of mission.  Best is highest
    /// </summary>
    public enum MissionInfo
    {
        Invalid = 0,

        /// <summary>
        /// The mission cannot be reached.
        /// </summary>
        Unreachable,

        //A subscription is a victim of the attack / defending mission
        Victim,

        /// <summary>
        /// Two subscriptions are attacking/defending
        /// </summary>
        Both,

        /// <summary>
        /// There is a conflict with no subscriptions
        /// </summary>
        None,

        /// <summary>
        /// A subscribed faction is attacking or defending against an unsubscribed faction.
        /// </summary>
        Benefit
    }
}
