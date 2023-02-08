using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace macinetest1
{
    class admin
    {
        public void adminmenu()
        {
            Console.WriteLine("Admin menu bar");
            Console.WriteLine("1.add customer");
            Console.WriteLine("2.edit/delete customer");
            Console.WriteLine("3.search");
            Console.WriteLine("4.logout");
            Console.WriteLine("enter your choice:");
            string ch = Console.ReadLine();
            if (ch == "1")
            {
                addcust();
            }
            else if (ch == "2")
            {
                editDelete();
            }
            else if (ch == "3")
            {
                search();
            }
            else if (ch == "4")
            {
                mainmenu obj = new mainmenu();
                obj.menu();
            }
            else
            {
                Console.WriteLine("enter correct choice");
            }
        }
        public static List<string> name = new List<string>();
        public static List<string> email = new List<string>();
        public List<int> account = new List<int>();
        public static List<string> accountype = new List<string>();
        public static List<string> phn = new List<string>();
        public List<int> pin = new List<int>();
        public static List<int> balance = new List<int>();
        public static List<int> minbal = new List<int>();
        public static long id = 33889873;
        static long generateId()
        {
            return id++;
        }
        public static long pinno = 338;
        static long generatepinno()
        {
            return pinno++;
        }


        public void addcust()
        {
            int accno = Convert.ToInt32(generateId());
            Console.WriteLine($"account no:{accno}");
            account.Add(accno);
            Console.WriteLine("name");
            name.Add(Console.ReadLine());
            Console.WriteLine("account type (1 for saving 2 for fixed)");
            accountype.Add(Console.ReadLine());
            Console.WriteLine("balance:1000");
            balance.Add(1000);
            Console.WriteLine("minimum balance:1000");
            minbal.Add(1000);
            Console.WriteLine("enter mobile number");
            phn.Add(Console.ReadLine());
            Console.WriteLine("enter the email id");
            email.Add(Console.ReadLine());
            int pno = Convert.ToInt32(generatepinno());
            Console.WriteLine($"pin:{pno}");
            pin.Add(pno);
            Console.WriteLine("added customer successfully");
            Console.WriteLine("do u want to add new customer y/n");
            string ch = Console.ReadLine();
            if (ch == "y")
            {
                addcust();
            }
            else if (ch == "n")
            {
                adminmenu();

            }
            else
            {
                Console.WriteLine("enter valid input");
            }

        }
        public void editDelete()
        {
            Console.WriteLine("do you want to edit or delete enter 1 for edit 2 for delete");
            string ch = Console.ReadLine();
            if (ch == "1")
            {
                edit();
            }
            else if (ch == "2")
            {
                delete();
            }
            else
            {

                Console.WriteLine("enter valid input");
            }
        }
        public void edit()
        {
            Console.WriteLine("enter account number");
            int accountno = Convert.ToInt32(Console.ReadLine());
           
            Console.WriteLine("what you want to update(1 for email 2 for phone 3 for both");
            string updateid = Console.ReadLine();
            if (updateid == "1")
            {
                Console.WriteLine("enter new email");
                string newmail = Console.ReadLine();

                int index = account.FindIndex(s => s == accountno);

                if (index != -1)
                    email[index] = newmail;
                Console.WriteLine("updated successfully");
                Console.WriteLine("do you want to edit another customer(y/n)");
                string ech = Console.ReadLine();
                if (ech == "y")
                {
                    edit();
                }
                else
                {
                    adminmenu();
                }


            }
            else if (updateid == "2")
            {
                Console.WriteLine("enter new phone number");
                string newmail = Console.ReadLine();

                int index = account.FindIndex(s => s == accountno);

                if (index != -1)
                    phn[index] = newmail;
                Console.WriteLine("updated successfully");
                Console.WriteLine("do you want to edit another customer(y/n)");
                string ech = Console.ReadLine();
                if (ech == "y")
                {
                    edit();
                }
                else
                {
                    adminmenu();
                }

            }
            else if (updateid == "3")
            {
                Console.WriteLine("enter new email");
                string newmail = Console.ReadLine();
                Console.WriteLine("enter new phone number");
                string newphn = Console.ReadLine();

                int index = account.FindIndex(s => s == accountno);

                if (index != -1)
                {
                    email[index] = newmail;
                    phn[index] = newphn;
                }
                Console.WriteLine("updated successfully");
                Console.WriteLine("do you want to edit another customer(y/n)");
                string ech = Console.ReadLine();
                if (ech == "y")
                {
                    edit();
                }
                else
                {
                    adminmenu();
                }



            }
            else
            {
                Console.WriteLine("entaer valid input");
                edit();
            }
            
        }
        public void delete()
        {
            Console.WriteLine("enter account number");
            int accountno = Convert.ToInt32(Console.ReadLine());
            int index = account.FindIndex(s => s == accountno);
            account.RemoveAt(index);
            accountype.RemoveAt(index);
            email.RemoveAt(index);
            phn.RemoveAt(index);
            minbal.RemoveAt(index);
            balance.RemoveAt(index);
            pin.RemoveAt(index);
            name.RemoveAt(index);
            Console.WriteLine("deleted successfully");
           
            Console.WriteLine("do you want to delete another customer(y/n)");
            string ech = Console.ReadLine();
            if (ech == "y")
            {
                delete();
            }
            else
            {
                adminmenu();
            }


        }
        public void search()
        {
            for (int i = 0; i < account.Count; i++)
            {
                Console.Write(account[i] + "\t");
                Console.Write(name[i] + "\t");
                Console.Write(email[i] + "\t");
                Console.Write(phn[i] + "\t");
                Console.Write(accountype[i] + "\t");
                Console.Write(balance[i] + "\t");
                Console.Write(pin[i] + "\t");
                Console.WriteLine(" ");
            }
            Console.WriteLine("enter something u want to search(y/n)");
            
            string searchword = Console.ReadLine();
            if (searchword == "y")
            {
                Console.WriteLine("enter the account number you want to search");
                int acntn =Convert.ToInt32( Console.ReadLine());
                bool isExist = account.Contains(acntn);
                if (isExist)
                {
                    Console.WriteLine("Account is found");
                    int i = account.IndexOf(acntn);
                    Console.Write(account[i] + "\t");
                    Console.Write(name[i] + "\t");
                    Console.Write(email[i] + "\t");
                    Console.Write(phn[i] + "\t");
                    Console.Write(accountype[i] + "\t");
                    Console.Write(balance[i] + "\t");
                    Console.Write(pin[i] + "\t");
                    Console.WriteLine(" ");
                    Console.WriteLine("you want to go back or continue the search(y/n) ");
                    string sch = Console.ReadLine();
                    if (sch == "y")
                    {
                        search();
                    }
                    else
                    {
                        adminmenu();
                    }
                    

                }
                else
                {
                    Console.WriteLine("Account is not found");
                    search();
                }

            }
            else
            {
                adminmenu();
            }

        }

        }
 
}
