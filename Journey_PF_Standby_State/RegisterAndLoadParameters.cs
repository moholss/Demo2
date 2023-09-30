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
using System.Linq;

namespace HIL_Test_Script
{
    partial class Journey_PF_Standby_State
    {
        /// Update the ACU NODE ID for requesting Product Info
        private byte ACU_NODE_ID = 0x1;
        public byte ACUNodeId
        {
            get { return ACU_NODE_ID; }
        }

        private void RegisterParameters()
        {
            // Create the Parameter file with Parameters Registered in this Method.
            Factory.GetParameterManager().CreateTpaFile();
            //TODO: Register any parameters which should be exported to tpa here

            // Save the Parameter File. It will create finally a *.tpa file which is a XML file with all the above Registered parameters
            Factory.GetParameterManager().Save();
        }


        private void LoadParameters()
        {
            // Loads the Parameters from the Test Manager to the Test Script.
            // If the Parameter Value is Changed in the Script and not updated in the test Manager then it will not be reflected in the test case.
            // The Value shown in the Test Manager for a Parameter is used if the parameter is loaded here. 

            Reporting.LogExtension("Parameter Loaded...");
        }
    }
}
