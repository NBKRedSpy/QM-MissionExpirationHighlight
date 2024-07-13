using MonoMod.RuntimeDetour.Platforms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using YamlDotNet.Serialization;
using YamlDotNet.Serialization.NodeTypeResolvers;

namespace QM_MissionExpirationHighlight
{
    public class ModConfig
    {
        public readonly static string LatestVerison = "2.0.0";

        public string Version { get; set; }

        public ColorConfig ColorConfig { get; set; }

        /// <summary>
        /// If true, will color the stations based on faction subscriptions.
        /// A color for victim, benefactor, or subscribed to both the victim and benefactor.
        /// </summary>
        public bool EnableSubscriptionColors { get; set; } = true;

        [YamlIgnore]
        public UnityColorConfig UnityColorConfig { get; set; }

        public void Init()
        {
            UnityColorConfig = ColorConfig.GetUnityConfig();
        }
    }
}
