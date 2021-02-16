using APIMoeda.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace APIMoeda.Repositorio
{
    public class MoedaRepositorio
    {
        private static List<Moeda> Moedas = new List<Moeda>();
        public void AddItemFila(Moeda moeda) {
            try
            {
                Moedas.Add(moeda);
            }
            catch (Exception)
            {

                throw new Exception("Não foi possivel adicionar Moeda.");
            }          
        }
        public Moeda GetItemFila()
        {
            if (Moedas.Count > 0)
            {
                Moeda retorno = Moedas.Last();
                Moedas.RemoveAt(Moedas.Count - 1);
                return retorno;
            }
            else
            {
                throw new Exception("Fila Vazia");
            }
           
        }
    }
}