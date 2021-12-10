using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Data.SqlClient;

namespace r2rStudio.Database
{
    public partial class RunSQL : UserControl
    {

        private string sScript;
        private string sCodeFolder;

        Control _nextControl;
        Control _prevControl;

        private void txtTitle_Leave(object sender, EventArgs e)
        {
            //.BackColor = SystemColors.ControlDark;
            pnlRunsql.BackColor = SystemColors.ControlDark;
            pnlHeader.BackColor = txtTitle.BackColor = SystemColors.Control;
        }

        private void txtTitle_Enter(object sender, EventArgs e)
        {
            pnlRunsql.BackColor = pnlHeader.BackColor = txtTitle.BackColor = Color.Gold;
        }

        private void pnlRunsql_Click(object sender, EventArgs e)
        {
            txtTitle.Focus();
            this.OnClick(new EventArgs());
        }

        private void pnlRunsql_MouseDown(object sender, MouseEventArgs e)
        {
            //txtTitle.Focus();
            this.OnMouseDown(e);
        }

        private void pnlRunsql_MouseMove(object sender, MouseEventArgs e)
        {
            //txtTitle.Focus();
            this.OnMouseMove(e);
        }

        private void pnlRunsql_MouseUp(object sender, MouseEventArgs e)
        {
            //txtTitle.Focus();
            this.OnMouseUp(e);
        }

        private void txtTitle_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Delete)
            {
                this.DeleteControl(sender, e);
            }
        }

        public string Script
        {
            get
            {
                return sScript;
            }
            set
            {
                sScript = value;

                lblScript.Text = "Script: " + sScript;

                Invalidate();
            }
        }

        public string CodeFolder
        {
            get
            {
                return sCodeFolder;
            }
            set
            {
                sCodeFolder = value;

                Invalidate();
            }
        }

        public Control NextControl
        {
            get
            {
                return _nextControl;
            }
            set
            {
                _nextControl = value;

                Invalidate();
            }
        }

        public Control PreviousControl
        {
            get
            {
                return _prevControl;
            }
            set
            {
                _prevControl = value;
                Invalidate();
            }
        }

        //control events
        public event KeyEventHandler DeleteControl;
        public RunSQL()
        {
            InitializeComponent();
            txtTitle.Text = this.Name;
        }

        public string GetCodeSnippet()
        {
            string res = "//CODE NOT AVAILABLE";

            if (System.IO.File.Exists(sCodeFolder + @"\Db\ConnectToDb.txt"))
            {
                using (StreamReader reader = new StreamReader(sCodeFolder + @"\Database\RunSQL.txt"))
                {
                    res = reader.ReadToEnd();
                }

                res = res.Replace("script", Script);
                //res = res.Replace("{0}", sServer);
                //res = res.Replace("{1}", sDb);
                //res = res.Replace("{2}", sServer);
                //res = res.Replace("{3}", sPwd);
            }
            return res;
        }


        //public long ExecuteDataSet(SqlCommand sqlCommand, out DataSet dataSet)
        //{
        //    long returnCode = -1;
        //    string sqlConnectionString = string.Empty;
        //    SqlConnection sqlConnection = new SqlConnection(sqlConnectionString);
            
        //    dataSet = new DataSet();
        //    try
        //    {
               
        //        SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand);


        //        SqlCommand cmd = new SqlCommand("InsertGmbDetails", sqlConnection);
        //            cmd.CommandType = CommandType.Text;
                   
        //        sqlCommand.Connection = sqlConnection;
        //        DateTime Startdateandtime = DateTime.Now;
        //        sqlDataAdapter.Fill(dataSet);
               
        //        returnCode = 0;

        //    }
        //    catch (SqlException ex)
        //    {
        //        returnCode = 1000;
               
        //    }
        //    finally
        //    {


        //    }
        //    return returnCode;

        //}

    }
}
