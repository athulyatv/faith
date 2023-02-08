using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace macinetest1
{
    class customer
    {
        public void custmenu()
        {
            Console.WriteLine("customer menu");
            Console.WriteLine("1.depposite");
            Console.WriteLine("2.withdraw");
            Console.WriteLine("3.showbalance");
            Console.WriteLine("4.transfer money");
            Console.WriteLine("5.logout");
            Console.WriteLine("enter your choice");
            string ch = Console.ReadLine();
            if (ch == "1")
            {
                depposite();

            }
            else if (ch == "2")
            {
                withdraw();
            }
            else if (ch == "3")
            {
                balance();
            }
            else if (ch == "4")
            {
                transfer();
            }
            else if (ch == "5")
            {
                mainmenu obj = new mainmenu();
                obj.menu();
            }
            else
            {
                Console.WriteLine("enter valid choice");
                custmenu();
            }

        }
        public static int bal;
        public void depposite()
        {
            customer obj = new customer();
            Console.WriteLine("enter the amount to be deposite ");
            bal = Convert.ToInt32(Console.ReadLine());
            if (bal > 50000)
            {
                Console.WriteLine("enter pancard number");
                string pan = Console.ReadLine();
            }
            Console.WriteLine("success your process");
            Console.WriteLine("goback press anykey");
            string key1 = Console.ReadLine();
            custmenu();


        }

        public void withdraw()
        {
            customer obj = new customer();
            int minbal = 1000;
            int availbal = bal - minbal;
            if (availbal < 1000)
            {
                Console.WriteLine("withdrawel is not possible due to insufficent balance");
                Console.WriteLine("goback press anykey");
                string key1 = Console.ReadLine();
                custmenu();
            }
            else
            {
                Console.WriteLine("available balance:" + availbal);
                Console.WriteLine("enter the amount to be withdraw ");

                int withdraw = Convert.ToInt32(Console.ReadLine());
                if (withdraw > availbal)
                {
                    Console.WriteLine("Insufficient funds..!!!");
                    Console.WriteLine("success your process");
                    Console.WriteLine("goback press anykey");
                    string key3 = Console.ReadLine();
                    custmenu();

                }
                if (withdraw > 50000)
                {
                    Console.WriteLine("enter pancard number");
                    string pan = Console.ReadLine();
                    bal -= withdraw;
                }
                else
                {
                    bal -= withdraw;

                }
                
                
                Console.WriteLine("success your process");
                Console.WriteLine("goback press anykey");
                string key1 = Console.ReadLine();
                custmenu();

            }
            
            
        }
        public void balance()
        {
            customer obj = new customer();
            Console.WriteLine("your balance is" +bal);
            
            Console.WriteLine("goback press anykey");
            string key1 = Console.ReadLine();
            custmenu();
        }
        public void transfer()
        {
            customer obj = new customer();
            Console.WriteLine("enter the account number you want to transfer the money");
            string n = Console.ReadLine();
            Console.WriteLine("enter the amount");
            int amount = Convert.ToInt32(Console.ReadLine());
            bal -= amount;
            Console.WriteLine("success your process");
            Console.WriteLine("goback press anykey");
            string key1 = Console.ReadLine();
            custmenu();
        }
    }
}
