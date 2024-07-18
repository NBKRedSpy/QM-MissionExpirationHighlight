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
    /// Handles modifying the graphics for the mod's functionality
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
                if (panel._prevStatus == StationStatus.Peaceful)
                {
                    continue;
                }

                Mission mission = panel._missions.Get(panel._station.Id);

                if (mission == null)
                {
                    continue;
                }

                CommonButton button = panel._visualWrapper._button;

                if (checkEta && (mission.ExpireTime < eta))
                {
                    
                    button.RefreshNormalСaptionColor(ColorConfig.ExpiredColor);
                    continue;
                }


                if(Plugin.ModConfig.EnableSubscriptionColors)
                {
                    MissionInfo info = MissionInfo.None;

                    //If both an attacker and a defender is
                    if (subscriptions.Contains(mission.VictimFactionId)) info |= MissionInfo.Victim;
                    if (subscriptions.Contains(mission.BeneficiaryFactionId)) info |= MissionInfo.Benefit;

                    Color conflictColor = (info & MissionInfo.Both) == MissionInfo.Both ? ColorConfig.ConflictBoth
                        : (info & MissionInfo.Benefit) == MissionInfo.Benefit ? ColorConfig.ConflictBenefit
                        : (info & MissionInfo.Victim) == MissionInfo.Victim ? ColorConfig.ConflictVictim
                        : Color.black;

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
    }
}
