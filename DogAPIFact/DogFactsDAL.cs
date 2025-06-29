using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DogAPIFact
{
    // Capa de Datos (DAL)
    public static class DogFactsDAL
    {
        private static readonly HttpClient client = new HttpClient();
        public static async Task<List<DogFact>> GetDogFactsAsync()
        {
            using (HttpClient client = new HttpClient())
            {
                string url = "https://dog.ceo/api/breeds/image/random";

                try
                {
                    HttpResponseMessage response = await client.GetAsync(url);
                    response.EnsureSuccessStatusCode();

                    string json = await response.Content.ReadAsStringAsync();
                    DogResponse dog = JsonConvert.DeserializeObject<DogResponse>(json);

                    Console.WriteLine("Imagen de perro aleatorio:");
                    Console.WriteLine(dog.Message);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
            }
            /*try
            {
                string url = "https://dogapi.dog/api/v2/facts";
                var response = await client.GetStringAsync(url);

                // Imprime la respuesta para verificar que llega bien
                Console.WriteLine($"Response: {response}");

                // Deserializa la respuesta JSON a un objeto DogFactsResponse
                var dogFactsResponse = JsonSerializer.Deserialize<DogFactsResponse>(response);

                // Verifica que la respuesta no sea null
                if (dogFactsResponse != null && dogFactsResponse.Facts != null)
                {
                    return dogFactsResponse.Facts.ConvertAll(f => new DogFact { Fact = f });
                }
                else
                {
                    MessageBox.Show("No se pudieron cargar los datos de la API.");
                    return new List<DogFact>();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener datos: {ex.Message}");
                return new List<DogFact>();
            }*/
        }


        private class DogFactsResponse
        {
            public List<string> Facts { get; set; }
        }
    }
}