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

// Uncomment as per category 
//? Please do select any one of the category not to get "Object refernce not set to instance of an object" error
//#define COOKING
//#define REFRIGERATION
//#define HAWASHER
#define VAWASHER
//#define DRYER
//---------------------------------------------------------------------------------------------------------
using System;
using System.Threading;
using System.Reflection;
using Etas.Eas.Atcl.Interfaces.Verdicts;
using Etas.Eas.Atcl.Interfaces.Ports;
using ComConfig;
using NTL_HIL;
using NKM_HIL;
//---------------------------------------------------------------------------------------------------------

namespace HIL_Test_Script
{
    partial class Journey_PF_Standby_State
    {
#if (REFRIGERATION)
        Refrigeration appliance;
#endif
#if (COOKING)
			Cooking appliance;
#endif
#if (HAWASHER)
			HAWasher appliance;
#endif
#if (VAWASHER)
			VAWasher appliance;
#endif
#if (DRYER)
			Dryer appliance;
#endif


        // Please add the MemoryMappedVariables.cs file to avoid error
        MMVariable mmvar = new MMVariable();

        //NKM Parser object creation
        NkmHILClass NKM = new NkmHILClass();


        private void InitTest()
        {
            // Instantiation of communication configuration
            Config comConfig = new Config();

            Verdict sectionVerdict = new Verdict(VerdictCode.None);
            try
            {
                RegisterMetaData();
                RegisterParameters();
                LoadParameters();
                RegisterPorts();
                // Begin section in report with Method Name
                Reporting.SectionBegin(MethodInfo.GetCurrentMethod().Name);
                // Configure and Start Port Access
                // It checks if the LCO Project is Downloaded to EE. If not it will downloads the LCO projects to EE and will start the Simulation
                Reporting.LogExtension("Starting Experimental Environemnt, if not already active.");
                if (maPort.GetState() != PortStatusEnum.PortConfigured)
                {
                    maPort.Configure(new string[0]);
                    Reporting.LogExtension("State = " + maPort.GetState().ToString());
                }

                // Start the Model Access Port. If the Model Access Port is not started. It is required to Get or Set Model Values.
                Reporting.LogExtension("Starting Simulation....");
                maPort.Start();
                Reporting.LogExtension("State = " + maPort.GetState().ToString());

                // Initialise communication channel, as of Now CCB2
                Reporting.LogExtension("Initialising and Configuring Wired Communication....");
                var com = comConfig.ConfigCommunication();
                // Object Instantiation as per category
                // Communication initialised
#if (COOKING)
				appliance = new Cooking();
				appliance.Init(com.Port.ToString(), 0x0E);
				appliance.StartLog(@"C:\logs\");
#endif

#if (REFRIGERATION)
                appliance = new Refrigeration();
                appliance.Init(com.Port.ToString(), 0x0E);
                appliance.StartLog(@"C:\logs\");
#endif

#if (HAWASHER)
				appliance = new HAWasher();
				appliance.Init(com.Port.ToString(), 0x0E);
				appliance.StartLog(@"C:\logs\");
#endif

#if (VAWASHER)
				appliance = new VAWasher();
				appliance.Init(com.Port.ToString(), 0x0E);
				appliance.StartLog(@"C:\logs\");
#endif

#if (DRYER)
				appliance = new Dryer();
				appliance.Init(com.Port.ToString(), 0x0E);
				appliance.StartLog(@"C:\logs\");
#endif

                Reporting.LogExtension("Communication configuration done....");
            }
            catch (Exception ex)
            {
                Reporting.SetErrorText(0, string.Format("Execption during Test Case initialization! Exception message is {0}", ex.Message), 0);
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
                Reporting.LogExtension("Model closed by Exception at Initialisation.");
                throw (ex);
            }
            finally
            {
            }
        }
        /// <summary>
        /// 
        /// </summary>
        private void RegisterMetaData()
        {
            // Add Meta Data like Author of the Test Case
            // Version of the Test Case
            // Purpose of the Test Case
            // Reviewer of the Test Case ETC could be added to the report as well when the script is executed.
            // The LCA API "AddMetaData" takes Any two String Parameters as arguments. 
            //It adds a row in the Report for each Meta data Entry
            AddMetaData("Test Case Developer", "HIL_USER");
            AddMetaData("Copyright", " - 2020");
            AddMetaData("Version", Assembly.GetExecutingAssembly().GetName().Version.ToString()); // Update the Test Case version if any
            AddMetaData("Project", "Journey_PF_Standby_State");
            AddMetaData("Purpose", "<== Add the purpose for this test case here.==>");
        }
        /// <summary>
        /// 
        /// </summary>

        private void RegisterPorts()
        {
            // All the Port used in the test cased should be Registered here.
            // The Whirlpool Setup has only ModelAccess Port used so the same is registered here.
            maPort = Factory.GetPortMA("ModelAccess");
            if (maPort.IsClosed)
            {
                maPort.Create();
                maPort.Timeout = -1;
                if (maPort.GetState() != PortStatusEnum.PortToolConfigured)
                {
                    maPort.ConfigureTool("ECU", new string[] { "" }, new string[] { "", "default", "default", "default" });
                    Reporting.LogExtension("State = " + maPort.GetState().ToString());
                }
            }
            //TODO: Register any port used in test case here
        }

        private void StopLogging()
        {
#if (REFRIGERATION)
            appliance.StopLog();
#endif
#if (COOKING)
			appliance.StopLog();
#endif
#if (HAWASHER)
			appliance.StopLog();
#endif
#if (VAWASHER)
			appliance.StopLog();
#endif
#if (DRYER)
			appliance.StopLog();
#endif

        }
    }
}
