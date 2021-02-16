using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Diagnostics;

namespace RotinaMoeda
{
    class Program
    {
        static Stopwatch relogio = new Stopwatch();
        static void Main(string[] args)
        {
            while (true)
            {
                relogio.Start();
                ConsumirAPI();
                System.Threading.Thread.Sleep(new TimeSpan(0, 2, 0));
            }
        }
        static void ConsumirAPI()
        {
            try
            {
                while (true)
                {
                    List<Moeda> moedasMatch = new List<Moeda>() { };
                    List<Moeda> TodasMoedasPlanilha = new List<Moeda>() { };
                    List<MoedaModel> TodasMoedasCotacaoPlanilha = new List<MoedaModel>() { };
                    int codCotacao = 0;
                    using (var client = new HttpClient())
                    {
                        client.BaseAddress = new System.Uri("https://localhost:44309/");
                        client.DefaultRequestHeaders.Accept.Clear();
                        client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                        HttpResponseMessage response = client.GetAsync("moeda/GetItemFila").Result;
                        if (response.IsSuccessStatusCode)
                        {  //GET
                            var moeda = response.Content.ReadAsStringAsync().Result;
                            if (moeda.Contains("Fila Vazia"))
                            {
                                Console.WriteLine("Fila vazia " + DateTime.Now);
                                //Para processamento.
                                return;
                            }
                            else
                            {
                                Moeda moedaObj = JsonConvert.DeserializeObject<Moeda>(moeda);
                                TodasMoedasPlanilha = retornaTodasMoedasPlanilha(moedaObj);
                                TodasMoedasPlanilha = TodasMoedasPlanilha.OrderBy(ord => ord.data_inicio).ToList();
                                codCotacao = retornaCodCotacao(moedaObj);
                                if (codCotacao == 0)
                                {
                                    Console.WriteLine("Não existe esse codigo de cotação.");
                                }
                                TodasMoedasCotacaoPlanilha = retornaTodasMoedasCotacaoPlanilha(codCotacao, moedaObj);
                                TodasMoedasCotacaoPlanilha = TodasMoedasCotacaoPlanilha.OrderBy(ord => ord.data_inicio).ToList();

                                salvarPlanilha(TodasMoedasCotacaoPlanilha);
                                relogio.Stop();
                                Console.WriteLine(DateTime.Now + " - Processado! - " + "Tempo de processamento: " + relogio.Elapsed);

                            }

                        }
                    }
                }
            }
            catch (Exception ex)
            {

                Console.WriteLine(ex.Message);
                Console.WriteLine(ex.InnerException);
            }
        }

        static List<Moeda> retornaTodasMoedasPlanilha(Moeda moeda)
        {
            List<Moeda> retorno = new List<Moeda>() { };
            using (var reader = new StreamReader(Environment.CurrentDirectory + "\\DadosMoeda.csv"))
            {
                string MoedaTipo;
                string[] datas;
                DateTime data;
                //pulamos a primeira linha do cabeçalho
                reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    MoedaTipo = values[0];
                    datas = values[1].Split('-');
                    data = new DateTime(Convert.ToInt32(datas[0]), Convert.ToInt32(datas[1]), Convert.ToInt32(datas[2]));

                    if (MoedaTipo.ToUpper() == moeda.TipoMoeda.ToUpper())
                    {

                        if (data >= moeda.data_inicio && data <= moeda.data_fim)
                        {
                            retorno.Add(new Moeda(MoedaTipo, data, data) { });
                        }
                    }
                }
            }
            return retorno;
        }
        static int retornaCodCotacao(Moeda moeda)
        {
            int retorno = 0;
            using (var reader = new StreamReader(Environment.CurrentDirectory + "\\CodigoCotacao.csv"))
            {
                string ID;
                string cod_cotacao;

                //pulamos a primeira linha do cabeçalho
                reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    ID = values[0];
                    cod_cotacao = values[1];

                    if (ID.ToUpper() == moeda.TipoMoeda.ToUpper())
                    {
                        return Convert.ToInt32(cod_cotacao);
                    }
                }
            }
            return retorno;
        }

        static List<MoedaModel> retornaTodasMoedasCotacaoPlanilha(int codCotacao, Moeda moeda)
        {
            List<MoedaModel> retorno = new List<MoedaModel>() { };
            using (var reader = new StreamReader(Environment.CurrentDirectory + "\\DadosCotacao.csv"))
            {
                string vlr_cotacao;
                string cod_cotacao;
                string dat_cotacao;
                string[] datas;
                DateTime data;
                //pulamos a primeira linha do cabeçalho
                reader.ReadLine();
                while (!reader.EndOfStream)
                {
                    var line = reader.ReadLine();
                    var values = line.Split(';');

                    vlr_cotacao = values[0];
                    cod_cotacao = values[1];
                    dat_cotacao = values[2];
                    datas = values[2].Split('/');
                    data = new DateTime(Convert.ToInt32(datas[2]), Convert.ToInt32(datas[1]), Convert.ToInt32(datas[0]));

                    if (Convert.ToInt32(cod_cotacao) == codCotacao)
                    {
                        if (data >= moeda.data_inicio && data <= moeda.data_fim)
                        {
                            retorno.Add(new MoedaModel(moeda.TipoMoeda, data, data, vlr_cotacao) { });
                        }

                    }
                }
            }
            return retorno;
        }

        static void salvarPlanilha(List<MoedaModel> moeda)
        {
            try
            {
                string filePath = Environment.CurrentDirectory + "\\Resultado_" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".csv";
                StringBuilder csv = new StringBuilder();
                csv.AppendLine("ID_MOEDA" + ";" + "DATA_REF" + ";" + "VL_COTACAO");
                foreach (var item in moeda)
                {
                    csv.AppendLine(item.TipoMoeda.ToUpper() + ";" + item.data_inicio.Day + "/" + item.data_inicio.Month + "/" + item.data_inicio.Year + ";" + item.vlr_cotacao);
                }
                File.WriteAllText(filePath, csv.ToString());
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
    }
}
