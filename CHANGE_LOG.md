# Stage Recovery :: Change log

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
