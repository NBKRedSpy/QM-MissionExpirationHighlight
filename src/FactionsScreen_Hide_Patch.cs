﻿using HarmonyLib;
using MGSC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace QM_MissionExpirationHighlight
{

    /// <summary>
    /// When returning to the Space Hud when the Space Stations panel is recreated (was hidden)
    /// </summary>
    [HarmonyPatch(typeof(FactionsScreen), nameof(FactionsScreen.Hide))]
    public static class FactionsScreen_Hide_Patch
    {
        public static void Postfix()
        {
            StationsRenderer.Modify(SingletonMonoBehaviour<SpaceUI>.Instance._spaceHudScreen.SpaceStationsWindow, null, null);
        }
    }
}
