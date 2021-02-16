using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotinaMoeda
{
    class Moeda
    {
        public Moeda()
        {

        }
        public Moeda(string TipoMoeda, DateTime data_inicio, DateTime data_fim)
        {
            this.TipoMoeda = TipoMoeda;
            this.data_inicio = data_inicio;
            this.data_fim = data_fim;
        }
        public string TipoMoeda { get; set; }

        public DateTime data_inicio { get; set; }

        public DateTime data_fim { get; set; }
    }
}
