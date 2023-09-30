//------------------------------------ Journey_PF_Standby_State  -------------------------------------------------------
// This module is genrated by template.
// Requested by : HIL_USER  on : 2/24/2020 12:29:53 PM
// The Module is generated on : PUGD2L8B9F2
// Copyright :  - 2020
// ComConfig.XML should be present @location : C:\TestScripts\Configuration\ComConfig.xml
// Project out file should be present @location : C:\TestScripts\<projectname>\ProjectSpecific
// MemoryMappedVariable.cs and .xml should be present @location : C:\TestScripts\<projectname>\ProjectSpecific
// GESE_HIL_Labels.dll should be present @location : C:\TestScripts\<projectname>\ProjectSpecific
//-----------------------------------------ver-3.7------------------5 Feb 2018--------------------------------

using System;
using System.Reflection;
using Etas.Eas.Atcl.Interfaces.Ports;
using Etas.Eas.Atcl.Interfaces.Verdicts;
using System.Threading;
using Etas.Eas.Atcl.Interfaces.Types;
using NTL_HIL;
using NTL;
using System.Diagnostics;

namespace HIL_Test_Script
{
    partial class Journey_PF_Standby_State
    {
        Verdict sectionVerdict = new Verdict(VerdictCode.Pass);
        MVM modelVariable = new MVM();
        Stopwatch st = new Stopwatch();
        Library_Functions_electrical_signal Lib_Loads = new Library_Functions_electrical_signal();
        Library_Functions_Communication Lib_comm = new Library_Functions_Communication();
        Library_Integration Lib_intgr = new Library_Integration();

        /// Method were the test functionality is scripted
        private void PerformTest()
        {
            // Begin section in report with Method Name
            Reporting.SectionBegin(MethodInfo.GetCurrentMethod().Name);
            try
            {
                // Test script goes here
                /******************************************
                * Make sure to turn on power to ACU board
                ******************************************/
                #region INIT
                Lib_Loads.Init(this, maPort, appliance);
                Lib_comm.Init1(this, maPort, appliance, NKM, Lib_Loads);
                Lib_intgr.Init(appliance, Lib_Loads);
                //Lib_comm.Init(this, maPort, appliance, Payload, Lib_Loads);
                #endregion INIT

                #region Powering UP ACU board
                Reporting.LogExtension("Switching ON ACU");
                modelVariable.PowerOnswitch.Value = 1;
                maPort.SetModelValue(modelVariable.PowerOnswitch);
                #endregion

                #region  ONLY FOR MEGA HMI INITILIZATION
                Lib_comm.Start_Timer_for_seconds(5);
                Lib_intgr.InitialiseMegaHmiData();
                #endregion

                #region Getting current regulation packet from ACU
                // API11Parser class variables abandonded from Template : 3.6
                // Please use NTL_HIL.dll version greater than 1.0.6570
                #endregion

                /******************************************
                * Actual test script goes after this
                ******************************************/

                #region Test case Implementation
                if (Lib_comm.Check_Precontions_1() == 1)
                {
                    Test_Case_Implementation();
                }
                else
                {
                    Lib_Loads.ProvideVerdict("System state is not in Programming", VerdictCode.Fail);
                }
                #endregion


                /******************************************
                * Actual test ends here
                ******************************************/

                #region Switching OFF ACU board
                Reporting.LogExtension("Switching ON ACU");
                modelVariable.PowerOnswitch.Value = 0;
                maPort.SetModelValue(modelVariable.PowerOnswitch);
                #endregion

                /******************************************
                 Section verdict Status
                ******************************************/
                #region Verdict_Set 
                if (Lib_Loads.verdict_status > 0)
                {
                    sectionVerdict.Fail();
                    Fail();
                    Reporting.SetInfoText(0, string.Format("Test Case: Journey_PF_Standby_State : Fail "), 0);
                    Reporting.LogExtension("Test Case: Journey_PF_Standby_State : please che3ck Error ");
                }
                else
                {
                    sectionVerdict.Pass();
                    Pass();
                    Reporting.SetInfoText(0, string.Format("Test Case: Journey_PF_Standby_State : Pass "), 0);
                    Reporting.LogExtension("Test Case: Journey_PF_Standby_State : please check Error ");
                }
                Reporting.SectionFinished("Test Case: Journey_PF_Standby_State :", sectionVerdict);
                #endregion
            }
            catch (Exception ex)
            {
                sectionVerdict.Error();
                Reporting.SetErrorText(0, ex.Message, 0);
                // Stop Model Access port if not stopped.
                if (maPort.GetState() != PortStatusEnum.PortStopped)
                {
                    maPort.Stop();
                }
                // Close Model Access port if not closed.
                if (maPort.GetState() != PortStatusEnum.PortClosed)
                {
                    maPort.Close();
                }
                Reporting.LogExtension("Exception closed the model");
                throw (ex);
            }
            finally
            {
                appliance.StopLog();
                //StopLogging();
                Reporting.SectionFinished("", sectionVerdict);
            }
        }

        #region Test case started
        private void Test_Case_Implementation()
        {
            #region Press Power ON/ OFF key to transit from Standby to Prg
            Lib_Loads.PressAndRelease_HMI_On_Off_Key();
            Lib_comm.Start_Timer_for_seconds(3);
            #endregion

            #region Check system state
            if (Lib_comm.Comm_Request_Specific_variable_System_State() != (byte)Library_Functions_Communication.Cycle_state.Programming)
                Lib_Loads.ProvideVerdict("System did not enter into Programming state", VerdictCode.Fail);
            else
                Lib_Loads.ProvideVerdict("System has entered into Programming state", VerdictCode.Pass);
            #endregion

            #region Press Power ON/ OFF key tp transit from Prg tp Standby
            Lib_Loads.PressAndRelease_HMI_On_Off_Key();
            Lib_comm.Start_Timer_for_seconds(3);
            #endregion

            #region PF
            Lib_Loads.Power_Off();
            Lib_comm.Start_Timer_for_seconds(10);

            Lib_Loads.Power_On();
            Lib_comm.Start_Timer_for_seconds(10);
            #endregion

            #region Check LED & System state
            bool[] res = Lib_intgr.GetLEDState(new string[] { "Sense", "Soak", "Wash", "Done", "Cold", "Cool", "Warm", "Hot", "Using Softner", "Not used", "Extra Rinse", "Not used", "30min Presoak", "Not used", "Deep water", "Lid Lock" });
            bool result = false;
            for (int i = 0; i < res.Length; i++)
            {
                result = result || res[i];
            }
            Lib_comm.printInHandlerAndReport_comm(" is any LED On : " + result);

            if (Lib_comm.Comm_Request_Specific_variable_System_State() == 1 && result == false)
                Lib_Loads.ProvideVerdict("ALL LEd Off & System entered into Standby state", VerdictCode.Pass);
            else
                Lib_Loads.ProvideVerdict("System has not entered into standby state", VerdictCode.Fail);

            #region Check system state
            if (Lib_comm.Comm_Request_Specific_variable_System_State() != (byte)Library_Functions_Communication.Cycle_state.Programming)
                Lib_Loads.ProvideVerdict("System did not enter into Programming state", VerdictCode.Fail);
            else
                Lib_Loads.ProvideVerdict("System has entered into Programming state", VerdictCode.Pass);
            #endregion

            #endregion
        }
        #endregion
    }
}

