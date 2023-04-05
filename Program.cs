using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SqlConsoleClient
{
    internal class Program
    {
        static SqlConnection con = null;
        // /connect BEST-KOMP\SQLEXPRESS VegetablesAndFruits
        static private void ConnectToDb(string connectionString, string catalog)
        {
            try
            {
                con = new SqlConnection(@"Data Source=" + $"{connectionString};" + $"Initial Catalog={catalog}; Integrated Security=SSPI;");
                con.Open();
                Console.WriteLine("[DataBaseController]=> Connect is success!");
            }
            catch(Exception ex){
                con = null;
                Console.WriteLine("[DataBaseConstroller]=> Connection isnt possible: \n" +
                    $"Exseption Message: {ex.Message}");
            }
        }
        static void Main(string[] args)
        {
            string command = null;
            string[] commandsList = new string[] { "/connect", "/exit" , "/print_all"};
            Console.WriteLine("Using this command:\n" +
                "/connect 'database fullname' 'catalog' => to connect database\n" +
                "/exit => to Exit");
            while(true)
            {
                Console.Write("[UserInput]=> ");
                command = Console.ReadLine();

                if(commandsList.Contains(command.Split(' ')[0]))
                {
                    if (command.Contains("/connect") && command.Split(' ')[1] != null)
                    {
                        if (command.Split(' ').Length == 3)
                        {
                            ConnectToDb(command.Split(' ')[1], command.Split(' ')[2]);
                        }
                        else
                        {
                            Console.WriteLine("[CommandHelper]=> Ur command have a no 2 arguments :(");
                        }
                    }
                    else if (command.Contains("/exit")) { break; }
                    if (con != null) {
                        if (command.Contains("/print_all")) {
                            SqlCommand cmd = new SqlCommand(@"SELECT * FROM VegetablesAndFruits_t", con);
                            SqlDataReader reader = cmd.ExecuteReader();
                            StringBuilder sb = new StringBuilder();
                            for (int i = 0; i < reader.FieldCount; i++)
                            {
                                sb.Append($"{reader.GetName(i), 12}");
                            }
                            sb.Append("\n");
                            while (reader.Read())
                            {
                                for (int i = 0; i < reader.FieldCount; i++)
                                {
                                    sb.Append($"{reader[i], 12}");
                                }
                                sb.Append('\n');
                            }
                            Console.WriteLine($"[DataBaseController]=>\n{sb.ToString()}");
                        }
                    }
                    else
                    {
                        Console.WriteLine($"[DataBaseController]=> Connection ur DataBase!");
                    }
                }
                else
                {
                    Console.WriteLine("[CommandHelper]=> Ur command is not found :(");
                }
            }
        }
    }
}
