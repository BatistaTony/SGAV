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
    public partial class editar_bilhete : Form
    {
        MySqlConnection conexao;
        string id_bilhete = bilhetes.id_bilhete;
        public editar_bilhete()
        {
            InitializeComponent();
            String strCon = "Server=localhost;Uid=tony;Pwd=thony@1;Database=appav";
            conexao = new MySqlConnection(strCon);

            String stmt = "SELECT * FROM bilhete where id=@id";
            MySqlCommand cmd = conexao.CreateCommand();
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@id", id_bilhete);
            cmd.CommandText = stmt;

            conexao.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
              
                bunifuMetroTextbodestino.Text = reader["destino"].ToString();
                bunifuMetroTextboxcodigo.Text = reader["codigobi"].ToString();
                bunifuMetroTextboxhora.Text = reader["horario"].ToString();
                bunifuDatepickerdata_de_partida.Value = Convert.ToDateTime(reader["data_partida"].ToString());
                bunifuMetroTextboxlocal_de_partida.Text = reader["local_partida"].ToString(); ;
                bunifuMetroTextboxvoo.Text = reader["voo"].ToString(); 
                bunifuMetroTextboxquantidade.Text  = reader["quantidade"].ToString();
                bunifuMetroTextboxpreco.Text = reader["preco"].ToString();


                if (reader["categoria"].ToString() == "Classe Económica")
                {
                    bunifuDropdowncategoria.selectedIndex = 0;
                }
                else
                {
                    bunifuDropdowncategoria.selectedIndex = 1;
                }

                conexao.Close();
            }




        }

        private void actualizar(object sender, EventArgs e)
        {
            conexao.Close();



            String destino = bunifuMetroTextbodestino.Text;
            String codigo = bunifuMetroTextboxcodigo.Text;
            String hora_partida = bunifuMetroTextboxhora.Text;
            string categoria = bunifuDropdowncategoria.selectedValue;
            string local_partida = bunifuMetroTextboxlocal_de_partida.Text;
            string voo = bunifuMetroTextboxvoo.Text;
            String quantidade = bunifuMetroTextboxquantidade.Text;
            String preco = bunifuMetroTextboxpreco.Text;

            DateTime dt = bunifuDatepickerdata_de_partida.Value.Date;
            String data_partida = dt.Year + "-" + dt.Month + "-" + dt.Day;

            DateTime dt1 = bunifuDatepickerdata_de_partida.Value.Date;
            String data_chegada = dt1.Year + "-" + dt1.Month + "-" + dt1.Day;

            if (destino == "" || codigo == "" || categoria == "" || voo == "" || bunifuMetroTextboxhora.Text == "" || quantidade == "" || preco == "")
            {
                MessageBox.Show("Há um ou mais campos vazios no formulario");
            }
            else
            {


               
                    try
                    {
                   
                        conexao.Close();
                        conexao.Open();

                        String query = "UPDATE  bilhete SET  codigobi=@codigo,data_partida=@data_partida,  horario=@horario,  destino=@destino, categoria=@categoria, local_partida=@local_partida, voo=@voo, quantidade=@quantidade, preco=@preco WHERE id=@id";
                        MySqlCommand cmd = new MySqlCommand(query, conexao);

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@id", id_bilhete);
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

                        MessageBox.Show("Bilhete de passagem actualizado");
                        this.Visible = false;
                    }
            
                    catch (Exception Ex)
                    {
                        MessageBox.Show(Ex.Message);
                    }
                


            }


        }

        private void Canelar(object sender, EventArgs e)
        {
            this.Visible = false;
            
        }

       
    }
}
