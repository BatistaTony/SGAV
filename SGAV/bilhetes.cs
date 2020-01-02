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

    public partial class bilhetes : Form
    {
        MySqlConnection conexao;
        public static string id_bilhete  = "";
        public bilhetes()
        {
            InitializeComponent();

            String strCon = "Server=localhost;Uid=tony;Pwd=thony@1;Database=appav";
            conexao = new MySqlConnection(strCon);
            this.loadData();

        }

        private void loadData()
        {
            conexao.Close();
            bunifuMetroTextboxdestino.Text = "";

            try
            {
                conexao.Close();
                conexao.Open();



                String query = "SELECT id as 'ID', codigobi as 'CODIGO', data_partida as 'DATA DE PARTIDA', horario as 'HORA', destino as 'DESTINO', categoria as 'CATEGORIA', local_partida as 'LOCAL DE PARTIDA', voo as 'VOO', quantidade as 'QUANTIDADE',  preco as 'PRECO' FROM bilhete";
                MySqlCommand cmd = new MySqlCommand(query, conexao);

                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                da.Fill(dt);
                BindingSource bsourece = new BindingSource();
                bsourece.DataSource = dt;

                dataGridView1.DataSource = bsourece;
                dataGridView1.Columns[0].Width = 50;
                dataGridView1.Columns[1].Width = 110;
                dataGridView1.Columns[3].Width = 110;
                dataGridView1.Columns[4].Width = 110;
                dataGridView1.Columns[5].Width = 110;
                dataGridView1.Columns[6].Width = 110;
                dataGridView1.Columns[7].Width = 110;
                dataGridView1.Columns[8].Width = 110;


                conexao.Close();

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        bool flag = false;


        private void CloseApp(object sender, EventArgs e)
        {
            dashboardAdmin Telaadim = new dashboardAdmin();
            Telaadim.Visible = true;
            this.Visible = false;
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

        private void pesquisar(object sender, EventArgs e)
        {
            String destino = bunifuMetroTextboxdestino.Text;
            DateTime dt = bunifuDatepickerdata_partida.Value.Date;
            String data_partida = dt.Year + "-" + dt.Month + "-" + dt.Day;


                try
                {
                    conexao.Open();



                    String query = "SELECT id as 'ID',codigobi as 'CODIGO', data_partida as 'DATA DE PARTIDA', horario as 'HORA', destino as 'DESTINO', categoria as 'CATEGORIA', local_partida as 'LOCAL DE PARTIDA', voo as 'VOO', quantidade as 'QUANTIDADE', preco as 'PRECO' FROM bilhete WHERE destino=@destino OR data_partida=@data_partida";
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
                    dataGridView1.Columns[0].Width = 50;
                    dataGridView1.Columns[1].Width = 110;
                    dataGridView1.Columns[3].Width = 110;
                    dataGridView1.Columns[4].Width = 110;
                    dataGridView1.Columns[5].Width = 110;
                    dataGridView1.Columns[6].Width = 110;
                    dataGridView1.Columns[7].Width = 110;
                    dataGridView1.Columns[8].Width = 110;

                conexao.Close();

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            

        }

        private void loadGridview(object sender, EventArgs e)
        {
            this.loadData();
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try {
                var item = dataGridView1.Rows[e.RowIndex].Cells[0].Value;
                if (item != DBNull.Value)
                {
                    id_bilhete = Convert.ToString(item);


                }
                else
                {

                }
            }catch(Exception Ex)
            {

            }

        }

        private bool CheckBi(string id)
        {

            String strCon = "Server=localhost;Uid=tony;Pwd=thony@1;Database=appav";
            MySqlConnection conexao = new MySqlConnection(strCon);

            String stmt = "SELECT id_bilhete FROM bilhete_vendido WHERE id_bilhete=@id";
            MySqlCommand cmd = conexao.CreateCommand();
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@id", id);
            cmd.CommandText = stmt;
            conexao.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        private void apagar_bilhete(object sender, EventArgs e)
        {

            if(id_bilhete == " ")
            {
                MessageBox.Show("Seleciona um bilhete para apagar");
                conexao.Close();
            }
            else
            {
                try
                {

                    if (MessageBox.Show("Tem a certeza que pretende eliminar o bilhete de passagem", "Remover Bilhete de passagem", MessageBoxButtons.YesNo) == DialogResult.Yes)
                    {
                        conexao.Close();
                        conexao.Open();

                        if (CheckBi(id_bilhete))
                        {
                            MessageBox.Show("Bilhte nao pode ser removido, porque ja foi feito alguma venda");
                        }
                        else
                        {
                            String query1 = "DELETE FROM bilhete where id=@id";
                            MySqlCommand cmd1 = new MySqlCommand(query1, conexao);
                            cmd1.Parameters.Clear();
                            cmd1.Parameters.AddWithValue("@id", id_bilhete);
                            cmd1.ExecuteNonQuery();

                            conexao.Close();


                            MessageBox.Show("Bilhete de passagem eliminado");
                        }

                       

                    }
                    else
                    {
                        conexao.Close();
                    }
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
            }
            
        }

        private void cadastrar_bilhete(object sender, EventArgs e)
        {
            cadastrar_bilhete Tela_bi = new cadastrar_bilhete();
            Tela_bi.ShowDialog();
        }

        private void actualizar_bilhete(object sender, EventArgs e)
        {
            if(id_bilhete != "")
            {
                editar_bilhete Tela_bi = new editar_bilhete();
                Tela_bi.ShowDialog();
            }
            else
            {
                MessageBox.Show("Seleciona o bilhete de passagem que dejesa alterar");
            }
            
        }
    }
}
