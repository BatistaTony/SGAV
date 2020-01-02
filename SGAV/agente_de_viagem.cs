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
    public partial class agente_de_viagem : Form
    {
        MySqlConnection conexao = new MySqlConnection("Server = localhost; Uid=tony;Pwd=thony@1;Database=appav");


        public static string id_usuario = Form1.idAdmin;
        
        public agente_de_viagem()
        {
            InitializeComponent();
            this.FindUsuer();
            this.loadData();
            
        }

        bool flag = false;


        public void FindUsuer()
        {
            try
            {
                MySqlConnection conexao = new MySqlConnection("Server = localhost; Uid=tony;Pwd=thony@1;Database=appav");

                String stmt = "SELECT * FROM usuario where id=@id";
                MySqlCommand cmd1 = conexao.CreateCommand();
                cmd1.Parameters.Clear();
                cmd1.Parameters.AddWithValue("@id", id_usuario);
                cmd1.CommandText = stmt;

                conexao.Open();

                MySqlDataReader reader = cmd1.ExecuteReader();

                String id_cliente = "";

                if (reader.Read())
                {

                    nome.Text = reader["nome"].ToString();

                }

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                conexao.Close();
            }
        }

        private void CloseApp(object sender, EventArgs e)
        {
            this.Visible = false;
            Form1 TelaLogin = new Form1();
            TelaLogin.Visible = true;
        }

        private void HideApp(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Minimized;
        }

        private void panel1_MouseDown(object sender, MouseEventArgs e)
        {
            flag = true;
        }

        private void panel1_MouseMove(object sender, MouseEventArgs e)
        {
            if (flag == true)
            {
                this.Location = Cursor.Position;
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            flag = false;
        }

        private void loadData()
        {
            conexao.Close();
            bunifuMetroTextboxdestino.Text = "";

            try
            {
                conexao.Close();
                conexao.Open();

                DateTime data1 = DateTime.Today;
                String data_actual = data1.ToString("yyyy-MM-dd");

                String query = "SELECT VD.id as 'ID',VD.data_venda as 'DATA',CL.nome as 'Cliente', CL.telefone1 as 'TELEFONE 1', CL.telefone2 as 'TELEFONE 2', CL.email as 'E-MAIL',FC.nome as 'AGENTE DE VIAGEM',VD.preco_total_venda as 'PRECO TOTAL' FROM venda as VD  INNER JOIN cliente as CL ON VD.id_cliente = CL.id INNER JOIN usuario as FC ON VD.id_usuario = FC.id WHERE VD.id_usuario = @id_user AND VD.data_venda = @data_a";
                MySqlCommand cmd = new MySqlCommand(query, conexao);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id_user", id_usuario);
                cmd.Parameters.AddWithValue("@data_a", data_actual);

                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                da.Fill(dt);
                BindingSource bsourece = new BindingSource();
                bsourece.DataSource = dt;

                dataGridView1.DataSource = bsourece;
                dataGridView1.Columns[0].Width = 50;
                dataGridView1.Columns[1].Width = 150;
                dataGridView1.Columns[2].Width = 150;
                dataGridView1.Columns[3].Width = 150;
                dataGridView1.Columns[4].Width = 150;
                dataGridView1.Columns[5].Width = 150;
                dataGridView1.Columns[6].Width = 150;
                dataGridView1.Columns[7].Width = 150;


                conexao.Close();

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }


        private void pesquisar(object sender, EventArgs e)
        {
            labeltb1.Text = "Bilhetes disponiveis";

            conexao.Close();
            String destino = bunifuMetroTextboxdestino.Text;

            if (destino == "")
            {
                MessageBox.Show("Campo de destino vazio");
            }
            else
            {

                DateTime dt = bunifuDatepicker_para.Value.Date;
                String data_partida = dt.Year + "-" + dt.Month + "-" + dt.Day;


                try
                {
                    conexao.Open();



                    String query = "SELECT id as 'ID', data_partida as 'DATA DE PARTIDA', horario as 'HORA', destino as 'DESTINO', categoria as 'CATEGORIA', local_partida as 'LOCAL DE PARTIDA', voo as 'VOO', quantidade as 'QUANTIDADE',  preco as 'PRECO' FROM bilhete WHERE quantidade > 0 AND  destino=@destino OR data_partida=@data_partida";
                    MySqlCommand cmd = new MySqlCommand(query, conexao);
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@destino", destino);
                    cmd.Parameters.AddWithValue("@data_partida", data_partida);

                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmd;
                    DataTable dt1 = new DataTable();
                    da.Fill(dt1);
                    BindingSource bsourece = new BindingSource();
                    bsourece.DataSource = dt1;

                    dataGridView1.DataSource = bsourece;
                    dataGridView1.Columns[0].Width = 100;
                    dataGridView1.Columns[1].Width = 118;
                    dataGridView1.Columns[3].Width = 118;
                    dataGridView1.Columns[4].Width = 118;
                    dataGridView1.Columns[5].Width = 118;
                    dataGridView1.Columns[6].Width = 125;
                    


                    conexao.Close();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }


        }

        public static string id_bilhete = "";
      
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            var item = dataGridView1.Rows[e.RowIndex].Cells[0].Value;
            if (item != DBNull.Value)
            {
                id_bilhete = Convert.ToString(item);
            }
            else
            {

            }

        }

        private void vender(object sender, EventArgs e)
        {
            if(id_bilhete   == "")
            {
                MessageBox.Show("Seleciona o bilhete que pretende vender");
               
            }
            else
            {
                vender TelaVenda = new vender();
                TelaVenda.ShowDialog();
            }
            
        }

       

        private void UpdateGrid(object sender, EventArgs e)
        {
            this.labeltb1.Text = "Vendas de hoje";
            this.loadData();
            id_bilhete = "";
        }

        
    }
}
