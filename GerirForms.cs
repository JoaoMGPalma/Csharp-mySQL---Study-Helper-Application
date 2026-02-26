using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Joao_Palma_10790
{
    
    internal class GerirForms
    {
        public static void AbrirForm(Form FormToHide, Form FormToOpen)
        {
            FormToHide.Hide();
            FormToOpen.ShowDialog();
            FormToHide.Close();
        }
    }
}
