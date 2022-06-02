using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Syphon
{
    /// <summary>
    /// Logique d'interaction pour MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            string[] classes = new string[]
            {
                "Neutral",
                "Mage",
                "Paladin",
                "Rogue",
                "Warlock",
                "Hunter",
                "Demon Hunter",
                "Shaman",
                "Priest",
                "Warrior",
                "Druid"
            };

            foreach (string c in classes)
                GetData(c);
        }

        public async void GetData(string chosenClass)
        {
            var client = new HttpClient();
            var request = new HttpRequestMessage
            {
                Method = HttpMethod.Get,
                RequestUri = new Uri("https://omgvamp-hearthstone-v1.p.rapidapi.com/cards/classes/" + chosenClass + "?collectible=1"),
                Headers =
                {
                    { "X-RapidAPI-Host", "omgvamp-hearthstone-v1.p.rapidapi.com" },
                    { "X-RapidAPI-Key", "52f7b10a6dmshfb3fda9417ec2e1p1dd1cdjsn0740a7db824f" },
                },
            };

            using (var response = await client.SendAsync(request))
            {
                response.EnsureSuccessStatusCode();
                var body = await response.Content.ReadAsStringAsync();
                File.WriteAllText(@"E:\Unity_Projects\data_" + chosenClass + ".txt", body);
            }
        }
    }
}
