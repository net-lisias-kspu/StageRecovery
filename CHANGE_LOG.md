# Stage Recovery :: Change log

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
