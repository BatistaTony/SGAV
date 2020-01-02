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
    public partial class Form1 : Form
    {
        int tentaivas = 0;
        public Form1()
        {
            InitializeComponent();
            this.labelErro.Visible = false;
            this.IconeErro.Visible = false;



        }

        public static string idAdmin;
        bool flag = false;

        private void Logar(object sender, EventArgs e)
        {

            String strCon = "Server=localhost;Uid=tony;Pwd=thony@1;Database=appav";
            MySqlConnection conexao = new MySqlConnection(strCon);

            

            String userLogin = login.Text;
            String userSenha = senha.Text;
            int erros = 0;

            if(userLogin == "")
            {
               MessageBox.Show("Login vazio");
                erros = 1;
            }
            else
            {
                if (userSenha == "")
                {
                    MessageBox.Show("Senha vazia");
                    erros = 1;

                }
                else
                {
                   
                }
                
            }


            if (erros == 0)
            {
                String stmt = "SELECT * FROM usuario WHERE login=@login and senha=@senha";
                MySqlCommand cmd = conexao.CreateCommand();
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@login", userLogin);
                cmd.Parameters.AddWithValue("@senha", userSenha);
                cmd.CommandText = stmt;


                try
                {
                    conexao.Open();

                }
                catch (Exception Ex)
                {
                    
                    MessageBox.Show("Problemas de conexão com a base de dados: " + Ex.Message);
                }



                try
                {

                    MySqlDataReader reader = cmd.ExecuteReader();

                    if (reader.Read())
                    {
                       
                        String cargo = reader["cargo"].ToString();

                        switch (cargo)
                        {
                            case "Administrador":
                                {

                                    this.Visible = false;
                                    idAdmin = reader["id"].ToString();
                                    dashboardAdmin dashboardAdmin = new dashboardAdmin();
                                    dashboardAdmin.ShowDialog();
                                  
                                    break;
                                }

                            case "Agente de viagem":
                                {
                                    this.Visible = false;
                                    idAdmin = reader["id"].ToString();
                                    agente_de_viagem TelaVendas = new agente_de_viagem();
                                    TelaVendas.ShowDialog();
                                    break;
                                }

                            case "Director Geral":
                                {
                                    this.Visible = false;
                                    idAdmin = reader["id"].ToString();
                                    director_geral TelaDG = new director_geral();
                                    TelaDG.ShowDialog();
                                    break;
                                }

                            case "Contabilista":
                                {
                                    idAdmin = reader["id"].ToString();
                                    this.Visible = false;
                                    contablista TelaCont = new contablista();
                                    TelaCont.ShowDialog();
                                    break;
                                }

                            default:
                                {
                                    MessageBox.Show("Usuário com cargo desconhecido");
                                    break;
                                }
                        }

                        conexao.Close();
                    }
                    else
                    {

                        this.IconeErro.Visible = true;
                        this.labelErro.Visible = true;
                        
                        tentaivas = tentaivas + 1;
                        if(tentaivas == 3)
                        {
                            
                            login.Enabled = false;
                            senha.Enabled = false;
                            bunifuThinButtonlogin.Enabled = false;
                            this.labelErro.Text = "Tela de login bloqueado por excesso de tentativas";

                        }
                        else
                        {
                            this.labelErro.Text = "Usuário não encontrado ";
                            this.labeltentativa.Visible = true;
                            this.labeltentativa.Text = "Tentativas: "+tentaivas;
                        }
                        conexao.Close();
                    }


                }
                catch (Exception Ex)
                {
                    MessageBox.Show("Erro de Sintaxe " + Ex.Message);
                }


            }
           
        }

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
            if(flag == true)
            {
                this.Location = Cursor.Position;
            }
        }

        private void panel1_MouseUp(object sender, MouseEventArgs e)
        {
            flag = false;
        }
    }
}
