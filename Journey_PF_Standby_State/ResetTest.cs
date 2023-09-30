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
using System.Reflection;
using Etas.Eas.Atcl.Interfaces.Ports;
using Etas.Eas.Atcl.Interfaces.Verdicts;

namespace HIL_Test_Script
{
    partial class Journey_PF_Standby_State
    {
        // Method to Reset the Action of Perform Test Function Method
        private void ResetTest()
        {
            Verdict sectionVerdict = new Verdict(VerdictCode.None);
            try
            {
                // Stop Port access
                // Need to re-visit this section again
                maPort.Stop();
                Reporting.SectionBegin(MethodInfo.GetCurrentMethod().Name);
                // Add your code here

                maPort = Factory.GetPortMA("ModelAccess");
                if (maPort.IsClosed)
                {
                    Reporting.LogExtension("State at reset if Model Access port is closed = " + maPort.GetState().ToString());
                    maPort.Create();
                    Reporting.LogExtension("State at reset after Model Access port created = " + maPort.GetState().ToString());
                    maPort.Timeout = -1;
                    if (maPort.GetState() != PortStatusEnum.PortToolConfigured)
                    {
                        // need to re-visit for this section
                        maPort.ConfigureTool("ECU", new string[] { "" }, new string[] { "", "default", "default", "default" });
                        Reporting.LogExtension("State = " + maPort.GetState().ToString());
                    }
                }

                if (maPort.GetState() != PortStatusEnum.PortConfigured)
                {
                    maPort.Configure(new string[0]);
                }
                maPort.Start();
                // ADD THE CODE BELOW THIS LINE. DONT CHANGE CODE ABOVE
                Reporting.LogExtension(" At Reset test method -- reseting configuration - if any");



                // END OF CODE. DONT ADD ANY CODE BELOW
                sectionVerdict.None();
                maPort.Stop();

            }
            catch (Exception ex)
            {
                Reporting.SetErrorText(0, string.Format("Execption during Test Case Reset! Exception message is {0}", ex.Message), 0);
                Error();
                throw (ex);
            }
            finally
            {
                Reporting.SectionFinished(sectionVerdict.ActualVerdictCode.ToString(), sectionVerdict);
                if (maPort.GetState() != PortStatusEnum.PortClosed)
                {
                    maPort.Close();
                    Reporting.LogExtension("Port is Closed in finally");
                }
            }
        }
    }
}
