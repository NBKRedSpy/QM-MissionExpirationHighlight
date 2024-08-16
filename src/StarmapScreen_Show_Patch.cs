using HarmonyLib;
using MGSC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
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

        /// <summary>
        /// This is the lighter green color is used for the game's default mission count in the panel.
        /// </summary>
        private static string GreenColorHtml { get; } = "#638C5E";

        public static void Postfix(StarmapScreen __instance)
        {
            HashSet<string> subscriptions = new HashSet<string>(__instance._factions.SubscribedFactions);

            foreach (var panel in __instance._panels)
            {

                //There are planets that are disabled.  
                //Maybe unlocked later or for future development?
                if(!panel._record.Available)
                {
                    continue;
                }

                double travelHoursBetweenPoints;

                if (__instance._travelMetadata.CurrentSpaceObject == panel.SpaceObjectId)
                {
                    travelHoursBetweenPoints = 0;
                }
                else
                {

                    travelHoursBetweenPoints = TravelSystem.GetTravelHoursBetweenPoints(
                        __instance._spaceObjects, __instance._travelMetadata.CurrentSpaceObject, panel.SpaceObjectId);
                }

                DateTime eta = __instance._spaceTime.Time.AddHours(travelHoursBetweenPoints);

                int totalMissions = 0;
                int availableMissions = 0;

                Color bestMissionColor = Color.black;
                MissionInfo bestMissionType = MissionInfo.Invalid;

                foreach (Mission mission in __instance._missions.Values)
                {
                    if (__instance._stations.Get(mission.StationId).Record.SpaceObjectId != panel._record.Id)
                    { 
                        continue; 
                    }

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

                panel._count.text = GetLabelText(availableMissions, totalMissions, bestMissionColor);


            }
        }

        /// <summary>
        /// Returns the mission number part of a planet.
        /// </summary>
        /// <param name="availableMissions">The missions that can be reached in time.</param>
        /// <param name="totalMissions">The total number of missions available.</param>
        /// <param name="color">The color to use.  If black, no color will be used.</param>
        /// <returns>The TextMeshPro formatted text</returns>
        private static string GetLabelText(int availableMissions, int totalMissions, Color color)
        {

            string baseText;

            //Recolor the text string to the game's green since the game's code changes the .color property
            //  based on the subscription status.  So it is confusing to see some numbers colored with a similar
            //  color to the mod's color palette.
            if(availableMissions == totalMissions)
            {
                baseText = $"<color={GreenColorHtml}>{{0}}{totalMissions}{{1}}</color>";
            }
            else
            {
                baseText = $"<color={GreenColorHtml}>{{0}}{availableMissions}{{1}} ({totalMissions})</color>";
            }

            string colorPrefix = "";
            string colorSuffix = "";

            if (color != Color.black)
            {
                colorPrefix = $"<color=#{ColorUtility.ToHtmlStringRGB(color)}>";
                colorSuffix = "</color>";
            }

            return string.Format(baseText, colorPrefix, colorSuffix);

        }
    }
}
