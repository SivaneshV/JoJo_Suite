using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JoJoSuite.Base
{
    [DefaultEvent("PropertyChanged")]
    public partial class PropInput : UserControl
    {
        public enum r2rDataType { String, Number, Date, Boolean, Collection, Password };

        private string _title = "Title";
        private string _text = "";
        private string[] _collection;

        private r2rDataType _type = r2rDataType.String;

        public event EventHandler PropertyChanged;

        public PropInput()
        {
            InitializeComponent();
        }

        //private void HandlePropertyChanged(object sender, EventArgs e)
        //{
        //    this.OnPropertyChanged(EventArgs.Empty);
        //}

        protected virtual void OnPropertyChanged(EventArgs e)
        {
            EventHandler handler = this.PropertyChanged;

            if (handler != null)
            {
                handler(this, e);
            }
        }


        public string Title
        {
            get
            {
                return _title;
            }
            set
            {
                lblTitle.Text = _title = value;
                Invalidate();
            }
        }

        public string Value
        {
            get
            {
                return _text;
            }
            set
            {
                txtVal.Text = _text = value;
                Invalidate();
            }
        }

        public string[] Collection
        {
            get
            {
                return _collection;
            }
            set
            {
                _collection = value;
                Invalidate();
            }
        }

        public r2rDataType Type
        {
            get
            {
                return _type;
            }
            set
            {
                _type = value;

                txtVal.Visible = false;
                txtPass.Visible = false;
                dtpVal.Visible = false;
                chkVal.Visible = false;
                btnCollection.Visible = false;
                //txtVal.UseSystemPasswordChar = false;

                if (_type == r2rDataType.String)
                {
                    txtVal.Visible = true;
                }
                else if (_type == r2rDataType.Password)
                {
                    txtPass.Visible = true;
                    txtVal.PasswordChar = '*';
                }
                else if (_type == r2rDataType.Number)
                {
                    txtVal.Visible = true;
                }
                else if (_type == r2rDataType.Date)
                {
                    dtpVal.Visible = true;
                }
                else if (_type == r2rDataType.Boolean)
                {
                    chkVal.Visible = true;
                }
                else if (_type == r2rDataType.Collection)
                {
                    btnCollection.Visible = true;
                }

                Invalidate();
            }
        }

        private void chkVar_CheckedChanged(object sender, EventArgs e)
        {
            cboVar.Visible = false;
            txtVal.Visible = false;
            txtPass.Visible = false;
            dtpVal.Visible = false;
            chkVal.Visible = false;
            btnCollection.Visible = false;
            //txtVal.UseSystemPasswordChar = false;

            if (chkVar.Checked)
            {
                cboVar.Visible = true;
            }
            else
            {
                if (_type == r2rDataType.String)
                {
                    txtVal.Visible = true;
                }
                else if (_type == r2rDataType.Password)
                {
                    txtPass.Visible = true;
                    txtVal.PasswordChar = '*';
                }
                else if (_type == r2rDataType.Number)
                {
                    txtVal.Visible = true;
                }
                else if (_type == r2rDataType.Date)
                {
                    dtpVal.Visible = true;
                }
                else if (_type == r2rDataType.Boolean)
                {
                    chkVal.Visible = true;
                }
                else if (_type == r2rDataType.Collection)
                {
                    btnCollection.Visible = true;
                }
            }
        }

        private void txtVal_TextChanged(object sender, EventArgs e)
        {
            _text = txtVal.Text;
            this.OnPropertyChanged(EventArgs.Empty);
        }

        private void txtPass_TextChanged(object sender, EventArgs e)
        {
            _text = txtPass.Text;
            this.OnPropertyChanged(EventArgs.Empty);
        }

        private void dtpVal_ValueChanged(object sender, EventArgs e)
        {
            _text = dtpVal.Text;
            this.OnPropertyChanged(EventArgs.Empty);
        }

        private void chkVal_CheckedChanged(object sender, EventArgs e)
        {
            _text = (chkVal.Checked) ? "True" : "False";
            this.OnPropertyChanged(EventArgs.Empty);
        }

        private void cboVar_SelectedIndexChanged(object sender, EventArgs e)
        {
            _text = "{" + cboVar.Text + "}";
            this.OnPropertyChanged(EventArgs.Empty);
        }

        private void btnCollection_Click(object sender, EventArgs e)
        {
            frmCollection frm1 = new frmCollection();

            frm1.txtItems.Lines = _collection;

            if (frm1.ShowDialog() == DialogResult.OK)
            {
                _collection = frm1.txtItems.Lines;
                _text = frm1.txtItems.Lines.Length.ToString();
                this.OnPropertyChanged(EventArgs.Empty);
            }
        }

    }
}
