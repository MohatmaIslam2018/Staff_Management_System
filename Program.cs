using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;

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
        private string dateOfBirth;
        private string NInumber;

        public Staff()
        {

        }

        public void setFirstName(string staffFirstName)
        {
            this.staffFirstName = staffFirstName;
        }
        public void setLastName(string staffLastName)
        {
            this.staffLastName = staffLastName;
        }
        public void setPhoneNumber(string phoneNumber)
        {
            this.phoneNumber = phoneNumber;
        }
        public void setEmailAddress(string emailAddress)
        {
            this.emailAddress = emailAddress;
        }
        public void setAddress(string address)
        {
            this.address = address;
        }
        public void setGender(string gender)
        {
            this.gender = gender;
        }
        public void setDob(string dateOfBirth)
        {
            this.dateOfBirth = dateOfBirth;
        }

        public void setNInumber(string NInumber)
        {
            this.NInumber = NInumber;
        }

        public string getFirstName()
        {
            return this.staffFirstName;
        }
        public string getLastName()
        {
            return this.staffLastName;
        }
        public string getDob()
        {
            return this.dateOfBirth;
        }

        public void setStaffDetails(string firstName, string lastName, string phoneNumber,
        string emailAddress, string address, string gender, string dateOfBirth, string NInumber)
        {
            this.staffFirstName = firstName;
            this.staffLastName = lastName;
            this.phoneNumber = phoneNumber;
            this.emailAddress = emailAddress;
            this.address = address;
            this.gender = gender;
            this.dateOfBirth = dateOfBirth;
            this.NInumber = NInumber;
        }


        public string displayStaffDetails()
        {
            return this.staffFirstName + "," + this.staffLastName + "," +
                this.phoneNumber + "," + this.emailAddress + "," +
                this.address + "," + this.gender + "," +
                this.dateOfBirth + "," + this.NInumber + "\n";
        }

        public (int, bool) getStaffRecord(List<Staff> staffListObjects, string identifyStaffFirstName,
            string identifyStaffLastName, string identifyStaffDob)
        {

            int staffObjectIndexNumber = 0;
            bool foundStaffObj = false;


            for (int i = 0; i < staffListObjects.Count; i++)
            {
                if (identifyStaffFirstName == staffListObjects[i].getFirstName() &&
                    identifyStaffLastName == staffListObjects[i].getLastName() &&
                    identifyStaffDob == staffListObjects[i].getDob())
                {
                    staffObjectIndexNumber = i;
                    foundStaffObj = true;
                }
            }

            return (staffObjectIndexNumber, foundStaffObj);
        }

        public void storeStaffRecordInList(List<Staff> staffListObjects)
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
                        individualStaffData[5], individualStaffData[6], individualStaffData[7]);

                    staffListObjects.Add(staffObject);
                }

            }
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


        public bool checkDob(string dobString)
        {
            string dobPattern = @"^\d{2}?((-)|(/))?\d{2}?((-)|(/))?\d{4}$";


            Regex dobRegex = new Regex(dobPattern);

            if (!dobRegex.IsMatch(dobString))
            {
                return false;
            }
            return true;

        }

        public bool checkPhoneNumber(string phoneNum)
        {
            string regPatternPhoneNumber = @"^((\+44)|(0))?\d{4}?\d{6}$";

            Regex rg = new Regex(regPatternPhoneNumber);

            if (!rg.IsMatch(phoneNum))
            {
                return false;
            }
            return true;

        }



        public bool checkNINumber(string NInum)
        {
            string regPattern = @"^\s*[A-Z]{2}(?:\s*\d\s*){6}[A-Z]?\s*$";

            Regex rg = new Regex(regPattern);

            if (!rg.IsMatch(NInum))
            {
                return false;
            }
            return true;
        }

    }

    [Serializable]
    class Program
    {


        public static int getMaxLoginAttempts(string getInput)
        {
            string[] split = getInput.Split(':');

            int maxAttempts = int.Parse(split[1]);

            return maxAttempts;

        }


        public static (bool, int) readLoginDetails(string loginDetailsFileName, string userName, string password)
        {


            StreamReader readLoginFile = new StreamReader(loginDetailsFileName);


            string usernameFromFile = readLoginFile.ReadLine();

            string passwordFromFile = readLoginFile.ReadLine();

            int storeMaxAttempts = getMaxLoginAttempts(readLoginFile.ReadLine());

            readLoginFile.Close();

            if (usernameFromFile == userName && passwordFromFile == password)
            {
                return (true, storeMaxAttempts);
            }

            return (false, storeMaxAttempts);
        }

        public static void OverrideOrCreateLoginFile(Validator validator, string loginDetailsFileName)
        {
            string updatedUsername = validator.validateStringInput("Enter new username: ");
            string updatedPassword = validator.validateStringInput("Enter new password: ");
            int updatedMaximumLoginAttemps = validator.validateIntegerInput("Enter maximum attempts: ");

            StreamWriter updateLoginDetailsToFile = new StreamWriter(loginDetailsFileName);

            updateLoginDetailsToFile.WriteLine(updatedUsername);
            updateLoginDetailsToFile.WriteLine(updatedPassword);
            updateLoginDetailsToFile.WriteLine("MaximumLoginAttemps:" + updatedMaximumLoginAttemps);


            updateLoginDetailsToFile.Close();

            Console.WriteLine("\nNew Login Details has been updated successfully!\n");


        }

        static void Main()
        {
            string username;
            string password;
            int attempts = 0;
            int maxAttempts = 0;

            bool exitProgam = true;
            
            Dictionary<string, int> staffDictionaryShift = new Dictionary<string, int>();

            List<string> staffListObjectsShift = new List<string>(staffDictionaryShift.Keys);


            List<Staff> staffListObjects = new List<Staff>();

            Staff staffObj = new Staff();

            Validator validator = new Validator();

            string loginDetailsFileName = "loginDetails.txt";

            if (!File.Exists(loginDetailsFileName))
            {
                Console.WriteLine("It looks like Login record does not exits!\n" +
                    "\nInput username and password to continue... \n");
                OverrideOrCreateLoginFile(validator, loginDetailsFileName);

            }


            if (File.Exists(loginDetailsFileName))
            {

                Console.WriteLine("***** Welcome to Staffing Recruitment *****");

                do
                {
                    Console.WriteLine("\nPlease login to continue...\n");


                    //Console.Write("Enter username: ");
                    //username = Console.ReadLine();

                    username = validator.validateStringInput("Enter username: ");

                    password = validator.validateStringInput("Enter Password: ");
                    //password = Console.ReadLine();

                    maxAttempts = readLoginDetails(loginDetailsFileName, username, password).Item2;



                    if (readLoginDetails(loginDetailsFileName, username, password).Item1)
                    {
                        Console.WriteLine("\nAccess granted!\n");

                        int option = -1;

                        staffObj.storeStaffRecordInList(staffListObjects);


                        do
                        {
                            Staff newStaffMember = new Staff();


                            Console.WriteLine("\n1. Create new Staff Detail");
                            Console.WriteLine("2. View all Staffs Details");
                            Console.WriteLine("3. Assign Staff to Shifts");   //Dictionary and List
                            Console.WriteLine("4. View all Staff shifts");
                            Console.WriteLine("5. Approved Staff Shifts"); //Dictionary and List
                            Console.WriteLine("6. View Staff Payments");
                            Console.WriteLine("7. Update stuff Record");
                            Console.WriteLine("8. Delete stuff Record");
                            Console.WriteLine("9. Update Login Details");
                            Console.WriteLine("0. Exit Program!");

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

                                    string firstName, lastName, phoneNumber,
                                    emailAddress, address, dateOfBirth, gender, NInumber;


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
                                        Console.WriteLine("Please include valid UK number!");
                                        phoneNumber = validator.validateStringInput("Enter Phone Number: ");
                                    }

                                    do
                                    {
                                        emailAddress = validator.validateStringInput("Enter email address: ");

                                    } while (new EmailAddressAttribute().IsValid(emailAddress) == false);


                                    address = validator.validateStringInput("Enter home address: ");
                                    //address = Console.ReadLine();

                                    gender = validator.validateStringInput("Enter gender: ");
                                    //gender = Console.ReadLine();

                                    dateOfBirth = validator.validateStringInput("Enter Date of Birth: ");

                                    while (!validator.checkDob(dateOfBirth))
                                    {
                                        Console.WriteLine("\nPlease enter a valid Birthdate like (dd/mm/yyyy) or (dd-mm-yyy) !\n");
                                        dateOfBirth = validator.validateStringInput("Enter Date of Birth: ");
                                    }


                                    ///// Your National Insurance number is 9 digits long and starts
                                    ///with two letters, followed by six numbers and one letter e.g. AB123456C
                                    NInumber = validator.validateStringInput("Enter National Insurance number: ");
                                    //NInumber = Console.ReadLine();

                                    while (!validator.checkNINumber(NInumber))
                                    {
                                        Console.WriteLine("Please input valid National Insurance number!");
                                        NInumber = validator.validateStringInput("Enter National Insurance number: ");
                                    }

                                    newStaffMember.setStaffDetails(firstName, lastName, phoneNumber,
                                        emailAddress, address, gender, dateOfBirth, NInumber);

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
                                    staffObj.storeStaffRecordInList(staffListObjects);

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


                                    string identifyStaffFirstName_3 = validator.validateStringInput("Enter Staff First Name: ");
                                    string identifyStaffLastName_3 = validator.validateStringInput("Enter Staff Last Name: ");
                                    string identifyStaffDob_3 = validator.validateStringInput("Enter Staff Date of Birth: ");


                                    int staffObjectIndexNumber_3 = newStaffMember.getStaffRecord(staffListObjects, identifyStaffFirstName_3,
                                        identifyStaffLastName_3, identifyStaffDob_3).Item1;

                                    bool foundStaffObj1 = newStaffMember.getStaffRecord(staffListObjects, identifyStaffFirstName_3,
                                        identifyStaffLastName_3, identifyStaffDob_3).Item2;



                                    break;

                                case 4:
                                    break;

                                case 5:
                                    break;

                                case 6:
                                    break;

                                case 7:



                                    Console.WriteLine("\n*******************\n");

                                    string identifyStaffFirstName_7 = validator.validateStringInput("Enter Staff First Name: ");
                                    string identifyStaffLastName_7 = validator.validateStringInput("Enter Staff Last Name: ");
                                    string identifyStaffDob_7 = validator.validateStringInput("Enter Staff Date of Birth: ");


                                    int staffObjectIndexNumber_7 = newStaffMember.getStaffRecord(staffListObjects, identifyStaffFirstName_7,
                                        identifyStaffLastName_7, identifyStaffDob_7).Item1;

                                    bool foundStaffObj_7 = newStaffMember.getStaffRecord(staffListObjects, identifyStaffFirstName_7,
                                        identifyStaffLastName_7, identifyStaffDob_7).Item2;


                                    if (foundStaffObj_7)
                                    {
                                        string updatedFirstName, updatedLastName, updatedPhoneNumber,
                                        updatedEmailAddress, updatedAddress, updatedDateOfBirth,
                                        updatedGender, updatedNInumber;

                                        //Console.WriteLine("Staff Index Number: " + staffObjectIndexNumber);

                                        Console.WriteLine("\nShowing selected Staff Record details....\n");

                                        Console.WriteLine(staffListObjects[staffObjectIndexNumber_7].displayStaffDetails());

                                        int choice = 0;


                                        Console.WriteLine("\nSelect Any option to update Staff Record...\n");

                                        Console.WriteLine("1. Update First Name");
                                        Console.WriteLine("2. Update Last Name");
                                        Console.WriteLine("3. Update Phone Number");
                                        Console.WriteLine("4. Update Email Address");
                                        Console.WriteLine("5. Update Address");
                                        Console.WriteLine("6. Update Gender");
                                        Console.WriteLine("7. Update Date of Birth");
                                        Console.WriteLine("8. Update National Insurance Number\n");

                                        choice = validator.validateIntegerInput("option: ");

                                        while (choice != 1 && choice != 2 && choice != 3 && choice != 4 &&
                                            choice != 5 && choice != 6 && choice != 7 && choice != 8)
                                        {
                                            Console.WriteLine("Invalid Input!, please enter enter your option again!");
                                            choice = validator.validateIntegerInput("option: ");
                                        }

                                        switch (choice)
                                        {
                                            case 1:

                                                updatedFirstName = validator.validateStringInput("Enter updated Staff First Name: ");
                                                staffListObjects[staffObjectIndexNumber_7].setFirstName(updatedFirstName);

                                                break;

                                            case 2:

                                                updatedLastName = validator.validateStringInput("Enter updated Staff Last Name: ");
                                                staffListObjects[staffObjectIndexNumber_7].setLastName(updatedLastName);

                                                break;

                                            case 3:

                                                updatedPhoneNumber = validator.validateStringInput("Enter updated Phone Number: ");

                                                while (!validator.checkPhoneNumber(updatedPhoneNumber))
                                                {
                                                    Console.WriteLine("Please include valid UK number!");
                                                    updatedPhoneNumber = validator.validateStringInput("Enter updated Phone Number: ");
                                                }

                                                staffListObjects[staffObjectIndexNumber_7].setPhoneNumber(updatedPhoneNumber);

                                                break;

                                            case 4:

                                                updatedEmailAddress = validator.validateStringInput("Enter updated Email Address: ");
                                                staffListObjects[staffObjectIndexNumber_7].setEmailAddress(updatedEmailAddress);

                                                break;

                                            case 5:


                                                do
                                                {
                                                    updatedAddress = validator.validateStringInput("Enter updated Address: ");

                                                } while (new EmailAddressAttribute().IsValid(updatedAddress) == false);

                                                staffListObjects[staffObjectIndexNumber_7].setAddress(updatedAddress);

                                                break;

                                            case 6:

                                                updatedGender = validator.validateStringInput("Enter updated gender: ");
                                                staffListObjects[staffObjectIndexNumber_7].setGender(updatedGender);

                                                break;
                                            case 7:

                                                updatedDateOfBirth = validator.validateStringInput("Enter updated Date of Birth: ");

                                                while (!validator.checkDob(updatedDateOfBirth))
                                                {
                                                    Console.WriteLine("\nPlease enter a valid Birthdate like (dd/mm/yyyy) or (dd-mm-yyy) !\n");
                                                    updatedDateOfBirth = validator.validateStringInput("Enter updated Date of Birth: ");
                                                }

                                                staffListObjects[staffObjectIndexNumber_7].setDob(updatedDateOfBirth);

                                                break;
                                            case 8:

                                                updatedNInumber = validator.validateStringInput("Enter updated National Insurance Number: ");

                                                while (!validator.checkNINumber(updatedNInumber))
                                                {
                                                    Console.WriteLine("Please input valid National Insurance number!");
                                                    updatedNInumber = validator.validateStringInput("Enter updated National Insurance Number: ");

                                                }

                                                staffListObjects[staffObjectIndexNumber_7].setNInumber(updatedNInumber);

                                                break;

                                        }



                                        Console.WriteLine("\nNew Staff Record has been updated successfully!");
                                        Console.WriteLine(staffListObjects[staffObjectIndexNumber_7].displayStaffDetails());

                                        Console.WriteLine("\nTesting...\n");
                                        StreamWriter writeToFile = new StreamWriter("staffDetails.txt");

                                        for (int x = 0; x < staffListObjects.Count; x++)
                                        {
                                            Console.WriteLine(staffListObjects[x].displayStaffDetails());

                                            writeToFile.Write(staffListObjects[x].displayStaffDetails());
                                        }

                                        writeToFile.Close();

                                        Console.WriteLine("\nTaking you back to main menu...\n");

                                        //Save the new list objects into a file
                                        //Delete all list objects
                                        //Read all the staff records from the file and store into a list

                                    }

                                    else
                                    {
                                        Console.WriteLine("\nSorry, Staff Record Not found! ");
                                    }
                                    break;

                                case 8:

                                    //Deleting staff Records
                                    Console.WriteLine();

                                    string indentifyStaffFirstName_8 = validator.validateStringInput("Enter Staff First Name: ");
                                    string identifyStaffLastName_8 = validator.validateStringInput("Enter Staff Last Name: ");
                                    string identifyStaffDob_8 = validator.validateStringInput("Enter Staff Date of Birth: ");


                                    int staffObjectIndexNum_8 = newStaffMember.getStaffRecord(staffListObjects, indentifyStaffFirstName_8,
                                        identifyStaffLastName_8, identifyStaffDob_8).Item1;

                                    bool foundStaffObj_8 = newStaffMember.getStaffRecord(staffListObjects, indentifyStaffFirstName_8,
                                        identifyStaffLastName_8, identifyStaffDob_8).Item2;

                                    for (int i = 0; i < staffListObjects.Count; i++)
                                    {
                                        if (indentifyStaffFirstName_8 == staffListObjects[i].getFirstName() &&
                                            identifyStaffLastName_8 == staffListObjects[i].getLastName() &&
                                            identifyStaffDob_8 == staffListObjects[i].getDob())
                                        {
                                            staffObjectIndexNum_8 = i;
                                            foundStaffObj1 = true;
                                        }
                                    }


                                    if (foundStaffObj_8)
                                    {
                                        Console.WriteLine("Staff Index Number: " + staffObjectIndexNum_8);

                                        Console.WriteLine("\nShowing selected Staff Record...\n");

                                        Console.WriteLine(staffListObjects[staffObjectIndexNum_8].displayStaffDetails());

                                        string removeStaffRecord = validator.validateStringInput("Are you sure," +
                                            " you want to delete the selected Staff Record (yes/no): ");

                                        if (removeStaffRecord == "yes")
                                        {


                                            staffListObjects.RemoveAt(staffObjectIndexNum_8);

                                            Console.WriteLine("Above Staff Record has been deleted! ");

                                            //Overwriting the staff record data since a record has been deleted from Staff List


                                            Console.WriteLine("\nOverriding staff records.......\n");

                                            StreamWriter writeToFile1 = new StreamWriter("staffDetails.txt");

                                            Console.WriteLine("Total Staff Records: " + staffListObjects.Count);

                                            for (int x = 0; x < staffListObjects.Count; x++)
                                            {
                                                Console.WriteLine(staffListObjects[x].displayStaffDetails());

                                                writeToFile1.Write(staffListObjects[x].displayStaffDetails());
                                            }

                                            writeToFile1.Close();

                                        }
                                        else if (removeStaffRecord == "no")
                                        {
                                            Console.WriteLine("\nCancelled Deletion of Staff Record!\n");
                                        }
                                        else
                                        {
                                            Console.WriteLine("\nWrong Input!\n");
                                        }


                                    }

                                    else
                                    {
                                        Console.WriteLine("Sorry, Staff Record Not found! ");
                                    }

                                    break;

                                case 9:

                                    username = validator.validateStringInput("Enter username: ");

                                    password = validator.validateStringInput("Enter Password: ");
                                    //password = Console.ReadLine();


                                    if (readLoginDetails(loginDetailsFileName, username, password).Item1)
                                    {

                                        OverrideOrCreateLoginFile(validator, loginDetailsFileName);

                                    }
                                    else
                                    {
                                        Console.WriteLine("Sorry, username and password does not match!");
                                    }

                                    break;


                            }


                        } while (option != 0);

                        exitProgam = false;

                    }
                    else
                    {
                        Console.WriteLine("\nUsername or Password doesn't match, Try again!");
                        attempts++;
                        Console.WriteLine("Login Attempt number: " + attempts);

                        if (attempts == maxAttempts)
                        {
                            Console.WriteLine("\nSorry your maximum login attemps is breached!\n");
                            Console.WriteLine("Closing Program...!");
                            exitProgam = false;
                        }
                    }

                } while (exitProgam);


            }


            else
            {
                Console.WriteLine("Cannot Procced Login file doesn't exits!");

            }


            Console.ReadKey();

        }
    }
}
