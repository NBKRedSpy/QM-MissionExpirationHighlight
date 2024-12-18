[h1]Quasimorph Mission Expire and Subscription Colors[/h1]



Adds the following indicators:

Adds a "X (Y)" number to the planet list where X is the non expiring missions and Y is the total number of missions.

Colors the missions as follows:
[list]
[*]Will expire before the ship can travel to the location.
[*]A subscribed faction is the benefit of the mission.
[*]Two subscribed factions are attacking each other.
[*]A subscribed faction is the victim of the mission.
[/list]

The subscription colors can be disabled and all colors can be customized.

[h1]Configuration[/h1]

The configuration file is located at [i]%UserProfile%\AppData\LocalLow\Magnum Scriptum Ltd\Quasimorph_ModConfigs\QM_MissionExpirationHighlight.yaml[/i] .
The file will be created the first time the game is run.

The [i]ColorConfig[/i] members contain all of the colors used by the mod.

Set [i]EnableSubscriptionColors[/i] to false to disable the subscription indicators.

[h1]Support[/h1]

If you enjoy my mods and want to buy me a coffee, check out my [url=https://ko-fi.com/nbkredspy71915]Ko-Fi[/url] page.
Thanks!

[h1]Source Code[/h1]

Source code is available on GitHub https://github.com/NBKRedSpy/QM-MissionExpirationHighlight

[h1]Change Log[/h1]

[h2]2.4.0[/h2]
[list]
[*]Moved config file directory.
[/list]

[h2]2.3.1[/h2]
[list]
[*]Fixed Null Reference exception with the new image object in some cases.
[/list]

Thanks to Discord user Verro for reporting.

[h2]2.3.0[/h2]
[list]
[*]Converted to colored bar at bottom of mission.
[/list]

[h2]2.2.1[/h2]
[list]
[*]Incorrect color Fix
[*]Non primary colors are still difficult to see.
[/list]

[h2]2.2.0[/h2]
[list]
[*]Preliminary support for .8 as the previous version crashes in .8.  Needs color fixes.
[/list]

[h2]2.1.0[/h2]

Fixes:
[list]
[*]Factions subscriptions screen not updating on change.
[*]Not updating when returning to the main hud.
[/list]

[h2]2.0.0[/h2]

Added faction subscription colors.
Thanks to Steam user RafaelKB for the suggestion.
