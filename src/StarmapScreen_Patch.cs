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
        public static readonly Color ExpiredColor;

        static StarmapScreen_Patch()
        {
            ExpiredColor = Plugin.ModConfig.ExpiredMissionUnityColor;
        }
        public static void Postfix(StarmapScreen __instance, StarmapSpaceObjectPanel obj)
        {

            if (obj.SpaceObjectId.Equals(__instance._travelMetadata.CurrentSpaceObject))
            {
                return;
            }

            double travelHoursBetweenPoints = TravelSystem.GetTravelHoursBetweenPoints(
                __instance._spaceObjects, __instance._travelMetadata.CurrentSpaceObject, obj.SpaceObjectId);

            DateTime eta = __instance._spaceTime.Time.AddHours(travelHoursBetweenPoints);


            foreach (SpaceStationPanel panel in SingletonMonoBehaviour<SpaceUI>.Instance.Hud.SpaceStationsWindow._panels)
            {
                if(panel._prevStatus == StationStatus.Peaceful)
                {
                    continue;
                }

                Mission mission = panel._missions.Get(panel._station.Id);


                if(mission?.ExpireTime < eta)
                {
                    CommonButton button = panel._visualWrapper._button;
                    button.RefreshNormalСaptionColor(ExpiredColor);
                }

            } 
        }
    }
}
