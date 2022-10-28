using System.Text;
using Newtonsoft.Json;
using ReversiMvcApp.Models;

namespace ReversiMvcApp.Data
{
    public class SpelApi
    {

        //string for ServerLink
        private string ServerLink = "https://localhost:7076/api/";

        // Get spellen from api
        public async Task<List<Spel>> GetSpellen()
        {
            using (HttpClient client = new HttpClient())
            {
                var response = client.GetAsync(ServerLink + "Spel/AlleSpellen").Result;
                string json = await response.Content.ReadAsStringAsync();
                List<Spel> spellen = JsonConvert.DeserializeObject<List<Spel>>(json);
                List<Spel> result = new List<Spel>();

                foreach (var spel in spellen)
                {
                    Spel newSpel = new Spel()
                    {
                        Omschrijving = spel.Omschrijving,
                        Speler1Token = spel.Speler1Token,
                        Speler2Token = spel.Speler2Token,
                        Token = spel.Token
                    };
                    result.Add(newSpel);
                }
                return result;
            }
        }

        // create spel in api
        public async Task<Spel> CreateSpel(Spel spel)
        {
            using (HttpClient client = new HttpClient())
            {
                string json = JsonConvert.SerializeObject(spel);
                StringContent content = new StringContent(json, Encoding.UTF8, "application/json");
                var response = client.PostAsync(ServerLink + "Spel/CreateGame", content).Result;
                string jsonResult = await response.Content.ReadAsStringAsync();
                Spel result = JsonConvert.DeserializeObject<Spel>(jsonResult);
                return result;
            }
        }
    }
}
