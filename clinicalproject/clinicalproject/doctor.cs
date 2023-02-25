using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace clinicalproject
{
    class doctor
    {

        public static string doctotId;
        public static string doctmenu(string d)
        {

            
            Console.WriteLine("doctor");
            Program o = new Program();
            string cs = "Data Source=DESKTOP-8OGPQNH;Initial catalog=project;User Id=sa;Password=athulya";
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            string nameqry = $"select * from staff where loginid='{d}'";
            SqlCommand namedisplay = new SqlCommand(nameqry, con);
            SqlDataReader reader = namedisplay.ExecuteReader();
            while (reader.Read())
            {
                string a = reader.GetValue(1).ToString();
                Console.WriteLine("----Doctor Menu------");
                Console.WriteLine("   welcome " + a);
                string staffid= reader.GetValue(0).ToString();
                doctotId=getDoctorId(staffid);
            }
            docmenu();
            reader.Close();
            
            return "a";
            
        }
        public static string getDoctorId(string staffid)
        {
            string cs = "Data Source=DESKTOP-8OGPQNH;Initial catalog=project;User Id=sa;Password=athulya";
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            string nameqry = $"select doctid from doctor where staffid='{staffid}'";
            SqlCommand namedisplay = new SqlCommand(nameqry, con);
            SqlDataReader reader = namedisplay.ExecuteReader();
            reader.Read();
            string doctid= reader.GetValue(0).ToString();
            return doctid;
        }
        public static void docmenu()
        {
            Console.WriteLine("1.View Appointment");
            Console.WriteLine("2.Logout");
            Console.WriteLine("Enter your choice");
            string ch = Console.ReadLine();
            switch (ch)
            {
                case "1":
                    viewAppointment();
                    break;
                case "2":
                    Program.login();
                    break;
                default:
                    Console.WriteLine("Enter a valid input");
                    docmenu();
                    break;
            }

        }
        public static void viewAppointment()

        {
            Console.WriteLine("-----------Today's Appointment-----------\n");
            string cs = "Data Source=DESKTOP-8OGPQNH;Initial catalog=project;User Id=sa;Password=athulya";
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            doctor obj = new doctor();
            var appointdate = DateTime.Today;
            
            string appointqry = $"select* from appointment where doctid='{doctotId}' and appointdate='{appointdate}' and status1='{"active"}' order by token";
            SqlCommand appointcmd = new SqlCommand(appointqry, con);
            SqlDataReader reader = appointcmd.ExecuteReader();
            int flag = 0;
            while (reader.Read())
            {
                flag = 1;
                Console.WriteLine("Token No: "+reader.GetValue(8).ToString());
                string patientId = reader.GetValue(7).ToString();
                getPatientdetails(patientId);
                Console.WriteLine(" ");
                
                
                
            }
            if (flag == 1)
            {
                nextPtient();
            }
            else
            {
                Console.WriteLine("!!!!!!!!!!No appointments today!!!!!!!!!!!!");
                Console.WriteLine("press any key for go back");
                string ch = Console.ReadLine();
                docmenu();
            }

            
            
        }
        public static void getPatientdetails(string patientId)
        {
            string cs = "Data Source=DESKTOP-8OGPQNH;Initial catalog=project;User Id=sa;Password=athulya";
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            doctor obj = new doctor();
            string appointqry = $"select* from patient where patientid='{patientId}'";
            SqlCommand appointcmd = new SqlCommand(appointqry, con);
            SqlDataReader reader = appointcmd.ExecuteReader();

            while (reader.Read())
            {
                
                Console.Write("Name: "+reader.GetValue(1).ToString()+" "+ reader.GetValue(2).ToString()+"\t");
                string dob = reader.GetValue(3).ToString();
                calculateAge(dob);
                Console.Write("Gender: " + reader.GetValue(6).ToString()+"\t");
                Console.WriteLine("Blood Group: " + reader.GetValue(7).ToString());
            }
            reader.Close();
        }
        public static void calculateAge(string dob)
        {
            var birthday = DateTime.Parse(dob);
            var age = DateTime.Now.Year - birthday.Year;
            Console.Write("Age: " + age+"\t");

        }
        public static void nextPtient()
        {
            Console.WriteLine("\n--------Consulting Patient--------\n");
            string cs = "Data Source=DESKTOP-8OGPQNH;Initial catalog=project;User Id=sa;Password=athulya";
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            doctor obj = new doctor();
            string status = "active";
            var appointdate = DateTime.Today;
            string consltdate1 = appointdate.ToString();
            string appointqry = $"SELECT min(token),patientid FROM appointment WHERE doctid = '{doctotId}' and appointdate='{consltdate1}' and status1='{status}' group by patientid";
            SqlCommand appointcmd = new SqlCommand(appointqry, con);
            SqlDataReader reader = appointcmd.ExecuteReader();
            string patientid="";
            int flag = 0;
            string diagnosis = "";
            string medicine = "";
            string notes = "";
            reader.Read();
            flag = 1;
            patientid = reader.GetValue(1).ToString();
            getPatientdetails(patientid);
            getPatientHistory(patientid);
            Console.WriteLine("Add diagnosis");
            diagnosis = Console.ReadLine();
            Console.WriteLine("Prescribe Medicine");
            medicine = Console.ReadLine();
            Console.WriteLine("Add notes");
            notes = Console.ReadLine();
            reader.Close();
            
            string insertqry = $"insert into history(diagnosis,notes,medicine,patientid,doctid,condate)values('{diagnosis}','{notes}','{medicine}','{patientid}','{doctotId}','{consltdate1}')";
            SqlCommand insertcmd = new SqlCommand(insertqry, con);
            insertcmd.ExecuteNonQuery();
            string status1 = "complete";
            string updateqry = $"update appointment set status1='{status1}' where patientid='{patientid}' and doctid='{doctotId}' and status1='{status}'";
            SqlCommand updatecmd = new SqlCommand(updateqry, con);
            updatecmd.ExecuteNonQuery();
            Console.WriteLine("-----Consultation Completed-----");
            breakMenu();
                

                
            
            
            
        }
        public static void breakMenu()
        {
            Console.WriteLine("1.Continue consultation");
            Console.WriteLine("2.take a break");
            Console.WriteLine("enter your choice");
            string ch = Console.ReadLine();
            switch (ch)
            {
                case "1":
                    viewAppointment();
                    break;
                case "2":
                    docmenu();
                    break;
                default:
                    Console.WriteLine("enter a valid input");
                    breakMenu();
                    break;
            }
        }
        public static void getPatientHistory(string patientid)
        {
            Console.WriteLine("\n--------Patient History--------\n");
            string cs = "Data Source=DESKTOP-8OGPQNH;Initial catalog=project;User Id=sa;Password=athulya";
            SqlConnection con = new SqlConnection(cs);
            con.Open();
            doctor obj = new doctor();
            
            string historyqry = $"SELECT * FROM history WHERE patientid='{patientid}'";
            SqlCommand historycmd = new SqlCommand(historyqry, con);
            SqlDataReader reader = historycmd.ExecuteReader();
            int flag = 0;
            while (reader.Read())
            {
                Console.WriteLine(reader.GetValue(1).ToString());
                Console.WriteLine(reader.GetValue(2).ToString());
                Console.WriteLine(reader.GetValue(3).ToString());
                Console.WriteLine(reader.GetValue(6).ToString());
                flag = 1;
            }
            if (flag == 0)
            {
                Console.WriteLine("No History Found");
            }
            Console.WriteLine(" ");
            reader.Close();
        }
    }
}
