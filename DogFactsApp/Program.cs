using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace DogFactsApp
{
    // Capa de Entidades (Models)
    public class DogFact
    {
        public string Fact { get; set; }
    }

    // Capa de Datos (DAL)
    public static class DogFactsDAL
    {
        private static readonly HttpClient client = new HttpClient();

        public static async Task<List<DogFact>> GetDogFactsAsync()
        {
            try
            {
                string url = "https://dog-api.kinduff.com/api/facts?number=5";
                var response = await client.GetStringAsync(url);
                var dogFactsResponse = JsonSerializer.Deserialize<DogFactsResponse>(response);
                return dogFactsResponse?.Facts ?? new List<DogFact>();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error al obtener datos: {ex.Message}");
                return new List<DogFact>();
            }
        }

        private class DogFactsResponse
        {
            public List<string> Facts { get; set; }
        }
    }

    // Capa de Negocios (BLL)
    public static class DogFactsBLL
    {
        public static async Task<List<DogFact>> ObtenerDogFactsAsync()
        {
            var facts = await DogFactsDAL.GetDogFactsAsync();
            return facts.ConvertAll(f => new DogFact { Fact = f });
        }
    }

    // Capa de Presentación (UI)
    public class MainForm : Form
    {
        private Button btnCargar;
        private ListBox lstFacts;

        public MainForm()
        {
            Text = "Dog Facts App";
            Width = 400;
            Height = 300;

            btnCargar = new Button() { Text = "Cargar Facts", Dock = DockStyle.Top, Height = 40 };
            lstFacts = new ListBox() { Dock = DockStyle.Fill };

            btnCargar.Click += async (sender, e) => await CargarFacts();

            Controls.Add(lstFacts);
            Controls.Add(btnCargar);
        }

        private async Task CargarFacts()
        {
            lstFacts.Items.Clear();
            var facts = await DogFactsBLL.ObtenerDogFactsAsync();
            foreach (var fact in facts)
            {
                lstFacts.Items.Add(fact.Fact);
            }
        }

        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new MainForm());
        }
    }
}