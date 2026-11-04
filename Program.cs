// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello c # Programmers!");
// Console.WriteLine("Hello c # Programmers!");
// Console.Write("First");
// Console.Write("Second");
// Console.WriteLine("Hello c # Programmers!");
// Console.Write("Third");

// in c # we dont have functions we called them methods. string[] -> array of strings, it's not a list. It's multiple strings.

// Console.WriteLine(args[0]);

using System;
using System.Data;
using System.Text.RegularExpressions;
using Dapper;
using HelloWorld.Data;
using HelloWorld.Models;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;



namespace HelloWorld
{

    internal class Program
    {
    
        static void Main(string[] args)
        {
            // string connectionString = "Server=localhost;Database=DotNetCourseDatabase;TrustServerCertificate=True;Trusted_Connection=True;";
            // IDbConnection dbConnection = new SqlConnection(connectionString);
            // twra poy exw ftiaksei to Dapper.cs anti na exw auto to block twn 2 grammwn:
            //ftiaxnw ena instnace ths klashs moy

            IConfiguration config = new ConfigurationBuilder()
            .AddJsonFile("appsettings.json")
            .Build();

            DataContextDapper dapper = new DataContextDapper(config);
            DataContextDapperEF entityFramework = new DataContextDapperEF(config);


            // string sqlCommand = "SELECT GETDATE()";

            // prin ftiaksw dapper:  DateTime rightnow = dbConnection.QuerySingle<DateTime>("SELECT GETDATE()"); // we use "QuerySingle" a dapper to run a query.
            DateTime rightnow = dapper.LoadDataSingle<DateTime>("SELECT GETDATE()");
            // Console.WriteLine(rightnow);

            // create a query: that is going to insert a row based around our computer object

            Computer myComputer = new Computer() // OUR FIRST INSTANCE OF our model ( comptuer class)
            {
                Motherboard = "Z690",

                HasWifi = true,
                HasLTE = false,
                ReleaseDate = DateTime.Now,
                Price = 943.87m,
                VideoCard = "RTX 2060"

            };

            entityFramework.Add(myComputer);
            entityFramework.SaveChanges();


            string sql = @"INSERT INTO TutorialAppSchema.Computer (   
                Motherboard,
                HasWifi,
                HasLTE, 
                ReleaseDate, 
                Price, 
                VideoCard
            ) VALUES ('" + myComputer.Motherboard + "','" + myComputer.HasWifi + "','" + myComputer.HasLTE + "','" + myComputer.ReleaseDate + "','" + myComputer.Price + "','" + myComputer.VideoCard + "')";
            // so all we have to do now is pass this query, this sql statement into a dapper.
            //   Console.WriteLine(sql);

            //  int result = dbConnection.Execute(sql); // the return value of this is going to be the number of rows that were affected by this query. So if we insert one row, then the return value is going to be one. 
            bool result = dapper.ExecuteSql(sql);       // our result is just whether or not successfull                           // Console.WriteLine(result);
                                                        //or
                                                        // int result = dapper.ExecuteSqlWithRowCount(sql);

            string sqlSelect = @"SELECT
                Computer.ComputerId,
                Computer.Motherboard,
                Computer.HasWifi,
                Computer.HasLTE, 
                Computer.ReleaseDate, 
                Computer.Price, 
                Computer.VideoCard
            FROM TutorialAppSchema.Computer";

            // IEnumerable<Computer> computers = dbConnection.Query<Computer>(sqlSelect);
            IEnumerable<Computer> computers = dapper.LoadData<Computer>(sqlSelect);

            Console.WriteLine("' ComputerId','Motherboard','HasWifi', 'HasLTE', 'ReleaseDate' " + ", 'Price', 'VideoCard '");

            foreach (Computer singleComputer in computers)
            {
                Console.WriteLine("'" + singleComputer.ComputerId
                    + "','" + singleComputer.Motherboard
                    + "','" + singleComputer.HasWifi
                    + "','" + singleComputer.HasLTE
                    + "','" + singleComputer.ReleaseDate
                    + "','" + singleComputer.Price
                    + "','" + singleComputer.VideoCard
                    + "'");
            }

            IEnumerable<Computer>? computersEF = entityFramework.Computer?.ToList<Computer>(); // entityFramework.Computer : Θέλω να αποκτήσω πρόσβαση στα δεδομένα του entity Computer μέσω του Entity Framework»
                                                                                               // .ToList<Computer>() : einai ena LINQ extension method, to EF kalei to antistoixo SQL query kai metatrepei ta object se mia lista antikeimenwn

            if (computersEF != null)
            {
                Console.WriteLine("' ComputerId','Motherboard','HasWifi', 'HasLTE', 'ReleaseDate' " + ", 'Price', 'VideoCard '");

                foreach (Computer singleComputer in computersEF)
                {
                    Console.WriteLine("'" + singleComputer.ComputerId
                        + "','" + singleComputer.Motherboard
                        + "','" + singleComputer.HasWifi
                        + "','" + singleComputer.HasLTE
                        + "','" + singleComputer.ReleaseDate
                        + "','" + singleComputer.Price
                        + "','" + singleComputer.VideoCard
                        + "'");
                }
            }

            // Console.WriteLine(myComputer.Motherboard);
            // Console.WriteLine(myComputer.HasWifi);
            // Console.WriteLine(myComputer.HasLTE);
            // Console.WriteLine(myComputer.ReleaseDate);
            // Console.WriteLine(myComputer.Price);
            // Console.WriteLine(myComputer.VideoCard);

        }
    }
}