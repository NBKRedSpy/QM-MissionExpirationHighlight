using HarmonyLib;
using MGSC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QM_MissionExpirationHighlight
{
    [HarmonyPatch(typeof(StarmapScreen), nameof(StarmapScreen.Hide))]
    public static class StarmapScreen_Hide_Patch
    {

        public static void Postfix(StarmapScreen __instance)
        {
            if (__instance._travelMetadata.IsInTravel)
            {
                return;
            }

            StationsRenderer.Modify(SingletonMonoBehaviour<SpaceUI>.Instance.Hud.SpaceStationsWindow, null, null);
        }

    }
}
