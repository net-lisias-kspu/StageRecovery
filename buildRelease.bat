
@echo off

rem Put the following text into the Post-build event command line:
rem without the "rem":

rem start /D $(SolutionDir) /WAIT deploy.bat  $(TargetDir) $(TargetFileName)
rem 
rem if $(ConfigurationName) == Release (
rem 
rem start /D $(SolutionDir) /WAIT buildRelease.bat $(TargetDir) $(TargetFileName)
rem 
rem )


rem Set variables here

rem H is the destination game folder
rem GAMEDIR is the name of the mod folder (usually the mod name)
rem GAMEDATA is the name of the local GameData
rem VERSIONFILE is the name of the version file, usually the same as GAMEDATA,
rem    but not always
rem LICENSE is the license file
rem README is the readme file

SETLOCAL enabledelayedexpansion

set GAMEDIR=StageRecovery
set GAMEDATA="GameData\"
set VERSIONFILE=%GAMEDIR%.version
set LICENSE=License.txt
set README=ReadMe.md

set RELEASEDIR="%~dp0\..\release"
IF NOT EXIST "%RELEASEDIR%" (
	MKDIR "%RELEASEDIR%"
)
set "ZIP=C:\Program Files\7-zip\7z.exe"
IF NOT EXIST "%ZIP%" (
	ECHO.Configuration error, could not find 7-Zip executable.
	ECHO.Please download and install 7-Zip from www.7-zip.org
	PAUSE
	GOTO DONE
)
REM set JQ="c:\local\jq-win64.exe"
set "JQ=S:\Personal\KSP\bin\jq-win64.exe"
IF NOT EXIST "%JQ%" (
	ECHO.Configuration error, could not find JQ utility.
	ECHO.Please download JQ from stedolan.github.io/jq/download/ and install into "C:\local"
	PAUSE
	GOTO DONE
)


rem Copy files to GameData locations

copy /Y "%1%2" "%GAMEDATA%\%GAMEDIR%\Plugins"
copy /Y %VERSIONFILE% %GAMEDATA%\%GAMEDIR%
copy /Y ..\MiniAVC.dll %GAMEDATA%\%GAMEDIR%

if "%LICENSE%" NEQ "" copy /y  %LICENSE% %GAMEDATA%\%GAMEDIR%
if "%README%" NEQ "" copy /Y %README% %GAMEDATA%\%GAMEDIR%

rem Get Version info

copy %VERSIONFILE% tmp.version
set VERSIONFILE=tmp.version

rem The following requires the JQ program, available here: https://stedolan.github.io/jq/download/
"%JQ%" ".VERSION.MAJOR" %VERSIONFILE% >tmpfile
set /P major=<tmpfile

"%JQ%" ".VERSION.MINOR"  %VERSIONFILE% >tmpfile
set /P minor=<tmpfile

"%JQ%" ".VERSION.PATCH"  %VERSIONFILE% >tmpfile
set /P patch=<tmpfile

"%JQ%" ".VERSION.BUILD"  %VERSIONFILE% >tmpfile
set /P build=<tmpfile
del tmpfile
del tmp.version
set VERSION=%major%.%minor%.%patch%
if "%build%" NEQ "0"  set VERSION=%VERSION%.%build%

echo Version:  %VERSION%


rem Build the zip FILE
cd %GAMEDATA%\..

set FILE="%RELEASEDIR%\%GAMEDIR%-%VERSION%.zip"
IF EXIST %FILE% del /F %FILE%
"%ZIP%" a -tzip %FILE% GameData

ECHO.SUCCESS! StageRecovery %VERSION% release created in %FILE%.
pause

:DONE
