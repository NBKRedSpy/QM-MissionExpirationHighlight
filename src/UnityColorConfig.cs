﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace QM_MissionExpirationHighlight
{
    /// <summary>
    /// The color from the config, translated to Unity Color objects for the mod's use.
    /// </summary>
    public class UnityColorConfig
    {
        public Color ExpiredColor { get; set; }

        public Color ConflictBoth { get; set; }

        public Color ConflictBenefit { get; set; }

        public Color ConflictVictim { get; set; }

    }
}
