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
    public partial class frm_revisao : Form
    {
        Color cor_correta = Color.ForestGreen;
        Color cor_errada = Color.LightCoral;

        private List<Pergunta> perguntas_rever;
        private string tema_escolhido;

        public frm_revisao(List<Pergunta> perguntas, string tema)
        {
            InitializeComponent();
            this.perguntas_rever = perguntas;
            tema_escolhido = tema;
        }

        private void btn_fechar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_revisao_Load(object sender, EventArgs e)
        {
            string minhaConString = ConfigurationManager.ConnectionStrings["minhaConnectionApp"].ConnectionString;

            // Select the actual columns from the table
            string sql = @"SELECT id, tema, data_resultado 
               FROM t_historico_testes 
               WHERE id_user = @userID AND id = @testID";

            using (MySqlConnection conn = new MySqlConnection(minhaConString))
            {
                try
                {
                    conn.Open();
                    MySqlCommand cmd = new MySqlCommand(sql, conn);
                    cmd.Parameters.AddWithValue("@userID", Sessao.CurrentUserID);
                    cmd.Parameters.AddWithValue("@testID", Sessao.id_revisao); // The ID of the specific test you are reviewing

                    using (MySqlDataReader rdr = cmd.ExecuteReader())
                    {
                        if (rdr.Read())
                        {
                            // Populate your labels directly
                            lbl_txt_num.Text =  rdr["id"].ToString();
                            lbl_txt_tema.Text = rdr["tema"].ToString();
                            lbl_txt_data.Text = Convert.ToDateTime(rdr["data_resultado"]).ToString("dd/MM/yyyy HH:mm");
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Erro ao carregar cabeçalho: " + ex.Message);
                }
            }

            TextBox[] txt_p = { txt_p1, txt_p2, txt_p3, txt_p4, txt_p5, txt_p6, txt_p7, txt_p8, txt_p9, txt_p10 };
            TextBox[] txt_r = { txt_r1, txt_r2, txt_r3, txt_r4, txt_r5, txt_r6, txt_r7, txt_r8, txt_r9, txt_r10 };
            Panel[] panels = {panel1, panel2, panel3, panel4, panel5, panel6, panel7, panel8, panel9, panel10};
            PictureBox[] pb_infos = {pb_info1, pb_info2, pb_info3, pb_info4, pb_info5, pb_info6, pb_info7, pb_info8, pb_info9, pb_info10};

            int i = 0;
            foreach (var p in perguntas_rever)
            {
                if (i >= panels.Length) break;

                txt_p[i].Text = p.texto;
                pb_infos[i].Tag = p.explicacao;

                if (p.resposta_escolhida == -1)
                {
                    txt_r[i].Text = "Sem Resposta";
                    panels[i].BackColor = cor_errada;
                }
                else
                {
                    txt_r[i].Text = p.respostas[p.resposta_escolhida].texto;

                    panels[i].BackColor = (p.respostas[p.resposta_escolhida] != null && p.respostas[p.resposta_escolhida].correta) ? cor_correta : cor_errada;
                }

                i++;
            }
        }

        private void pb_info1_Click(object sender, EventArgs e)
        {
            PictureBox clickedBox = (PictureBox)sender;

            if (clickedBox.Tag != null)
            {
                string explicacao = clickedBox.Tag.ToString();
                MessageBox.Show(explicacao, "Explicação da Pergunta");
            }
        }
    }
}
