using BepInEx;
using HarmonyLib;
using MGSC;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using YamlDotNet.Serialization;

namespace QM_MissionExpirationHighlight
{
    public static class Plugin
    {
        public static ModConfig ModConfig{ get; set; }

        [Hook(ModHookType.AfterBootstrap)]  
        public static void Awake(IModContext context)
        {
            LoadConfig();
            ModConfig.Init();

            new Harmony("QM_MissionExpirationHighlight").PatchAll();
        }

        private static void LoadConfig()
        {
            string configPath = Path.Combine(Application.persistentDataPath, Assembly.GetExecutingAssembly().GetName().Name) + ".yaml";


            if (File.Exists(configPath))
            {
                var deseralizer = new DeserializerBuilder()
                    .Build();

                try
                {
                    ModConfig = deseralizer.Deserialize<ModConfig>(File.ReadAllText(configPath));
                    return;
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Error deserializing configuration file {configPath}.  Loading defaults");
                    Debug.LogException(ex);
                }
            }

            //Exception or not set.
            ModConfig = SaveDefaults(configPath);
        }

        private static ModConfig SaveDefaults(string configPath)
        {
            ModConfig config = new ModConfig();

            var seralizer = new Serializer();
            File.WriteAllText(configPath, seralizer.Serialize(config));

            return config;
        }
    }
}
