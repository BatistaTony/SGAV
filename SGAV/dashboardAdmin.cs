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
   
    public partial class dashboardAdmin : Form
    {
        MySqlConnection conexao;
        public dashboardAdmin()
        {
            InitializeComponent();
            Alertstockbaixo();

            this.loadControl();
          
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

            //dados do grafico

           // chart1.Visible = true;


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

        private void SeeRel(object sender, EventArgs e)
        {
            verRelatorio TelaRel = new verRelatorio();
            TelaRel.ShowDialog();
        }

        private void Telabilhetes(object sender, EventArgs e)
        {
            bilhetes TelaBi = new bilhetes();
            TelaBi.Visible = true; 
            this.Visible = false;
              
        }

        private void TelaFunc(object sender, EventArgs e)
        {
            this.Visible = false;
            funcionarios TelaFunc = new funcionarios();
           TelaFunc.ShowDialog();
           
        }


        private string totalBiE(String classe)
        {
            String strCon = "Server=localhost;Uid=tony;Pwd=thony@1;Database=appav";
            MySqlConnection conexao1 = new MySqlConnection(strCon);

            int bilhetesEconomica = 0;
            
            conexao1.Open();

            String stmt = "SELECT * FROM bilhete WHERE categoria = @classe";
            MySqlCommand cmd = conexao1.CreateCommand();
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@classe", classe);
            cmd.CommandText = stmt;

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                bilhetesEconomica = bilhetesEconomica + Convert.ToInt32(reader["quantidade"].ToString());
            }

            conexao1.Close();

            return Convert.ToString(bilhetesEconomica);


        }


        private string valoresArrecadados()
        {
            //valores vendidos de bilhetes
            String strCon = "Server=localhost;Uid=tony;Pwd=thony@1;Database=appav";
            MySqlConnection conexao1 = new MySqlConnection(strCon);

            int bilhetesEconomica = 0;

            conexao1.Open();

            String stmt = "SELECT * FROM venda";
            MySqlCommand cmd = conexao1.CreateCommand();
            cmd.Parameters.Clear();
            cmd.CommandText = stmt;

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                bilhetesEconomica = bilhetesEconomica + Convert.ToInt32(reader["preco_total_venda"].ToString());
            }

            conexao1.Close();

            return Convert.ToString(bilhetesEconomica);

        }


        private string bilhetesVendidos()
        {
            //total de bilhetes vendidos
            String strCon = "Server=localhost;Uid=tony;Pwd=thony@1;Database=appav";
            MySqlConnection conexao1 = new MySqlConnection(strCon);

            int bilhetesEconomica = 0;

            conexao1.Open();

            String stmt = "SELECT * FROM bilhete_vendido";
            MySqlCommand cmd = conexao1.CreateCommand();
            cmd.Parameters.Clear();
            cmd.CommandText = stmt;

            MySqlDataReader reader = cmd.ExecuteReader();

            while (reader.Read())
            {
                bilhetesEconomica = bilhetesEconomica + Convert.ToInt32(reader["quantidade"].ToString());
            }

            conexao1.Close();

            return Convert.ToString(bilhetesEconomica);

        }


        private void btnloadData(object sender, EventArgs e)
        {
            conexao.Close();
            this.loadControl();
            this.Alertstockbaixo();
        }



        private void loadControl()
        {

            Int64 numAr = Convert.ToInt64(this.valoresArrecadados());

            labeltotalarrecadado.Text = $"{numAr:N}"; 
            labeltotalbilhetesE.Text = this.totalBiE("Classe Economica");
            labeltotalbilhetesP.Text = this.totalBiE("Primeira Classe");
            labelbilhetesvendidos.Text = this.bilhetesVendidos();
            
        }

        private void vendas(object sender, EventArgs e)
        {
            vendas TelaVenda = new SGAV.vendas();
            TelaVenda.Visible = true;
            this.Visible = false;
        }

        private void stock_baixo(object sender, EventArgs e)
        {
            StockBaixo TelaStock = new StockBaixo();
            TelaStock.Visible = true;
        }

        private void Alertstockbaixo()
        {
            String strCon = "Server=localhost;Uid=tony;Pwd=thony@1;Database=appav";
            MySqlConnection conexao = new MySqlConnection(strCon);

          

            conexao.Close();
            String stmt = "SELECT * FROM bilhete WHERE quantidade < 5";
            MySqlCommand cmd = conexao.CreateCommand();
            cmd.Parameters.Clear();
            cmd.CommandText = stmt;

            conexao.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                 int numBi = 1;

                while (reader.Read())
                {
                        numBi = numBi + 1;
                }

                if(numBi > 0)
                {
                    this.label2_stockbaixo.Visible = true;
                    this.label2_stockbaixo.Text = Convert.ToString(numBi);
                }
                else
                {

                }
            }

          
           

            

            

            conexao.Close();
        
    }

        private void VerAjuda(object sender, EventArgs e)
        {
            
            ajuda TelaAjuda = new ajuda();
            TelaAjuda.Show();
        }
    }
}
