using System;
using System.Data;
using System.Text.Json;
using System.Text.RegularExpressions;
using Dapper;
using HelloWorld.Data;
using HelloWorld.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace HelloWorld
{

    internal class Program
    {

        static void Main(string[] args)
        {
            IConfiguration config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();

            DataContextDapper dapper = new DataContextDapper(config);

            // string sql = @"INSERT INTO TutorialAppSchema.Computer (   
            //     Motherboard,
            //     HasWifi,
            //     HasLTE, 
            //     ReleaseDate, 
            //     Price, 
            //     VideoCard
            // ) VALUES ('" + myComputer.Motherboard 
            //          + "','" + myComputer.HasWifi 
            //          + "','" + myComputer.HasLTE 
            //          + "','" + myComputer.ReleaseDate 
            //          + "','" + myComputer.Price 
            //          + "','" + myComputer.VideoCard 
            //          + "')";

            // File.WriteAllText("log.txt", sql);
            // using StreamWriter openFile = new("log.txt", append: true);
            // openFile.WriteLine("\n" + sql + "\n");
            // openFile.Close();

            string computersJson = File.ReadAllText("Computers.json");

            //File.ReadAllText("log.txt");
            // Console.WriteLine(computersJson);

            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase //ta onomata twn C# properties metatrepontai se camelCase otan sugkrinonte me to JSON
            };

            IEnumerable<Computer>? computersNewtonsoft = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Computer>>(computersJson, options);
            IEnumerable<Computer>? computersSystem = JsonConvert.DeserializeObject<IEnumerable<Computer>>(computersJson);    // Deserialize: to string computersJson ginetai lista apo Computer

            if (computersNewtonsoft != null)
            {
                foreach (Computer computer in computersNewtonsoft)
                {
                    string sql = @"INSERT INTO TutorialAppSchema.Computer (   
                        Motherboard,
                        HasWifi,
                        HasLTE, 
                        ReleaseDate, 
                        Price, 
                        VideoCard
                    ) VALUES ('" + EscapeSingleQuote(computer.Motherboard)
                             + "','" + computer.HasWifi
                             + "','" + computer.HasLTE
                             + "','" + computer.ReleaseDate
                             + "','" + computer.Price
                             + "','" + EscapeSingleQuote(computer.VideoCard)
                             + "')";

                    dapper.ExecuteSql(sql);

                    //Console.WriteLine(computer.Motherboard);
                    //Console.WriteLine(computer.ComputerId);
                    //Console.WriteLine(computer.HasWifi);
                }
            }

            JsonSerializerSettings settings = new JsonSerializerSettings()
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver()
            };

            // Serialize: h lista apo Computer ginetai pali string
            string computersCopyNewtonsoft = JsonConvert.SerializeObject(computersNewtonsoft, settings); // that's similar to the string we deserialized "computers" 
            File.WriteAllText("computersCopyNewtonsoft.txt", computersCopyNewtonsoft);

            string computesCopySystem = System.Text.Json.JsonSerializer.Serialize(computersSystem, options); // that's similar to the string we deserialized "computers" 
            File.WriteAllText("computersCopySystem.txt", computesCopySystem);

            // Serialize = antikeimena C# -> keimeno JSON.
            // By default kai oi dyo vivliothikes grafoun ta keys opws ta onomata twn C# properties (PascalCase).
            // Gia camelCase keys xreiazetai rythmisi se KATHE vivliothiki ksexwrista:
            //   Newtonsoft       -> JsonSerializerSettings + CamelCasePropertyNamesContractResolver
            //   System.Text.Json -> JsonSerializerOptions + JsonNamingPolicy.CamelCase
            // Sto Deserialize o kanonas orizei poio key PSAXNOUME, sto Serialize poio key GRAFOUME.
        }
        static string EscapeSingleQuote(string input)
        {
            string output = input.Replace("'", "''");
            return output;
        }

    }
}