using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace macinetest1
{
    class Program
    {
        static void Main(string[] args)
        {
            mainmenu obj = new mainmenu();
            obj.menu();
            
        }
    }
    public class mainmenu
    {
        public void menu()
        {
            Console.WriteLine("Welcome to bank");
            Console.WriteLine("1.Admin");
            Console.WriteLine("2.customer");
            Console.WriteLine("enter your choice");
            string ch = Console.ReadLine();
            mainmenu obj = new mainmenu();
           
            obj.login(ch);
        }
        public void login(string ch)
        {
            int adminusr = 1;
            int adminpass = 1;
            
            Console.WriteLine("enter username");
            int usename =Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("enter password");
            int password = Convert.ToInt32(Console.ReadLine());
            admin aobj = new admin();
            bool uname =aobj.account.Contains(usename);
            bool pswd = aobj.pin.Contains(password);
            if ((ch == "1") && (usename == adminusr) && (password == adminpass))
            {
                admin adobj = new admin();
                adobj.adminmenu();
            }
            else if ((ch == "2")&&(usename==2)&&(password==2))
            {
                customer cust = new customer();
                
                cust.custmenu();
            }
            else
            {
                Console.WriteLine("incorrect username or password");
                mainmenu obj = new mainmenu();
                for (int i = 0; i < 3; i++)
                {
                    obj.menu();

                }

            }
            
        }
    }
}
