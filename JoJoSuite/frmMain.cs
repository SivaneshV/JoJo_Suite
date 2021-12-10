using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

using r2rStudio.Logic;
using r2rStudio.Web;
using r2rStudio.Database;
using r2rStudio.Pdf;
using r2rStudio.Email;
using r2rStudio.Office.Excel;
using System.Configuration;
using System.IO;

namespace r2rStudio
{
    public partial class frmMain : Form
    {
        bool isDragStart = false;
        Point mouseLoc;
        List<r2rLine> lstLines = new List<r2rLine>();
        int count = 0;

        public frmMain()
        {
            InitializeComponent();

            cbCodeLang.SelectedIndex = 0;
            cbCodeType.SelectedIndex = 0;
        }

        private void label4_Click(object sender, EventArgs e)
        {
        }

        private void btnFileNew_Click(object sender, EventArgs e)
        {
            count++;
            tcMain.Visible = true;
            TabPage tpNew = new TabPage();

            tpNew.Text = "Untitled - " + count;
            tpNew.BackColor = Color.White;
            tpNew.AllowDrop = true;
            tpNew.AutoScroll = true;

            tpNew.Width = tcMain.Width;

            tpNew.DragDrop += new DragEventHandler(tabPage_DragDrop);
            tpNew.DragEnter += new DragEventHandler(tabPage_DragEnter);
            tpNew.MouseMove += new MouseEventHandler(tabPage_MouseMove);
            tpNew.Paint += new PaintEventHandler(tabPage_Paint);

            LogicStart s1 = new LogicStart();
            s1.Location = new Point((tpNew.Width / 2) - 37, 10);
            s1.Name = "LogicStart1";

            tpNew.Controls.Add(s1);

            tcMain.TabPages.Add(tpNew);

            tcMain.SelectedTab = tpNew;

            btnFileSave.Enabled = true;

        }

        private void frmMain_Load(object sender, EventArgs e)
        {
            tvComponent.Nodes["nodeWeb"].ExpandAll();
            tvComponent.Nodes["nodeLogic"].ExpandAll();
            tvComponent.Nodes["nodeDatabase"].ExpandAll();
            tvComponent.Nodes["nodePdf"].ExpandAll();
        }

        private void tvComponent_ItemDrag(object sender, ItemDragEventArgs e)
        {
            TreeNode selNode = (TreeNode)e.Item;
            DoDragDrop(selNode.Name, DragDropEffects.Copy);
        }

        private void tabPage_DragDrop(object sender, DragEventArgs e)
        {
            TabPage crTab = (TabPage)sender;

            e.Effect = DragDropEffects.Copy;

            var selNode = e.Data.GetData("System.String");

            //WEB - Maha
            if (selNode.ToString() == "nodeWebHPLogin")
            {
                HPLogin nc1 = new HPLogin();

                Point loc = crTab.PointToClient(new Point(e.X, e.Y)); ;
                loc.X -= (nc1.Width / 2);
                loc.Y -= (nc1.Height / 2);
                nc1.Location = loc;
                nc1.Click += new EventHandler(Control_Click);
                nc1.MouseDown += new MouseEventHandler(Control_MouseDown);
                nc1.MouseMove += new MouseEventHandler(Control_MouseMove);
                nc1.MouseUp += new MouseEventHandler(Control_MouseUp);
                nc1.DeleteControl += new KeyEventHandler(Control_DeleteControl);
                nc1.CodeFolder = ConfigurationManager.AppSettings["r2rCodeFolder"];

                crTab.Controls.Add(nc1);
                nc1.BringToFront();
            }
            //LOGIC - Shameem
            else if (selNode.ToString() == "nodeLogicIf")
            {
                LogicIf nc1 = new LogicIf();

                Point loc = crTab.PointToClient(new Point(e.X, e.Y)); ;
                loc.X -= (nc1.Width / 2);
                loc.Y -= (nc1.Height / 2);
                nc1.Location = loc;
                nc1.Click += new EventHandler(Control_Click);
                nc1.MouseDown += new MouseEventHandler(Control_MouseDown);
                nc1.MouseMove += new MouseEventHandler(Control_MouseMove);
                nc1.MouseUp += new MouseEventHandler(Control_MouseUp);
                nc1.DeleteControl += new KeyEventHandler(Control_DeleteControl);
                //nc1.CodeFolder = ConfigurationManager.AppSettings["r2rCodeFolder"];

                //nc1.DragDrop += new DragEventHandler(Logic_If_DragDrop);

                crTab.Controls.Add(nc1);
                nc1.BringToFront();
            }
            else if (selNode.ToString() == "nodeLogicWhile")
            {
                LogicWhile nc1 = new LogicWhile();

                Point loc = crTab.PointToClient(new Point(e.X, e.Y)); ;
                loc.X -= (nc1.Width / 2);
                loc.Y -= (nc1.Height / 2);
                nc1.Location = loc;
                nc1.Click += new EventHandler(Control_Click);
                nc1.MouseDown += new MouseEventHandler(Control_MouseDown);
                nc1.MouseMove += new MouseEventHandler(Control_MouseMove);
                nc1.MouseUp += new MouseEventHandler(Control_MouseUp);
                nc1.DeleteControl += new KeyEventHandler(Control_DeleteControl);

                crTab.Controls.Add(nc1);
                nc1.BringToFront();
            }
            else if (selNode.ToString() == "nodeLogicAssign")
            {
                LogicAssign nc1 = new LogicAssign();

                Point loc = crTab.PointToClient(new Point(e.X, e.Y)); ;
                loc.X -= (nc1.Width / 2);
                loc.Y -= (nc1.Height / 2);
                nc1.Location = loc;
                nc1.Click += new EventHandler(Control_Click);
                nc1.MouseDown += new MouseEventHandler(Control_MouseDown);
                nc1.MouseMove += new MouseEventHandler(Control_MouseMove);
                nc1.MouseUp += new MouseEventHandler(Control_MouseUp);
                nc1.DeleteControl += new KeyEventHandler(Control_DeleteControl);

                crTab.Controls.Add(nc1);
                nc1.BringToFront();
            }
            else if (selNode.ToString() == "nodeLogicFor")
            {
                LogicFor nc1 = new LogicFor();

                Point loc = crTab.PointToClient(new Point(e.X, e.Y)); ;
                loc.X -= (nc1.Width / 2);
                loc.Y -= (nc1.Height / 2);
                nc1.Location = loc;
                nc1.Click += new EventHandler(Control_Click);
                nc1.MouseDown += new MouseEventHandler(Control_MouseDown);
                nc1.MouseMove += new MouseEventHandler(Control_MouseMove);
                nc1.MouseUp += new MouseEventHandler(Control_MouseUp);
                nc1.DeleteControl += new KeyEventHandler(Control_DeleteControl);
                //nc1.CodeFolder = ConfigurationManager.AppSettings["r2rCodeFolder"];

                //nc1.DragDrop += new DragEventHandler(Logic_If_DragDrop);

                crTab.Controls.Add(nc1);
                nc1.BringToFront();
            }
            else if (selNode.ToString() == "nodeLogicBreak")
            {
                LogicBreak nc1 = new LogicBreak();

                Point loc = crTab.PointToClient(new Point(e.X, e.Y)); ;
                loc.X -= (nc1.Width / 2);
                loc.Y -= (nc1.Height / 2);
                nc1.Location = loc;
                nc1.Click += new EventHandler(Control_Click);
                nc1.MouseDown += new MouseEventHandler(Control_MouseDown);
                nc1.MouseMove += new MouseEventHandler(Control_MouseMove);
                nc1.MouseUp += new MouseEventHandler(Control_MouseUp);
                nc1.DeleteControl += new KeyEventHandler(Control_DeleteControl);
                //nc1.CodeFolder = ConfigurationManager.AppSettings["r2rCodeFolder"];

                //nc1.DragDrop += new DragEventHandler(Logic_If_DragDrop);

                crTab.Controls.Add(nc1);
                nc1.BringToFront();
            }
            else if (selNode.ToString() == "nodeLogicWait")
            {
                LogicWait nc1 = new LogicWait();

                Point loc = crTab.PointToClient(new Point(e.X, e.Y)); ;
                loc.X -= (nc1.Width / 2);
                loc.Y -= (nc1.Height / 2);
                nc1.Location = loc;
                nc1.Click += new EventHandler(Control_Click);
                nc1.MouseDown += new MouseEventHandler(Control_MouseDown);
                nc1.MouseMove += new MouseEventHandler(Control_MouseMove);
                nc1.MouseUp += new MouseEventHandler(Control_MouseUp);
                nc1.DeleteControl += new KeyEventHandler(Control_DeleteControl);
                //nc1.CodeFolder = ConfigurationManager.AppSettings["r2rCodeFolder"];

                //nc1.DragDrop += new DragEventHandler(Logic_If_DragDrop);

                crTab.Controls.Add(nc1);
                nc1.BringToFront();
            }
            //DATABASE - Kapil
            else if (selNode.ToString() == "nodeDatabaseConnect")
            {
                ConnectToDb nc1 = new ConnectToDb();

                Point loc = crTab.PointToClient(new Point(e.X, e.Y)); ;
                loc.X -= (nc1.Width / 2);
                loc.Y -= (nc1.Height / 2);
                nc1.Location = loc;
                nc1.Click += new EventHandler(Control_Click);
                nc1.MouseDown += new MouseEventHandler(Control_MouseDown);
                nc1.MouseMove += new MouseEventHandler(Control_MouseMove);
                nc1.MouseUp += new MouseEventHandler(Control_MouseUp);
                nc1.DeleteControl += new KeyEventHandler(Control_DeleteControl);
                nc1.CodeFolder = ConfigurationManager.AppSettings["r2rCodeFolder"];

                crTab.Controls.Add(nc1);
                nc1.BringToFront();
            }
            //EMAIL - Alster
            else if (selNode.ToString() == "nodeEmailLogin")
            {
                EmailLogin nc1 = new EmailLogin();

                Point loc = crTab.PointToClient(new Point(e.X, e.Y)); ;
                loc.X -= (nc1.Width / 2);
                loc.Y -= (nc1.Height / 2);
                nc1.Location = loc;
                nc1.Click += new EventHandler(Control_Click);
                nc1.MouseDown += new MouseEventHandler(Control_MouseDown);
                nc1.MouseMove += new MouseEventHandler(Control_MouseMove);
                nc1.MouseUp += new MouseEventHandler(Control_MouseUp);
                nc1.DeleteControl += new KeyEventHandler(Control_DeleteControl);
                nc1.CodeFolder = ConfigurationManager.AppSettings["r2rCodeFolder"];

                crTab.Controls.Add(nc1);
                nc1.BringToFront();
            }
            else if (selNode.ToString() == "nodeEmailGetEmails")
            {
                EmailRead nc1 = new EmailRead();

                Point loc = crTab.PointToClient(new Point(e.X, e.Y)); ;
                loc.X -= (nc1.Width / 2);
                loc.Y -= (nc1.Height / 2);
                nc1.Location = loc;
                nc1.Click += new EventHandler(Control_Click);
                nc1.MouseDown += new MouseEventHandler(Control_MouseDown);
                nc1.MouseMove += new MouseEventHandler(Control_MouseMove);
                nc1.MouseUp += new MouseEventHandler(Control_MouseUp);
                nc1.DeleteControl += new KeyEventHandler(Control_DeleteControl);
                nc1.CodeFolder = ConfigurationManager.AppSettings["r2rCodeFolder"];

                crTab.Controls.Add(nc1);
                nc1.BringToFront();
            }
            //EXCEL - Magesh
            else if (selNode.ToString() == "nodeExcelOpenWorkbook")
            {
                OpenWorkbook nc1 = new OpenWorkbook();

                Point loc = crTab.PointToClient(new Point(e.X, e.Y)); ;
                loc.X -= (nc1.Width / 2);
                loc.Y -= (nc1.Height / 2);
                nc1.Location = loc;
                nc1.Click += new EventHandler(Control_Click);
                nc1.MouseDown += new MouseEventHandler(Control_MouseDown);
                nc1.MouseMove += new MouseEventHandler(Control_MouseMove);
                nc1.MouseUp += new MouseEventHandler(Control_MouseUp);
                nc1.DeleteControl += new KeyEventHandler(Control_DeleteControl);
                nc1.CodeFolder = ConfigurationManager.AppSettings["r2rCodeFolder"];

                crTab.Controls.Add(nc1);
                nc1.BringToFront();
            }
            else if (selNode.ToString() == "nodeExcelCreateWorkbook")
            {
                CreateWorkbook nc1 = new CreateWorkbook();

                Point loc = crTab.PointToClient(new Point(e.X, e.Y)); ;
                loc.X -= (nc1.Width / 2);
                loc.Y -= (nc1.Height / 2);
                nc1.Location = loc;
                nc1.Click += new EventHandler(Control_Click);
                nc1.MouseDown += new MouseEventHandler(Control_MouseDown);
                nc1.MouseMove += new MouseEventHandler(Control_MouseMove);
                nc1.MouseUp += new MouseEventHandler(Control_MouseUp);
                nc1.DeleteControl += new KeyEventHandler(Control_DeleteControl);
                nc1.CodeFolder = ConfigurationManager.AppSettings["r2rCodeFolder"];

                crTab.Controls.Add(nc1);
                nc1.BringToFront();
            }
            //PDF - Vijay, Steve
            else if (selNode.ToString() == "nodePdfToText")
            {
                PdfToText nc1 = new PdfToText();

                Point loc = crTab.PointToClient(new Point(e.X, e.Y)); ;
                loc.X -= (nc1.Width / 2);
                loc.Y -= (nc1.Height / 2);
                nc1.Location = loc;
                nc1.Click += new EventHandler(Control_Click);
                nc1.MouseDown += new MouseEventHandler(Control_MouseDown);
                nc1.MouseMove += new MouseEventHandler(Control_MouseMove);
                nc1.MouseUp += new MouseEventHandler(Control_MouseUp);
                nc1.DeleteControl += new KeyEventHandler(Control_DeleteControl);
                nc1.CodeFolder = ConfigurationManager.AppSettings["r2rCodeFolder"];

                crTab.Controls.Add(nc1);
                nc1.BringToFront();
            }
            else if (selNode.ToString() == "nodePdfToHtml")
            {
                PdfToHtml nc1 = new PdfToHtml();

                Point loc = crTab.PointToClient(new Point(e.X, e.Y)); ;
                loc.X -= (nc1.Width / 2);
                loc.Y -= (nc1.Height / 2);
                nc1.Location = loc;
                nc1.Click += new EventHandler(Control_Click);
                nc1.MouseDown += new MouseEventHandler(Control_MouseDown);
                nc1.MouseMove += new MouseEventHandler(Control_MouseMove);
                nc1.MouseUp += new MouseEventHandler(Control_MouseUp);
                nc1.DeleteControl += new KeyEventHandler(Control_DeleteControl);
                nc1.CodeFolder = ConfigurationManager.AppSettings["r2rCodeFolder"];

                crTab.Controls.Add(nc1);
                nc1.BringToFront();
            }
            //SAP - Saravana


        }

        private void tabPage_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = DragDropEffects.Copy;
        }

        private void tabPage_MouseMove(object sender, MouseEventArgs e)
        {
            mouseLoc = e.Location;

            Console.WriteLine(mouseLoc.X.ToString() + "-" + mouseLoc.Y.ToString());
        }

        private void tabPage_Paint(object sender, PaintEventArgs e)
        {
            drawLines((Control)sender);
        }

        private void drawLines(Control tabPage)
        {
            int arrowSize = 7;

            Graphics g = tabPage.CreateGraphics();

            g.Clear(Color.White);
            lstLines.Clear();

            g.SmoothingMode = System.Drawing.Drawing2D.SmoothingMode.AntiAlias;

            foreach (Control c1 in tabPage.Controls)
            {
                Point pointStart = new Point();
                Point pointEnd = new Point();

                //WEB - Maha
                if (c1.GetType().ToString() == "r2rStudio.Web.HPLogin")
                {
                    HPLogin c1a = (HPLogin)c1;

                    if (c1a.PreviousControl != null)
                    {
                        pointStart.X = c1a.PreviousControl.Location.X + (c1a.PreviousControl.Width / 2);
                        pointStart.Y = c1a.PreviousControl.Location.Y + c1a.PreviousControl.Height;

                        pointEnd.X = c1a.Location.X + (c1a.Width / 2);
                        pointEnd.Y = c1a.Location.Y - arrowSize;

                        lstLines.Add(new r2rLine(Color.Silver, 1f, pointStart, pointEnd, c1));
                    }
                }
                //LOGIC - Shameem
                else if (c1.GetType().ToString() == "r2rStudio.Logic.LogicIf")
                {
                    LogicIf c1a = (LogicIf)c1;

                    if (c1a.PreviousControl != null)
                    {
                        pointStart.X = c1a.PreviousControl.Location.X + (c1a.PreviousControl.Width / 2);
                        pointStart.Y = c1a.PreviousControl.Location.Y + c1a.PreviousControl.Height;

                        pointEnd.X = c1a.Location.X + (c1a.Width / 2);
                        pointEnd.Y = c1a.Location.Y - arrowSize;

                        lstLines.Add(new r2rLine(Color.Silver, 1f, pointStart, pointEnd, c1));
                    }
                }
                else if (c1.GetType().ToString() == "r2rStudio.Logic.LogicWhile")
                {
                    LogicWhile c1a = (LogicWhile)c1;

                    if (c1a.PreviousControl != null)
                    {
                        pointStart.X = c1a.PreviousControl.Location.X + (c1a.PreviousControl.Width / 2);
                        pointStart.Y = c1a.PreviousControl.Location.Y + c1a.PreviousControl.Height;

                        pointEnd.X = c1a.Location.X + (c1a.Width / 2);
                        pointEnd.Y = c1a.Location.Y - arrowSize;

                        lstLines.Add(new r2rLine(Color.Silver, 1f, pointStart, pointEnd, c1));
                    }
                }
                else if (c1.GetType().ToString() == "r2rStudio.Logic.LogicAssign")
                {
                    LogicAssign c1a = (LogicAssign)c1;

                    if (c1a.PreviousControl != null)
                    {
                        pointStart.X = c1a.PreviousControl.Location.X + (c1a.PreviousControl.Width / 2);
                        pointStart.Y = c1a.PreviousControl.Location.Y + c1a.PreviousControl.Height;

                        pointEnd.X = c1a.Location.X + (c1a.Width / 2);
                        pointEnd.Y = c1a.Location.Y - arrowSize;

                        lstLines.Add(new r2rLine(Color.Silver, 1f, pointStart, pointEnd, c1));
                    }
                }
                else if (c1.GetType().ToString() == "r2rStudio.Logic.LogicFor")
                {
                    LogicFor c1a = (LogicFor)c1;

                    if (c1a.PreviousControl != null)
                    {
                        pointStart.X = c1a.PreviousControl.Location.X + (c1a.PreviousControl.Width / 2);
                        pointStart.Y = c1a.PreviousControl.Location.Y + c1a.PreviousControl.Height;

                        pointEnd.X = c1a.Location.X + (c1a.Width / 2);
                        pointEnd.Y = c1a.Location.Y - arrowSize;

                        lstLines.Add(new r2rLine(Color.Silver, 1f, pointStart, pointEnd, c1));
                    }
                }
                else if (c1.GetType().ToString() == "r2rStudio.Logic.LogicBreak")
                {
                    LogicBreak c1a = (LogicBreak)c1;

                    if (c1a.PreviousControl != null)
                    {
                        pointStart.X = c1a.PreviousControl.Location.X + (c1a.PreviousControl.Width / 2);
                        pointStart.Y = c1a.PreviousControl.Location.Y + c1a.PreviousControl.Height;

                        pointEnd.X = c1a.Location.X + (c1a.Width / 2);
                        pointEnd.Y = c1a.Location.Y - arrowSize;

                        lstLines.Add(new r2rLine(Color.Silver, 1f, pointStart, pointEnd, c1));
                    }
                }
                else if (c1.GetType().ToString() == "r2rStudio.Logic.LogicWait")
                {
                    LogicWait c1a = (LogicWait)c1;

                    if (c1a.PreviousControl != null)
                    {
                        pointStart.X = c1a.PreviousControl.Location.X + (c1a.PreviousControl.Width / 2);
                        pointStart.Y = c1a.PreviousControl.Location.Y + c1a.PreviousControl.Height;

                        pointEnd.X = c1a.Location.X + (c1a.Width / 2);
                        pointEnd.Y = c1a.Location.Y - arrowSize;

                        lstLines.Add(new r2rLine(Color.Silver, 1f, pointStart, pointEnd, c1));
                    }
                }
                //DATABASE - Kapil
                else if (c1.GetType().ToString() == "r2rStudio.Database.ConnectToDb")
                {
                    ConnectToDb c1a = (ConnectToDb)c1;

                    if (c1a.PreviousControl != null)
                    {
                        pointStart.X = c1a.PreviousControl.Location.X + (c1a.PreviousControl.Width / 2);
                        pointStart.Y = c1a.PreviousControl.Location.Y + c1a.PreviousControl.Height;

                        pointEnd.X = c1a.Location.X + (c1a.Width / 2);
                        pointEnd.Y = c1a.Location.Y - arrowSize;

                        lstLines.Add(new r2rLine(Color.Silver, 1f, pointStart, pointEnd, c1));
                    }
                }
                //PDF - Vijay, Steve
                else if (c1.GetType().ToString() == "r2rStudio.Pdf.PdfToText")
                {
                    PdfToText c1a = (PdfToText)c1;

                    if (c1a.PreviousControl != null)
                    {
                        pointStart.X = c1a.PreviousControl.Location.X + (c1a.PreviousControl.Width / 2);
                        pointStart.Y = c1a.PreviousControl.Location.Y + c1a.PreviousControl.Height;

                        pointEnd.X = c1a.Location.X + (c1a.Width / 2);
                        pointEnd.Y = c1a.Location.Y - arrowSize;

                        lstLines.Add(new r2rLine(Color.Silver, 1f, pointStart, pointEnd, c1));
                    }
                }
                else if (c1.GetType().ToString() == "r2rStudio.Pdf.PdfToHtml")
                {
                    PdfToHtml c1a = (PdfToHtml)c1;

                    if (c1a.PreviousControl != null)
                    {
                        pointStart.X = c1a.PreviousControl.Location.X + (c1a.PreviousControl.Width / 2);
                        pointStart.Y = c1a.PreviousControl.Location.Y + c1a.PreviousControl.Height;

                        pointEnd.X = c1a.Location.X + (c1a.Width / 2);
                        pointEnd.Y = c1a.Location.Y - arrowSize;

                        lstLines.Add(new r2rLine(Color.Silver, 1f, pointStart, pointEnd, c1));
                    }
                }
                //EMAIL - Alster
                else if (c1.GetType().ToString() == "r2rStudio.Email.EmailLogin")
                {
                    EmailLogin c1a = (EmailLogin)c1;

                    if (c1a.PreviousControl != null)
                    {
                        pointStart.X = c1a.PreviousControl.Location.X + (c1a.PreviousControl.Width / 2);
                        pointStart.Y = c1a.PreviousControl.Location.Y + c1a.PreviousControl.Height;

                        pointEnd.X = c1a.Location.X + (c1a.Width / 2);
                        pointEnd.Y = c1a.Location.Y - arrowSize;

                        lstLines.Add(new r2rLine(Color.Silver, 1f, pointStart, pointEnd, c1));
                    }
                }
                else if (c1.GetType().ToString() == "r2rStudio.Email.EmailRead")
                {
                    EmailRead c1a = (EmailRead)c1;

                    if (c1a.PreviousControl != null)
                    {
                        pointStart.X = c1a.PreviousControl.Location.X + (c1a.PreviousControl.Width / 2);
                        pointStart.Y = c1a.PreviousControl.Location.Y + c1a.PreviousControl.Height;

                        pointEnd.X = c1a.Location.X + (c1a.Width / 2);
                        pointEnd.Y = c1a.Location.Y - arrowSize;

                        lstLines.Add(new r2rLine(Color.Silver, 1f, pointStart, pointEnd, c1));
                    }
                }
                //EXCEL - Magesh
                else if (c1.GetType().ToString() == "r2rStudio.Office.Excel.OpenWorkbook")
                {
                    OpenWorkbook c1a = (OpenWorkbook)c1;

                    if (c1a.PreviousControl != null)
                    {
                        pointStart.X = c1a.PreviousControl.Location.X + (c1a.PreviousControl.Width / 2);
                        pointStart.Y = c1a.PreviousControl.Location.Y + c1a.PreviousControl.Height;

                        pointEnd.X = c1a.Location.X + (c1a.Width / 2);
                        pointEnd.Y = c1a.Location.Y - arrowSize;

                        lstLines.Add(new r2rLine(Color.Silver, 1f, pointStart, pointEnd, c1));
                    }
                }
                else if (c1.GetType().ToString() == "r2rStudio.Office.Excel.CreateWorkbook")
                {
                    CreateWorkbook c1a = (CreateWorkbook)c1;

                    if (c1a.PreviousControl != null)
                    {
                        pointStart.X = c1a.PreviousControl.Location.X + (c1a.PreviousControl.Width / 2);
                        pointStart.Y = c1a.PreviousControl.Location.Y + c1a.PreviousControl.Height;

                        pointEnd.X = c1a.Location.X + (c1a.Width / 2);
                        pointEnd.Y = c1a.Location.Y - arrowSize;

                        lstLines.Add(new r2rLine(Color.Silver, 1f, pointStart, pointEnd, c1));
                    }
                }
            }

            foreach (r2rLine L in lstLines)
            {
                Pen p = new Pen(L.LineColor, L.LineWidth);

                Point[] points = new Point[4];

                points[0] = new Point(L.End.X - arrowSize, L.End.Y);
                points[1] = new Point(points[0].X + arrowSize, points[0].Y + arrowSize);
                points[2] = new Point(points[1].X + arrowSize, points[1].Y - arrowSize);
                points[3] = new Point(points[2].X - arrowSize, points[2].Y);

                //draw single line
                //g.DrawLine(p, L.Start, L.End);

                //draw elbow lines

                if (L.End.Y < L.Start.Y)
                {
                    //dragged control is above parent control
                    Point[] points2 = new Point[6];
                    points2[0] = L.Start;
                    points2[1] = new Point(L.Start.X, L.Start.Y + 20);

                    //points2[2] = new Point(L.Start.X + ((L.End.X - L.Start.X) / 2), L.Start.Y + 20);

                    if (L.Start.X > L.End.X)
                    {
                        points2[2] = new Point(L.End.X + ((L.Control.Width / 2) + 30), L.Start.Y + 20);
                    }
                    else
                    {
                        points2[2] = new Point(L.End.X - ((L.Control.Width / 2) + 30), L.Start.Y + 20);
                    }

                    points2[3] = new Point(points2[2].X, L.End.Y - 20);
                    points2[4] = new Point(L.End.X, points2[3].Y);
                    points2[5] = L.End;

                    g.DrawLines(p, points2);
                }
                else
                {
                    //dragged control is below parent control
                    Point[] points2 = new Point[4];
                    points2[0] = L.Start;
                    points2[1] = new Point(L.Start.X, L.Start.Y + ((L.End.Y - L.Start.Y) / 2));
                    points2[2] = new Point(L.End.X, points2[1].Y);
                    points2[3] = L.End;

                    g.DrawLines(p, points2);
                }

                //draw arrow
                SolidBrush brush = new SolidBrush(Color.Silver);
                g.FillPolygon(brush, points);

                brush.Dispose();
                p.Dispose();
            }
        }

        private void Control_MouseDown(object sender, MouseEventArgs e)
        {
            isDragStart = true;
            mouseLoc = e.Location;

            Control ctrl = (Control)sender;

            ctrl.BringToFront();
        }

        private void Control_MouseMove(object sender, MouseEventArgs e)
        {
            if (isDragStart)
            {
                Control ctrl = (Control)sender;
                ctrl.Left = e.X + ctrl.Left - mouseLoc.X;
                ctrl.Top = e.Y + ctrl.Top - mouseLoc.Y;
            }
        }

        private void Control_MouseUp(object sender, MouseEventArgs e)
        {
            isDragStart = false;
            Control ctrl = (Control)sender;
            Control parent = getCollide(ctrl);

            if (parent != null)
            {
                ctrl.Left = parent.Left;
                ctrl.Top = parent.Top + parent.Height + 20;

                if (ctrl.Width > parent.Width)
                {
                    int a = ctrl.Width - parent.Width;
                    ctrl.Left -= (a / 2);
                }
                else if (ctrl.Width < parent.Width)
                {
                    int a = parent.Width - ctrl.Width;
                    ctrl.Left += (a / 2);
                }

                //WEB - Maha
                if (ctrl.GetType().ToString() == "r2rStudio.Web.HPLogin")
                {
                    HPLogin c1a = (HPLogin)ctrl;
                    c1a.PreviousControl = parent;
                }
                //LOGIC - Shameem
                else if (ctrl.GetType().ToString() == "r2rStudio.Logic.LogicIf")
                {
                    LogicIf c1a = (LogicIf)ctrl;
                    c1a.PreviousControl = parent;
                }
                else if (ctrl.GetType().ToString() == "r2rStudio.Logic.LogicWhile")
                {
                    LogicWhile c1a = (LogicWhile)ctrl;
                    c1a.PreviousControl = parent;
                }
                else if (ctrl.GetType().ToString() == "r2rStudio.Logic.LogicFor")
                {
                    LogicFor c1a = (LogicFor)ctrl;
                    c1a.PreviousControl = parent;
                }
                else if (ctrl.GetType().ToString() == "r2rStudio.Logic.LogicAssign")
                {
                    LogicAssign c1a = (LogicAssign)ctrl;
                    c1a.PreviousControl = parent;
                }
                else if (ctrl.GetType().ToString() == "r2rStudio.Logic.LogicBreak")
                {
                    LogicBreak c1a = (LogicBreak)ctrl;
                    c1a.PreviousControl = parent;
                }
                else if (ctrl.GetType().ToString() == "r2rStudio.Logic.LogicWait")
                {
                    LogicWait c1a = (LogicWait)ctrl;
                    c1a.PreviousControl = parent;
                }
                //DATABASE - Kapil
                else if (ctrl.GetType().ToString() == "r2rStudio.Database.ConnectToDb")
                {
                    ConnectToDb c1a = (ConnectToDb)ctrl;
                    c1a.PreviousControl = parent;
                }
                //PDF - Vijay, Steve
                else if (ctrl.GetType().ToString() == "r2rStudio.Pdf.PdfToText")
                {
                    PdfToText c1a = (PdfToText)ctrl;
                    c1a.PreviousControl = parent;
                }
                else if (ctrl.GetType().ToString() == "r2rStudio.Pdf.PdfToHtml")
                {
                    PdfToHtml c1a = (PdfToHtml)ctrl;
                    c1a.PreviousControl = parent;
                }
                //EXCEL - Magesh
                else if (ctrl.GetType().ToString() == "r2rStudio.Office.Excel.OpenWorkbook")
                {
                    OpenWorkbook c1a = (OpenWorkbook)ctrl;
                    c1a.PreviousControl = parent;
                }
                else if (ctrl.GetType().ToString() == "r2rStudio.Office.Excel.CreateWorkbook")
                {
                    CreateWorkbook c1a = (CreateWorkbook)ctrl;
                    c1a.PreviousControl = parent;
                }
                //SAP - Saravana
                //EMAIL - Alster
                else if (ctrl.GetType().ToString() == "r2rStudio.Email.EmailLogin")
                {
                    EmailLogin c1a = (EmailLogin)ctrl;
                    c1a.PreviousControl = parent;
                }
                else if (ctrl.GetType().ToString() == "r2rStudio.Email.EmailRead")
                {
                    EmailRead c1a = (EmailRead)ctrl;
                    c1a.PreviousControl = parent;
                }


                //WEB - Maha
                if (parent.GetType().ToString() == "r2rStudio.Web.HPLogin")
                {
                    HPLogin c1a = (HPLogin)parent;
                    c1a.NextControl = ctrl;
                }
                //LOGIC - Shameem
                else if (parent.GetType().ToString() == "r2rStudio.Logic.LogicStart")
                {
                    LogicStart c1a = (LogicStart)parent;
                    c1a.NextControl = ctrl;
                }
                else if (parent.GetType().ToString() == "r2rStudio.Logic.LogicIf")
                {
                    LogicIf c1a = (LogicIf)parent;
                    c1a.NextControl = ctrl;
                }
                else if (parent.GetType().ToString() == "r2rStudio.Logic.LogicWhile")
                {
                    LogicWhile c1a = (LogicWhile)parent;
                    c1a.NextControl = ctrl;
                }
                else if (parent.GetType().ToString() == "r2rStudio.Logic.LogicFor")
                {
                    LogicFor c1a = (LogicFor)parent;
                    c1a.NextControl = ctrl;
                }
                else if (parent.GetType().ToString() == "r2rStudio.Logic.LogicAssign")
                {
                    LogicAssign c1a = (LogicAssign)parent;
                    c1a.NextControl = ctrl;
                }
                else if (parent.GetType().ToString() == "r2rStudio.Logic.LogicBreak")
                {
                    LogicBreak c1a = (LogicBreak)parent;
                    c1a.NextControl = ctrl;
                }
                else if (parent.GetType().ToString() == "r2rStudio.Logic.LogicWait")
                {
                    LogicWait c1a = (LogicWait)parent;
                    c1a.NextControl = ctrl;
                }
                //DATABASE - Kapil
                else if (parent.GetType().ToString() == "r2rStudio.Database.ConnectToDb")
                {
                    ConnectToDb c1a = (ConnectToDb)parent;
                    c1a.NextControl = ctrl;
                }
                //PDF - Vijay, Steve
                else if (parent.GetType().ToString() == "r2rStudio.Pdf.PdfToText")
                {
                    PdfToText c1a = (PdfToText)parent;
                    c1a.NextControl = ctrl;
                }
                else if (parent.GetType().ToString() == "r2rStudio.Pdf.PdfToHtml")
                {
                    PdfToHtml c1a = (PdfToHtml)parent;
                    c1a.NextControl = ctrl;
                }
                //EXCEL - Magesh
                else if (parent.GetType().ToString() == "r2rStudio.Office.Excel.OpenWorkbook")
                {
                    OpenWorkbook c1a = (OpenWorkbook)parent;
                    c1a.NextControl = ctrl;
                }
                else if (parent.GetType().ToString() == "r2rStudio.Office.Excel.CreateWorkbook")
                {
                    CreateWorkbook c1a = (CreateWorkbook)parent;
                    c1a.NextControl = ctrl;
                }
                //SAP - Saravana
                //EMAIL - Alster
                else if (parent.GetType().ToString() == "r2rStudio.Email.EmailLogin")
                {
                    EmailLogin c1a = (EmailLogin)parent;
                    c1a.NextControl = ctrl;
                }
                else if (parent.GetType().ToString() == "r2rStudio.Email.EmailRead")
                {
                    EmailRead c1a = (EmailRead)parent;
                    c1a.NextControl = ctrl;
                }
            }

            drawLines(ctrl.Parent);
        }

        private bool checkCollide(Control ctrl)
        {
            bool res = false;

            foreach (Control c1 in ctrl.Parent.Controls)
            {
                if (c1.Equals(ctrl) == false)
                {
                    if ((ctrl.Left >= c1.Left && ctrl.Left <= c1.Left) && (ctrl.Top >= c1.Top && ctrl.Top <= (c1.Top + c1.Height)))
                    {
                        res = true;
                        continue;
                    }
                }
            }
            return res;
        }

        private Control getCollide(Control ctrl)
        {
            Control res = null;

            foreach (Control c1 in ctrl.Parent.Controls)
            {
                if (c1.Equals(ctrl) == false)
                {
                    if ((ctrl.Left >= c1.Left && ctrl.Left <= (c1.Left + c1.Width)) && (ctrl.Top >= c1.Top && ctrl.Top <= (c1.Top + c1.Height)))
                    {
                        res = c1;
                        continue;
                    }
                }
            }
            return res;
        }

        private void Control_DeleteControl(object sender, KeyEventArgs e)
        {
            Control ctrl = (Control)sender;
            Control parent = (Control)ctrl.Parent;

            bool endless = true;

            while (endless)
            {

                if (parent.GetType().ToString() == "System.Windows.Forms.TabPage")
                {
                    parent.Controls.Remove(ctrl);
                    endless = false;
                }
                else
                {
                    ctrl = parent;
                    parent = parent.Parent;
                }
            }
        }


        public void Control_Click(object sender, EventArgs e)
        {
            //Control ctrl = (Control)sender;
            
            ////load code in browser
            //r2rStudio.Base.CSharpFormat csFormat = new Base.CSharpFormat();
            //string code = "//NO CODE AVAILABLE...";
            //csFormat.EmbedStyleSheet = true;
            //code = csFormat.FormatCode(code);
            //wbCode.DocumentText = code;
        }

        private void btnVarAdd_Click(object sender, EventArgs e)
        {
            frmVars frm1 = new frmVars();

            if (frm1.ShowDialog() == DialogResult.OK)
            {
                ListViewItem[] foundItems = lvVar.Items.Find(frm1.txtName.Text.Trim(), true);

                if (foundItems.Length == 0)
                {
                    ListViewItem var1 = new ListViewItem(frm1.txtName.Text.Trim());

                    var1.Name = frm1.txtName.Text.Trim();
                    var1.SubItems.Add(frm1.cbType.Text);

                    if (frm1.cbType.SelectedIndex == 0 || frm1.cbType.SelectedIndex == 1)
                    {
                        var1.SubItems.Add(frm1.txtValue.Text);
                    }
                    else if (frm1.cbType.SelectedIndex == 2)
                    {
                        var1.SubItems.Add(frm1.dtpValue.Text);
                    }
                    else if (frm1.cbType.SelectedIndex == 3)
                    {
                        if (frm1.chkValue.Checked)
                        {
                            var1.SubItems.Add("True");
                        }
                        else
                        {
                            var1.SubItems.Add("False");
                        }
                    }

                    lvVar.Items.Add(var1);
                }
                else
                {
                    MessageBox.Show("Variable name already exists.");
                }
            }
        }

        private void btnVarDel_Click(object sender, EventArgs e)
        {
            if (lvVar.SelectedItems.Count > 0)
            {
                if (MessageBox.Show("Sure to delete the selected variable?", "DELETE", MessageBoxButtons.YesNo) == DialogResult.Yes)
                {
                    lvVar.Items.Remove(lvVar.SelectedItems[0]);
                }
            }
        }

        private void btnFileSave_Click(object sender, EventArgs e)
        {
            if (sfdSave.ShowDialog() == DialogResult.OK)
            {

                StreamWriter sw = new StreamWriter(sfdSave.FileName);

                sw.WriteLine("<Workflow>");
                sw.WriteLine("\t<Controls>");

                Control[] ctrls = tcMain.SelectedTab.Controls.Find("LogicStart1", true);

                LogicStart ls1 = (LogicStart)ctrls[0];

                sw.WriteLine("\t\t<" + ls1.GetType().ToString() + " />");

                Control nc = ls1.NextControl;

                bool endless = true;

                while (endless)
                {
                    if (nc == null)
                    {
                        endless = true;
                        break;
                    }

                    sw.WriteLine("\t\t<" + nc.GetType().ToString() + " />");

                    //WEB - Maha
                    if (nc.GetType().ToString() == "r2rStudio.Web.HPLogin")
                    {
                        HPLogin c1a = (HPLogin)nc;
                        nc = c1a.NextControl;
                    }
                    //LOGIC - Shameem
                    else if (nc.GetType().ToString() == "r2rStudio.Logic.LogicIf")
                    {
                        LogicIf c1a = (LogicIf)nc;
                        nc = c1a.NextControl;
                    }
                    else if (nc.GetType().ToString() == "r2rStudio.Logic.LogicWhile")
                    {
                        LogicWhile c1a = (LogicWhile)nc;
                        nc = c1a.NextControl;
                    }
                    else if (nc.GetType().ToString() == "r2rStudio.Logic.LogicFor")
                    {
                        LogicFor c1a = (LogicFor)nc;
                        nc = c1a.NextControl;
                    }
                    else if (nc.GetType().ToString() == "r2rStudio.Logic.LogicAssign")
                    {
                        LogicAssign c1a = (LogicAssign)nc;
                        nc = c1a.NextControl;
                    }
                    else if (nc.GetType().ToString() == "r2rStudio.Logic.LogicBreak")
                    {
                        LogicBreak c1a = (LogicBreak)nc;
                        nc = c1a.NextControl;
                    }
                    else if (nc.GetType().ToString() == "r2rStudio.Logic.LogicWait")
                    {
                        LogicWait c1a = (LogicWait)nc;
                        nc = c1a.NextControl;
                    }
                    //DATABASE - Kapil
                    else if (nc.GetType().ToString() == "r2rStudio.Database.ConnectToDb")
                    {
                        ConnectToDb c1a = (ConnectToDb)nc;
                        nc = c1a.NextControl;
                    }
                    //PDF - Vijay, Steve
                    else if (nc.GetType().ToString() == "r2rStudio.Pdf.PdfToText")
                    {
                        PdfToText c1a = (PdfToText)nc;
                        nc = c1a.NextControl;
                    }
                    else if (nc.GetType().ToString() == "r2rStudio.Pdf.PdfToHtml")
                    {
                        PdfToHtml c1a = (PdfToHtml)nc;
                        nc = c1a.NextControl;
                    }
                    //EXCEL - Magesh
                    else if (nc.GetType().ToString() == "r2rStudio.Office.Excel.OpenWorkbook")
                    {
                        OpenWorkbook c1a = (OpenWorkbook)nc;
                        nc = c1a.NextControl;
                    }
                    else if (nc.GetType().ToString() == "r2rStudio.Office.Excel.CreateWorkbook")
                    {
                        CreateWorkbook c1a = (CreateWorkbook)nc;
                        nc = c1a.NextControl;
                    }
                    //SAP - Saravana
                    //EMAIL - Alster
                    else if (nc.GetType().ToString() == "r2rStudio.Email.EmailLogin")
                    {
                        EmailLogin c1a = (EmailLogin)nc;
                        nc = c1a.NextControl;
                    }
                    else if (nc.GetType().ToString() == "r2rStudio.Email.EmailRead")
                    {
                        EmailRead c1a = (EmailRead)nc;
                        nc = c1a.NextControl;
                    }
                }

                sw.WriteLine("\t</Controls>");
                sw.WriteLine("</Workflow>");

                sw.Close();


                FileInfo fi = new FileInfo(sfdSave.FileName);

                tcMain.SelectedTab.Text = fi.Name;

            }
        }
    }

    class r2rLine
    {
        public Color LineColor { get; set; }
        public float LineWidth { get; set; }
        public bool Selected { get; set; }
        public Point Start { get; set; }
        public Point End { get; set; }
        public Control Control { get; set; }

        public r2rLine(Color c, float w, Point s, Point e, Control ctrl)
        {
            LineColor = c;
            LineWidth = w;
            Start = s;
            End = e;
            Control = ctrl;
        }

        public void Draw(Graphics G)
        {
            using (Pen pen = new Pen(LineColor, LineWidth)) G.DrawLine(pen, Start, End);
        }

        public bool HitTest(Point Pt)
        {
            // test if we fall outside of the bounding box:
            if ((Pt.X < Start.X && Pt.X < End.X) || (Pt.X > Start.X && Pt.X > End.X) ||
                (Pt.Y < Start.Y && Pt.Y < End.Y) || (Pt.Y > Start.Y && Pt.Y > End.Y))
                return false;

            // now we calculate the distance:
            float dy = End.Y - Start.Y;
            float dx = End.X - Start.X;
            float Z = dy * Pt.X - dx * Pt.Y + Start.Y * End.X - Start.X * End.Y;
            float N = dy * dy + dx * dx;
            float dist = (float)(Math.Abs(Z) / Math.Sqrt(N));
            // done:
            return dist < LineWidth / 2f;
        }
    }
}
