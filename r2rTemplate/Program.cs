using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using r2rStudio.Library.Web;
using OpenQA.Selenium;
using r2rStudio.Library.Database;
using System.Data;
using r2rStudio.Library.Office.Excel;
using OfficeOpenXml;
using r2rStudio.Library.IO;
using r2rStudio.Library.Email;



namespace r2rTemplate
{
    class Program
    {


        static void Main(string[] args)
        {
            //Create ConnectToDatabase object 
            r2rConnectToDatabase Connect_1 = new r2rConnectToDatabase();
            //Assign Server value for ConnectToDatabase 
            Connect_1.Server = "c1w28278.itcs.hpicorp.net";
            //Assign Database value for ConnectToDatabase 
            Connect_1.Database = "PdmDbDev";
            //Assign User value for ConnectToDatabase 
            Connect_1.User = "sa";
            //Assign Password value for ConnectToDatabase 
            Connect_1.Password = "omni1234$$";
            //To Call ConnectToDatabase function 

            if (Connect_1.DoAction() == false)
            {
                Console.WriteLine(Connect_1.ErrorMessage);
                System.Threading.Thread.Sleep(5000);
                return;
            }
            //Create NonDataQuery object 
            r2rNonDataQuery NonDataQuery_1 = new r2rNonDataQuery();
            //Assign Query value for NonDataQuery 
            NonDataQuery_1.Query = "Insertr2rLogMst";
            //Assign Parameters value for NonDataQuery 
            NonDataQuery_1.Parameters = new string[] { "@varLogText", "@varEmail" };
            //Assign Valueslist value for NonDataQuery 
            NonDataQuery_1.ValuesList = new string[] { "PDM Portal Automation completed", "shameem.ahmed.a@hp.com" };
            //Assign sqlConnection value for NonDataQuery 
            NonDataQuery_1.SqlConn = Connect_1.sqlConnection;
            //To Call NonDataQuery function 

            if (NonDataQuery_1.DoAction() == false)
            {
                Console.WriteLine(NonDataQuery_1.ErrorMessage);
                System.Threading.Thread.Sleep(5000);
                return;
            }

        }
    }
}

