# Quasimorph Mission Expire and Subscription Colors

![Color Key](media/thumbnail.png)
![Planet Mission Count](media/PlanetList.png)


# Important - This version of the mod is only for the opt in beta version of the game!

This is a temporary version that supports the 0.8.6 beta, and not the current game's version.
For the non beta version of the game, use the mod of the same name without the BETA suffix.

The non beta version *must* be uninstalled.  
When the 0.8.6 becomes the main version, this mod will be deleted.
Note that the beta is expected to change many times before release.
As such, this mod may break on each beta release.

# Docs


Adds the following indicators:

Adds a "X (Y)" number to the planet list where X is the non expiring missions and Y is the total number of missions.

Colors the missions as follows:
* Will expire before the ship can travel to the location.
* A subscribed faction is the benefit of the mission.
* Two subscribed factions are attacking each other.
* A subscribed faction is the victim of the mission.

The subscription colors can be disabled and all colors can be customized.

# Configuration
The configuration file is located at ```%UserProfile%\AppData\LocalLow\Magnum Scriptum Ltd\Quasimorph_ModConfigs\QM_MissionExpirationHighlight.yaml``` .
The file will be created the first time the game is run.

The `ColorConfig` members contain all of the colors used by the mod.

Set `EnableSubscriptionColors` to false to disable the subscription indicators.

# Support
If you enjoy my mods and want to buy me a coffee, check out my [Ko-Fi](https://ko-fi.com/nbkredspy71915) page.
Thanks!

# Source Code
Source code is available on GitHub https://github.com/NBKRedSpy/QM-MissionExpirationHighlight

# Change Log

## 2.5.0 
* Compatible with the 0.8.6 opt in beta 

## 2.4.0
* Moved config file directory.

## 2.3.1
* Fixed Null Reference exception with the new image object in some cases.

Thanks to Discord user Verro for reporting.
