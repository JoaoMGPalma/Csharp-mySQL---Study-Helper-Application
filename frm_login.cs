using Google.Protobuf.WellKnownTypes;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Joao_Palma_10790
{
    public partial class frm_login : Form
    {
        bool passCharOn = true;

        string user_ph_text = "Utilizador";
        string pass_ph_text = "Password";

        MySqlConnection con;
        MySqlCommand cmd;
        MySqlDataReader dr;

        public frm_login()
        {
            InitializeComponent();
            string minhaConString = ConfigurationManager.ConnectionStrings["minhaConnectionApp"].ToString();
            con = new MySqlConnection(minhaConString);
        }

        private void pb_hidePass_Click(object sender, EventArgs e)
        {
            if (!passCharOn)
            { 
                passCharOn = true;
                txt_pass.PasswordChar = '*';
                pb_hidePass.Image = Properties.Resources.showPass_logo;
            }
            else if (passCharOn) 
            {
                passCharOn = false;
                txt_pass.PasswordChar = '\0';
                pb_hidePass.Image = Properties.Resources.hidePass_logo;
            }

        }

        private void pb_user_Click(object sender, EventArgs e)
        {
            txt_user.Focus();
        }

        private void pb_pass_Click(object sender, EventArgs e)
        {
            txt_pass.Focus();
        }

        private void pnl_user_Click(object sender, EventArgs e)
        {
            txt_user.Focus();
        }

        private void pnl_pass_Click(object sender, EventArgs e)
        {
            txt_pass.Focus();
        }

        private void pb_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_login_Click(object sender, EventArgs e)
        {
            try
            {
                string query = "Select * from t_users where user_name = @user and user_pass = @pass";

                using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["minhaConnectionApp"].ConnectionString))
                {
                    using (MySqlCommand cmd = new MySqlCommand(query, con))
                    {
                        cmd.Parameters.AddWithValue("@user", txt_user.Text);
                        cmd.Parameters.AddWithValue("@pass", txt_pass.Text);

                        con.Open();

                        using (MySqlDataReader dr = cmd.ExecuteReader())
                        {
                            if (dr.Read())
                            {
                                MessageBox.Show("Login efetuado com sucesso!\n Bem vindo " + dr[1].ToString() + "!", "LOGIN", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                Sessao.CurrentUser = dr["user_name"].ToString();
                                Sessao.NivelAcesso = dr["acesso"].ToString();
                                Sessao.CurrentUserID = Convert.ToInt32(dr["id"]);
                                GerirForms.AbrirForm(this, new frm_main());
                            }
                            else
                            {
                                txt_user.Text = "";
                                txt_pass.Text = "";
                                lbl_failedLogin.Visible = true;
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void txt_user_TextChanged(object sender, EventArgs e)
        {
            lbl_failedLogin.Visible = false;
        }

        private void txt_pass_TextChanged(object sender, EventArgs e)
        {
            lbl_failedLogin.Visible = false;
        }

        private void btn_registar_Click(object sender, EventArgs e)
        {
            try
            {
                string user;
                if (txt_user.Text.Length != 0)
                    user = txt_user.Text;
                else
                {
                    MessageBox.Show("Insira valores válidos", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt_user.Clear();
                    txt_pass.Clear();
                    return;
                }
                string pass;
                if (txt_pass.Text.Length != 0)
                    pass = txt_pass.Text;
                else
                {
                    MessageBox.Show("Insira valores válidos", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt_user.Clear();
                    txt_pass.Clear();
                    return;
                }

                string query = "insert into t_users (user_name, user_pass, acesso) values (@nome, @pass, 'Guest')";

                using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["minhaConnectionApp"].ConnectionString))
                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@nome", user);
                    cmd.Parameters.AddWithValue("@pass", pass);
                    con.Open();
                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MessageBox.Show("Registo efetuado com sucesso!\n Já pode fazer login com a sua nova conta...", "Registo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Erro ao inserir");

                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void frm_login_Load(object sender, EventArgs e)
        {
            txt_user.ForeColor = Color.Gray;
            txt_pass.ForeColor = Color.Gray;

            txt_user.Text = user_ph_text;
            txt_pass.Text = pass_ph_text;
            txt_pass.PasswordChar = '\0';
        }

        private void txt_user_Leave(object sender, EventArgs e)
        {
            if(txt_user.Text == "")
            {
                txt_user.ForeColor = Color.Gray;
                txt_user.Text = user_ph_text;
            }
        }

        private void txt_pass_Leave(object sender, EventArgs e)
        {
            if (txt_pass.Text == "")
            {
                txt_pass.ForeColor = Color.Gray;
                txt_pass.PasswordChar = '\0';
                txt_pass.Text = pass_ph_text;
            }
            
        }

        private void txt_user_Enter(object sender, EventArgs e)
        {
            if (txt_user.Text == user_ph_text)
                txt_user.Text = "";
            txt_user.ForeColor = Color.Black;
        }

        private void txt_pass_Enter(object sender, EventArgs e)
        {
            if (txt_pass.Text == pass_ph_text)
                txt_pass.Text = "";

            txt_pass.PasswordChar = '*';
            txt_pass.ForeColor = Color.Black;
        }
    }
}
