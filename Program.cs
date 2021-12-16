using System;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Runtime.Serialization.Formatters.Binary;
using System.Linq;

namespace Staff_Management_System
{

    using System;
    using System.ComponentModel.DataAnnotations; //C# library to validate email address

    [Serializable]

    class Customer
    {
        protected string customerFirstName;
        protected string customerLastName;
        protected string customerPhoneNumber;
        private string emailAddress;
        private string gender;


        public string getCustomerFirstName()
        {
            return this.customerFirstName;
        }

        public string getCustomerLastName()
        {
            return this.customerLastName;
        }

        public string getCustomerPhoneNumber()
        {
            return this.customerPhoneNumber;
        }

        public void setCustomerDetails(string customerFirstName, string customerLastName,
            string phoneNumber, string emailAddress, string gender)
        {
            this.customerFirstName = customerFirstName;
            this.customerLastName = customerLastName;
            this.customerPhoneNumber = phoneNumber;
            this.emailAddress = emailAddress;
            this.gender = gender;
        }

        public string displayCustomerDetail()
        {
            return this.customerFirstName + "," + this.customerLastName + "," +
                this.customerPhoneNumber + "," + this.emailAddress + "," + this.gender + "\n";
        }
    }

    [Serializable]

    class Booking : Customer
    {
        private string bookingDate;
        private string typeOfService;
        private bool serviceCompleted;
        private double paymentMade;

        public void setBookingDetail(string customerFirstName, string customerLastName,
            string customerPhoneNumber, string bookingDate, string typeOfService,
            bool serviceCompleted, double paymentMade)
        {
            this.customerFirstName = customerFirstName;
            this.customerLastName = customerLastName;
            this.customerPhoneNumber = customerPhoneNumber;
            this.bookingDate = bookingDate;
            this.typeOfService = typeOfService;
            this.serviceCompleted = serviceCompleted;
            this.paymentMade = paymentMade;
        }

        public string displayBookingDetails()
        {
            return this.customerFirstName + "," + this.customerLastName + "," +
                this.customerPhoneNumber + "," + this.bookingDate + "," +
                this.typeOfService + "," + this.serviceCompleted + "," +
                this.paymentMade + "\n";
        }
    }

    [Serializable]

    class Staff
    {
        protected string staffFirstName;
        protected string staffLastName;
        private string phoneNumber;
        private string emailAddress;
        private string address;
        private string gender;
        protected string dateOfBirth;
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

    [Serializable]

    //Derived Class inherits Staff class attributes for Staff First Name and Last Name
    class StaffWeeklyJob : Staff
    {
        private string JobDescribtion;
        private DateTime workDate;
        private double hoursWorked;
        private double hourlyPay;
        private double payment;


        public double getPayment()
        {
            return this.payment = payment;
        }

        public void setJobRecord(string staffFirstName, string staffLastName, string dateOfBirth,
            string JobDescribtion, DateTime workDate, double hoursWorked, double hourlyPay, double payment)
        {
            this.staffFirstName = staffFirstName;
            this.staffLastName = staffLastName;
            this.dateOfBirth = dateOfBirth;
            this.JobDescribtion = JobDescribtion;
            this.workDate = workDate;
            this.hoursWorked = hoursWorked;
            this.hourlyPay = hourlyPay;
            this.payment = payment;
        }

        public DateTime getWorkDate()
        {
            return this.workDate;
        }


        public string displayStaffWeekyJobRecord()
        {
            return this.staffFirstName + "," + this.staffLastName + "," + this.dateOfBirth + "," +
                this.JobDescribtion + "," + this.workDate.ToShortDateString() + "," +
                this.hoursWorked + "," + this.hourlyPay + "," + this.payment + "\n";

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

        public double validateDoubleInput(string userInput)
        {
            Console.Write(userInput);

            do
            {
                try
                {
                    userInput = Console.ReadLine();
                    return double.Parse(userInput);
                }
                catch
                {
                    Console.Write("Sorry, float number is accepted, please try again: ");
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
            string dobPattern = @"^\d{2}?(/)?\d{2}?(/)?\d{4}$";


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

        public static void displayStaffManagementMenu()
        {
            Console.WriteLine("\n1. Create New Staff Detail");
            Console.WriteLine("2. View all Staffs Details");
            Console.WriteLine("3. Store Staff Job Details");
            Console.WriteLine("4. View all Staff shifts and payments");
            Console.WriteLine("5. Find Total Payment made to Staff");
            Console.WriteLine("6. Find Staff Records Details by date range");
            Console.WriteLine("7. Update Staff Record");
            Console.WriteLine("8. Delete Staff Record");
            Console.WriteLine("9. Update Login Details");
            Console.WriteLine("10. View Total Revenue and Expenses");
            Console.WriteLine("0. Exit Program!");
        }


        public static void displayAppointmentManagementMenu()
        {
            Console.WriteLine("\n1. Create New Customer Detail");
            Console.WriteLine("2. View all Customer Details");
            Console.WriteLine("3. Make a Booking for a Customer");
            Console.WriteLine("4. Complete Booking");
            Console.WriteLine("4. View all Bookings");
            Console.WriteLine("5. View Bookings with date range");
            Console.WriteLine("6. Cancel Booking");
            Console.WriteLine("0. Exit Program!");

        }

        static void Main()
        {
            int bookingManagementOption = -1;

            Console.WriteLine("***** Welcome to Saloon Booking Management System *****");


            Console.WriteLine("1. Store Customer Details and manage booking");
            Console.WriteLine("2. Admin Panel - Staff Management");

            Console.Write("Enter your option: ");
            int initialOption = int.Parse(Console.ReadLine());

            if(initialOption == 1)
            {
                do
                {

                    displayAppointmentManagementMenu();
                    bookingManagementOption = int.Parse(Console.ReadLine());

                    switch (bookingManagementOption)
                    {
                        case 1:
                        //Create new Customer Detail


                            break;

                        case 2:
                        //View all Customer Records


                            break;

                        case 3:
                        //Create new Customer Booking

                            break;

                        case 4:
                        //Complete Booking

                            break;

                        case 5:
                        //View All Customer Bookings

                            break;

                        case 6:
                            //Cancel Booking

                            break;
                    }
                } while (bookingManagementOption != 0);
                
            }


            else if(initialOption == 2)
            { 
            string username;
            string password;
            int attempts = 0;
            int maxAttempts = 0;
            string staffJobRecordsFileName = "staffJobRecords.dat";
            bool staffJobRecordsFileNameExists = false;


            Staff staffJobRec = new Staff();


            StaffWeeklyJob weekyJob = new StaffWeeklyJob();
            List<StaffWeeklyJob> weekyJobList = new List<StaffWeeklyJob>();



            if (File.Exists(staffJobRecordsFileName))
            {
                BinaryFormatter bf1 = new BinaryFormatter();

                // Open the file storing the binary data.
                FileStream file1 = File.OpenRead(staffJobRecordsFileName);

                weekyJobList = (List<StaffWeeklyJob>)bf1.Deserialize(file1);

                file1.Close();

                staffJobRecordsFileNameExists = true;
            }

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

                        int staffManagementOptions = -1;

                        staffObj.storeStaffRecordInList(staffListObjects);



                        do
                        {
                            Staff newStaffMember = new Staff();

                                displayStaffManagementMenu();
                            

                            staffManagementOptions = validator.validateIntegerInput("\nEnter option: ");
                            //option = int.Parse(Console.ReadLine());

                            //use robust function for this purpose
                            while (staffManagementOptions < 0 || staffManagementOptions > 9)
                            {
                                Console.WriteLine("Wrong Input! ");

                            }

                            switch (staffManagementOptions)
                            {
                                case 1:

                                    string firstName, lastName, phoneNumber,
                                    emailAddress, address, dateOfBirth, gender, NInumber;


                                    Console.WriteLine("\nEnter Staff Details...\n");

                                    firstName = validator.validateStringInput("Enter first Name: ");


                                    lastName = validator.validateStringInput("Enter last Name: ");

                                    phoneNumber = validator.validateStringInput("Enter Phone Number: ");


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
                                        Console.WriteLine("\nPlease enter a valid Birthdate like (dd/mm/yyyy)!\n");
                                        dateOfBirth = validator.validateStringInput("Enter Date of Birth: ");
                                    }


                                    ///// Your National Insurance number is 9 digits long and starts
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


                                    int staffObjectIndexNumber_3 = staffJobRec.getStaffRecord(staffListObjects, identifyStaffFirstName_3,
                                        identifyStaffLastName_3, identifyStaffDob_3).Item1;

                                    bool foundStaffObj_3 = staffJobRec.getStaffRecord(staffListObjects, identifyStaffFirstName_3,
                                        identifyStaffLastName_3, identifyStaffDob_3).Item2;

                                    if (foundStaffObj_3)
                                    {


                                        Console.WriteLine("\nIndex Num: " + staffObjectIndexNumber_3);

                                        string staffFName = staffListObjects[staffObjectIndexNumber_3].getFirstName();
                                        string staffLName = staffListObjects[staffObjectIndexNumber_3].getLastName();
                                        string staffDob = staffListObjects[staffObjectIndexNumber_3].getDob();

                                        Console.WriteLine("Staff Name: " + staffFName + " " + staffLName);

                                        string jobDescribtion = validator.validateStringInput("Enter Job Describtion: ");

                                        string tempStartDate = validator.validateStringInput("Enter Work Date: ");
                                        DateTime workDate = DateTime.Parse(tempStartDate);

                                        double hoursWorked = validator.validateDoubleInput("Enter Hours worked: ");

                                        double hourlyPay = validator.validateDoubleInput("Enter Hourly Pay: ");


                                        double totalPayment = hoursWorked * hourlyPay;

                                        Console.WriteLine("Total payment made to staff: " + totalPayment);


                                        weekyJob.setJobRecord(staffFName, staffLName, staffDob,
                                            jobDescribtion, workDate, hoursWorked, hourlyPay, totalPayment);

                                        weekyJobList.Add(weekyJob);

                                        BinaryFormatter bfObject = new BinaryFormatter();

                                        // Create and open a binary file for storing the data.
                                        FileStream file = File.Create(staffJobRecordsFileName);

                                        // Write the data to the file, converting to binary in the process.
                                        bfObject.Serialize(file, weekyJobList);

                                        // Close the file.
                                        file.Close();

                                        if (!staffJobRecordsFileNameExists)
                                        {
                                            BinaryFormatter bf1 = new BinaryFormatter();

                                            // Open the file storing the binary data.
                                            FileStream file1 = File.OpenRead(staffJobRecordsFileName);

                                            weekyJobList = (List<StaffWeeklyJob>)bf1.Deserialize(file1);

                                            file1.Close();
                                        }


                                    }
                                    else
                                    {
                                        Console.WriteLine("\nSorry, Staff Record not found!");
                                    }




                                    break;

                                case 4:
                                    //Reading from Binary File

                                    if (weekyJobList.Count > 0)
                                    {
                                        Console.WriteLine("\nDisplaying Job Record in desending order according to payments made....\n");

                                        List<StaffWeeklyJob> staffs = weekyJobList.OrderByDescending(payment => payment.getPayment()).ToList();
                                        foreach (StaffWeeklyJob staff in staffs)
                                        {
                                            Console.WriteLine(staff.displayStaffWeekyJobRecord());
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("\nSorry no Staff Job Record found!\n");
                                    }





                                    break;

                                //Total Payment of Made to Staff
                                case 5:

                                    if (weekyJobList.Count > 0)
                                    {

                                        double totalPaymentMadeToStaff = 0;
                                        bool foundStaffRecord = false;

                                        string identifyStaffFirstName_5 = validator.validateStringInput("Enter Staff First Name: ");
                                        string identifyStaffLastName_5 = validator.validateStringInput("Enter Staff Last Name: ");
                                        string identifyStaffDob_5 = validator.validateStringInput("Enter Staff Date of Birth: ");

                                        Console.WriteLine();

                                        for (int i = 0; i < weekyJobList.Count; i++)
                                        {
                                            if (identifyStaffFirstName_5 == weekyJobList[i].getFirstName() &&
                                                identifyStaffLastName_5 == weekyJobList[i].getLastName() &&
                                                identifyStaffDob_5 == weekyJobList[i].getDob())
                                            {
                                                Console.WriteLine(weekyJobList[i].displayStaffWeekyJobRecord());
                                                totalPaymentMadeToStaff += weekyJobList[i].getPayment();
                                                foundStaffRecord = true;
                                            }

                                        }
                                        if (foundStaffRecord)
                                        {
                                            Console.WriteLine("\nTotal Payment Made to " + identifyStaffFirstName_5 +
                                               " " + identifyStaffLastName_5 + " : " + totalPaymentMadeToStaff);

                                        }
                                        else
                                        {
                                            Console.WriteLine("Sorry, No Staff Record found!");
                                        }

                                    }
                                    else
                                    {
                                        Console.WriteLine("\nSorry no Staff Job Record found!\n");
                                    }
                                    break;

                                //How to find objects from Date range
                                case 6:

                                    if (weekyJobList.Count > 0)
                                    {

                                        string tempFirstDate = validator.validateStringInput("Enter First Date range: ");
                                        string tempSecondDate = validator.validateStringInput("Enter Second Date range: ");

                                        DateTime firstDate = DateTime.Parse(tempFirstDate);
                                        DateTime secondDate = DateTime.Parse(tempSecondDate);

                                        List<StaffWeeklyJob> findStaffRecord = weekyJobList.Where(
                                            find => find.getWorkDate() >= firstDate && find.getWorkDate() <= secondDate).ToList();

                                        foreach (StaffWeeklyJob staff in findStaffRecord)
                                        {
                                            Console.WriteLine(staff.displayStaffWeekyJobRecord());
                                        }
                                    }
                                    else
                                    {
                                        Console.WriteLine("\nSorry no Staff Job Record found!\n");
                                    }
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
                                            foundStaffObj_8 = true;
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
                                default:
                                    Console.WriteLine("Invalid Input, Please try again!");
                                    break;


                            }


                        } while (staffManagementOptions != 0);

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


        }
            Console.WriteLine("\nClosing Program...");

            Console.ReadKey();

        }
    }
}
