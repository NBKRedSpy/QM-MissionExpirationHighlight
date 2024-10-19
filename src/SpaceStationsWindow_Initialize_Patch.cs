using HarmonyLib;
using MGSC;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace QM_MissionExpirationHighlight
{


    /// <summary>
    /// When returning to the Space Hud when the Space Stations panel is recreated (was hidden)
    /// </summary>
    [HarmonyPatch(typeof(SpaceStationsWindow), nameof(SpaceStationsWindow.Initialize))]
    public static class SpaceStationsWindow_Initialize_Patch
    {

        private static bool _prefabInited;

        public static void Prefix(SpaceStationsWindow __instance)
        {
            //Debug for rectangle test.
            if (_prefabInited == false)
            {
                _prefabInited = true;

                GameObject parentGameObject = __instance._panelsPool.prefab;

                SetPanelObjects(parentGameObject);

                //Even on init, there are pre allocated objects.  Update them as well.
                foreach (GameObject panel in __instance._panelsPool.objects)
                {
                    SetPanelObjects(panel);
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

        public static void Postfix(SpaceStationsWindow __instance)
        {


            StationsRenderer.Modify(__instance, null, null);
        }
    }
}
