using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIMoeda.Models
{
    public class Moeda
    {
        public string TipoMoeda { get; set; }

        public DateTime data_inicio { get; set; }

        public DateTime data_fim { get; set; }
    }
}