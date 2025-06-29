using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DogAPIFact
{

    public partial class Form1 : Form
    {
        private Button btnCargar;
        private ListBox lstFacts;
        public Form1()
        {
            InitializeComponent();
            Text = "Dog Facts App";
            //Width = 400;
            //Height = 300;
            MaximizeBox = false;
            FormBorderStyle = FormBorderStyle.FixedSingle;

            btnCargar = new Button() { Text = "Cargar Facts", Dock = DockStyle.Top, Height = 40 };
            lstFacts = new ListBox() { Dock = DockStyle.Fill };

            btnCargar.Click += async (sender, e) => await CargarFacts();

            Controls.Add(listBox1);
            Controls.Add(btnCargar);
        }

        private async Task CargarFacts()
        {
            listBox1.Items.Clear();
            var facts = await DogFactsBLL.ObtenerDogFactsAsync();
            foreach (var fact in facts)
            {
                listBox1.Items.Add(fact.Fact);
            }
        }
    }
}
