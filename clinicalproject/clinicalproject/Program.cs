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
            Console.WriteLine("enter user name");
            string uname = Console.ReadLine();
            Console.WriteLine("enter password");
            string pass = Console.ReadLine();
            string sqlqry1 = $"select * from login where username='{uname}' and password='{pass}'";
            SqlCommand discmd = new SqlCommand(sqlqry1, con);
            SqlDataReader reader = discmd.ExecuteReader();
            int flag = 0;
            while (reader.Read())
            {
                string a = reader.GetValue(1).ToString();
                string b = reader.GetValue(2).ToString();
                string c = reader.GetValue(3).ToString();
                string d = reader.GetValue(0).ToString();

                if ((uname == a) && (pass == b) && (c == "1"))
                {
                    //doctor doc = new doctor();
                    doctor.doctmenu(d);
                   
                    flag = 1;
                }
                else if ((uname == a) && (pass == b) && (c == "2"))
                {
                    receptionist recpt = new receptionist();
                    recpt.recptmenu(d);
                    flag = 1;

                }

            }
            if (flag == 0)
            {
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
