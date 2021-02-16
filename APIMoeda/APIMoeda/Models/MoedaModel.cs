using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIMoeda.Models
{
    public class MoedaModel
    {
        public MoedaModel()
        {

        }
        public MoedaModel(string TipoMoeda, string data_inicio,string data_fim)
        {
            this.TipoMoeda = TipoMoeda;
            this.data_inicio = data_inicio;
            this.data_fim = data_fim;
        }
        public string TipoMoeda { get; set; }

        public string data_inicio { get; set; }

        public string data_fim { get; set; }
    }
}