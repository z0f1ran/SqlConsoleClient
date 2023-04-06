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
        // /connect MASHINAUBIZA\SQLEXPRESS VegetablesAndFruits
        static private void Print_All()
        {
            SqlCommand cmd = new SqlCommand(@"SELECT * FROM VAndF_t", con);
            SqlDataReader reader = cmd.ExecuteReader();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                sb.Append($"{reader.GetName(i),12}");
            }
            sb.Append("\n");
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    sb.Append($"{reader[i],12}");
                }
                sb.Append('\n');
            }
            reader.Close();
            Console.WriteLine($"[DataBaseController]=>\n{sb.ToString()}");
        }

        static private void Print_Names()
        {
            SqlCommand cmd = new SqlCommand(@"SELECT Name_p FROM VAndF_t", con);
            SqlDataReader reader = cmd.ExecuteReader();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                Console.WriteLine($"{reader.GetName(i),12}");
            }
            while (reader.Read())
            {
                string columnData = reader["Name_p"].ToString();
                Console.WriteLine($"{columnData,12}");
            }
            reader.Close();
        }
        static private void Print_Colors()
        {
            SqlCommand cmd = new SqlCommand(@"SELECT Color_p FROM VAndF_t", con);
            SqlDataReader reader = cmd.ExecuteReader();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                Console.WriteLine($"{reader.GetName(i),12}");
            }
            while (reader.Read())
            {
                string columnData = reader["Color_p"].ToString();
                Console.WriteLine($"{columnData, 12}");
            }
            reader.Close();
        }

        static private void Print_Max_Caloric()
        {
            SqlCommand cmd = new SqlCommand(@"SELECT * FROM VAndF_t ORDER BY Caloric_p", con);
            SqlDataReader reader = cmd.ExecuteReader();
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < reader.FieldCount; i++)
            {
                sb.Append($"{reader.GetName(i),12}");
            }
            sb.Append("\n");
            while (reader.Read())
            {
                for (int i = 0; i < reader.FieldCount; i++)
                {
                    sb.Append($"{reader[i],12}");
                }
                sb.Append('\n');
            }
            reader.Close();
        }

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
            string[] commandsList = new string[] { "/connect", "/exit" , "/print_all", "/print_names", "/print_colors", "/print_max_caloric"};
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
                        if (command.Contains("/print_all"))
                        {
                            Print_All();
                        }
                        else if (command.Contains("/print_names"))
                        {
                            Print_Names();
                        }
                        else if (command.Contains("/print_colors"))
                        {
                            Print_Colors();
                        }
                        else if (command.Contains("/print_max_caloric"))
                        {
                            Print_Max_Caloric();
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
