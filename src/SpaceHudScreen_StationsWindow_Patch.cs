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
    [HarmonyPatch(typeof(SpaceHudScreen), nameof(SpaceHudScreen.RefreshUIOnArrival))]
    public static class SpaceHudScreen_StationsWindow_Patch
    {
        public static void Postfix(SpaceHudScreen __instance)
        {

            StationsRenderer.Modify(__instance.SpaceStationsWindow, null, null);
        }
    }
}
