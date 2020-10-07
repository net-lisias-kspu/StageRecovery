# Stage Recovery :: Change log

* 2019-0820: 1.9.1.2 (LinuxGuruGamer) for KSP 1.7.3
	+ Thanks to forum user @aerospike for this:
		- Fix issue with stock engine plates being in wrong stage
* 2019-0724: 1.9.1.1 (LinuxGuruGamer) for KSP 1.7.3
	+ Fixed issue with showing remaining fuel on one stage and another stage not having liquid fuel (ie: solid), was getting GUI errors
	+ Moved RegisterToolbar into it's own file
	+ Added InstallChecker
	+ Updated AssemblyVersion.tt
* 2019-0206: 1.9.1 (LinuxGuruGamer) for KSP 1.6.1
	+ Added display of remaining fuel after recovery
	+ Added "Fuel" section to the SR window, only if there was any leftover fuel
* 2019-0124: 1.9.0.7 (LinuxGuruGamer) for KSP 1.6.1
	+ Fixed registration with the RecoveryController by moving initialization OUT of the Log.Info line
* 2019-0124: 1.9.0.6 (LinuxGuruGamer) for KSP 1.6.1
	+ Changed startup from AllGameScenes to FlightEditorAndKSC
	+ Fixed Nullref when going into settings before any game is loaded
* 2019-0106: 1.9.0.4 (LinuxGuruGamer) for KSP 1.6.0
	+ Fixed recalculation, needed for 1.6 where root part wasn't part[0] anymore, added search for part with no parent
	+ Added new event to recalculate automatically when ship is modified.  Does not recalculate when ship is deleted
	+ Updated windows with support for ClickThroughBlocker
* 2018-1128: 1.9.0.3 (LinuxGuruGamer) for KSP 1.5.1
	+ Fixed .version file
* 2018-1128: 1.9.0.2 (LinuxGuruGamer) for KSP 1.5.1
	+ Removed old, unused code, and code obsoleted by the ToolbarController
	+ Changed default for UseDistanceOverride from true to false
	+ Removed some unnecessary window calls
	+ Added back button on Spacecenter screen, opens window to direct people to Stock Settings pages
	+ Added option to disable the button on spacecenter scene
* 2018-1126: 1.9.0.1 (LinuxGuruGamer) for KSP 1.5.1
	+ Changed license to MIT
	+ Moved all settings into stock settings page, both to reduce code and to have the settings persist on a per-save basis
		- Note:  Existing settings are not migrated
	+ Replaced all Debug.Log with new Log.Info to reduce log spam
	+ Added code to remove events when mod is destroyed (ie: scene change)
* 2018-1026: 1.9.0 (linuxgurugamer) for KSP 1.5.1
	+ Adoption by Linuxgurugamer
	+ Replaced toolbar code with the ToolbarController
	+ Added support for the Clickthroughblocker
	+ Fixed bug where toobar button wasn't being displayed after first scene if using the stock toolbar
	+ Removed blizzy option from settings
	+ Version bump from 1.6.5 due to incorrect version info
	+ Updated changelog with data from spacedock
* 2018-0311: 1.8.0 (magico13) for KSP 1.4.1
	+ Built against KSP 1.4.0. Might be backward compatible.
	+ Pre-recovery now fully recovers vessels and not just kerbals
	+ Add distance restriction to pre-recovery to prevent it from firing at the wrong time
	+ Recovery now fires the OnVesselRecovered event but forcibly removes the funds handling and scene changes associated with it
		- The above changes make contracts that require recovering vessels/crew work much better, but keep your timewarp down to 10x or so
	+ Removed explicit ScrapYard support due to the above changes not requiring it anymore
	+ Kerbals and Science are now recovered by default and cannot be turned off
	+ Use funds symbol in stock recovery messages (thanks Starwaster)
* 2017-0601: 1.7.2 (magico13) for KSP 1.3.1
	+ 1.7.2 (06/01/2017)
		- Built against KSP 1.3, but should still support KSP 1.2.2
		- Added support for ScrapYard mod. Adds parts to inventory upon recovery.
* 2017-0410: 1.7.1 (magico13) for KSP 1.2.2
	+ 1.7.1 (04/09/2017)
			- Added support for RecoveryController, an improved form of compatibility between recovery mods (thanks LinuxGuruGamer!)
* 2017-0318: 1.7.0 (magico13) for KSP 1.2.2
	+ 1.7.0 (03/18/2017)
		- Added support for the updated FMRS
		- Fixed bug with crew recovery that resulted in them still being marked dead
		- Added option to disable kerbal pre-recovery
* 2017-0129: 1.6.8 (magico13) for KSP 1.2.2
	+ Fixed issue where parachutes seemingly weren't being accounted for when speeds were greater than DRMaxVelocity
* 2017-0124: 1.6.7 (magico13) for KSP 1.2.2
	+ Fixed issue with RealChute integration
* 2017-0123: 1.6.6 (magico13) for KSP 1.2.2
	+ Update to KSP 1.2.2.
	+ Powered recovery now tries to perform a reentry burn to reduce speed and avoid burning up.
	+ Kraken finds Kerbals to be less tasty when changing vessels/scenes (less randomly disappearing)
	+ Terminal velocity estimations centralized and can be called through API/wrapper
* 2016-1021: 1.6.5 (magico13) for KSP 1.2
	+ 1.6.5 (10/20/2016)
		- Update to KSP 1.2
		- New estimator for stock parachutes based on physics. Not perfect, will try to improve
		- No longer spams messages when the recovery errors (#32)
		- Added a global recovery modifier option (#39)
		- Settings now go in StageRecovery/PluginData/Config.txt
		- Editor GUI now supports any IStageSeparators
		- Pulled in some fixes for the Toolbar mod
	+ Thank you linuxgurugamer, Iskie, and Kerbas-ad-astra for your PRs!
* 2016-1010: 1.6.4.6 (magico13) for KSP 1.2 PRE-RELEASE
	+ Another pre-release update for 1.2. A few minor additions:
		- Editor GUI now splits on any IStageSeparator (so mod decouplers should work)
		- Settings have been moved into StageRecovery/PluginData
		- Added a global recovery modifier that applies to all recoveries (default 100%, but can go down to 0%)
		- Added a safeguard against trying to recover the same vessel multiple times. Testing will require a bit of work, but it shouldn't spam messages or give you unlimited funds when an error occurs while a vessel is being destroyed.
	+ Thanks to linuxgurugamer for the PR for the latest KSP pre-release, and Iskie for the PR that lets the editor GUI work with any IStageSeparator.
* 2016-0916: 1.6.4.5 (magico13) for KSP 1.2 PRE-RELEASE
	+ Recompile for KSP 1.2 Pre-release. Untested.
* 2016-0827: 1.6.4.4 (magico13) for KSP 1.1.3 PRE-RELEASE
	+ Added workaround for RealChuteFAR. FAR should now work mostly correctly.
* 2016-0708: 1.6.4.3 (magico13) for KSP 1.1.3 PRE-RELEASE
	+ This is an untested prerelease for KSP 1.1.3 that is simply a recompile. It may fix the issues with RealChute. It requires the last full release as a base, then replace the old .dll with this one.
* 2016-0423: 1.6.4.2 (magico13) for KSP 1.1.2. PRE-RELEASE
	+ Small update to fix some issues with mods using the API. KCT should work now. KK might work now but untested.
* 2016-0422: 1.6.4.1 (magico13) for KSP 1.1.2. PRE-RELEASE
	+ Should now work with RealChutes v1.4
	+ Note that some discrepancies between what the editor helper predicts and what is seen in flight have been noticed. I'm looking into it.
* 2016-0503: 1.6.4.0 (magico13) for KSP 1.1.2.
	+ Update to KSP 1.1.2.
* 2016-0221: 1.6.3 (magico13) for KSP 1.0.5
	+ Fix for kerbals being killed in landed vessels
	+ MechJeb should appear as a valid control source for landed recovery (untested)
* 2016-0102: 1.6.2 (magico13) for KSP 1.0.5
	+ Fix for things below 23km not being added to Recovery Queue
	+ Fix for ModuleAblators that don't use Ablator resource
* 2015-1229: 1.6.1 (magico13) for KSP 1.0.5
	+ Small bugfix update to make powered recovery work properly with pre-recovered Kerbals.
* 2015-1221: 1.6.0 (magico13) for KSP 1.0.5
	+ Changes since 1.5.8:
		- Hovering over Stock the SR icon in flight lets you preview the SR window
		- TweakScaled parachutes now supported
		- Tourists and other crew are pre-recovered while in the flight scene, preventing contract failure
		- Improved the Editor Helper. Now splits the vessel into individual stages and provides velocity estimates for each stage. Will be improved after receiving feedback
		- New setting to completely remove all SR buttons, accessible only through the config file
* 2015-1109: 1.5.8 (magico13) for KSP 1.0.5
	+ Update to KSP 1.0.5
* 2015-0711: 1.5.7 (magico13) for KSP 0.24.2
	+ Fixed issues with kerbal recovery causing lost levels
	+ Fixed compatibility issue with RSS (checking only for Kerbin)
	+ Added info for when a stage is lost because no pilot or probe with SAS
	+ Fixed up Tracking Station related things a bit. Powered recovery should
	+ work now and you can terminate a flight and get things recovered as long as
	+ its in the atmosphere at the time (including "orbiting" vessels)
* 2015-0515: 1.5.6 (magico13) for KSP 0.24.2
	+ 1.5.6 - (05/15/2015)
		- Non-parachuted stages now have a fixed Vt of 200 m/s
		- Powered recovery should work properly now. Requires 300m/s of dV to work.
		- Added OnRecoveryProcessingStart and OnRecoveryProcessingFinish events to
	+ API, for Kerbal Konstructs support. Fire when first starting recovery code
	+ (after determining viability for recovery, but before Vt calculations) and at
	+ the end of recovery code (doesn't include information about recovery, just for
	+ cleaning up)
		- Support for RealChuteLite in FAR
		- Hopefully fixed issues with ridiculous distances from KSC. If not, now
	+ logging lat/lon of stage and KSC, to help debug.
* 2015-0502: 1.5.5 (magico13) for KSP 0.24.2
	+ 1.5.5 - (05/02/2015)
		- Updated to KSP 1.0.2
		- Fixed calculation of Vt for stages. Not 100% accurate, but fairly close
		- Added option to choose which Toolbar to use (when Toolbar Mod is installed)
		- Switched to KCT's code for calculating building levels, since that worked
	+ in 0.90
* 2015-0428: 1.5.4 (magico13) for KSP 0.24.2
	+ Updated to KSP 1.0
	+ Tie into the stock upgrade system.
	+ Tracking Center upgrades improve distance based returns
	+ Pilot kerbals or probes with SAS are needed for powered recovery
	+ Added FMRS support. The two shouldn't try to recover the same things now
	+ and should play nicely together!
		- Added colors in recovery messages
		- A bit of Flight GUI rework to make it easier to use, take less screen
	+ space, and generally be better.
		- Deadly Reentry support moved to use Stock system. Doesn't scale with Stock
	+ setting yet, but if Stock is set to 0% then SR won't burn things up.
* 2014-1223: 1.5.3 (magico13) for KSP 0.24.2
	+ Fixed issue with losing experience on recovery. Kerbals now gain
	+ experience as appropriate for landing on Kerbin.
* 2014-1216: 1.5.2.1 (magico13) for KSP 0.24.2
	+ Sorry for the hotfix update!
		- Made min TWR setting functional
		- Fixed issue with calculating parachute drag values that caused parachute
	+ recovery to not function.
* 2014-1216: 1.5.2 (magico13) for KSP 0.24.2
	+ 1.5.2 - (12/15/2014)
		- Compatibility update for KSP 0.90
		- Automatic recovery of launch clamps when they are unloaded.
		- Right clicking on a stage in the flight GUI will now delete it.
		- Added indicator to flight GUI showing which stage is selected.
		- Several bug fixes.
		- Contains a bug where kerbals will lose experience if they are in the craft
	+ when it's "recovered". Will be fixed soon.
* 2014-1007: 1.5.1 (magico13) for KSP 0.24.2
	+ 1.5.1 - (10/-07/2014)
		- Compatibility update for KSP 0.25
	+ NOTE: Does not include any changes due to strategies, so you won't get extra
	+ recovery factor for the one (yet).
* 2014-0906: 1.5.0 (magico13) for KSP 0.24.2
	+ Added Ignore List. Any stages made up entirely of parts in the ignore list
	+ won't attempt to be recovered.
		- Reworked the flight-GUI a bit. Made it smaller, draggable, and minimizes to
	+ just the list until a stage is selected. Hopefully even less intrusive now.
		- Found a general solution to the fuel use problem for powered recovery. Can
	+ now handle engines that require any fuel amounts without being CPU intensive.
		- Forced no checking on scene change. Should fix erroneous messages appearing
	+ on scene change for some users.
* 2014-0830: 1.4.3 (magico13) for KSP 0.24.2
	+ Should have been 0.01 pressure. I'm sorry about the second update! :(
* 2014-0830: 1.4.2 (magico13) for KSP 0.24.2
	+ Changed recovery code to check for altitudes above 100 meters and pressures
	+ above 0.1 instead of just searching for below 35km.
		- Fixed issue with displaying orbital velocity vector instead of speed in
	+ Flight GUI.
		- Was returning funds even for stages that had burned up, fixed now.
* 2014-0822: 1.4.1.0 (magico13) for KSP 0.24.2
	+ Added error catching to recovery code. Even if there's a bug, it shouldn't
	+ break your game now.
		- Removed a bunch of debug code from the Powered Recovery code.
		- Remembered to include the license in the download.
* 2014-0818: 1.4.0.0 (magico13) for KSP 0.24.2
	+ Powered recovery. Controlled stages can be landed with their engines.
	+ Requirements will be listed in a separate section.
		- Editor helper now shows results for current fuel levels and with empty fuel
	+ levels.
		- Several small improvements to flight GUI (wording and such).
		- Several bug fixes for Vt calculation and with stock parachutes and crashes.
* 2014-0807: 1.3.0.0 (magico13) for KSP 0.24.2
	+ No changelog provided
