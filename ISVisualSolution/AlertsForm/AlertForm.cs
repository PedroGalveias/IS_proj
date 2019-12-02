using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace AlertsForm
{
    public partial class AlertForm : Form
    {
        public AlertForm()
        {
            InitializeComponent();

        }

        private void ButtonLimpar_Click(object sender, EventArgs e)
        {
            textBoxValor.Text = "";
            textBoxValor2.Text = "";
            textBoxValor2.Visible = false;
            //falta valores das comboBox

        }

        private void Button1_Click(object sender, EventArgs e)
        {
           /* if
            double valor1 = textBoxValor.Text*/
        }

        private Boolean ValidateAlertInfo()
        {
            String strTemp = textBoxValor.Text;
            if (strTemp.Trim().Length <= 0)
            {
                return false;
            }
            strTemp = textBoxValor2.Text;
            //falta validar
            if (strTemp.Trim().Length <= 0 && comboBoxOperacao.SelectedItem == "")
            {
                return false;
            }
           /* strTemp = textBoxAvatarLoc.Text;
            if (strTemp.Trim().Length <= 0)
            {
                return false;
            }
            if (!File.Exists(strTemp))
            {
                return false;
            }
            */
            return true;
        }
    }
}
