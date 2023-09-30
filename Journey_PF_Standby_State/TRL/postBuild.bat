SET TCDCMD=%TESTAUTOMATIONSUITE_HOME%\TestTools\bin\TCDCMD.exe

SET TC=%1

"%TCDCMD%" StdExe "%TC%.exe" "%TC%.tpa" "%TC%.thd" "%TC%.tad" ComConfig.dll DatasetReadWrite.dll NLog.dll NTL.dll NTL_HIL.dll ProgramAgentLib.dll WhirlpoolCommunication.Common.dll WideBoxLib.dll WiredLibModule.dll GESE_HIL_Labels.dll