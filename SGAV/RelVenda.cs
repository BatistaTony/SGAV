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
    public partial class RelVenda : Form
    {
        public static int id_relatorio = 0;
        public RelVenda()
        {
            InitializeComponent();
                    if(director_geral.id_rel == 0)
                    {

                    }
                    else
                    {
                        id_relatorio = director_geral.id_rel;
                        MessageBox.Show(id_relatorio.ToString());
                    }


            if(vendas.id_rel == 0)
            {
                
            }
            else
            {
                id_relatorio = vendas.id_rel;
                MessageBox.Show(id_relatorio.ToString());
            }


            if (contablista.id_rel == 0)
                {
                    
                }
                else
                {
                    id_relatorio = contablista.id_rel;
                    MessageBox.Show(id_relatorio.ToString());
                }
            
        }

        private void RelVenda_Load(object sender, EventArgs e)
        {

            this.reportViewer1.RefreshReport();
        }
    }
}
