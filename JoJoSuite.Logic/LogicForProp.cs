using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JoJoSuite.Logic
{
    public partial class LogicForProp : UserControl
    {
        private string[] _collection;
        private string _colVar;

        private LogicFor logicFor;
        
        public LogicForProp()
        {
            InitializeComponent();
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
        public string CollectionVariable
        {
            get
            {
                return _colVar;
            }
            set
            {
                _colVar = value;
                Invalidate();
            }
        }

        public LogicFor LogicFor
        {
            get
            {
                return logicFor;
            }
            set
            {
                logicFor = value;

                //txtVal1.Text = sVal1 = value.Value1;
                //txtVal2.Text = sVal2 = value.Value2;

                Invalidate();
            }
        }

        private void piCollection_PropertyChanged(object sender, EventArgs e)
        {
            logicFor.Collection = _collection = piCollection.Collection;
            logicFor.CollectionVariable = _colVar = piCollection.Value;
        }
    }
}
