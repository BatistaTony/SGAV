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
    public partial class cadastrar_bilhete : Form
    {
        MySqlConnection conexao;
        public cadastrar_bilhete()
        {
            InitializeComponent();
            String strCon = "Server=localhost;Uid=tony;Pwd=thony@1;Database=appav";
            conexao = new MySqlConnection(strCon);
        }

        private void Cadastrar_Bilhete(object sender, EventArgs e)
        {
            conexao.Close();
            String destino = bunifuMetroTextboxdestino.Text;
            String codigo = bunifuMetroTextboxcodigo.Text;
            String hora_partida = bunifuMetroTextboxhora.Text + ":"+ bunifuMetroTextboxminuto.Text;
            string categoria = bunifuDropdowncategoria.selectedValue;
            string local_partida = bunifuMetroTextboxlocal_de_partida.Text;
            string voo = bunifuMetroTextboxvoo.Text;
            String quantidade = bunifuMetroTextboxquantidade.Text;
            String preco = bunifuMetroTextboxpreco.Text;

            DateTime dt = bunifuDatepickerdata_de_partida.Value.Date;
            String data_partida = dt.Year + "-" + dt.Month + "-" + dt.Day;

            DateTime dt1 = bunifuDatepickerdata_de_partida.Value.Date;
            String data_chegada = dt1.Year + "-" + dt1.Month + "-" + dt1.Day;

            if (destino == "" || codigo == "" || categoria == "" || voo == "" || bunifuMetroTextboxhora.Text == "" || bunifuMetroTextboxminuto.Text == "" || quantidade == " " || preco == "")
            {
                MessageBox.Show("Há um ou mais campos vazios no formulario");
            }
            else
            {
                if (Convert.ToInt16(bunifuMetroTextboxhora.Text) > 23 || Convert.ToInt16(bunifuMetroTextboxminuto.Text) > 60 || Convert.ToInt16(bunifuMetroTextboxhora.Text) < 0 || Convert.ToInt16(bunifuMetroTextboxminuto.Text) < 0)
                {
                    MessageBox.Show("Formato de Tempo invalido");
                }
                else { 
                try
                {
                        if (bilheteExist(codigo))
                        {
                            MessageBox.Show("Bilhete de passagem ja existe");
                        }
                        else
                        {

                            conexao.Close();
                            conexao.Open();

                            String camposFunc = "(codigobi,data_partida,horario,destino,categoria,local_partida,voo, quantidade, preco)";
                            String query = "INSERT INTO bilhete" + camposFunc + "  VALUES (@codigo,@data_partida,@horario,@destino,@categoria,@local_partida,@voo, @quantidade, @preco)";
                            MySqlCommand cmd = new MySqlCommand(query, conexao);
                            cmd.Parameters.Clear();
                            cmd.Parameters.AddWithValue("@codigo", codigo);
                            cmd.Parameters.AddWithValue("@data_partida", data_partida);
                            cmd.Parameters.AddWithValue("@horario", hora_partida);
                            cmd.Parameters.AddWithValue("@destino", destino);
                            cmd.Parameters.AddWithValue("@categoria", categoria);
                            cmd.Parameters.AddWithValue("@local_partida", local_partida);
                            cmd.Parameters.AddWithValue("@voo", voo);
                            cmd.Parameters.AddWithValue("@quantidade", quantidade);
                            cmd.Parameters.AddWithValue("@preco", preco);
                            cmd.ExecuteNonQuery();



                            conexao.Close();

                            MessageBox.Show("Bilhete de passagem cadastrado");
                            this.Visible = false;
                        }
                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }

            }

            }


        }

        private void Canelar(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private bool bilheteExist(String numBi)
        {
            
           // String strCon = "Server=localhost;Uid=tony;Pwd=thony@1;Database=appav";
           // MySqlConnection conexao = new MySqlConnection(strCon);



            conexao.Close();
            String stmt = "SELECT * FROM bilhete WHERE id = @id";
            MySqlCommand cmd = conexao.CreateCommand();
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@id", numBi);
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

        private void label9_Click(object sender, EventArgs e)
        {

        }

       
    }
}
