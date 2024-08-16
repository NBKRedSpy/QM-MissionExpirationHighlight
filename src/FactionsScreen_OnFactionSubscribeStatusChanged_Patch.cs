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
    /// Handles refreshing the stations box when the subscriptions change.
    /// </summary>
    [HarmonyPatch(typeof(FactionsScreen), nameof(FactionsScreen.OnFactionSubscribeStatusChanged))]
    public static class FactionsScreen_OnFactionSubscribeStatusChanged_Patch
    {
        public static void Postfix(FactionsScreen __instance)
        {

            StationsRenderer.Modify(SingletonMonoBehaviour<SpaceUI>.Instance.Hud.SpaceStationsWindow);
        }
    }
}
