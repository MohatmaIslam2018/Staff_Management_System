using System;
using System.IO;
using System.Collections.Generic;

namespace Staff_Management_System
{
    using System;
    using System.ComponentModel.DataAnnotations; //C# library to validate email address

    class Staff
    {
        private string staffFirstName;
        private string staffLastName;
        private string phoneNumber;
        private string emailAddress;
        private string address;
        private string gender;
        private int age;
        private string NInumber;

        public Staff()
        {

        }

        public string getName()
        {
            return this.staffFirstName;
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
            return this.staffFirstName + "," + this.staffLastName + "," +
                this.phoneNumber + ", " + this.emailAddress + "," +
                this.address + "," + this.gender + "," +
                this.age + "," + this.NInumber + "\n";
        }

        public string storeStaffRecordsInFileAfterDeletion()
        {
            return this.staffFirstName + "," + this.staffLastName + "," +
                this.phoneNumber + "," + this.emailAddress + "," +
                this.address + "," + this.gender + "," +
                this.age + "," + this.NInumber;
        }


    }

    class Validator
    {

        public int validateIntegerInput(string userInput)
        {
            Console.Write(userInput);

            do
            {
                try
                {
                    userInput = Console.ReadLine();
                    return int.Parse(userInput);
                }
                catch
                {
                    Console.Write("Sorry, only Integer value is accepted, please try again: ");
                }

            } while (true);

        }

        public string validateStringInput(string stringInput)
        {
            string storeStringInput;
            do
            {
                Console.Write(stringInput);

                storeStringInput = Console.ReadLine();

            } while (storeStringInput == "");

            return storeStringInput;
        }


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

    [Serializable]
    class Program
    {
        public static List<Staff> staffListObjects = new List<Staff>();

        public static void storeStaffRecordInList()
        {
            if (File.Exists("staffDetails.txt"))
            {
                string[] staffDetailsFromFile = File.ReadAllLines("staffDetails.txt");

                //Console.WriteLine("Total Staff Details stored in File: " + staffDetailsFromFile.Length);

                for (int i = 0; i < staffDetailsFromFile.Length; i++)
                {
                    string staffDetailsSeparator = staffDetailsFromFile[i];
                    string[] individualStaffData = staffDetailsSeparator.Split(',');

                    Staff staffObject = new Staff();

                    staffObject.setStaffDetails(individualStaffData[0], individualStaffData[1],
                        individualStaffData[2], individualStaffData[3], individualStaffData[4],
                        individualStaffData[5], int.Parse(individualStaffData[6]), individualStaffData[7]);

                    staffListObjects.Add(staffObject);
                }

            }
        }




        static void Main()
        {
            string username;
            string password;

            //Dictionary<staff, staff> staffDictionary = new Dictionary<staff, staff>();

            string loginDetailsFileName = "loginDetails.txt";

            if (File.Exists(loginDetailsFileName))
            {


                StreamReader readLoginFile = new StreamReader(loginDetailsFileName);

                Validator validator = new Validator();

                string usernameFromFile = readLoginFile.ReadLine();

                Console.WriteLine(usernameFromFile);

                string passwordFromFile = readLoginFile.ReadLine();
                Console.WriteLine(passwordFromFile);

                readLoginFile.Close();

                Console.WriteLine("***** Welcome to Staffing Recruitment *****");


                Console.WriteLine("\nPlease login to continue...\n");


                //Console.Write("Enter username: ");
                //username = Console.ReadLine();

                username = validator.validateStringInput("Enter username: ");

                password = validator.validateStringInput("Enter Password: ");
                //password = Console.ReadLine();


                if (username == usernameFromFile && password == passwordFromFile)
                {
                    Console.WriteLine("\nAccess granted!\n");

                    int option = 0;

                    storeStaffRecordInList();


                    do
                    {
                        Staff newStaffMember = new Staff();


                        Console.WriteLine("\n1. Create new Staff Detail");
                        Console.WriteLine("2. View all Staffs Details");
                        Console.WriteLine("3. Assign Staff to Shifts");   //Dictionary and List
                        Console.WriteLine("4. View all Staff shifts");
                        Console.WriteLine("5. Approved Staff Shifts"); //Dictionary and List
                        Console.WriteLine("6. View Staff Payments");
                        Console.WriteLine("7. Delete stuff Record");
                        Console.WriteLine("8. Exit Program!");

                        option = validator.validateIntegerInput("\nEnter option: ");
                        //option = int.Parse(Console.ReadLine());

                        //use robust function for this purpose
                        while (option < 0 || option > 9)
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

                                firstName = validator.validateStringInput("Enter first Name: ");
                                //firstName = Console.ReadLine();

                                lastName = validator.validateStringInput("Enter last Name: ");
                                //lastName = Console.ReadLine();

                                ////// PHONE NUMBER VALIDATION
                                ///
                                phoneNumber = validator.validateStringInput("Enter Phone Number: ");
                                //phoneNumber = Console.ReadLine();

                                while (!validator.checkPhoneNumber(phoneNumber))
                                {
                                    Console.WriteLine("Phone number should be at 11 digits, Example: 07xxxxxxxxx!");
                                    phoneNumber = validator.validateStringInput("Enter Phone Number: ");
                                }

                                //for (int i = 0; i < phoneNumber.Length; ++i)
                                //{
                                //    if (!Char.IsDigit(phoneNumber[i]))
                                //    {
                                //        throw new ArgumentException("Phone numbers may only contain digits.");
                                //    }
                                //}

                                ///////// EMAIL ADDREDSS VALICATION
                                //emailAddress = Console.ReadLine();

                                do
                                {
                                    emailAddress = validator.validateStringInput("Enter email address: ");

                                }while (new EmailAddressAttribute().IsValid(emailAddress) == false);


                                //while (!emailAddress.Contains("@"))
                                //{

                                //}

                                address = validator.validateStringInput("Enter home address: ");
                                //address = Console.ReadLine();

                                gender = validator.validateStringInput("Enter gender: ");
                                //gender = Console.ReadLine();

                                age = validator.validateIntegerInput("Enter age: ");
                                //age = int.Parse(Console.ReadLine());


                                ///// Your National Insurance number is 9 digits long and starts
                                ///with two letters, followed by six numbers and one letter e.g. AB123456C
                                NInumber = validator.validateStringInput("Enter National Insurance number: ");
                                //NInumber = Console.ReadLine();

                                while (!validator.checkNINumber(NInumber))
                                {
                                    Console.WriteLine("Phone number should be at 9 digits!");
                                    NInumber = validator.validateStringInput("Enter National Insurance number: ");
                                }

                                newStaffMember.setStaffDetails(firstName, lastName, phoneNumber,
                                    emailAddress, address, gender, age, NInumber);

                                Console.WriteLine("\nNew staff Record has been added!");
                                Console.WriteLine(newStaffMember.displayStaffDetails() + "\n");

                                File.AppendAllText("staffDetails.txt", newStaffMember.displayStaffDetails());

                                Console.WriteLine("\nTotal Records in the List (Before Deletiong): " + staffListObjects.Count);


                                //Delete the existing staff records from the List
                                //for (int y=0; y<staffListObjects.Count; y++)
                                //{
                                //    staffListObjects.RemoveAt(y);
                                //}

                                staffListObjects.Clear();

                                Console.WriteLine("\nTotal Records in the List (After Deletion): " + staffListObjects.Count);

                                //Loading the newly added staff records into the staff List
                                storeStaffRecordInList();

                                Console.WriteLine("\nTotal Records in the List (Reading from file): " + staffListObjects.Count);


                                Console.WriteLine("you are good to go!");


                                break;

                            case 2:

                                //Displaying all staff records
                                if (staffListObjects.Count > 0)
                                {

                                    Console.WriteLine();

                                    for (int x = 0; x < staffListObjects.Count; x++)
                                    {

                                        Console.WriteLine(staffListObjects[x].displayStaffDetails());

                                    }

                                }
                                else
                                {
                                    Console.WriteLine("No Staff Record is found!");
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

                                //Deleting staff Records
                                Console.WriteLine();

                                int staffObjectIndexNumber = 0;
                                bool foundStaffObj = false;


                                Console.WriteLine("\n*******************\n");

                                Console.Write("Enter staff Name: ");
                                string identifyStaffName = Console.ReadLine();

                                for (int i = 0; i < staffListObjects.Count; i++)
                                {
                                    if (identifyStaffName == staffListObjects[i].getName())
                                    {
                                        staffObjectIndexNumber = i;
                                        foundStaffObj = true;
                                    }
                                }


                                if (foundStaffObj)
                                {
                                    Console.WriteLine(staffListObjects[staffObjectIndexNumber].displayStaffDetails());

                                    staffListObjects.RemoveAt(staffObjectIndexNumber);

                                    Console.WriteLine("Above Staff Record has been deleted! ");

                                    //Overwriting the staff record data since a record has been deleted from Staff List


                                    Console.WriteLine("\nOverriding staff records.......\n");

                                    StreamWriter writeToFile = new StreamWriter("staffDetails.txt");

                                    Console.WriteLine("Total Staff Records: " + staffListObjects.Count);

                                    for (int x = 0; x < staffListObjects.Count; x++)
                                    {
                                        Console.WriteLine(staffListObjects[x].displayStaffDetails());

                                        writeToFile.WriteLine(staffListObjects[x].storeStaffRecordsInFileAfterDeletion());
                                    }

                                    writeToFile.Close();

                                }

                                else
                                {
                                    Console.WriteLine("Sorry, Staff Record Not found! ");
                                }

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
