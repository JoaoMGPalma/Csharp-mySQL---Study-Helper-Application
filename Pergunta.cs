using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Joao_Palma_10790
{
    public class Pergunta
    {
        public int id { get; set; }
        public string texto { get; set; }
        public string explicacao { get; set; }
        public List<Resposta> respostas { get; set; } = new List<Resposta>();
        public int resposta_escolhida { get; set; } = -1;
    }

}
