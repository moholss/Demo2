//------------------------------------ Journey_PF_Standby_State  --------------------------------------------------------
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
using Etas.Eas.Atcl.Interfaces;
using Etas.Eas.Atcl.Interfaces.Ports;

namespace HIL_Test_Script
{
    /// <summary>
    /// This section is main entry point of executable
    /// The major functionalities are described here are common to all test case project
    /// </summary>
    partial class Journey_PF_Standby_State : TestCase
    {
        #region Main Entry Point and Constructor

        private IPortMA maPort = null;
        /// <summary>
        /// The Test Case is divided into 3 main sections 
        /// </summary>
        static void Main(string[] args)
        {
            // creating a local instance of Test Case
            Journey_PF_Standby_State tc = new Journey_PF_Standby_State();
            try
            {
                /// The Init Test Method is at high level having 4 methods, 
                /// Register Ports, Register Parameters, Load Parameters and  Register Test Meta Data
                tc.InitTest();
                /// This is the method were the actual test case is defined.
                tc.PerformTest();
                /// This is the method were the actions taken in the test case are reset here so that the next test case is executed correctly.
                tc.ResetTest();
            }
            catch (Exception ex)
            {
                // On exception, set the test case verdict to error, display the error
                // message in the Test Handler and the report.
                tc.Error();
                tc.Reporting.LogExtension(string.Format("Exception occurred in Test Case: {0}", ex.Message));
                tc.Reporting.SetErrorText(0, ex.Message, 0);
            }

            finally
            {
                // This Finish Method is not defined in this test case template it is inherited from the base class.
                tc.Finished();
            }

        }
        // Constructor of the test case class
        public Journey_PF_Standby_State()
                : base("Journey_PF_Standby_State")
        {

        }
        #endregion
    }
}
