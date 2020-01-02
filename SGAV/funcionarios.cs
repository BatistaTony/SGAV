using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MySql.Data.MySqlClient;

namespace SGAV
{
    public partial class funcionarios : Form
    {
        MySqlConnection conexao;
        public static int id_user = 0;
        public funcionarios()
        {
            InitializeComponent();

            String strCon = "Server=localhost;Uid=tony;Pwd=thony@1;Database=appav";
            conexao = new MySqlConnection(strCon);
            this.loadData();

           
           

        }

        

        private void loadData()
        {
            bunifuMetroTextboxCodfunc.Text = " ";
                try
                {
                    conexao.Open();

               

                    String query = "SELECT id as 'ID',nome as 'NOME',sexo as 'SEXO', data_nascimento as 'Data de Nascimento',email as 'E-MAIL', telefone1 as 'TELEFONE 1', telefone2 as 'TELEFONE 2',bairro as 'BAIRRO', rua as 'RUA',municipio as 'MUNICIPIO', provincia as 'PROVINCIA', cargo as 'CARGO' FROM usuario WHERE cargo != 'Administrador'";
                    MySqlCommand cmd = new MySqlCommand(query, conexao);
               
                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmd;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    BindingSource bsourece = new BindingSource();
                    bsourece.DataSource = dt;

                    dataGridView1.DataSource = bsourece;
                    dataGridView1.Columns[0].Width = 50;
                    dataGridView1.Columns[1].Width = 150;
                    dataGridView1.Columns[3].Width = 90;
                    dataGridView1.Columns[4].Width = 100;
                    dataGridView1.Columns[5].Width = 100;
                    dataGridView1.Columns[6].Width = 100;
                    dataGridView1.Columns[7].Width = 100;
                    dataGridView1.Columns[8].Width = 50;
                    dataGridView1.Columns[9].Width = 100;
                    dataGridView1.Columns[10].Width = 100;
                    dataGridView1.Columns[11].Width = 120;

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
            this.Visible = false;
            dashboardAdmin TelaAdmin = new dashboardAdmin();
            TelaAdmin.Visible = true;
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


       

       

        private void Search_User(object sender, EventArgs e)
        {
            try
            {
                

                String id = bunifuMetroTextboxCodfunc.Text;

                if (id == "")
                {
                    MessageBox.Show("campo vazio");
                }
                else
                {

                    String query = "SELECT id as 'ID',nome as 'NOME',sexo as 'SEXO', data_nascimento as 'Data de Nascimento',email as 'E-MAIL', telefone1 as 'TELEFONE 1', telefone2 as 'TELEFONE 2',bairro as 'BAIRRO', rua as 'RUA',municipio as 'MUNICIPIO', provincia as 'PROVINCIA', cargo as 'CARGO' FROM usuario WHERE cargo != 'Administrador' AND id=@id";
                    MySqlCommand cmd = new MySqlCommand(query, conexao);
                    cmd.Parameters.Clear();
                    cmd.Parameters.AddWithValue("@id", id);

                    MySqlDataAdapter da = new MySqlDataAdapter();
                    da.SelectCommand = cmd;
                    DataTable dt = new DataTable();
                    da.Fill(dt);
                    BindingSource bsourece = new BindingSource();
                    bsourece.DataSource = dt;
                    dataGridView1.DataSource = bsourece;

                    dataGridView1.DataSource = bsourece;
                    dataGridView1.Columns[0].Width = 50;
                    dataGridView1.Columns[1].Width = 150;
                    dataGridView1.Columns[3].Width = 90;
                    dataGridView1.Columns[4].Width = 100;
                    dataGridView1.Columns[5].Width = 100;
                    dataGridView1.Columns[6].Width = 100;
                    dataGridView1.Columns[7].Width = 100;
                    dataGridView1.Columns[8].Width = 50;
                    dataGridView1.Columns[9].Width = 100;
                    dataGridView1.Columns[10].Width = 100;
                    dataGridView1.Columns[11].Width = 120;

                    conexao.Close();
                }
                
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }

           
        }

       

       
        private void btnloadData(object sender, EventArgs e)
        {
            conexao.Close();
            this.loadData();
        }

        private void cadastrar_func(object sender, EventArgs e)
        {
            cadastrar_funcionario TelaCad = new cadastrar_funcionario();
            TelaCad.ShowDialog();
        }

        
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            try {
                var item = dataGridView1.Rows[e.RowIndex].Cells[0].Value;
                if (item != DBNull.Value)
                {
                    id_user = Convert.ToInt16(item);

                }
                else
                {

                }
            }catch(Exception Ex)
            {

            }

}

        private void ApgarUser(object sender, EventArgs e)
        {
            if (id_user > 0)
            {
                try
                {
                    
                    if(MessageBox.Show("Tem a certeza que pretende eliminar o funcionario","Remover funcionario", MessageBoxButtons.YesNo) == DialogResult.Yes) {
                        conexao.Open();
                        String query1 = "DELETE FROM usuario where id=@id";
                        MySqlCommand cmd1 = new MySqlCommand(query1, conexao);
                        cmd1.Parameters.Clear();
                        cmd1.Parameters.AddWithValue("@id", id_user);
                        cmd1.ExecuteNonQuery();
                        conexao.Close();
                        

                        MessageBox.Show("Funcionario eliminado");

                    }
                    else
                    {

                    }
                }
                catch (Exception Ex)
                {
                    
                    MessageBox.Show(Ex.Message);
                }
               
            }
            else
            {
                MessageBox.Show("Seleciona o usuario para remover");
            }
        }

        private void actualizardadosfunc(object sender, EventArgs e)
        {
            if (id_user > 0)
            {
                editar_funcionario TelaEdi_func = new editar_funcionario();
                TelaEdi_func.ShowDialog();
            }
            else
            {
                MessageBox.Show("Seleciona o usuario que deseja actulizar os dados");
            }
            
        }

        
    }
}
