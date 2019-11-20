
@echo off

rem H is the destination game folder
rem GAMEDIR is the name of the mod folder (usually the mod name)
rem GAMEDATA is the name of the local GameData
rem VERSIONFILE is the name of the version file, usually the same as GAMEDATA,
rem    but not always

set H=%KSPDIR%
set GAMEDIR=StageRecovery
set GAMEDATA="GameData"
set VERSIONFILE=%GAMEDIR%.version

REM
REM Update development GameData directory.
REM
copy /Y "%1%2" "%GAMEDATA%\%GAMEDIR%\Plugins"
copy /Y %VERSIONFILE% %GAMEDATA%\%GAMEDIR%

REM
REM Update KSP GameData directory
REM
IF "%H%"=="" (
	ECHO.Invalid configuration; KSPDIR not specified.
	ECHO.Either set KSPDIR environment variable or edit this script to continue.
	PAUSE
	GOTO DONE
)
IF NOT EXIST "%H%\GameData" (
	ECHO.Invalid configuration; destination path %H%\GameData does not exist.
	PAUSE
	GOTO DONE
)

REM Comment out the following line if you don't want to automatically deploy
REM the build results to your KSP directory.
xcopy /y /s /I %GAMEDATA%\%GAMEDIR% "%H%\GameData\%GAMEDIR%"

ECHO.Success; build files copied to "%H%\GameData\%GAMEDIR%"
REM PAUSE

:DONE
