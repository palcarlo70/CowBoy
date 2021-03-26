using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CowBoy.WF
{
    public partial class frmAvvio : Form
    {
        public frmAvvio()
        {
         //   this.TopMost = true;
           // this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            InitializeComponent();
            

        }

        private void frmAvvio_Load(object sender, EventArgs e)
        {

        }

        private void tmrClose_Tick(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
