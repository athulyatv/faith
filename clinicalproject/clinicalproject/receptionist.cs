using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace clinicalproject
{
    class receptionist
    {
        public string recptmenu(string d)
        {
            Program obj = new Program();

            string cs = "Data Source=DESKTOP-8OGPQNH;Initial catalog=project;User Id=sa;Password=athulya";
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            string nameqry = $"select * from staff where loginid={d}";
            SqlCommand namedisplay = new SqlCommand(nameqry, con);
            SqlDataReader reader = namedisplay.ExecuteReader();
            int flag = 0;
            while (reader.Read())
            {
                string a = reader.GetValue(1).ToString();
                Console.WriteLine("----receptionist Menu------\n");
                Console.WriteLine("   welcome " + a);
            }
            menu();

            reader.Close();
            return ("a");
        }
        public static void menu()
        {
            Console.WriteLine("");
            Console.WriteLine("------Receptionist Dashboard-----\n");
            Console.WriteLine("1.Add Patient New");
            Console.WriteLine("2.Existing Patient");
            Console.WriteLine("3.Today patient list");
            Console.WriteLine("4.Reset token");
            Console.WriteLine("5.logout");
            Console.WriteLine("enter your choice");
            string ch = Console.ReadLine();
            switch (ch)
            {
                case "1":
                    addpatient();
                    break;
                case "2":
                    existpatient();
                    break;
                case "3":
                    todaypatient();
                    break;
                case "4":
                    resetToken();
                    break;
                case "5":
                    Program.login();
                    break;

                default:
                    Console.WriteLine("enter a correct choice");
                    break;


            }

        }
        public static void resetToken()
        {
            Console.WriteLine("!!!!!!!Are you sure do you want to reset Token(y/n)!!!!!!!!");
            string ch = Console.ReadLine();
            if (ch == "y")
            {
                string cs = "Data Source=DESKTOP-8OGPQNH;Initial catalog=project;User Id=sa;Password=athulya";
                SqlConnection con = new SqlConnection(cs);
                con.Open();
                int a = 0;
                string updateqry = $"update token set token='{a}'";
                SqlCommand updatecmd = new SqlCommand(updateqry, con);
                updatecmd.ExecuteNonQuery();
                Console.WriteLine("----Token Reseted----");
                menu();

            }
            else if (ch == "n")
            {
                menu();
            }

            else
            {
                Console.WriteLine("enter valid ip");
                resetToken();
            }
        }
        public static void todaypatient()
        {
            
            string cs = "Data Source=DESKTOP-8OGPQNH;Initial catalog=project;User Id=sa;Password=athulya";
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            DateTime today = DateTime.Today;
            string date = today.ToString();
            
            string selectqry = $"select * from appointment where appointdate='{date}' order by doctid";
            SqlCommand display = new SqlCommand(selectqry, con);
            SqlDataReader reader = display.ExecuteReader();
            
            while (reader.Read())
            {
                Console.Write("Patient Reg No: " + reader.GetValue(0).ToString());
                Console.Write("\t Token: " + reader.GetValue(8).ToString());
                string docid=reader.GetValue(5).ToString();
                doctorName(docid);
                string deptid=reader.GetValue(6).ToString();
                deptname(deptid);
            }
            menu();
        }
        public static void deptname(string deptid)
        {
            string cs = "Data Source=DESKTOP-8OGPQNH;Initial catalog=project;User Id=sa;Password=athulya";
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            string nameqry = $"select * from department where deptid={deptid}";
            SqlCommand namecmd = new SqlCommand(nameqry, con);
            SqlDataReader reader = namecmd.ExecuteReader();

            while (reader.Read())
            {
                Console.WriteLine("\t Department: " + reader.GetValue(1).ToString());

            }
            reader.Close();

        }
        public static void doctorName(string docid)
        {
            string cs = "Data Source=DESKTOP-8OGPQNH;Initial catalog=project;User Id=sa;Password=athulya";
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            string nameqry = $"select * from doctor where doctid={docid}";
            SqlCommand namecmd = new SqlCommand(nameqry, con);
            SqlDataReader reader = namecmd.ExecuteReader();

            while (reader.Read())
            {
                string staffid = reader.GetValue(2).ToString();
                staffName(staffid);

            }
            reader.Close();

        }
        public static void staffName(string staffid)
        {
            string cs = "Data Source=DESKTOP-8OGPQNH;Initial catalog=project;User Id=sa;Password=athulya";
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            string nameqry = $"select * from staff where staffid={staffid}";
            SqlCommand namecmd = new SqlCommand(nameqry, con);
            SqlDataReader reader = namecmd.ExecuteReader();

            while (reader.Read())
            {
                string staffname = reader.GetValue(1).ToString();
                string staffname1 = reader.GetValue(2).ToString();
                Console.Write("\t Doctor: "+staffname + " " + staffname1);
                

            }
            reader.Close();

        }
        public static void addpatient()
        {
            string cs = "Data Source=DESKTOP-8OGPQNH;Initial catalog=project;User Id=sa;Password=athulya";
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            Console.WriteLine("--------patient registration---------\n");
            
            string fname = validation.firstname("first");
            
            string lname = validation.lastname("last");
            
            string dob = validation.ValBirth();
            
            string address = validation.address();
            
            string phn = validation.Valph();
            
            string gender = validation.gend();
            
            string bld = validation.blood();
            string status = "active";
            string insertqry = $"insert into patient(fname,lname,dob,phone,address,gender,bloodgrp,status1)values('{fname}','{lname}','{dob}','{phn}','{address}','{gender}','{bld}','{status}')";
            SqlCommand insertcmd = new SqlCommand(insertqry, con);
            insertcmd.ExecuteNonQuery();
            Console.WriteLine("\n#####---Registration completed successfully---#####");
            Console.WriteLine("\n Registration fee=" + 200);
            Console.WriteLine(" ");
            string idqry = $"select patientid from patient where fname='{fname}' and lname='{lname}' and phone='{phn}' and bloodgrp='{bld}'";
            SqlCommand idcmd = new SqlCommand(idqry, con);
            SqlDataReader reader = idcmd.ExecuteReader();

            while (reader.Read())
            {
                string a = reader.GetValue(0).ToString();
                appointment(a);
            }
            reader.Close();

        }

        public static void appointment(string id)
        {
            string cs = "Data Source=DESKTOP-8OGPQNH;Initial catalog=project;User Id=sa;Password=athulya";
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            Console.WriteLine("\n -------Appointment------");
            Console.WriteLine("Patient id:" + id);
            string nameqry = $"select fname from patient where patientid='{id}'";
            SqlCommand namecmd = new SqlCommand(nameqry, con);
            SqlDataReader reader = namecmd.ExecuteReader();

            while (reader.Read())
            {
                string a = reader.GetValue(0).ToString();
                Console.WriteLine("Patient Name:" + a);
            }
            reader.Close();
            DateTime today = DateTime.Today;
            string date = today.ToString();
            Console.WriteLine("Appointment date:" + today);
            
            Console.WriteLine("Select department");
            department();
            departmentlabel:
            Console.WriteLine("Enter department Id");
            int dept = Convert.ToInt32(Console.ReadLine());
            
            doctorlist(dept);
            doctorlabel:
            Console.WriteLine("Select Doctor:");
            int doct = Convert.ToInt32(Console.ReadLine());
            
            
            int fee=consultfee(doct);
            string status = "active";
            int token1 = token(doct);

            string insertqry = $"insert into appointment(status1,conseltfee,appointdate,consltfeedate,doctid,deptid,patientid,token)values('{status}','{fee}','{date}','{today}','{doct}','{dept}','{id}','{token1}')";
            SqlCommand insertcmd = new SqlCommand(insertqry, con);
            insertcmd.ExecuteNonQuery();
            string updateqry = $"update patient set lastvisit='{date}',lastconsultfeedate='{date}' where patientid='{id}'";
            SqlCommand updatecmd = new SqlCommand(updateqry, con);
            updatecmd.ExecuteNonQuery();
            Console.WriteLine("------appointment confirmed-------");
            Console.WriteLine("Token:" + token1);
            Console.WriteLine("Consultation fee=" + fee);
            Console.WriteLine("-----Total amount:" + (fee + 200));
            Console.WriteLine("press any key to go dashboard");
            string ch = Console.ReadLine();
            
            menu();
            



        }
        public static int consultfee(int docid)
        {
            string cs = "Data Source=DESKTOP-8OGPQNH;Initial catalog=project;User Id=sa;Password=athulya";
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            string nameqry = $"select * from doctor where doctid={docid}";
            SqlCommand namecmd = new SqlCommand(nameqry, con);
            SqlDataReader reader = namecmd.ExecuteReader();
            int fee = 1;
            while (reader.Read())
            {
                string a = reader.GetValue(1).ToString();
                fee = int.Parse(a);
                
            }
            reader.Close();
            return fee;


        }
        
        public static void department()
        {
            string cs = "Data Source=DESKTOP-8OGPQNH;Initial catalog=project;User Id=sa;Password=athulya";
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            string nameqry = $"select * from department";
            SqlCommand namecmd = new SqlCommand(nameqry, con);
            SqlDataReader reader = namecmd.ExecuteReader();

            while (reader.Read())
            {
                string a = reader.GetValue(0).ToString();
                string b = reader.GetValue(1).ToString();
                Console.WriteLine(a + "\t" + b);

            }
            reader.Close();

        }
        public static void doctorlist(int id)
        {
            string cs = "Data Source=DESKTOP-8OGPQNH;Initial catalog=project;User Id=sa;Password=athulya";
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            string nameqry = $"select * from doctor where deptid={id}";
            SqlCommand namecmd = new SqlCommand(nameqry, con);
            SqlDataReader reader = namecmd.ExecuteReader();

            while (reader.Read())
            {
                string a = reader.GetValue(0).ToString();
                string b = reader.GetValue(1).ToString();
                string c = reader.GetValue(2).ToString();
                getdocName(a,c, b);

            }
            reader.Close();
        }
        public static void getdocName(string did,string c, string b)
        {
            int staffid = int.Parse(c);
            string cs = "Data Source=DESKTOP-8OGPQNH;Initial catalog=project;User Id=sa;Password=athulya";
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            string nameqry = $"select * from staff where staffid={staffid}";
            SqlCommand namedisplay = new SqlCommand(nameqry, con);
            SqlDataReader reader = namedisplay.ExecuteReader();

            while (reader.Read())
            {
                string z = reader.GetValue(0).ToString();
                string a = reader.GetValue(1).ToString();
                string d = reader.GetValue(2).ToString();
                Console.WriteLine(did + " " + a + " " + d + " " + b);
            }


            reader.Close();

        }


        public static void existpatient()
        {
            Console.WriteLine("----------Search Patient-----------\n");
            search();


        }
        public static void search()
        {
            Console.WriteLine("1.By patient reg no.");
            Console.WriteLine("2.By patient phone no");
            Console.WriteLine("3.Go Back");
            Console.WriteLine("Enter your choice");
            string ch = Console.ReadLine();
            switch (ch)
            {
                case "1":
                    searchByReg();
                    break;
                case "2":
                    searchByPhone();
                    break;
                case "3":
                    menu();
                    break;
                default:
                    Console.WriteLine("enter correct input");
                    search();
                    break;
            }
        }
        public static void searchByReg()
        {
            
            string regno = validation.pid();
            string cs = "Data Source=DESKTOP-8OGPQNH;Initial catalog=project;User Id=sa;Password=athulya";
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            string regid = $"select * from patient where patientid='{regno}'";
            SqlCommand display = new SqlCommand(regid, con);
            SqlDataReader reader = display.ExecuteReader();
            int f = 0;
            while (reader.Read())
            {
                Console.WriteLine("----Patient Found----\n");
                Console.WriteLine("Patient Reg No:" + reader.GetValue(0).ToString());
                Console.WriteLine("Name:" + reader.GetValue(1).ToString() + " " + reader.GetValue(2).ToString());
                Console.WriteLine("Address:" + reader.GetValue(5).ToString());
                Console.WriteLine("Phone No:" + reader.GetValue(4).ToString());
                Console.WriteLine("Blood Group:" + reader.GetValue(7).ToString());
                Console.WriteLine("Gender:" + reader.GetValue(6).ToString());
                f = 1;
                subMenu();

            }
            if (f == 0)
            {
                Console.WriteLine("Sorry , there is no such patient");
                menu();
            }

            reader.Close();

        }
        public static void searchByPhone()
        {
            Console.WriteLine("Enter Phone Number");
            string phone = validation.Valph();
            string cs = "Data Source=DESKTOP-8OGPQNH;Initial catalog=project;User Id=sa;Password=athulya";
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            string regid = $"select * from patient where phone={phone}";
            SqlCommand display = new SqlCommand(regid, con);
            SqlDataReader reader = display.ExecuteReader();
            int f = 0;
            while (reader.Read())
            {
                Console.WriteLine("Patient Reg No:" + reader.GetValue(0).ToString());
                Console.WriteLine("Name:" + reader.GetValue(1).ToString() + " " + reader.GetValue(2).ToString());
                Console.WriteLine("Address:" + reader.GetValue(5).ToString());
                Console.WriteLine("Phone No:" + reader.GetValue(4).ToString());
                Console.WriteLine("Blood Group:" + reader.GetValue(7).ToString());
                Console.WriteLine("Gender:" + reader.GetValue(6).ToString());
                Console.WriteLine("");
                f = 1;
                //subMenu();
            }
            if (f == 0)
            {
                Console.WriteLine("Sorry , there is no such patient");
                menu();
            }
            else
            {
                subMenu();
            }
            reader.Close();


        }
        public static void subMenu()
        {
            Console.WriteLine("Enter selected Patient Id:");
            string id = Console.ReadLine();
            Console.WriteLine("\n1.Book Appointment");
            Console.WriteLine("2.Edit Patient");
            Console.WriteLine("3.Patient Disable");
            Console.WriteLine("4.Dashboard");
            Console.WriteLine("Enter your choice");
            string ch = Console.ReadLine();
            switch (ch)
            {
                case "1":
                    existAppointment(id);
                    break;
                case "2":
                    editPatient(id);
                    break;
                case "3":
                    disablePatient(id);
                    break;
                case "4":
                    menu();
                    break;
                default:
                    Console.WriteLine("enter a valid input");
                    subMenu();
                    break;

            }
        }
        public static void editPatient(string id)
        {
            
            string cs = "Data Source=DESKTOP-8OGPQNH;Initial catalog=project;User Id=sa;Password=athulya";
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            searchlabel:
            Console.WriteLine("which field you want to edit");
            Console.WriteLine("1.name");
            Console.WriteLine("2.address");
            Console.WriteLine("3.phone No");
            Console.WriteLine("enter your choice");
            string ch = Console.ReadLine();

            if (ch == "1")
            {
                Console.WriteLine("enter new name");
                string fname = Console.ReadLine();
                string insertqry = $"update patient set fname='{fname}' where patientid='{id}'";
                SqlCommand insertcmd = new SqlCommand(insertqry, con);
                insertcmd.ExecuteNonQuery();
                Console.WriteLine("Updated successfully");
                Console.WriteLine("Do you want to continue editing(y/n)");
                string c = Console.ReadLine();
                if (c == "y")
                {
                    goto searchlabel;
                }
                else
                {
                    menu();

                }
               
                

            }
            else if (ch == "2")
            {
                Console.WriteLine("enter new Address");
                string address = Console.ReadLine();
                string insertqry = $"update patient set address='{address}' where patientid='{id}'";
                SqlCommand insertcmd = new SqlCommand(insertqry, con);
                insertcmd.ExecuteNonQuery();
                Console.WriteLine("Updated successfully");
                Console.WriteLine("Do you want to continue editing(y/n)");
                string c = Console.ReadLine();
                if (c == "y")
                {
                    goto searchlabel;
                }
                else
                {
                    menu();
                }

            }
            else if (ch == "3")
            {
                Console.WriteLine("enter new phone no");
                string phone = Console.ReadLine();
                string insertqry = $"update patient set phone='{phone}' where patientid='{id}'";
                SqlCommand insertcmd = new SqlCommand(insertqry, con);
                insertcmd.ExecuteNonQuery();
                Console.WriteLine("Updated successfully");
                Console.WriteLine("Do you want to continue editing(y/n)");
                string c = Console.ReadLine();
                if (c == "y")
                {
                    goto searchlabel;
                }
                else
                {
                    menu();
                }
            }
            else
            {
                Console.WriteLine("enter correct input");

                goto searchlabel;

            }
        }
        
        public static void disablePatient(string id)
        {
            
            
            string cs = "Data Source=DESKTOP-8OGPQNH;Initial catalog=project;User Id=sa;Password=athulya";
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            string statusqry = $"select status1 from patient where patientid='{id}'";
            SqlCommand statuscmd = new SqlCommand(statusqry, con);
            SqlDataReader reader = statuscmd.ExecuteReader();
            reader.Read();
            string sts = reader.GetValue(0).ToString();
            reader.Close();
            if(sts== "disable")
            {
                Console.WriteLine("the patient is already disabled");
                Console.WriteLine("Do You want to enable");
                Console.WriteLine("Are sure y/n");
                string ch = Console.ReadLine();
                if (ch == "y")
                {
                    string status = "disable";
                    string insertqry = $"update patient set status1='{status}' where patientid='{id}'";
                    SqlCommand insertcmd = new SqlCommand(insertqry, con);
                    insertcmd.ExecuteNonQuery();
                    Console.WriteLine("Disabled successfully");

                }
                menu();
            }
            else
            {
                Console.WriteLine("Are sure y/n");
                string ch = Console.ReadLine();
                if (ch == "y")
                {
                    string status = "disable";
                    string insertqry = $"update patient set status1='{status}' where patientid='{id}'";
                    SqlCommand insertcmd = new SqlCommand(insertqry, con);
                    insertcmd.ExecuteNonQuery();
                    Console.WriteLine("Disabled successfully");

                }
                menu();
            }
            
            
            

        }
        public static void existAppointment(string id)
        {
            string cs = "Data Source=DESKTOP-8OGPQNH;Initial catalog=project;User Id=sa;Password=athulya";
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            Console.WriteLine("-------Appointment------");
            Console.WriteLine("Patient id:" + id);
            string nameqry = $"select fname from patient where patientid='{id}'";
            SqlCommand namecmd = new SqlCommand(nameqry, con);
            SqlDataReader reader = namecmd.ExecuteReader();

            while (reader.Read())
            {
                string a = reader.GetValue(0).ToString();
                Console.WriteLine("Patient Name:" + a);
            }
            reader.Close();

            DateTime today = DateTime.Today;
            string date = today.ToString();
            Console.WriteLine("Appointment date:" + today);
            Console.WriteLine("Select department");
            department();
            Console.WriteLine("Enter department id");
            int dept = Convert.ToInt32(Console.ReadLine());
            doctorlist(dept);
            Console.WriteLine("Select Doctor:");
            int doct = Convert.ToInt32(Console.ReadLine());
            int fee = consultfee(doct);
            string status = "active";
            
            string consult = $"select * from patient where patientid='{id}'";
            SqlCommand consulcmd = new SqlCommand(consult, con);
            SqlDataReader reader1 = consulcmd.ExecuteReader();
            int flag = 2;
            string lastfeedate = "";
            int token1 = token(doct);
            while (reader1.Read())
            {
                string latvist = reader1.GetValue(10).ToString();
                lastfeedate = reader1.GetValue(11).ToString();
                Console.WriteLine("Last consultation date="+latvist);
                Console.WriteLine("Last consultation fee date"+lastfeedate);

                flag=existConsult(latvist, lastfeedate);

            }
            reader1.Close();
            if (flag == 1)
            {

                Console.WriteLine("No consultation fee");
                string insertqry = $"insert into appointment(status1,conseltfee,appointdate,consltfeedate,doctid,deptid,patientid,token)values('{status}','{fee}','{date}','{lastfeedate}','{doct}','{dept}','{id}','{token1}')";
                SqlCommand insertcmd = new SqlCommand(insertqry, con);
                insertcmd.ExecuteNonQuery();
                string insertqry1 = $"update patient set lastvisit='{date}',lastconsultfeedate='{lastfeedate}' where patientid='{id}'";
                SqlCommand insertcmd1 = new SqlCommand(insertqry1, con);
                insertcmd1.ExecuteNonQuery();
            }
            else if(flag==0)
            {
                Console.WriteLine(fee);
                string insertqry = $"insert into appointment(status1,conseltfee,appointdate,consltfeedate,doctid,deptid,patientid,token)values('{status}','{fee}','{date}','{today}','{doct}','{dept}','{id}','{token1}')";
                SqlCommand insertcmd = new SqlCommand(insertqry, con);
                insertcmd.ExecuteNonQuery();
                string insertqry1 = $"update patient set lastvisit='{date}',lastconsultfeedate='{date}' where patientid='{id}'";
                SqlCommand insertcmd1 = new SqlCommand(insertqry1, con);
                insertcmd1.ExecuteNonQuery();

            }
            else
            {

            }
            Console.WriteLine("appointment confirmed");
            Console.WriteLine("Token:" + token1);
            Console.WriteLine("press 0 to go dashboard");
            string ch = Console.ReadLine();
            if (ch == "0")
            {
                menu();
            }



        }
        public static int token(int docid)
        {
            
            string cs = "Data Source=DESKTOP-8OGPQNH;Initial catalog=project;User Id=sa;Password=athulya";
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            string tokenqry = $"select * from token where doctid={docid}";
            SqlCommand tokencmd = new SqlCommand(tokenqry, con);
            SqlDataReader reader = tokencmd.ExecuteReader();
            reader.Read();
            string a = reader.GetValue(1).ToString();
            int t = int.Parse(a);
            int token = t + 1;
            
            reader.Close();
            string updateqry = $"update token set token='{token}'where doctid='{docid}'";
            SqlCommand updateqrycmd = new SqlCommand(updateqry, con);
            updateqrycmd.ExecuteNonQuery();
            return token;
            

        }
        public static int existConsult(string latvist, string lastfeedate)
        {
            var lastfeedate1 = DateTime.Parse(lastfeedate);
            DateTime currentdate = DateTime.Now;
            TimeSpan difference = currentdate.Subtract(lastfeedate1);
            int flag = 0;
            if (difference.TotalDays < 7)
            {
                
                flag = 1;
            }
            return flag;

        }

    }
}
