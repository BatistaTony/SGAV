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
    public partial class RelVendaTotal : Form
    {
        public RelVendaTotal()
        {
            InitializeComponent();

           
                string data_de = verRelatorio.data_de1;
                string data_ate = verRelatorio.data_ate1;
           
            

            MessageBox.Show("Ver relatorio de " + data_de +" ate "+data_ate);
        }

        private void RelVendaTotal_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }
    }
}
