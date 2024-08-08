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
    /// Responsible for the grid of missions at a station.
    /// This is the box on the space main screen and the also the box on the right side of the star map.
    /// </summary>
    public static class StationsRenderer
    {
        public static UnityColorConfig ColorConfig;

        public static void Modify(SpaceStationsWindow window, SpaceObjects spaceObjects = null, string spaceObjectId = null)
        {


            bool checkEta = spaceObjects != null 
                && !string.IsNullOrEmpty(spaceObjectId) 
                && (!spaceObjectId.Equals(window._travelMetadata.CurrentSpaceObject));

            DateTime eta = DateTime.MinValue;

            if (checkEta)
            {

                double travelHoursBetweenPoints = TravelSystem.GetTravelHoursBetweenPoints(
                    spaceObjects, window._travelMetadata.CurrentSpaceObject, spaceObjectId);

                eta = window._spaceTime.Time.AddHours(travelHoursBetweenPoints);
            }

            HashSet<string> subscriptions = new HashSet<string>(window._factions.SubscribedFactions);

            foreach (SpaceStationPanel panel in SingletonMonoBehaviour<SpaceUI>.Instance.Hud.SpaceStationsWindow._panels)
            {
                //IIRC, at this point the game has the current status as the "prevStatus"
                if (panel._prevStatus == StationStatus.Peaceful)
                {
                    continue;
                }

                //Missions is the database of all missions, keyed by station id.
                Mission mission = panel._missions.Get(panel._station.Id);

                if (mission == null)
                {
                    continue;
                }

                //The individual location box.
                CommonButton button = panel._visualWrapper._button;

                if (checkEta && (mission.ExpireTime < eta))
                {
                    
                    button.RefreshNormalСaptionColor(ColorConfig.ExpiredColor);
                    continue;
                }


                if(Plugin.ModConfig.EnableSubscriptionColors)
                {
                    Color conflictColor = GetConflictColor(subscriptions, mission, out MissionInfo _);

                    if (conflictColor == Color.black)
                    {
                        //Not sure why it is called previous status.  
                        //It is the current status after the init.
                        panel.RefreshStatus(panel._prevStatus);
                    }
                    else
                    {
                        button.RefreshNormalСaptionColor(conflictColor);
                    }
                }

            }
        }

        /// <summary>
        /// Returns the mission color and the overall type of the mission.
        /// </summary>
        /// <param name="subscriptions">The user's faction subscriptions</param>
        /// <param name="mission">The mission to check</param>
        /// <param name="missionInfo">Type of the mission from best to worst.</param>
        /// <returns>The mission color.  Returns Color.black if there is no conflict</returns>
        public static Color GetConflictColor(HashSet<string> subscriptions, Mission mission, out MissionInfo missionInfo)
        {

            bool hasVictim = false;
            bool hasBenefit = false;

            if (subscriptions.Contains(mission.VictimFactionId)) hasVictim = true;
            if (subscriptions.Contains(mission.BeneficiaryFactionId)) hasBenefit = true;

            missionInfo =
                hasVictim && hasBenefit ? MissionInfo.Both :
                hasVictim ? MissionInfo.Victim :
                hasBenefit ? MissionInfo.Benefit :
                MissionInfo.None;   
                

            Color conflictColor;

            switch (missionInfo)
            {
                case MissionInfo.Victim:
                    conflictColor = ColorConfig.ConflictVictim;
                    break;
                case MissionInfo.Both:
                    conflictColor = ColorConfig.ConflictBoth;
                    break;
                case MissionInfo.None:
                    conflictColor = ColorConfig.NormalConflict;
                    break;
                case MissionInfo.Benefit:
                    conflictColor = ColorConfig.ConflictBenefit;
                    break;
                default:
                    throw new ApplicationException($"Unexpected mission info value: {missionInfo}");
            }

            return conflictColor;
        }
    }
}
