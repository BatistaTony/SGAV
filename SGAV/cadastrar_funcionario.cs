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
    public partial class cadastrar_funcionario : Form
    {
        MySqlConnection conexao;
        
        public cadastrar_funcionario()
        {
            InitializeComponent();
            String strCon = "Server=localhost;Uid=tony;Pwd=thony@1;Database=appav";
            conexao = new MySqlConnection(strCon);

            
           
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

        private void cadastrar(object sender, EventArgs e)
        {
           
            String nome = bunifuMetroTextboxNome.Text+" "+bunifuMetroTextboxsobrenome.Text;
            
            String email = bunifuMetroTextboxemail.Text;
            String num_bi = bunifuMetroTextboxnumbi.Text;

            
            DateTime dt = bunifuDatepickerdatadensac.Value.Date;
            
            String data_nasc = dt.Year + "-" + dt.Month + "-" + dt.Day;
            String sexo = bunifuDropdownsexo.selectedValue.ToString();
            String municipio = bunifuMetroTextboxmunicipio.Text;
            String bairro = bunifuMetroTextboxbairro.Text;
            String login = bunifuMetroTextboxlogin.Text;
            String senha = bunifuMetroTextboxsenha.Text;
            String cargo = bunifuDropdowncargo.selectedValue.ToString();
            String provincia = bunifuMetroTextboxprovincia.Text;
            String rua = bunifuMetroTextboxrua.Text;
            String telefone1 = bunifuMetroTextboxtelefone1.Text;
            String telefone2 = bunifuMetroTextboxtelefone2.Text;

            if (nome == "" || email == "" || num_bi == "" || municipio == "" || bairro == "" || sexo == "" || cargo == "" || login == "" || senha == "" || provincia == "" || rua == "" || telefone1 == "")
            {
                MessageBox.Show("Há um ou mais campos vazios no formulario");
               
            }
            else
            {
                

                if (this.MailValidater(email) == false) {
                    MessageBox.Show("E-mail invalido");
                }
                else {

                    if(Convert.ToInt16(num_bi.Length) == 14)
                    {
                        conexao.Close();
                        String stmt1 = "SELECT * FROM usuario where BI=@numbi";
                        MySqlCommand cmd4 = conexao.CreateCommand();
                        cmd4.Parameters.Clear();
                        cmd4.Parameters.AddWithValue("@numbi", num_bi);
                        cmd4.CommandText = stmt1;

                        conexao.Open();

                        MySqlDataReader reader1 = cmd4.ExecuteReader();

                        if (reader1.Read())
                        {
                            MessageBox.Show("Funcionario com o numero do Bilhete de Identidade ja existe");
                        }
                        else
                        {

                            DateTime dateYear = DateTime.UtcNow.Date;

                            if ((dateYear.Year - dt.Year) > 18)
                            {
                    try
                    {
                    
                    conexao.Close();
                    conexao.Open();

                                    if (EmailExist(email))
                                    {
                                        MessageBox.Show("Ja existe um funcionario com este email");
                                    }
                                    else
                                    {

                                        String camposFunc = "(cargo, senha, login, BI,nome,sexo,telefone1,email,telefone2,rua,bairro,municipio,provincia,data_nascimento)";
                                        String query = "INSERT INTO usuario" + camposFunc + "  VALUES (@cargo,@senha, @login,@bi,@nome,@sexo,@telefone1,@email,@telefone2,@rua,@bairro,@municipio,@provincia,@data_nascimento)";
                                        MySqlCommand cmd = new MySqlCommand(query, conexao);
                                        cmd.Parameters.Clear();
                                        cmd.Parameters.AddWithValue("@bi", num_bi);
                                        cmd.Parameters.AddWithValue("@nome", nome);
                                        cmd.Parameters.AddWithValue("@sexo", sexo);
                                        cmd.Parameters.AddWithValue("@telefone1", telefone1);
                                        cmd.Parameters.AddWithValue("@telefone2", telefone2);
                                        cmd.Parameters.AddWithValue("@email", email);
                                        cmd.Parameters.AddWithValue("@rua", rua);
                                        cmd.Parameters.AddWithValue("@bairro", bairro);
                                        cmd.Parameters.AddWithValue("@municipio", municipio);
                                        cmd.Parameters.AddWithValue("@provincia", provincia);
                                        cmd.Parameters.AddWithValue("@data_nascimento", data_nasc);
                                      
                                            cmd.Parameters.AddWithValue("@login", login);
                                            cmd.Parameters.AddWithValue("@cargo", cargo);
                                            cmd.Parameters.AddWithValue("@senha", senha);
                                            cmd.ExecuteNonQuery();

                                            conexao.Close();

                                            MessageBox.Show("Funcionario cadastrado");
                                            this.Visible = false;
                                        
                                       

                                    }        

                }
                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                }
                            }
                            else {

                                MessageBox.Show("Menores de 18 anos não são permitidos nesta empresa");

                            }
                    
                    
                        }
                    }
                    else
                    {
                        MessageBox.Show("O numero do Bilhete de Identidade deve ter apenas 14 carecteres");
                    }

                
                }
                
            }
            
        }

       

        private void CancelEdit(object sender, EventArgs e)
        {
            this.Visible = false;
        }

        private bool EmailExist(String email)
        {
            String strCon = "Server=localhost;Uid=tony;Pwd=thony@1;Database=appav";
            MySqlConnection conexao = new MySqlConnection(strCon);



            conexao.Close();
            String stmt = "SELECT * FROM usuario WHERE email = @email";
            MySqlCommand cmd = conexao.CreateCommand();
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@email", email);
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
    }
}
