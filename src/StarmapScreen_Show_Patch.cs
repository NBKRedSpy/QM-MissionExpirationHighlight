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
    /// Handles the list of stations on the left side of the star map.
    /// For Example:  Venus 1 (3)
    /// </summary>
    [HarmonyPatch(typeof(StarmapScreen), nameof(StarmapScreen.Show))]
    public static partial class StarmapScreen_Show_Patch
    {

        public static void Postfix(StarmapScreen __instance)
        {
            foreach (var panel in __instance._panels)
            {

                if(__instance._travelMetadata.CurrentSpaceObject == panel.SpaceObjectId)
                {
                    continue;
                }

                double travelHoursBetweenPoints;
                try
                {
                    //I'm currently unsure why this throws an exception.
                    //  I've accessed every variable used by GetTravelHoursBetweenPoints, but it only fails in the
                    //function call.  It seems to only affect the current location


                    travelHoursBetweenPoints = TravelSystem.GetTravelHoursBetweenPoints(
                        __instance._spaceObjects, __instance._travelMetadata.CurrentSpaceObject, panel.SpaceObjectId);
                }
                catch (Exception)
                {
                    continue;
                }


                DateTime eta = __instance._spaceTime.Time.AddHours(travelHoursBetweenPoints);

                int totalMissions = 0;
                int availableMissions = 0;

                Color bestMissionColor = Color.black;
                MissionInfo bestMissionType = MissionInfo.Invalid;

                HashSet<string> subscriptions = new HashSet<string>(__instance._factions.SubscribedFactions);

                foreach (Mission mission in __instance._missions.Values)
                {
                    if (__instance._stations.Get(mission.StationId).Record.SpaceObjectId.Equals(panel._record.Id))
                    {
                        bool canReach = false;

                        if (mission.ExpireTime > eta)
                        {
                            availableMissions++;
                            canReach = true;
                        }

                        totalMissions++;

                        if (canReach)
                        {
                            //Get the best color for the missions
                            Color missionColor;
                            missionColor = StationsRenderer.GetConflictColor(subscriptions, mission, out MissionInfo missionType);

                            if (missionType > bestMissionType)
                            {
                                bestMissionType = missionType;
                                bestMissionColor = missionColor;
                            }
                        } 
                    }
                }

                //Todo:  new level:
                //  can reach
                //  less than benefit subscription.
                //  no subscription
\                if (availableMissions > 0)
                {
                    if (bestMissionColor == Color.black)
                    {
                        //Should be all unsubscribed.
                        panel._count.text = $"{availableMissions} ({totalMissions})";
                    }
                    else
                    {
                        //One or more subscribed.
                        string htmlColor = ColorUtility.ToHtmlStringRGB(bestMissionColor);
                        panel._count.text = $"<color={htmlColor}>{availableMissions}</color>({totalMissions})";
                    }
                }
                else
                {
                    //Should be all unreachable.  Use the game's single count text.
                    panel._count.text = $"{availableMissions}";
                }
            }
        }

    }
}
