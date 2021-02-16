using APIMoeda.Models;
using APIMoeda.Repositorio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace APIMoeda.Controllers
{
    public class MoedaController : Controller
    {
        static MoedaRepositorio Moedas = new MoedaRepositorio();
        // GET: Moeda
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]

        public ActionResult GetItemFila()
        {
            try
            {
                Moeda retorno = Moedas.GetItemFila();
                MoedaModel newRetorno = new MoedaModel(retorno.TipoMoeda,retorno.data_inicio.Date.ToString("yyyy-MM-dd"), retorno.data_fim.Date.ToString("yyyy-MM-dd"));
                //Json()
                //java serializer = new JavaScriptSerializer();
                //response.Write(serializer.Serialize(Data));
                return Json(newRetorno, JsonRequestBehavior.AllowGet);
            }
            catch (Exception ex)
            {

                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }      
        }
        public ActionResult AddItemFila()
        {
            return View();
        }
        [HttpPost]
        public ActionResult AddItemFila(Moeda model)
        {
            try
            {
                if (model.TipoMoeda == "" || model.data_inicio == null || model.data_fim == null)
                {
                    return Json("Não é possivel adicionar uma moeda vazia");
                }
                Moedas.AddItemFila(model);
                return Json("Moeda adicionada com Sucesso.");
            }
            catch (Exception ex)
            {

                return Json(ex.Message, JsonRequestBehavior.AllowGet);
            }           
        }
    }
}
