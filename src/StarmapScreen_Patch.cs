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
    [HarmonyPatch(typeof(StarmapScreen), nameof(StarmapScreen.PanelOnSelected))]
    public static class StarmapScreen_Patch
    {
        public static void Postfix(StarmapScreen __instance, StarmapSpaceObjectPanel obj)
        {
            StationsRenderer.Modify(SingletonMonoBehaviour<SpaceUI>.Instance.Hud.SpaceStationsWindow, __instance._spaceObjects, obj.SpaceObjectId);
        }
    }
}
