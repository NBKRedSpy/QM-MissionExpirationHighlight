using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using YamlDotNet.Serialization;

namespace QM_MissionExpirationHighlight
{
    public class ModConfig
    {
        private static string DefaultHtmlColor = "#f475ee";
        public string ExpiredMissionColor { get; set; } = DefaultHtmlColor;

        [YamlIgnore]
        public Color ExpiredMissionUnityColor { get; set; }

        public void Init()
        {
            if (ColorUtility.TryParseHtmlString(ExpiredMissionColor, out Color color))
            {
                ExpiredMissionUnityColor = color;
            }
            else
            {
                ColorUtility.TryParseHtmlString(ExpiredMissionColor, out Color defaultColor);
                ExpiredMissionUnityColor = defaultColor;
            }


        }

    }
}
