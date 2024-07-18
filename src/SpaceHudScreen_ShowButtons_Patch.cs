using HarmonyLib;
using MGSC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QM_MissionExpirationHighlight
{
    [HarmonyPatch(typeof(SpaceHudScreen), nameof(SpaceHudScreen.ShowButtons))]
    public static class SpaceHudScreen_ShowButtons_Patch
    {

        public static void Postfix(SpaceHudScreen __instance)
        {

            StationsRenderer.Modify(__instance.SpaceStationsWindow);
        }

    }
}
