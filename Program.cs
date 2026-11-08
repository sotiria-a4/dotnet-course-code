using System;
using System.Data;
using System.Text.Json;
using System.Text.RegularExpressions;
using AutoMapper;
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

            string computersJson = File.ReadAllText("ComputersSnake.json");
            // ftiaxnw tou kanones gia to AutoMapper gia na metatrepsei ta onomata twn properties apo snake_case se PascalCase
            Mapper mapper = new Mapper(new MapperConfiguration((cfg) =>
            {
                cfg.CreateMap<ComputerSnake, Computer>() // source: ComptuerSnake, destination: Computer
                .ForMember(destination => destination.ComputerId, options => // anonymous function: to destination.ComputerId pairnei tin timi tou source.computer_id
                    options.MapFrom(source => source.computer_id))
                .ForMember(destination => destination.HasWifi, options =>
                    options.MapFrom(source => source.has_wifi))
                .ForMember(destination => destination.HasLTE, options =>
                    options.MapFrom(source => source.has_lte))
                .ForMember(destination => destination.ReleaseDate, options =>
                    options.MapFrom(source => source.release_date))
                .ForMember(destination => destination.VideoCard, options =>
                    options.MapFrom(source => source.video_card))
                .ForMember(destination => destination.CPUCores, options =>
                    options.MapFrom(source => source.cpu_cores))
                .ForMember(destination => destination.Motherboard, options =>
                    options.MapFrom(source => source.motherboard))
                .ForMember(destination => destination.Price, options =>
                    options.MapFrom(source => source.price));
                //  options.MapFrom(source => source.price * .8m));   in the long run is not just mapping names but also converting the values 

            }));

            IEnumerable<ComputerSnake>? computersSystem = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<ComputerSnake>>(computersJson);

            if (computersSystem != null)
            {
                // edw ousiastika ginetai to mapping apo snake_case se PascalCase me to AutoMapper
                IEnumerable<Computer> computerResult = mapper.Map<IEnumerable<Computer>>(computersSystem);
                Console.WriteLine("Automapper Count:" + computerResult.Count());
                // foreach (Computer computer in computerResult)
                // {
                //     Console.WriteLine(computer.Motherboard);
                //     //so if this mapping succeeded, then our motherboard value will be the same as our motherboard value from our ComputerSnake case.
                // }

            }
            // alternative way 
            IEnumerable<Computer>? computersSystemJsonPropertyMapping = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Computer>>(computersJson);
            if (computersSystemJsonPropertyMapping != null)
            {
                Console.WriteLine("JSON Property: " + computersSystemJsonPropertyMapping.Count());
                // foreach (Computer computer in computersSystemJsonPropertyMapping)
                // {
                //     Console.WriteLine(computer.Motherboard);
                // }
            }


            //File.ReadAllText("log.txt");
            // Console.WriteLine(computersJson);

            // JsonSerializerOptions options = new JsonSerializerOptions()
            // {
            //     PropertyNamingPolicy = JsonNamingPolicy.CamelCase //ta onomata twn C# properties metatrepontai se camelCase otan sugkrinonte me to JSON
            // };

            // IEnumerable<Computer>? computersNewtonsoft = JsonConvert.DeserializeObject<IEnumerable<Computer>>(computersJson);
            // IEnumerable<Computer>? computersSystem = System.Text.Json.JsonSerializer.Deserialize<IEnumerable<Computer>>(computersJson, options);

            // if (computersNewtonsoft != null)
            // {
            //     foreach (Computer computer in computersNewtonsoft)
            //     {
            //         string sql = @"INSERT INTO TutorialAppSchema.Computer (   
            //             Motherboard,
            //             HasWifi,
            //             HasLTE, 
            //             ReleaseDate, 
            //             Price, 
            //             VideoCard
            //         ) VALUES ('" + EscapeSingleQuote(computer.Motherboard)
            //                  + "','" + computer.HasWifi
            //                  + "','" + computer.HasLTE
            //                  + "','" + computer.ReleaseDate
            //                  + "','" + computer.Price
            //                  + "','" + EscapeSingleQuote(computer.VideoCard)
            //                  + "')";

            //         dapper.ExecuteSql(sql);

            //         //Console.WriteLine(computer.Motherboard);
            //         //Console.WriteLine(computer.ComputerId);
            //         //Console.WriteLine(computer.HasWifi);
            //     }
            // }

            // JsonSerializerSettings settings = new JsonSerializerSettings()
            // {
            //     ContractResolver = new CamelCasePropertyNamesContractResolver()
            // };

            // // Serialize: h lista apo Computer ginetai pali string
            // string computersCopyNewtonsoft = JsonConvert.SerializeObject(computersNewtonsoft, settings); // that's similar to the string we deserialized "computers" 
            // File.WriteAllText("computersCopyNewtonsoft.txt", computersCopyNewtonsoft);

            // string computesCopySystem = System.Text.Json.JsonSerializer.Serialize(computersSystem, options); // that's similar to the string we deserialized "computers" 
            // File.WriteAllText("computersCopySystem.txt", computesCopySystem);

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