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
using System.Net.Mail;

namespace SGAV
{
    public partial class editar_funcionario : Form
    {
        MySqlConnection conexao;
        int id_user = funcionarios.id_user;
        public editar_funcionario()
        {
            
            InitializeComponent();
            String strCon = "Server=localhost;Uid=tony;Pwd=thony@1;Database=appav";
            conexao = new MySqlConnection(strCon);

            String stmt = "SELECT * FROM usuario where id=@id";
            MySqlCommand cmd = conexao.CreateCommand();
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@id", id_user);
            cmd.CommandText = stmt;

            conexao.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                bunifuMetroTextboxNome.Text = reader["nome"].ToString();
                bunifuMetroTextboxemail.Text = reader["email"].ToString();
                bunifuMetroTextboxnumbi.Text = reader["BI"].ToString();
                bunifuMetroTextboxmunicipio.Text = reader["municipio"].ToString();
                bunifuMetroTextboxbairro.Text = reader["bairro"].ToString();
                bunifuMetroTextboxprovincia.Text = reader["provincia"].ToString();
                bunifuMetroTextboxrua.Text = reader["rua"].ToString();
                bunifuDatepickerdatadensac.Value = Convert.ToDateTime(reader["data_nascimento"].ToString());
                bunifuMetroTextboxtelefone1.Text = reader["telefone1"].ToString();
                bunifuMetroTextboxtelefone2.Text = reader["telefone2"].ToString();


                if (reader["sexo"].ToString() == "Masculino")
                {
                    bunifuDropdownsexo.selectedIndex = 0;
                }
                else
                {
                    bunifuDropdownsexo.selectedIndex = 1;
                }
                
                conexao.Close();
            }

            
        }

        public bool MailValidater(string emailaddress)
        {
            try
            {
                MailAddress m = new MailAddress(emailaddress);
                return true;
            }
            catch (FormatException)
            {
                return false;
            }
        }

        private void actualizar_dados(object sender, EventArgs e)
        {
            try
            {
                String nome = bunifuMetroTextboxNome.Text;
                String email = bunifuMetroTextboxemail.Text;
                String numbi = bunifuMetroTextboxnumbi.Text;
                String municipio = bunifuMetroTextboxmunicipio.Text;
                String bairro = bunifuMetroTextboxbairro.Text;
                String rua = bunifuMetroTextboxrua.Text;
                String provincia = bunifuMetroTextboxprovincia.Text;
                String sexo = bunifuDropdownsexo.selectedValue.ToString();
                String telefone1 = bunifuMetroTextboxtelefone1.Text;
                String telefone2 = bunifuMetroTextboxtelefone2.Text;
                DateTime dt = bunifuDatepickerdatadensac.Value.Date;
                String data_nasc = dt.Year + "-" + dt.Month + "-" + dt.Day;

                if (this.MailValidater(email) == false)
                {
                    MessageBox.Show("E-mail invalido");
                }
                else
                {

                    if (nome == "" || email == "" || numbi == "" || municipio == "" || bairro == "" || sexo == "" || provincia == "" || rua == "" || telefone1 == "")
                    {
                        MessageBox.Show("Há um ou mais campos vazios no formulario");

                    }
                    else
                    {

                        if (Convert.ToInt16(numbi.Length) == 14)
                        {

                            DateTime dateYear = DateTime.UtcNow.Date;

                            if ((dateYear.Year - dt.Year) > 18)
                            {
                                    
                                String stmt = "UPDATE usuario SET nome=@nome, sexo=@sexo, telefone1=@telefone1, email=@email,  telefone2=@telefone2,rua=@rua, BI=@numbi, municipio=@municipio, bairro=@bairro, provincia=@provincia, data_nascimento=@data_nasc WHERE id=@id_user";

                                conexao.Open();
                                MySqlCommand cmd = conexao.CreateCommand();
                                cmd.Parameters.Clear();
                                cmd.Parameters.AddWithValue("@nome", nome);
                                cmd.Parameters.AddWithValue("@sexo", sexo);
                                cmd.Parameters.AddWithValue("@telefone1", telefone1);
                                cmd.Parameters.AddWithValue("@telefone2", telefone2);
                                cmd.Parameters.AddWithValue("@email", email);
                                cmd.Parameters.AddWithValue("@rua", rua);
                                cmd.Parameters.AddWithValue("@numbi", numbi);
                                cmd.Parameters.AddWithValue("@municipio", municipio);
                                cmd.Parameters.AddWithValue("@bairro", bairro);
                                cmd.Parameters.AddWithValue("@provincia", provincia);
                                cmd.Parameters.AddWithValue("@data_nasc", data_nasc);
                                cmd.Parameters.AddWithValue("@id_user", id_user);
                                cmd.CommandText = stmt;

                                cmd.ExecuteNonQuery();

                                MessageBox.Show("Dados actualizado");
                                this.Visible = false;

                                conexao.Close();
                            }
                            else
                            {
                                MessageBox.Show("Menores de 18 anos não são permitidos nesta empresa");
                            }


                        }
                        else
                        {
                            MessageBox.Show("O numero do Bilhete de Identidade deve ter apenas 14 carecteres");
                        }

                            

                    }


                       
                 }
                }catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
            }
       

        }

        private void CancelEdit(object sender, EventArgs e)
        {
            this.Visible = false;
        }
    }
}
