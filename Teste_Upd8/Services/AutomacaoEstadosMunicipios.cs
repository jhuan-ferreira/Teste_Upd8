using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace Teste_Upd8.Services
{
    public class AutomacaoEstadosMunicipios
    {
        public List<string> BuscarMunicipios()
        {

            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri($"https://servicodados.ibge.gov.br/api/v1/localidades/distritos");

            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("").Result;

            List<string> Municipios = new List<string>();

            if (response.IsSuccessStatusCode)
            {
                var dataObjectJson = response.Content.ReadAsStringAsync();
                var jsonObjectSerialized = JsonConvert.SerializeObject(dataObjectJson.Result);
                var objectDeserialized = JsonConvert.DeserializeObject<string>(jsonObjectSerialized).Replace("[", "").Replace("]", "").Split(',');



                foreach (var obj in objectDeserialized)
                {

                    if (obj.Contains("nome"))
                    {
                        Municipios.Add(obj.ToString().Replace("{", "").Replace("nome", "").Replace(":", "").Replace('"', '*').Replace("*", null).Replace("}", "")) ;
                    }


                }

            }

            return Municipios;


        }



        public List<string> BuscarEstados()
        {

            HttpClient client = new HttpClient();

            client.BaseAddress = new Uri($"https://brasilapi.com.br/api/ibge/uf/v1");

            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            HttpResponseMessage response = client.GetAsync("").Result;

            List<string> Estados = new List<string>();

            if (response.IsSuccessStatusCode)
            {
                var dataObjectJson = response.Content.ReadAsStringAsync();
                var jsonObjectSerialized = JsonConvert.SerializeObject(dataObjectJson.Result);
                var objectDeserialized = JsonConvert.DeserializeObject<string>(jsonObjectSerialized).Replace("[", "").Replace("]", "").Split(',');


                foreach (var obj in objectDeserialized)
                {


                    if (obj.Contains("sigla") && !obj.Contains("CO") && !obj.Contains("NE") && !obj.Contains("SE"))
                    {
                        var dadosEstado = obj.ToString().Replace("{", "").Replace("sigla", "").Replace(":", "").Replace('"', '*').Replace("*", null);

                        if (dadosEstado.Length == 2)
                        {
                            Estados.Add(dadosEstado);
                        }

                    }


                }

            }
            Estados.Add("SE");
            return Estados;
        }
    }
}
