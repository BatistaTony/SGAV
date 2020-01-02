using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SGAV
{
    public partial class vender : Form
    {

        MySqlConnection conexao = new MySqlConnection("Server = localhost; Uid=tony;Pwd=thony@1;Database=appav");

        String id_bilhete = agente_de_viagem.id_bilhete;
        String id_usuario = agente_de_viagem.id_usuario;


        public vender()
        {
            InitializeComponent();
            receberID();
        }

       

        private void receberID()
        {

            String stmt = "SELECT * FROM bilhete where id=@id";
            MySqlCommand cmd = conexao.CreateCommand();
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@id", id_bilhete);
            cmd.CommandText = stmt;

            conexao.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {

                destino.Text = reader["destino"].ToString();
                codigoBi.Text = reader["codigobi"].ToString();
                hora.Text = reader["horario"].ToString();
                datapartida.Value = Convert.ToDateTime(reader["data_partida"].ToString());
                localdepartida.Text = reader["local_partida"].ToString(); ;
                voo.Text = reader["voo"].ToString();
                categoria.Text = reader["categoria"].ToString();

                preco.Text = reader["preco"].ToString();

                conexao.Close();
            }

        }

        private void Preco_total()
        {
            Double preco_unit = Convert.ToDouble(preco.Text);
            String quant = quantidade.Text;

            try
            {
                labelPrecototal.Text = Convert.ToString(preco_unit * Convert.ToInt32(quant));
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
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


        public bool cadastrarCliente()
        {

            if (nome.Text == "" || email.Text == "" || nropassaporte.Text == "" || nacionalidade.Text == "" || telefone1.Text == "")
            {
                MessageBox.Show("Há um ou mais campos vazios no formulario");
                return false;

            }
            else
            {


                if (this.MailValidater(email.Text) == false)
                {
                    MessageBox.Show("E-mail invalido");
                    return false;
                }
                else
                {
                    try
                    {
                        this.conexao.Close();

                        MySqlConnection conexao = new MySqlConnection("Server = localhost; Uid=tony;Pwd=thony@1;Database=appav");


                        conexao.Open();

                        String camposFunc = "(nome,sexo,email,data_nascimento,num_passaporte,nacionalidade,telefone1,telefone2)";

                        String query = "INSERT INTO cliente" + camposFunc + "  VALUES (@nome,@sexo,@email,@data_nascimento,@num_passaporte,@nacionalidade,@telefone1,@telefone2)";

                        MySqlCommand cmd = new MySqlCommand(query, conexao);

                        DateTime dt = datanascimento.Value.Date;

                        String data_nasc = dt.Year + "-" + dt.Month + "-" + dt.Day;

                        cmd.Parameters.Clear();
                        cmd.Parameters.AddWithValue("@nome", nome.Text);
                        cmd.Parameters.AddWithValue("@sexo", sexo.selectedValue.ToString());
                        cmd.Parameters.AddWithValue("@num_passaporte", nropassaporte.Text);
                        cmd.Parameters.AddWithValue("@telefone1", telefone1.Text);
                        cmd.Parameters.AddWithValue("@telefone2", telefone2.Text);
                        cmd.Parameters.AddWithValue("@email", email.Text);
                        cmd.Parameters.AddWithValue("@data_nascimento", data_nasc);
                        cmd.Parameters.AddWithValue("@nacionalidade", nacionalidade.Text);
                        cmd.ExecuteNonQuery();
                        conexao.Close();


                        return true;
                    }
                    catch (Exception Ex)
                    {
                        
                        MessageBox.Show(Ex.Message);
                        conexao.Close();
                        return false;
                    }

                }
            }
        }



        public int idCliente()
        {

            MySqlConnection conexao = new MySqlConnection("Server = localhost; Uid=tony;Pwd=thony@1;Database=appav");


            String stmt = "SELECT * FROM cliente where email=@email";
            MySqlCommand cmd1 = conexao.CreateCommand();
            cmd1.Parameters.Clear();
            cmd1.Parameters.AddWithValue("@email", email.Text);
            cmd1.CommandText = stmt;

            conexao.Open();

            MySqlDataReader reader = cmd1.ExecuteReader();

            String id_cliente = "";

            if (reader.Read())
            {

                id_cliente = reader["id"].ToString();


            }

            conexao.Close();
            return Convert.ToInt16(id_cliente);

        }

        public int IdVenda()
        {
            conexao.Close();

            String id_ven = "";

            try
            {



                String stmt = "SELECT id FROM venda WHERE id_cliente=@id";
                MySqlCommand cmd = conexao.CreateCommand();
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id", this.idCliente());
                cmd.CommandText = stmt;

                conexao.Open();

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    id_ven = reader["id"].ToString();
                    conexao.Close();
                    return Convert.ToInt16(id_ven);

                }
                else
                {
                    conexao.Close();
                    return 0;

                }
            }
            catch (Exception eX)
            {
                MessageBox.Show(eX.Message);
                conexao.Close();
                return 0;

            }



        }

        public void cadastrarBilheteVendido()
        {
            try
            {
                MySqlConnection conexao = new MySqlConnection("Server = localhost; Uid=tony;Pwd=thony@1;Database=appav");


                conexao.Open();

                String camposFunc = "(id_venda,preco_venda,quantidade,id_bilhete)";

                String query = "INSERT INTO bilhete_vendido" + camposFunc + "  VALUES (@id_venda,@preco_venda,@quantidade,@id_bilhete)";

                MySqlCommand cmd = new MySqlCommand(query, conexao);

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id_venda", this.IdVenda());
                cmd.Parameters.AddWithValue("@preco_venda", preco.Text);
                cmd.Parameters.AddWithValue("@quantidade", quantidade.Text);
                cmd.Parameters.AddWithValue("@id_bilhete", id_bilhete);
                cmd.ExecuteNonQuery();
                conexao.Close();


            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                conexao.Close();
            }
        }


        public bool cadastrarVenda()
        {
            try
            {
                MySqlConnection conexao = new MySqlConnection("Server = localhost; Uid=tony;Pwd=thony@1;Database=appav");

                String stmt = "SELECT * FROM cliente where email=@email";
                MySqlCommand cmd1 = conexao.CreateCommand();
                cmd1.Parameters.Clear();
                cmd1.Parameters.AddWithValue("@email", email.Text);
                cmd1.CommandText = stmt;

                conexao.Open();

                MySqlDataReader reader = cmd1.ExecuteReader();

                String id_cliente = "";

                if (reader.Read())
                {

                    id_cliente = reader["id"].ToString();

                   
                }


                conexao.Close();
               
                conexao.Open();

                String camposFunc = "(data_venda,id_cliente,id_usuario,preco_total_venda)";

                String query = "INSERT INTO venda" + camposFunc + "  VALUES (@data_venda, @id_cliente, @id_usuario, @preco_total)";

                MySqlCommand cmd = new MySqlCommand(query, conexao);


                DateTime data_actual = DateTime.Today;

                this.Preco_total();


                String preco_total = labelPrecototal.Text;

                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@data_venda", data_actual.ToString("yyyy-MM-dd"));
                cmd.Parameters.AddWithValue("@id_cliente", id_cliente);
                cmd.Parameters.AddWithValue("@id_usuario", id_usuario);
                cmd.Parameters.AddWithValue("@preco_total", preco_total);
                cmd.ExecuteNonQuery();
                conexao.Close();

                return true;

            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message);
                conexao.Close();
                return false;
            }
        }


        public int Qbilhete()
        {


            try
            {

                String id_ven = "";

                MySqlConnection conexao = new MySqlConnection("Server = localhost; Uid=tony;Pwd=thony@1;Database=appav");


                String stmt = "SELECT quantidade FROM bilhete WHERE id=@id";
                MySqlCommand cmd = conexao.CreateCommand();
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id", id_bilhete);
                cmd.CommandText = stmt;


                conexao.Open();

                MySqlDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    id_ven = reader["quantidade"].ToString();
                    conexao.Close();
                    return Convert.ToInt16(id_ven);

                }
                else
                {
                    conexao.Close();
                    return 0;

                }
            }
            catch (Exception eX)
            {
                MessageBox.Show(eX.Message);
                conexao.Close();
                return 0;

            }

        }

        public void DimBilhete()
        {
            conexao.Close();
            try
            {

                MySqlConnection conexao = new MySqlConnection("Server = localhost; Uid=tony;Pwd=thony@1;Database=appav");


                int quan = Convert.ToInt16(quantidade.Text);
                quan = this.Qbilhete() - quan;

                conexao.Open();
                String stmt = "UPDATE  bilhete SET quantidade=@quantidade WHERE id=@id";
                MySqlCommand cmd = conexao.CreateCommand();
                cmd.Parameters.Clear();
                cmd.Parameters.AddWithValue("@id", id_bilhete);
                cmd.Parameters.AddWithValue("@quantidade", quan);
                cmd.CommandText = stmt;
                cmd.ExecuteNonQuery();

                

            }
            catch (Exception eX)
            {
                
                MessageBox.Show(eX.Message);
                conexao.Close();
            }

        }

        private void venderBi(object sender, EventArgs e)
        {
            if (this.EmailExist(email.Text))
            {
                MessageBox.Show("Email Ja existe");
            }
            else
            {
                try
                {

                    conexao.Close();

                    if (cadastrarCliente()) {                   
                        if (cadastrarVenda())
                        {
                            
                            cadastrarBilheteVendido();
                            DimBilhete();

                            MessageBox.Show("Venda feita com sucesso");

                            this.Visible = false;

                            factura TelaFat = new factura();
                            TelaFat.ShowDialog();

                            }
                        else
                        {
                            MessageBox.Show("Erro ao fazer a venda");
                        }

                    }
                    else
                    {
                        MessageBox.Show("Erro ao cadastrar cliente");
                    }


                    

                    

                }

                catch (Exception Ex)
                {
                    MessageBox.Show(Ex.Message);
                    conexao.Close();
                }
            }

        }

        private bool EmailExist(String email)
        {
            String strCon = "Server=localhost;Uid=tony;Pwd=thony@1;Database=appav";
            MySqlConnection conexao = new MySqlConnection(strCon);



            conexao.Close();
            String stmt = "SELECT * FROM cliente WHERE email = @email";
            MySqlCommand cmd = conexao.CreateCommand();
            cmd.Parameters.Clear();
            cmd.Parameters.AddWithValue("@email", email);
            cmd.CommandText = stmt;

            conexao.Open();

            MySqlDataReader reader = cmd.ExecuteReader();

            if (reader.Read())
            {
                conexao.Close();
                return true;

            }
            else
            {
                conexao.Close();
                return false;

            }
        }







    }
}
