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

            StationsRenderer.ColorConfig = ModConfig.UnityColorConfig;

            new Harmony("QM_MissionExpirationHighlight").PatchAll();
        }

        private static void LoadConfig()
        {
            string configPath = Path.Combine(Application.persistentDataPath, Assembly.GetExecutingAssembly().GetName().Name) + ".yaml";


            if (File.Exists(configPath))
            {
                var deserializer = new Deserializer();

                try
                {
                    string yaml = File.ReadAllText(configPath);
                    ModConfig = deserializer.Deserialize<ModConfig>(yaml);

                    //Update the config with any new defaults.
                    string serializeYaml = new Serializer().Serialize(ModConfig);

                    if(serializeYaml != yaml)
                    {
                        File.WriteAllText(configPath, serializeYaml);
                    }

                    if (ModConfig.Version == ModConfig.LatestVerison)
                    {
                        return;
                    }
                    else
                    {
                        //Backup the old version, but create a new one.
                        File.Copy(configPath, configPath + ".bak");
                        //Fall through to the create.
                    }
                }
                catch (Exception ex)
                {
                    Debug.LogError($"Error deserializing configuration file {configPath}.  Loading defaults");
                    Debug.LogException(ex);

                    //Continue to new create.
                }
            }

            ModConfig = SaveDefaults(configPath);
        }

        private static ModConfig SaveDefaults(string configPath)
        {
            ModConfig config = new ModConfig()
            {
                ColorConfig = new ColorConfig(),
                Version = ModConfig.LatestVerison
            };

            var serializer = new Serializer();
            File.WriteAllText(configPath, serializer.Serialize(config));

            return config;
        }
    }
}
