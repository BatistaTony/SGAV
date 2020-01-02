using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SGAV
{
    public partial class verRelatorio : Form
    {
        public static string data_de1;
        public static string data_ate1;
        public verRelatorio()
        {
            InitializeComponent();
        }

        private void VisualizarRel(object sender, EventArgs e)
        {
            DateTime dt_de = bunifuDatepicker_de.Value.Date;
            String data_de = dt_de.Year + "-" + dt_de.Month + "-" + dt_de.Day;

            DateTime dt_para = bunifuDatepicker_para.Value.Date;
            String data_para = dt_para.Year + "-" + dt_para.Month + "-" + dt_para.Day;

            data_de1 = data_de;
            data_ate1 = data_para;
            

            RelVendaTotal TelaRelaT = new RelVendaTotal();
            TelaRelaT.ShowDialog();
            
        }
    }
}
