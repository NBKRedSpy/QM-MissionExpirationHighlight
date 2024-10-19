using HarmonyLib;
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
    [HarmonyPatch(typeof(SpaceStationsWindow), nameof(SpaceStationsWindow.Initialize))]
    public static class SpaceStationsWindow_Initialize_Patch
    {
        public static void Postfix(SpaceStationsWindow __instance)
        {

            StationsRenderer.Modify(__instance, null, null);
        }
    }
}
