using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using System.Text;
using Newtonsoft.Json;
using System.IO;
using System.Linq;

namespace Solution
{
    internal class Program
    {
        static void Main(string[] args)
        {
            var jsonText = File.ReadAllText("C:\\Users\\tsebr\\source\\repos\\TestSolution\\code1.json");
            StringBuilder jsonTextBuilder = new StringBuilder(jsonText);
            jsonTextBuilder.Replace("\"Клиент\": ", "");
            jsonTextBuilder.Insert(1, "\"Клиент\": [");
            jsonTextBuilder.Insert(jsonTextBuilder.Length - 2, "]");
            Root root = JsonConvert.DeserializeObject<Root>(jsonTextBuilder.ToString());

            var result = new List<Client>();
            var listClient = root.Clients;
            foreach (var client in listClient)
            {
                //Оставшееся время по договору должно быть более 2 часов.
                var сontracts = client.Contracts.Where(contract => contract.NumberHours > 2);

                foreach (var contract in сontracts)
                {
                    //Оставшееся время по участку "Системное администрирование" более 0,5 часа.
                    var OurСlient = contract.Plots
                        .Where(x => x.Name == "Системное администрирование")
                        .Where(plot => plot.NumberHours > 0.5);

                    if (OurСlient.Count() > 0)
                        result.Add(client);
                }
            }

            foreach (var one in result)
                Console.WriteLine("Имя Клиента - " + one.Name);

            Console.ReadKey();
        }
    }

    public class Root
    {
        [JsonProperty("Клиент")]
        public Client[] Clients { get; set; }
    }

    public class Contract
    {
        [JsonProperty("Ссылка")]
        public string Link { get; set; }
        [JsonProperty("Наименование")]
        public string Name { get; set; }
        [JsonProperty("Факт")]
        public double Fact { get; set; }
        [JsonProperty("Предъявлено")]
        public double Presented { get; set; }
        [JsonProperty("КоличествоЧасов")]
        public double NumberHours { get; set; }
        [JsonProperty("Участки")]
        public List<Plot> Plots { get; set; }
    }

    public class Client
    {
        [JsonProperty("Ссылка")]
        public string Link { get; set; }
        [JsonProperty("Наименование")]
        public string Name { get; set; }
        [JsonProperty("Факт")]
        public double Fact { get; set; }
        [JsonProperty("Предъявлено")]
        public double Presented { get; set; }
        [JsonProperty("КоличествоЧасов")]
        public double NumberHours { get; set; }
        [JsonProperty("ДоговорыКонтрагента")]
        public List<Contract> Contracts { get; set; }
    }

    public class Plot
    {
        [JsonProperty("Ссылка")]
        public string Link { get; set; }
        [JsonProperty("Наименование")]
        public string Name { get; set; }
        [JsonProperty("Факт")]
        public double Fact { get; set; }
        [JsonProperty("Предъявлено")]
        public double Presented { get; set; }
        [JsonProperty("КоличествоЧасов")]
        public double NumberHours { get; set; }
    }
}
