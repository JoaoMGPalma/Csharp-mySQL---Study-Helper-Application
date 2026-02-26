using MySql.Data.MySqlClient;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Diagnostics.Eventing.Reader;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Joao_Palma_10790
{
    public partial class frm_main : Form
    {
        int pergunta_atual = -1;
        string tema_escolhido;
        List<Pergunta> perguntas = new List<Pergunta>();

        // Gestao de Utilizadores
        int id_selecionado = -1;


        private void CarregarPerguntas(int IdTema)
        {
            perguntas.Clear();
            string minhaConString = ConfigurationManager.ConnectionStrings["minhaConnectionApp"].ToString();

            using (MySqlConnection conn = new MySqlConnection(minhaConString))
            {
                conn.Open();

                // BUSCAR 10 PERGUNTAS ALEATÓRIAS
                string query_p = "SELECT id, pergunta, explicacao FROM t_perguntas WHERE id_tema = @tema ORDER BY RAND() LIMIT 10";
                MySqlCommand cmd_p = new MySqlCommand(query_p, conn);
                cmd_p.Parameters.AddWithValue("@tema", IdTema);

                using (MySqlDataReader reader_p = cmd_p.ExecuteReader())
                {
                    while (reader_p.Read())
                    {
                        Pergunta p = new Pergunta();
                        p.id = reader_p.GetInt32("id");
                        p.texto = reader_p.GetString("pergunta");
                        p.explicacao = reader_p.GetString("explicacao");
                        perguntas.Add(p);
                    }
                }

                // BUSCAR RESPOSTAS PARA CADA PERGUNTA CARREGADA
                foreach (var p in perguntas)
                {
                    string query_r = "SELECT resposta, correta FROM t_respostas WHERE id_pergunta = @id";
                    MySqlCommand cmd_r = new MySqlCommand(query_r, conn);
                    cmd_r.Parameters.AddWithValue("@id", p.id);

                    using (MySqlDataReader reader_r = cmd_r.ExecuteReader())
                    {
                        while (reader_r.Read())
                        {
                            p.respostas.Add(new Resposta
                            {
                                texto = reader_r.GetString("resposta"),
                                correta = reader_r.GetBoolean("correta")
                            });
                        }
                    }
                }
            }
        }

        //
        // FUNÇÃO QUE CONTROLA A NAVEGAÇÃO POR PERGUNTA - PARA INCREMENTAR > '1' PARA  DECREMENTAR > '-1? 
        // - MUDA O NUMERO DA PERGUNTA SELECIONADA E LIMITA-O A 1-10
        // - MUDA A PERGUNTA QUE ESTÁ A SER MOSTRADA NO MOMENTO E AS RESPETIVAS RESPOSTAS
        private void ControloNavegacao(int Direcao) 
        {
           
            pergunta_atual += Direcao;
            
            if (pergunta_atual < 0)
                pergunta_atual = 0;

            if (pergunta_atual > 9)
                pergunta_atual = 9;

            if (pergunta_atual < 10)
                lbl_pergunta.Text = (pergunta_atual+1).ToString();
            else if (pergunta_atual >= 10)
                lbl_pergunta.Text = (pergunta_atual+1).ToString();

            
            txt_pergunta.Text = perguntas[pergunta_atual].texto;
            txt_A.Text = perguntas[pergunta_atual].respostas[0].texto;
            txt_B.Text = perguntas[pergunta_atual].respostas[1].texto;
            txt_C.Text = perguntas[pergunta_atual].respostas[2].texto;
            txt_D.Text = perguntas[pergunta_atual].respostas[3].texto;

            if (perguntas[pergunta_atual].resposta_escolhida == 0)
                check_A.Checked = true;

            else if (perguntas[pergunta_atual].resposta_escolhida == 1)
                check_B.Checked = true;

            else if (perguntas[pergunta_atual].resposta_escolhida == 2)
                check_C.Checked = true;

            else if (perguntas[pergunta_atual].resposta_escolhida == 3)
                check_D.Checked = true;
        }

        private int CalcularRespostasCertas()
        {
            int certas = 0;

            foreach (var p in perguntas)
            {
               if (p.resposta_escolhida < 0)
                    continue;

               if (p.respostas[p.resposta_escolhida].correta)
                    certas++;
            }

            return certas;
        }

        public void permitirUmCheck(CheckBox check) 
        { 
            check_A.Checked = false;
            check_B.Checked = false;
            check_C.Checked = false;
            check_D.Checked = false;
            check.Checked = true;
        }

        private void guardarRespostaEscolhida()
        {
            if (check_A.Checked)
                perguntas[pergunta_atual].resposta_escolhida = 0;

            else if (check_B.Checked)
                perguntas[pergunta_atual].resposta_escolhida = 1;

            else if (check_C.Checked)
                perguntas[pergunta_atual].resposta_escolhida = 2;

            else if (check_D.Checked)
                perguntas[pergunta_atual].resposta_escolhida = 3;
        }

        public frm_main()
        {
            InitializeComponent();
        }

        private void frm_main_Load(object sender, EventArgs e)
        {
            // DADOS DA SESSÃO (USER E NIVEL DE ACESSO)
            lbl_txt_acesso.Text = Sessao.NivelAcesso;
            lbl_txt_user.Text = Sessao.CurrentUser;
            lbl_txt_data.Text = DateTime.Today.ToString("dd/MM/yyyy");

            tab1.TabPages.Remove(tab_page_resultados);

            if (Sessao.NivelAcesso == "Guest")
            {
                tab1.TabPages.Remove(tab_page_gestao);
            }
            else if (Sessao.NivelAcesso == "Admin")
            {
                btn_eliminar.Enabled = false;
                btn_eliminar.Enabled = false;
                btn_atualizar.Enabled = false;
                btn_inserir.Enabled = false;

                string minhaConString = ConfigurationManager.ConnectionStrings["minhaConnectionApp"].ToString();

                using (MySqlConnection conn = new MySqlConnection(minhaConString))
                {
                    conn.Open();

                    string sql = "Select id as ID, user_name as Nome, '****' as Pass, data_inscricao as 'Data de Inscrição', acesso as 'Nível de Acesso' from t_users";

                    using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["minhaConnectionApp"].ConnectionString))
                    using (MySqlCommand cmd = new MySqlCommand(sql, con))
                    using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                    {
                        DataTable dt = new DataTable();
                        con.Open();
                        da.Fill(dt);
                        grid_data.DataSource = dt;

                    }
                    grid_data.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                }
            }

            lbl_pergunta.Text = " " + 0.ToString();

            if (cb_tema_escolhido.SelectedIndex == -1)
            {
                btn_iniciar.Enabled = false;
                btn_anterior.Enabled = false;
                btn_proxima.Enabled = false;
                btn_terminar.Enabled = false;
                check_A.Enabled = false;
                check_B.Enabled = false;
                check_C.Enabled = false;
                check_D.Enabled = false;
            }
        }

        private void btn_menu_MouseHover(object sender, EventArgs e)
        {
            btn_menu.ForeColor = Color.DarkGray;
            btn_menu.FlatAppearance.BorderColor = Color.DarkGray;
        }

        private void btn_menu_MouseLeave(object sender, EventArgs e)
        {
            btn_menu.ForeColor = Color.LightGray;
            btn_menu.FlatAppearance.BorderColor = Color.LightGray;
        }

        private void btn_menu_Click(object sender, EventArgs e)
        {
            GerirForms.AbrirForm(this, new frm_login());
        }

        private void pb_exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btn_iniciar_Click(object sender, EventArgs e)
        {
            btn_anterior.Enabled = true;
            btn_proxima.Enabled = true;
            btn_terminar.Enabled = true;
            check_A.Enabled = true;
            check_B.Enabled = true;
            check_C.Enabled = true;
            check_D.Enabled = true;

            if (cb_tema_escolhido.SelectedIndex == 0)
            {
                // CARREGA IMAGEM REFERENTE AO TEMA
                pb_tema_img.Image = Properties.Resources.Csharp_IMG;
                tema_escolhido = "Programação C#";
            }
            else if (cb_tema_escolhido.SelectedIndex == 1)
            {
                // CARREGA IMAGEM REFERENTE AO TEMA
                pb_tema_img.Image = Properties.Resources.poo_IMG;
                tema_escolhido = "P. Orientada a Objetos";
            }
            else if (cb_tema_escolhido.SelectedIndex == 2)
            {
                // CARREGA IMAGEM REFERENTE AO TEMA
                pb_tema_img.Image = Properties.Resources.sql_IMG;
                tema_escolhido = "Consultas SQL";
            }

            CarregarPerguntas(cb_tema_escolhido.SelectedIndex + 1);
            
            pergunta_atual = 1;
            ControloNavegacao(-1);

            lbl_A.Enabled = true;
            lbl_B.Enabled = true;
            lbl_C.Enabled = true;
            lbl_D.Enabled = true;
            txt_A.Enabled = true;
            txt_B.Enabled = true;
            txt_C.Enabled = true;
            txt_D.Enabled = true;
            check_A.Enabled = true;
            check_B.Enabled = true;
            check_C.Enabled = true;
            check_D.Enabled = true;
        }

        private void lbl_A_Click(object sender, EventArgs e)
        {
            permitirUmCheck(check_A);
            guardarRespostaEscolhida();
        }

        private void txt_A_MouseClick(object sender, EventArgs e)
        {
            permitirUmCheck(check_A);
            guardarRespostaEscolhida();
        }

        private void lbl_B_Click(object sender, EventArgs e)
        {
            permitirUmCheck(check_B);
            guardarRespostaEscolhida();
        }

        private void txt_B_MouseClick(object sender, EventArgs e)
        {
            permitirUmCheck(check_B);
            guardarRespostaEscolhida();
        }

        private void lbl_C_Click(object sender, EventArgs e)
        {
            permitirUmCheck(check_C);
            guardarRespostaEscolhida();
        }

        private void txt_C_MouseClick(object sender, EventArgs e)
        {
            permitirUmCheck(check_C);
            guardarRespostaEscolhida();
        }

        private void lbl_D_Click(object sender, EventArgs e)
        {
            permitirUmCheck(check_D);
            guardarRespostaEscolhida();
        }

        private void txt_D_MouseClick(object sender, EventArgs e)
        {
            permitirUmCheck(check_D);
            guardarRespostaEscolhida();
        }

        private void btn_proxima_Click(object sender, EventArgs e)
        {
            guardarRespostaEscolhida();

            check_A.Checked = false;
            check_B.Checked = false;
            check_C.Checked = false;
            check_D.Checked = false;
            ControloNavegacao(1);
        }
        private void btn_anterior_Click(object sender, EventArgs e)
        {
            guardarRespostaEscolhida();

            check_A.Checked = false;
            check_B.Checked = false;
            check_C.Checked = false;
            check_D.Checked = false;
            ControloNavegacao(-1);
        }
        

        private void cb_tema_escolhido_TextChanged(object sender, EventArgs e)
        {
            btn_iniciar.Enabled = true;
        }

        private void btn_terminar_Click(object sender, EventArgs e)
        {
            tab1.TabPages.Insert(0 ,tab_page_resultados);
            tab1.TabPages.Remove(tab_page_quiz);
            tab1.SelectedTab = tab_page_resultados;
            btn_reset.Visible = true;

            long id_teste_atual;

            string minhaConString = ConfigurationManager.ConnectionStrings["minhaConnectionApp"].ToString();
            using (MySqlConnection conn = new MySqlConnection(minhaConString))
            {
                conn.Open(); 
                string sql = @"insert into t_historico_testes (id_user, tema, resultado, data_resultado) 
                           values (@userId, @tema, @certas, now())";

                using (MySqlTransaction tr = conn.BeginTransaction())
                {
                    try
                    {
                        using (MySqlCommand cmd = new MySqlCommand(sql, conn, tr))
                        {
                            cmd.Parameters.AddWithValue("@userId", Sessao.CurrentUserID);
                            cmd.Parameters.AddWithValue("@certas", CalcularRespostasCertas());
                            cmd.Parameters.AddWithValue("@tema", tema_escolhido);
                            cmd.ExecuteNonQuery();

                            id_teste_atual = cmd.LastInsertedId;
                        }

                        string sqlDet = @"insert into t_resultados_testes (id_resultado, id_pergunta, id_resposta, correta) 
                              values (@id_resultado, @id_pergunta, @id_resposta, @bool_correta)";

                        foreach (var p in perguntas)
                        {
                            using (MySqlCommand cmdDet = new MySqlCommand(sqlDet, conn, tr))
                            {
                                cmdDet.Parameters.AddWithValue("@id_resultado", id_teste_atual);
                                cmdDet.Parameters.AddWithValue("@id_pergunta", p.id);
                                if (p.resposta_escolhida == -1)
                                {
                                    cmdDet.Parameters.AddWithValue("@id_resposta", DBNull.Value);
                                    cmdDet.Parameters.AddWithValue("@bool_correta", false);
                                }
                                else
                                {
                                    cmdDet.Parameters.AddWithValue("@id_resposta", p.resposta_escolhida);
                                    cmdDet.Parameters.AddWithValue("@bool_correta", p.respostas[p.resposta_escolhida].correta);
                                }
                                
                                cmdDet.ExecuteNonQuery();
                            }
                        }

                        // If we reached here without errors, save everything!
                        tr.Commit();
                    }
                    catch (Exception ex)
                    {
                        // If anything fails, undo all changes to keep database clean
                        tr.Rollback();
                        MessageBox.Show("Erro ao gravar histórico: " + ex.Message);
                    }
                }

                // QUERY PARA PREENCHER A DATAGRID DO RESULTADOS TESTES REALIZADO PELO UTILIZADOR LOGADO
                sql = @"select id, @userName as Nome, tema as Tema, resultado as Certas, (10 - resultado) as Erradas, data_resultado as 'Data do Teste'
                        from t_historico_testes where id_user = @userID";

                using (MySqlCommand cmd = new MySqlCommand(sql, conn))
                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    cmd.Parameters.AddWithValue("@userID", Sessao.CurrentUserID);
                    cmd.Parameters.AddWithValue("@userName", Sessao.CurrentUser);
                    cmd.ExecuteNonQuery();
                    da.Fill(dt);
                    grid_historico.DataSource = dt;
                    grid_historico.Columns["id"].Visible = false;
                }
                grid_data.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }

            bar_certas.Width = CalcularRespostasCertas() * 35;
            bar_erradas.Width = (10-CalcularRespostasCertas()) * 35;
        }

        private void check_A_MouseClick(object sender, MouseEventArgs e)
        {
            permitirUmCheck(check_A);
        }

        private void check_B_MouseClick(object sender, MouseEventArgs e)
        {
            permitirUmCheck(check_B);
        }

        private void check_C_MouseClick(object sender, MouseEventArgs e)
        {
            permitirUmCheck(check_C);
        }

        private void check_D_MouseClick(object sender, MouseEventArgs e)
        {
            permitirUmCheck(check_D);
        }

        private void btn_selecionar_Click(object sender, EventArgs e)
        {
            if(!string.IsNullOrWhiteSpace(txt_gest_id.Text))
            {
                id_selecionado = Convert.ToInt32(txt_gest_id.Text);
                string query = "Select id as ID, user_name as Nome, '****' as Pass, data_inscricao as 'Data de Inscrição', acesso as 'Nível de Acesso' " +
                               "from t_users where id = @id_selecionado";

                using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["minhaConnectionApp"].ConnectionString))
                using (MySqlCommand cmd = new MySqlCommand(query, con))
                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    con.Open();
                    cmd.Parameters.AddWithValue("@id_selecionado", id_selecionado);
                    da.Fill(dt);
                    grid_data.DataSource = dt;

                }
                grid_data.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

                btn_atualizar.Enabled = true;
                btn_eliminar.Enabled = true;
                txt_gest_nome.Enabled = true;
                txt_gest_pass.Enabled = true;
                cb_gest_acesso.Enabled = true;
            }
            else
            {
                btn_eliminar.Enabled = false;
                btn_atualizar.Enabled = false;

                string sql = "Select id as ID, user_name as Nome, '****' as Pass, data_inscricao as 'Data de Inscrição', acesso as 'Nível de Acesso' from t_users";

                using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["minhaConnectionApp"].ConnectionString))
                using (MySqlCommand cmd = new MySqlCommand(sql, con))
                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    con.Open();
                    da.Fill(dt);
                    grid_data.DataSource = dt;
                }
                grid_data.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
        }

        private void txt_gest_nome_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txt_gest_nome.Text)
                && !string.IsNullOrWhiteSpace(txt_gest_pass.Text)
                && !string.IsNullOrWhiteSpace(cb_gest_acesso.Text))
            {
                btn_inserir.Enabled = true;
            }
        }

        private void txt_gest_pass_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txt_gest_nome.Text)
                && !string.IsNullOrWhiteSpace(txt_gest_pass.Text)
                && !string.IsNullOrWhiteSpace(cb_gest_acesso.Text))
            {
                btn_inserir.Enabled = true;
            }
        }

        private void cb_gest_acesso_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txt_gest_nome.Text)
                && !string.IsNullOrWhiteSpace(txt_gest_pass.Text)
                && !string.IsNullOrWhiteSpace(cb_gest_acesso.Text))
            {
                btn_inserir.Enabled = true;
            }
        }

        private void btn_inserir_Click(object sender, EventArgs e)
        {
            try
            {
                string user;
                if (txt_gest_nome.Text.Length != 0)
                    user = txt_gest_nome.Text;
                else
                {
                    MessageBox.Show("Insira valores válidos", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt_gest_nome.Clear();
                    txt_gest_nome.Clear();
                    return;
                }
                string pass;
                if (txt_gest_pass.Text.Length != 0)
                    pass = txt_gest_pass.Text;
                else
                {
                    MessageBox.Show("Insira valores válidos", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    txt_gest_nome.Clear();
                    txt_gest_nome.Clear();
                    return;
                }

                string query = "insert into t_users (user_name, user_pass, acesso) values (@nome, @pass, 'Guest')";

                using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["minhaConnectionApp"].ConnectionString))
                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@nome", user);
                    cmd.Parameters.AddWithValue("@pass", pass);
                    con.Open();
                    string checkQuery = "SELECT COUNT(*) FROM t_users WHERE user_name = @nome";
                    using (MySqlCommand checkCmd = new MySqlCommand(checkQuery, con))
                    {
                        checkCmd.Parameters.AddWithValue("@nome", user);
                        int userExists = Convert.ToInt32(checkCmd.ExecuteScalar());

                        if (userExists > 0)
                        {
                            MessageBox.Show("Este nome de utilizador já existe!", "Erro", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                            return;
                        }
                    }

                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MessageBox.Show("Utilizador adicionado à base de dados!", "Registo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Erro ao inserir");

                    }
                }

                string sql = "Select id as ID, user_name as Nome, '****' as Pass, data_inscricao as 'Data de Inscrição', acesso as 'Nível de Acesso' from t_users";

                using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["minhaConnectionApp"].ConnectionString))
                using (MySqlCommand cmd = new MySqlCommand(sql, con))
                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    con.Open();
                    da.Fill(dt);
                    grid_data.DataSource = dt;
                }
                grid_data.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_atualizar_Click(object sender, EventArgs e)
        {
            string nome = string.IsNullOrWhiteSpace(txt_gest_nome.Text) ? null : txt_gest_nome.Text;
            string pass = string.IsNullOrWhiteSpace(txt_gest_pass.Text) ? null : txt_gest_pass.Text;
            string nivel = string.IsNullOrWhiteSpace(cb_gest_acesso.Text) ? null : cb_gest_acesso.Text;

            string query = @"UPDATE t_users SET 
                 user_name = COALESCE(@nome, user_name), 
                 user_pass = COALESCE(@pass, user_pass), 
                 acesso = COALESCE(@acesso, acesso) 
                 WHERE id = @id";

            using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["minhaConnectionApp"].ConnectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, con))
            {
                // O AddWithValue com null às vezes falha no MySQL, por isso usamos esta forma mais segura:
                cmd.Parameters.Add("@nome", MySqlDbType.VarChar).Value = (object)nome ?? DBNull.Value;
                cmd.Parameters.Add("@pass", MySqlDbType.VarChar).Value = (object)pass ?? DBNull.Value;
                cmd.Parameters.Add("@acesso", MySqlDbType.VarChar).Value = (object)nivel ?? DBNull.Value;
                cmd.Parameters.AddWithValue("@id", id_selecionado);

                con.Open();
                cmd.ExecuteNonQuery();
                MessageBox.Show("Utilizador atualizado com sucesso!");
            }

            id_selecionado = Convert.ToInt32(txt_gest_id.Text);
            query = "Select id as ID, user_name as Nome, '****' as Pass, data_inscricao as 'Data de Inscrição', acesso as 'Nível de Acesso' " +
                           "from t_users where id = @id_selecionado";

            using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["minhaConnectionApp"].ConnectionString))
            using (MySqlCommand cmd = new MySqlCommand(query, con))
            using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
            {
                DataTable dt = new DataTable();
                con.Open();
                cmd.Parameters.AddWithValue("@id_selecionado", id_selecionado);
                da.Fill(dt);
                grid_data.DataSource = dt;
            }
            grid_data.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
        }

        private void btn_reset_Click(object sender, EventArgs e)
        {
            GerirForms.AbrirForm(this, new frm_main());
        }

        private void btn_eliminar_Click(object sender, EventArgs e)
        {
            try
            {
                string nivelSelecionado = grid_data.CurrentRow.Cells["Nível de Acesso"].Value.ToString();

                if (nivelSelecionado == "Admin")
                {
                    MessageBox.Show("Não é permitido eliminar um utilizador com nível de acesso Admin!",
                                    "Proteção de Sistema",
                                    MessageBoxButtons.OK,
                                    MessageBoxIcon.Stop);
                    return;
                }

                string query = "delete from t_users where id = @id_escolhido";

                using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["minhaConnectionApp"].ConnectionString))
                using (MySqlCommand cmd = new MySqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@id_escolhido", id_selecionado);
                    con.Open();


                    int result = cmd.ExecuteNonQuery();
                    if (result > 0)
                    {
                        MessageBox.Show("Utilizador removido da base de dados!", "Registo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        MessageBox.Show("Erro ao eliminar");
                    }
                }

                string sql = "Select id as ID, user_name as Nome, '****' as Pass, data_inscricao as 'Data de Inscrição', acesso as 'Nível de Acesso' from t_users";

                using (MySqlConnection con = new MySqlConnection(ConfigurationManager.ConnectionStrings["minhaConnectionApp"].ConnectionString))
                using (MySqlCommand cmd = new MySqlCommand(sql, con))
                using (MySqlDataAdapter da = new MySqlDataAdapter(cmd))
                {
                    DataTable dt = new DataTable();
                    con.Open();
                    da.Fill(dt);
                    grid_data.DataSource = dt;
                }
                grid_data.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ocorreu um erro: " + ex.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btn_rever_Click(object sender, EventArgs e)
        {
            // 1. Verificação de segurança (caso o ID na sessão seja inválido ou zero)
            if (Sessao.id_revisao <= 0)
            {
                MessageBox.Show("Por favor, selecione um teste na lista antes de continuar.", "Aviso");
                return;
            }

            try
            {
                // Obtemos o tema diretamente da linha atual (como auxílio visual)
                string temaEscolhido = grid_historico.CurrentRow?.Cells["tema"].Value?.ToString() ?? "Revisão";

                List<Pergunta> perguntasRevisao = new List<Pergunta>();
                string connString = ConfigurationManager.ConnectionStrings["minhaConnectionApp"].ConnectionString;

                using (MySqlConnection con = new MySqlConnection(connString))
                {
                    con.Open();

                    // Query otimizada usando p.pergunta
                    string sql = @"
                            SELECT 
                                p.id AS pergId, 
                                p.pergunta AS pergTexto, 
                                p.explicacao,
                                r.resposta AS respTexto, 
                                r.correta AS respCorreta,
                                rt.id_resposta AS indiceEscolhido
                            FROM t_resultados_testes rt
                            INNER JOIN t_perguntas p ON rt.id_pergunta = p.id
                            INNER JOIN t_respostas r ON p.id = r.id_pergunta
                            WHERE rt.id_resultado = @idH
                            ORDER BY rt.id ASC, r.id ASC";

                    using (MySqlCommand cmd = new MySqlCommand(sql, con))
                    {
                        // Usamos o ID que o teu evento SelectionChanged guardou na Sessão
                        cmd.Parameters.AddWithValue("@idH", Sessao.id_revisao);

                        using (MySqlDataReader dr = cmd.ExecuteReader())
                        {
                            Pergunta p = null;
                            int ultimoIdPerg = -1;

                            while (dr.Read())
                            {
                                int idAtual = Convert.ToInt32(dr["pergId"]);

                                if (idAtual != ultimoIdPerg)
                                {
                                    p = new Pergunta();
                                    p.id = idAtual;
                                    p.texto = dr["pergTexto"].ToString();

                                    p.explicacao = dr["explicacao"] == DBNull.Value
                                        ? "Sem explicação disponível."
                                        : dr["explicacao"].ToString();

                                    p.resposta_escolhida = dr["indiceEscolhido"] == DBNull.Value
                                        ? -1
                                        : Convert.ToInt32(dr["indiceEscolhido"]);

                                    perguntasRevisao.Add(p);
                                    ultimoIdPerg = idAtual;
                                }

                                p.respostas.Add(new Resposta
                                {
                                    texto = dr["respTexto"].ToString(),
                                    correta = dr["respCorreta"] != DBNull.Value && Convert.ToBoolean(dr["respCorreta"])
                                });
                            }
                        }
                    }
                }

                if (perguntasRevisao.Count > 0)
                {
                    // Chamada do formulário passando a lista e o tema
                    frm_revisao form_revisao = new frm_revisao(perguntasRevisao, temaEscolhido);
                    form_revisao.ShowDialog();
                }
                else
                {
                    MessageBox.Show("Não foram encontrados dados para este teste.");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Erro técnico ao carregar: {ex.Message}");
            }
        }

        private void grid_historico_SelectionChanged(object sender, EventArgs e)
        {
            if (grid_historico.CurrentRow != null)
            {
                var cellValue = grid_historico.CurrentRow.Cells["id"].Value;
                if (cellValue != null && cellValue != DBNull.Value)
                {
                    Sessao.id_revisao = Convert.ToInt32(cellValue);
                }
            }
        }
    }
}
