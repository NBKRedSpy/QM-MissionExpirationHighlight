using MGSC;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

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

            foreach (SpaceStationPanel panel in window._panels)
            {
                Image image = panel._visualWrapper.transform
                    .Find("Conflict Image")?.GetComponent<Image>();

                if (image == null)
                {

                    //Previously tried to setup the prefab for the panel, but the game
                    //  will create four new station chits when coming in and out of the game.
                    //  Just moved it all to here.

                    StationsRenderer.SetPanelObjects(panel.gameObject);

                    image = panel._visualWrapper.transform
                        .Find("Conflict Image")?.GetComponent<Image>();


                    if (image == null)
                    {
                        Debug.LogError($"Unable to find the 'Conflict image' even after init attempt");
                        continue;
                    }
                }

                image.color = Color.clear;

                if (panel._prevStatus == StationStatus.Peaceful)
                {
                    continue;
                }

                Mission mission = panel._missions.Get(panel._station.Id, false);

                if (mission == null)
                {
                    continue;
                }


                if (checkEta && (mission.ExpireTime < eta))
                {

                    image.color = ColorConfig.ExpiredColor;
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
                        image.color = conflictColor;
                    }
                }

            }
        }

        private static void SetPanelObjects(GameObject parentGameObject)
        {
            //Canvas attempt
            RectTransform parentRec = parentGameObject.GetComponent<RectTransform>();

            GameObject subObject = new GameObject("Conflict Image");
            subObject.AddComponent<Canvas>();
            subObject.transform.SetParent(parentRec, worldPositionStays: false);

            Image image = subObject.AddComponent<Image>();
            //image.color = new Color(1.0F, 0.0F, .0F, .5F);

            //ColorUtility.TryParseHtmlString("#E7700D", out Color color);
            image.color = Color.clear;

            RectTransform conflictTransform = subObject.GetComponent<RectTransform>();
            //conflictTransform.sizeDelta = parentRec.sizeDelta;
            //conflictTransform.sizeDelta = new Vector2(46, 16);

            conflictTransform.sizeDelta = new Vector2(46, 2);

            Vector2 anchorPoint = new Vector2(0, 0);
            conflictTransform.anchorMin = anchorPoint;
            conflictTransform.anchorMax = anchorPoint;
            conflictTransform.pivot = anchorPoint;



        }

    }
}
