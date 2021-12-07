using System;
using System.IO;
using System.Collections.Generic;

namespace Staff_Management_System
{
    using System;

    class staff
    {
        private string staffFirstName;
        private string staffLastName;
        private string phoneNumber;
        private string emailAddress;
        private string address;
        private string gender;
        private int age;
        private string NInumber;

        public staff()
        {

        }


        public void setStaffDetails(string firstName, string lastName, string phoneNumber,
        string emailAddress, string address, string gender, int age, string NInumber)
        {
            this.staffFirstName = firstName;
            this.staffLastName = lastName;
            this.phoneNumber = phoneNumber;
            this.emailAddress = emailAddress;
            this.address = address;
            this.gender = gender;
            this.age = age;
            this.NInumber = NInumber;
        }


        public string displayStaffDetails()
        {
            return this.staffFirstName + ", " + this.staffLastName + ", " +
                this.phoneNumber + ", " + this.emailAddress + ", " +
                this.address + ", " + this.gender + ", " +
                this.age + ", " + this.NInumber + "\n";
        }


    }

    class Validation
    {
        public bool checkPhoneNumber(string phoneNum)
        {
            if (phoneNum.Length < 11)
            {
                return false;
            }
            return true;

        }



        public bool checkNINumber(string NInum)
        {
            if (NInum.Length < 9)
            {
                return false;
            }
            return true;
        }

    }


    class Program
    {


        static void Main()
        {
            string username;
            string password;

            List<staff> staffListObjects = new List<staff>();

            string loginDetailsFileName = "loginDetails.txt";

            if (File.Exists(loginDetailsFileName))
            {


                StreamReader readLoginFile = new StreamReader(loginDetailsFileName);

                Validation validation = new Validation();

                string usernameFromFile = readLoginFile.ReadLine();
      
                Console.WriteLine(usernameFromFile);

                string passwordFromFile = readLoginFile.ReadLine();
                Console.WriteLine(passwordFromFile);

                readLoginFile.Close();

                Console.WriteLine("***** Welcome to Staffing Recruitment *****");


                Console.WriteLine("\nPlease login to continue...\n");


                Console.Write("Enter username: ");
                username = Console.ReadLine();

                Console.Write("Enter Password: ");
                password = Console.ReadLine();


                if (username == usernameFromFile && password == passwordFromFile)
                {
                    Console.WriteLine("Access granted!");

                    int option = 0;


                    do
                    {
                        staff newStaffMember = new staff();


                        Console.WriteLine("\n1. Create new Staff Detail");
                        Console.WriteLine("2. View all Staffs Details");
                        Console.WriteLine("3. Assign Staff to Shifts");
                        Console.WriteLine("4. View all Staff shifts");
                        Console.WriteLine("5. View Staff Payments");
                        Console.WriteLine("6. Update Staff Record");
                        Console.WriteLine("7. Delete stuff Record");
                        Console.WriteLine("8. Exit Program!");

                        Console.Write("\nEnter option: ");
                        option = int.Parse(Console.ReadLine());

                        //use robust function for this purpose
                        while(option < 0 || option > 9)
                        {
                            Console.WriteLine("Wrong Input! ");

                        }

                        switch (option)
                        {
                            case 1:

                                string firstName, lastName,
                                phoneNumber, emailAddress, address, gender, NInumber;

                                int age;
                                
                                Console.WriteLine("\nEnter Staff Details...\n");

                                Console.Write("Enter first Name: ");
                                firstName = Console.ReadLine();

                                Console.Write("Enter last Name: ");
                                lastName = Console.ReadLine();
                                
                                ////// PHONE NUMBER VALIDATION
                                Console.Write("Enter Phone Number: ");
                                phoneNumber = Console.ReadLine();

                                while (!validation.checkPhoneNumber(phoneNumber))
                                {
                                    Console.WriteLine("Phone number should be at 11 digits, Example: 07xxxxxxxxx!");
                                    Console.Write("Enter Phone Number: ");
                                    phoneNumber = Console.ReadLine();
                                }

                                //for (int i = 0; i < phoneNumber.Length; ++i)
                                //{
                                //    if (!Char.IsDigit(phoneNumber[i]))
                                //    {
                                //        throw new ArgumentException("Phone numbers may only contain digits.");
                                //    }
                                //}

                                ///////// EMAIL ADDREDSS VALICATION
                                Console.Write("Enter email address: ");
                                emailAddress = Console.ReadLine();

                                //while (!emailAddress.Contains("@"))
                                //{

                                //}

                                Console.Write("Enter home address: ");
                                address = Console.ReadLine();

                                Console.Write("Enter gender: ");
                                gender = Console.ReadLine();

                                Console.Write("Enter age: ");
                                age = int.Parse(Console.ReadLine());


                                ///// Your National Insurance number is 9 digits long and starts
                                ///with two letters, followed by six numbers and one letter e.g. AB123456C
                                Console.Write("Enter National Insurance number: ");
                                NInumber = Console.ReadLine();

                                while (!validation.checkNINumber(NInumber))
                                {
                                    Console.WriteLine("Phone number should be at 9 digits!");
                                    Console.Write("Enter National Insurance number: ");
                                    NInumber = Console.ReadLine();
                                }

                                newStaffMember.setStaffDetails(firstName, lastName, phoneNumber,
                                    emailAddress, address, gender, age, NInumber);

                                Console.WriteLine(newStaffMember.displayStaffDetails());

                                File.AppendAllText("staffDetails.txt", newStaffMember.displayStaffDetails());

                                Console.WriteLine("you are good to go!");



                                break;

                            case 2:

                                if (File.Exists("staffDetails.txt"))
                                {
                                    string[] staffDetailsFromFile = File.ReadAllLines("staffDetails.txt");

                                    for(int i=0; i<staffDetailsFromFile.Length; i++)
                                    {
                                        string staffDetailsSeparator = staffDetailsFromFile[i];
                                        string[] individualStaffData = staffDetailsSeparator.Split(',');

                                        staff staffObject = new staff();

                                        staffObject.setStaffDetails(individualStaffData[0], individualStaffData[1],
                                            individualStaffData[2], individualStaffData[3], individualStaffData[4],
                                            individualStaffData[5], int.Parse(individualStaffData[6]), individualStaffData[7]);

                                        staffListObjects.Add(staffObject);

                                    }


                                    

                                    Console.WriteLine();

                                    for (int x = 0; x < staffListObjects.Count; x++)
                                    {

                                        Console.WriteLine(staffListObjects[x].displayStaffDetails());

                                    }


                                }
                                else
                                {
                                    Console.WriteLine("File does not exists, please check your folder!");
                                }
                                break;

                            case 3:
                                break;

                            case 4:
                                break;

                            case 5:
                                break;

                            case 6:
                                break;

                            case 7:
                                break;


                        }


                    } while (option != 8);
                }
                else
                {
                    Console.WriteLine("\nUsername or Password doesn't match, closing program!");
                }


            }
            else
            {
                Console.WriteLine("Cannot Procced Login file doesn't exits, closing program!");
            }

            Console.ReadKey();

        }
    }
}
