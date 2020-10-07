# Stage Recovery /L Unofficial

This mod allows funds to be recovered, at a reduced rate, from dropped stages so long as they have parachutes attached (not necessarily deployed).

Unofficial fork by Lisias.


## In a Hurry

* [Latest Release](https://github.com/net-lisias-kspu/StageRecovery/releases)
	+ [Binaries](https://github.com/net-lisias-kspu/StageRecovery/tree/Archive)
* [Source](https://github.com/net-lisias-kspu/StageRecovery)
* Documentation
	+ [Project's README](https://github.com/net-lisias-kspu/StageRecovery/blob/master/README.md)
	+ [Install Instructions](https://github.com/net-lisias-kspu/StageRecovery/blob/master/INSTALL.md)
	+ [Change Log](./CHANGE_LOG.md)
	+ [TODO](./TODO.md) list


## Description

This mod allows funds to be recovered, at a reduced rate, from dropped stages so long as they have parachutes attached (not necessarily deployed).

What can this mod do:

* Recover funds from stages that have enough parachutes to drop the terminal velocity to a safe level
* Two funds recovery models: Flat Rate, where once the velocity drops below a cutoff a flat rate is returned; or Variable Rate (default), where the velocity within an intermediate range determines the recovery percentage (following a quadratic relationship)
* Recovery percentage (flat rate model only) is configurable (default of 75% of the normal for the current distance from KSC, 100% if a probe core or Kerballed command pod is attached)
* Works with Stock parachutes and Real Chutes parachutes
* All cutoff velocities are easily configurable with in-game sliders in the in-game settings menu
* Recovery does not require that parachutes are deployed, only attached (but it is a good idea to deploy them at low altitudes)
* Recover Kerbals in dropped/returned stages (improved in 1.6.0, hopefully no more contract failures)
* Recover science in dropped/returned stages
* Integrates with stock message system (reports can be disabled in the setting file)
* Easy to use, non-intrusive GUI for viewing Recovered and Destroyed Stages.
* Editor helper to help you figure out just how many parachutes you need.
* Has an easy to use API that other mods can tie into to be notified of recovery events (success or failure)
* Support for KSP-AVC for version checking
* Powered recovery of stages. Land with rockets, instead of just parachutes!
* Unfocused stages can be destroyed by thermal heating if they are going too fast (check below for further details, it's not exactly a scientific way of handling this)
* Designed to work alongside [FMRS](https://github.com/net-lisias-kspu/StageRecovery)! If FMRS is deactivated, SR will handle recovery. If FMRS isn't handling parachute stages or has deferred them to SR, then SR will handle them while FMRS handles controlled stages.
* With 1.7.1, more advanced support for FMRS. With the new [RecoveryController integration](https://forum.kerbalspaceprogram.com/index.php?/topic/158970-12-recoverycontroller-let-fmrs-and-stagerecovery-work-together) you can designate stages to be recovered by StageRecovery, FMRS, auto, or no mods during construction or flight.

### Powered Recovery requirements:

Now you can be your very own Elon Musk and have your rockets land with their engines! This feature is a work in progress, however, and will be improved in future updates. With that said, if you are having issues with recovery, first try going into the settings and disabling Powered Recovery, then you can report your bugs if they didn't go away.

So, what are the requirements for powered recovery?

* An activated engine (or multiple), excluding SolidFuel powered engines (engine ISP will be properly averaged).
* Atmospheric Delta-V of at least 1.5 times the terminal velocity after parachutes. (about 300 m/s without chutes, otherwise just 1.5x the terminal velocity indicated in the editor helper)
* A point of control, such as a probe body or Kerballed command pod, that has SAS capabilities (so a Pilot kerbal or a probe with SAS)
* A total Thrust-To-Weight ratio of greater than 1. Even if using parachutes.

Explicitly calculating the fuel amounts required for every engine is hard. Several approximations are made:

1. All engines are averaged together to act as a single engine.
2. All required propellant types are assumed to be the same as the first engine (non-SRB) found.
3. This is designed primarily for stock-alike engines. Mod engines that require strange fuels may have unexpected issues.

Powered recovery is made possible by Malkuth, of [Mission Controller Extended](https://forum.kerbalspaceprogram.com/threads/43645-KSP-23-5-Mission-Controller-Extended-(EVA-Fix)-Version-69-(5-30-14)). Malkuth graciously [offered the MCE powered recovery code](https://forum.kerbalspaceprogram.com/threads/86677-0-24-2-StageRecovery-Recover-Funds-from-Dropped-Stages-v1-4-%288-18-14%29?p=1326559&viewfull=1#post1326559) to me, which was used as a base for the code implemented here.

### Some notes about stages burning up:

The chance of burning up should be 100 x 2 x [(srf_speed / DR_Velocity) - 1], which in other words is 2% per 1% that the surface speed is faster than the DR Velocity setting. As an example, if you set the DR_Velocity to 1500, then a 2000 m/s surface speed will have a 67% chance of burning up, rather than 0% at the default setting of 2000 m/s.

Adding a heat shield can reduce this chance by up to 100% (note that the chance of burning up can exceed 100%) based on how much material remains. If 50% of the heat shield material remains, then the chance of burning up reduces by 50%. 100% remaining -> 100% reduction, 10% remaining -> 10% reduction, and so on.

As an example, consider a stage going 3200 m/s at the point of "deletion" when SR kicks in. This stage has a 120% chance of burning up, meaning it will always be destroyed if it doesn't have a heatshield. Luckily, it has a heatshield attached that has 80% of the ablator left. This reduces the chance of burning up by 80%, down to 40% (120-80=40), greatly increasing the odds of the stage surviving reentry.

As of 1.6.6 for KSP 1.2.2, stages eligible for powered recovery will attempt to reduce their speed to below the minimum speed for burning up by using fuel.

**PSA: Set your chutes to open at Min Pressure of 0.5 and activate/stage them with your stages!**

KSP 1.0 added reentry heating (and parachute breaking) and an increased physics range (now 22.5km). If you drop a stage below ~10km it might hit the ground before you are out of physics range. Setting Min Pressure to 0.5 and activating the chutes will cause them to deploy around 4.5 km at a hopefully low enough speed. Set it higher than 0.5 if you'll be dropping them at speeds higher than 250 m/s at altitudes around 5km or less so they aren't ripped off.


## Installation

Detailed installation instructions are now on its own file (see the [In a Hurry](#in-a-hurry) section) and on the distribution file.

## License:

This work is licensed as follows:

* [GPL 3.0](https://www.gnu.org/licenses/gpl-3.0.txt).
    + You are free to:
        - Use : unpack and use the material in any computer or device
        - Redistribute : redistribute the original package in any medium
        - Adapt : Reuse, modify or incorporate source code into your works (and redistribute it!)
    + Under the following terms:
        - You retain any copyright notices
        - You recognise and respect any trademarks
        - You don't impersonate the authors, neither redistribute a derivative that could be misrepresented as theirs.
        - You credit the author and republish the copyright notices on your works where the code is used.
        - You relicense (and fully comply) your works using GPL 3.0 (or later)
        - You don't mix your work with GPL incompatible works.
* Contains code from contributions licensed under the [MIT](https://opensource.org/licenses/MIT).
	* See [LICENSE](./LICENSE) for details


Please note the copyrights and trademarks in [NOTICE](./NOTICE).

### Important

Besides the [authorisation](https://forum.kerbalspaceprogram.com/index.php?/topic/78226-14-stagerecovery-recover-funds-from-dropped-stages-v180-march-11-2018/&do=findComment&comment=3452853) ([local copy](https://github.com/net-lisias-kspu/StageRecovery/blob/master/Docs/LMJ/PermissionToChangeOriginalLicense.png)) from the original [Author](https://forum.kerbalspaceprogram.com/index.php?/profile/73338-magico13/), granting permission to current [Maintainer](https://forum.kerbalspaceprogram.com/index.php?/profile/129964-linuxgurugamer/) to switch the license to MIT, I found prudent to keep the license on the GPLv3 as stated on this [commit](https://github.com/net-lisias-kspu/StageRecovery/commit/e24cf465bf69e795eaea017aa3452a6ae3484ea1) ([link](https://github.com/magico13/StageRecovery/commit/e24cf465bf69e795eaea017aa3452a6ae3484ea1) to the commit on the original repository).

The rationale is that the original Author accepted contributions from third parties, that were not consulted about the permission for switching licenses. Besides the original Author being still the owner of his commits, the contributors are still the owners of **their** commits - and without their permission, their code **must** be removed from the codebase in order to allow license switching.

I chose the safer and easier way out of the mess: I switched back to GPLv3. With the new commits licensed under the MIT, I can legally relicense such contributions under GPLv3 avoiding any potential problems in the future - not to mention being able to merge fixes done by forks of the original work.

* List of contributors affected and their commits
	+ [fommil](https://github.com/net-lisias-kspu/StageRecovery/commits?author=fommil)
		- [e45e519](https://github.com/net-lisias-kspu/StageRecovery/commit/e45e5197f28e5bb5706683cfc75128d49e853fb2) 
	+ [Kerbas-ad-astra](https://github.com/net-lisias-kspu/StageRecovery/commits?author=Kerbas-ad-astra)
		- [2ef028c](https://github.com/net-lisias-kspu/StageRecovery/commit/2ef028ccd117aefbbc33f011177b1124d560d8b0)
		- [91f4ba6](https://github.com/net-lisias-kspu/StageRecovery/commit/91f4ba6c6eb6433831ff3706abb46ff28661b5cf)
	+ [rem02](https://github.com/net-lisias-kspu/StageRecovery/commits?author=rem02)
		- [28bf76d](https://github.com/net-lisias-kspu/StageRecovery/commit/28bf76d845e3d16a142e96198bb0d2be91e2a5cc)
		- [3aca4a9](https://github.com/net-lisias-kspu/StageRecovery/commit/3aca4a9dc37a842a5d8d83fa7dee4d7c4b6021f1)
		- [eed032b](https://github.com/net-lisias-kspu/StageRecovery/commit/eed032bc7cc3df92313100995b64c29039cee6d8)
	+ [phwoelfel](https://github.com/net-lisias-kspu/StageRecovery/commits?author=phwoelfel)
		- [d824a14](https://github.com/net-lisias-kspu/StageRecovery/commit/d824a14bb8554d90c967baa47b6d1fd504e14835)
		- [ee74a58](https://github.com/net-lisias-kspu/StageRecovery/commit/ee74a580762d89d11f14c8dbf0720b4d9ef911a4)
	+ Commits of the current maintainer were omitted as he retains the ownership of such material.


## UPSTREAM

* [LinuxGuruGamer](https://forum.kerbalspaceprogram.com/index.php?/profile/129964-linuxgurugamer/)
	+ [Forum](https://forum.kerbalspaceprogram.com/index.php?/topic/158970-*)
	+ [Spacedock](https://spacedock.info/mod/219/StageRecovery)
	+ [Github](https://github.com/linuxgurugamer/StageRecovery)
* [magico13](https://forum.kerbalspaceprogram.com/index.php?/profile/73338-magico13/) ROOT
	+ [Forum](https://forum.kerbalspaceprogram.com/index.php?/topic/78226-*/)
	+ [CurseForge](https://kerbal.curseforge.com/projects/stagerecovery?gameCategorySlug=ksp-mods&projectID=223119) M.I.A.
	+ [SpaceDock](http://spacedock.info/mod/219/StageRecovery)
	+ [Github](https://github.com/magico13/StageRecovery)
* Some interesting parallel forks:
	+ [PaletzTheWise](https://github.com/PaletzTheWise/StageRecoveryRGU)
	+ [mwerle](https://github.com/mwerle/StageRecovery)
 
