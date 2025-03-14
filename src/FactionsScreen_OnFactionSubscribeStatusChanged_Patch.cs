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
    [HarmonyPatch(typeof(FactionsScreen), nameof(FactionsScreen.OnFactionSubscribeStatusChanged))]
    public static class FactionsScreen_OnFactionSubscribeStatusChanged_Patch
    {
        public static void Postfix(FactionsScreen __instance)
        {

            StationsRenderer.Modify(__instance._spaceStationsWindow);
        }
    }
}
