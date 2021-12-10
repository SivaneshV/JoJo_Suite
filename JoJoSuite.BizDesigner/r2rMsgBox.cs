using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace JoJoSuite.UI
{
    public enum r2rMsgBoxResult { Ok, Cancel, No };

    public enum r2rMsgBoxButtons { Ok, OkCancel, OkNoCancel };

    public class r2rMsgBox
    {
        Window parent = new Window();

        public r2rMsgBox(Window Owner)
        {
            parent = Owner;
        }

        #region errorfile
        public static void errorfile(string methodname, Exception ex)
        {
            string dir = System.AppDomain.CurrentDomain.BaseDirectory + "Errorlog";  // folder location
            if (!Directory.Exists(dir))
            {
                Directory.CreateDirectory(dir);
            }
            File.AppendAllText(dir + "\\Error.txt", "Message :" + ex.Message + "<br/>" + Environment.NewLine + "methodname: " + methodname + Environment.NewLine + "StackTrace :" + ex.StackTrace +
         "" + Environment.NewLine + "Date :" + DateTime.Now.ToString());
            string New = Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine;
            File.AppendAllText(dir + "\\Error.txt", New);
        }
        #endregion

        public r2rMsgBoxResult Show(string Message, string Title, r2rMsgBoxButtons Buttons)
        {
            r2rMsgBoxResult res = r2rMsgBoxResult.Cancel; 

            AlertWindow aw1 = new AlertWindow();

            aw1.lblMsg.Content = Message;
            aw1.lblTitle.Content = Title;

            if (Buttons == r2rMsgBoxButtons.Ok)
            {
                aw1.btnOK.Content = "OK";
                aw1.btnNo.Visibility = aw1.btnCancel.Visibility = System.Windows.Visibility.Collapsed;
            }
            else if (Buttons == r2rMsgBoxButtons.OkCancel)
            {
                aw1.btnOK.Content = "OK";
                aw1.btnNo.Visibility = System.Windows.Visibility.Collapsed;
            }
            else
            {
                aw1.btnOK.Content = "Yes";
            }

            aw1.Owner = parent;

            aw1.ShowDialog();

            if (aw1.btnRes == 0)
            {
                res = r2rMsgBoxResult.Ok;
            }
            else if (aw1.btnRes == 1)
            {
                res = r2rMsgBoxResult.No;
            }
            else 
            {
                res = r2rMsgBoxResult.Cancel;
            }

            return res;
        }
    }
}
