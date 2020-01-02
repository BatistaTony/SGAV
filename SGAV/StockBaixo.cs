using MySql.Data.MySqlClient;
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
    public partial class StockBaixo : Form
    {
        public StockBaixo()
        {
            InitializeComponent();
            String strCon = "Server=localhost;Uid=tony;Pwd=thony@1;Database=appav";
            MySqlConnection conexao = new MySqlConnection(strCon);

            try
            {
                conexao.Open();



                String query = "SELECT id as 'CODIGO', destino as 'DESTINO', data_partida as 'DATA PARTIDA', categoria as 'CATEGORIA', quantidade as 'QUANTIDADE' FROM bilhete  WHERE quantidade < 5";
                MySqlCommand cmd = new MySqlCommand(query, conexao);

                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                da.Fill(dt);
                BindingSource bsourece = new BindingSource();
                bsourece.DataSource = dt;

                dataGridView1.DataSource = bsourece;
                dataGridView1.Columns[0].Width = 150;
                dataGridView1.Columns[1].Width = 150;
                dataGridView1.Columns[2].Width = 150;
                dataGridView1.Columns[3].Width = 150;
                dataGridView1.Columns[4].Width = 150;

                conexao.Close();

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }
    }
}
