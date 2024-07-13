using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using YamlDotNet.Serialization;

namespace QM_MissionExpirationHighlight
{
    public class ColorConfig
    {

        public string ExpiredColor { get; set; }

        public string ConflictBoth { get; set; }

        public string ConflictBenefit { get; set; }

        public string ConflictVictim { get; set; }

        public ColorConfig()
        {
            ExpiredColor = "#f475ee";
            ConflictBoth = "#FFD800";
            ConflictBenefit = "#4CFF00";
            ConflictVictim = "#FF0000";
        }
        public UnityColorConfig GetUnityConfig()
        {
            ColorConfig defaults = new ColorConfig();

            return new UnityColorConfig()
            {

                ExpiredColor = Utility.ParseHtmlColor(ExpiredColor, defaults.ExpiredColor),
                ConflictBoth = Utility.ParseHtmlColor(ConflictBoth, defaults.ConflictBoth),
                ConflictBenefit = Utility.ParseHtmlColor(ConflictBenefit, defaults.ConflictBenefit),
                ConflictVictim = Utility.ParseHtmlColor(ConflictVictim, defaults.ConflictVictim),
            };
        }
    }
}
