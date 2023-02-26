using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;
using System.IO;

namespace clinicalproject
{
    class Program
    {
        static void Main(string[] args)
        {

            login();

            Console.ReadKey();
        }
        public static void login()
        {
            for (int i = 0; i < 3; i++)
            {
                if ((i == 3) || (log() == 1))
                {
                    break;
                }
            }

        }
        public static int log()
        {
            string cs = "Data Source=DESKTOP-8OGPQNH;Initial catalog=project;User Id=sa;Password=athulya";
            SqlConnection con = new SqlConnection(cs);
            con.Open();

            Console.WriteLine("-------login--------");
            lb1:
            Console.WriteLine("enter user name");
            string uname = Console.ReadLine();
            if (uname.Length < 6)
            {
                Console.WriteLine("Minimum 6 character");
                goto lb1;
            }
        lb2:
            Console.WriteLine("enter password");
            string pass = Orb.App.Console.ReadPassword();
            
            if (pass.Length < 6)
            {
                Console.WriteLine("Minimum 6 character");
                goto lb2;
            }
            string sqlqry1 = $"select * from login where username='{uname}' and password='{pass}'";
            SqlCommand discmd = new SqlCommand(sqlqry1, con);
            SqlDataReader reader = discmd.ExecuteReader();
            int flag = 0;
            string username = "";
            string password = "";
            while (reader.Read())
            {
                username = reader.GetValue(1).ToString();
                password = reader.GetValue(2).ToString();
                string c = reader.GetValue(3).ToString();
                string d = reader.GetValue(0).ToString();

                if ((uname == username) && (pass == password) && (c == "1"))
                {
                    //doctor doc = new doctor();
                    doctor.doctmenu(d);
                   
                    flag = 1;
                }
                else if ((uname == username) && (pass == password) && (c == "2"))
                {
                    receptionist recpt = new receptionist();
                    recpt.recptmenu(d);
                    flag = 1;

                }

            }
            if (flag == 0)
            {
                
                Console.WriteLine("Invalid Username or Password");
                Console.WriteLine("login failed");
                FileStream f1 = new FileStream("file.txt", FileMode.Append);
                StreamWriter s1 = new StreamWriter(f1);
                s1.Write(uname + "\t");
                s1.WriteLine(pass);
                s1.Close();
                f1.Close();
            }
            reader.Close();
            return flag;

        }

    }
}
namespace Orb.App
{
    /// <summary>
    /// Adds some nice help to the console. Static extension methods don't exist (probably for a good reason) so the next best thing is congruent naming.
    /// </summary>
    static public class Console
    {
        /// <summary>
        /// Like System.Console.ReadLine(), only with a mask.
        /// </summary>
        /// <param name="mask">a <c>char</c> representing your choice of console mask</param>
        /// <returns>the string the user typed in </returns>
        public static string ReadPassword(char mask)
        {
            const int ENTER = 13, BACKSP = 8, CTRLBACKSP = 127;
            int[] FILTERED = { 0, 27, 9, 10 /*, 32 space, if you care */ }; // const

            var pass = new Stack<char>();
            char chr = (char)0;

            while ((chr = System.Console.ReadKey(true).KeyChar) != ENTER)
            {
                if (chr == BACKSP)
                {
                    if (pass.Count > 0)
                    {
                        System.Console.Write("\b \b");
                        pass.Pop();
                    }
                }
                else if (chr == CTRLBACKSP)
                {
                    while (pass.Count > 0)
                    {
                        System.Console.Write("\b \b");
                        pass.Pop();
                    }
                }
                else if (FILTERED.Count(x => chr == x) > 0) { }
                else
                {
                    pass.Push((char)chr);
                    System.Console.Write(mask);
                }
            }

            System.Console.WriteLine();

            return new string(pass.Reverse().ToArray());
        }

        /// <summary>
        /// Like System.Console.ReadLine(), only with a mask.
        /// </summary>
        /// <returns>the string the user typed in </returns>
        public static string ReadPassword()
        {
            return Orb.App.Console.ReadPassword('*');
        }
    }
}