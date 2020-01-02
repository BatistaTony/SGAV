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
    public partial class director_geral : Form
    {
        MySqlConnection conexao;
        public static int id_rel = 0;
        public director_geral()
        {
            InitializeComponent();
            int id = Convert.ToInt16(Form1.idAdmin);

            String strCon = "Server=localhost;Uid=tony;Pwd=thony@1;Database=appav";
            conexao = new MySqlConnection(strCon);

            String stmt = "SELECT * FROM usuario where id=@id";
            MySqlCommand cmd = conexao.CreateCommand();
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@id", id);
            cmd.CommandText = stmt;



            try
            {
                conexao.Open();

            }
            catch (Exception Ex)
            {

                MessageBox.Show("Problemas de conexao com a base de dados: " + Ex.Message);
            }

            try
            {

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    this.nomeAdmin.Text = reader["nome"].ToString();
                    conexao.Close();
                }



            }
            catch (Exception Ex)
            {
                MessageBox.Show("Erro de Syntaxy " + Ex.Message);
            }
            this.loadData();
        }

        private void loadData()
        {
            try
            {
                conexao.Open();



                String query = "SELECT VD.id as 'ID',VD.data_venda as 'DATA',CL.nome as 'Cliente', CL.telefone1 as 'TELEFONE 1', CL.telefone2 as 'TELEFONE 2', CL.email as 'E-MAIL',FC.nome as 'AGENTE DE VIAGEM',VD.preco_total_venda as 'PRECO TOTAL' FROM venda as VD  INNER JOIN cliente as CL ON VD.id_cliente = CL.id INNER JOIN usuario as FC ON VD.id_usuario = FC.id";
                MySqlCommand cmd = new MySqlCommand(query, conexao);

                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                da.Fill(dt);
                BindingSource bsourece = new BindingSource();
                bsourece.DataSource = dt;

                dataGridView1.DataSource = bsourece;
                dataGridView1.Columns[0].Width = 50;
                dataGridView1.Columns[1].Width = 200;
                dataGridView1.Columns[2].Width = 200;
                dataGridView1.Columns[3].Width = 200;
                dataGridView1.Columns[4].Width = 200;


                conexao.Close();

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
        }

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {

            var item = dataGridView1.Rows[e.RowIndex].Cells[0].Value;
            if (item != DBNull.Value)
            {
                id_rel = Convert.ToInt16(item);
            }
            else
            {

            }

        }

        private void Search_venda(object sender, EventArgs e)
        {
            try
            {




                DateTime dt_de = bunifuDatepicker_de.Value.Date;
                String data_de = dt_de.Year + "-" + dt_de.Month + "-" + dt_de.Day;

                DateTime dt_para = bunifuDatepicker_para.Value.Date;
                String data_para = dt_para.Year + "-" + dt_para.Month + "-" + dt_para.Day;



                String query = "SELECT VD.id as 'ID',VD.data_venda as 'DATA',CL.nome as 'Cliente', CL.telefone1 as 'TELEFONE 1', CL.telefone2 as 'TELEFONE 2', CL.email as 'E-MAIL',FC.nome as 'AGENTE DE VIAGEM',VD.preco_total_venda as 'PRECO TOTAL' FROM venda as VD  INNER JOIN cliente as CL ON VD.id_cliente = CL.id INNER JOIN usuario as FC ON VD.id_usuario = FC.id  WHERE VD.data_venda BETWEEN @de AND @para ";
                MySqlCommand cmd = new MySqlCommand(query, conexao);
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@de", data_de);
                cmd.Parameters.AddWithValue("@para", data_para);


                MySqlDataAdapter da = new MySqlDataAdapter();
                da.SelectCommand = cmd;
                DataTable dt = new DataTable();
                da.Fill(dt);
                BindingSource bsourece = new BindingSource();
                bsourece.DataSource = dt;
                dataGridView1.DataSource = bsourece;

                dataGridView1.DataSource = bsourece;
                dataGridView1.Columns[0].Width = 50;
                dataGridView1.Columns[1].Width = 200;
                dataGridView1.Columns[2].Width = 200;
                dataGridView1.Columns[3].Width = 200;
                dataGridView1.Columns[4].Width = 200;

                conexao.Close();


            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }


        }

        private void visualizarRel(object sender, EventArgs e)
        {
            RelVenda TelaRelVenda = new RelVenda();
            TelaRelVenda.ShowDialog();
        }

        private void btnloadData(object sender, EventArgs e)
        {
            conexao.Close();
            this.loadData();
        }



        bool flag = false;



        private void CloseApp(object sender, EventArgs e)
        {
            Application.Exit();
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

        private void SeeRel(object sender, EventArgs e)
        {
            verRelatorio TelaRel = new verRelatorio();
            TelaRel.ShowDialog();
        }

        private void LogOut(object sender, EventArgs e)
        {
            if (MessageBox.Show("Sair da aplicação", "Sair", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Visible = false;
                Form1 TelaLogin = new Form1();
                TelaLogin.Visible = true;

            }
            else
            {

            }

        }
    }
}
