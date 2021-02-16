using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotinaMoeda
{
    class MoedaModel
    {
        public MoedaModel()
        {

        }
        public MoedaModel(string TipoMoeda, DateTime data_inicio, DateTime data_fim,string vlr_cotacao)
        {
            this.TipoMoeda = TipoMoeda;
            this.data_inicio = data_inicio;
            this.data_fim = data_fim;
            this.vlr_cotacao = vlr_cotacao;
        }
        public string TipoMoeda { get; set; }

        public DateTime data_inicio { get; set; }

        public DateTime data_fim { get; set; }
        public string vlr_cotacao { get; set; }
    }
}
