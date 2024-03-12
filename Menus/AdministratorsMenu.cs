using System;

namespace HospitalManagementSystem
{
    public static class AdministratorsMenu
    {
        public static void ShowAdministratorsMenu(string administratorID)
        {
            while (true)
            {
                DisplayMenu(TxtHandler.GetAdministratorsName(administratorID));

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                char choice = keyInfo.KeyChar;

                switch (choice)
                {
                    case '1':
                        ListAllDoctors();
                        break;
                    case '2':
                        CheckDoctorDetails();
                        break;
                    case '3':
                        ListAllPatients();
                        break;
                    case '4':
                        CheckPatientDetails();
                        break;
                    case '5':
                        AddDoctor();
                        break;
                    case '6':
                        AddPatient();
                        break;
                    case '7':
                        Console.Clear();
                        Login.ShowLoginMenu();
                        return;
                    case '8':
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid choice. Press any key to try again...");
                        Console.ReadKey(true);
                        break;
                }
            }
        }

        // Displays the administrator menu
        private static void DisplayMenu(string administratorName)
        {
            Console.Clear();
            Helper.DisplayHeading("Administrator Menu");
            Console.WriteLine($"Welcome to DOTNET Hospital Management System, {administratorName}\n");
            Console.WriteLine("Please choose an option:");
            Console.WriteLine("1. List all doctors");
            Console.WriteLine("2. Check doctor details");
            Console.WriteLine("3. List all patients");
            Console.WriteLine("4. Check patient details");
            Console.WriteLine("5. Add doctor");
            Console.WriteLine("6. Add patient");
            Console.WriteLine("7. Logout");
            Console.WriteLine("8. Exit");
        }

        // Lists all doctors registered to the system
        private static void ListAllDoctors()
        {
            Console.Clear();
            Helper.DisplayHeading("All Doctors");
            Console.WriteLine("All doctors registered to the DOTNET Hostpital Management System\n");
            Console.WriteLine($"{Helper.Padding("Name", 20)}| {Helper.Padding("Email Address", 25)}| {Helper.Padding("Phone", 15)}| Address");
            for (int i = 0; i < Console.WindowWidth / 2; i++)
            {
                Console.Write("- ");
            }
            var doctors = TxtHandler.ListAllDoctors();
            foreach (var doctor in doctors)
            {
                Console.WriteLine(doctor.ToString());
            }
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(true);
        }

        // Displays information of a doctor from their Doctor ID
        private static void CheckDoctorDetails()
        {
            Console.Clear();
            Helper.DisplayHeading("Doctor Details");
            Console.Write("Enter the ID of the doctor whose details you are checking or type \"n\" to return to the menu: ");

            string doctorID = Console.ReadLine();

            if (doctorID.Equals("n", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            Console.WriteLine();

            var doctor = TxtHandler.GetDoctorDetails(doctorID);

            if (doctor != null)
            {
                Console.WriteLine("Details for " + TxtHandler.GetDoctorName(doctorID) + "\n");
                Console.WriteLine($"{Helper.Padding("Name", 20)}| {Helper.Padding("Email Address", 25)}| {Helper.Padding("Phone", 15)}| Address");

                for (int i = 0; i < Console.WindowWidth / 2; i++)
                {
                    Console.Write("- ");
                }
                Console.WriteLine(doctor.ToString());
            }
            else
            {
                Console.WriteLine("Doctor not found.");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(true);
        }

        // List all patients registered to the system
        private static void ListAllPatients()
        {
            Console.Clear();
            Helper.DisplayHeading("All Patients");
            var patients = TxtHandler.ListAllPatients();
            Console.WriteLine("All patients registered to the DOTNET Hospital Management System\n");
            Console.WriteLine($"{Helper.Padding("Patient", 15)}| {Helper.Padding("Doctor", 20)}| {Helper.Padding("Email Address", 25)}| {Helper.Padding("Phone", 11)}| Address");
            for (int i = 0; i < Console.WindowWidth / 2; i++)
            {
                Console.Write("- ");
            }
            foreach (var patient in patients)
            {
                Console.WriteLine(patient.ToString());
            }
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(true);
        }

        // Displays information of a patient based on their Patient ID
        private static void CheckPatientDetails()
        {
            Console.Clear();
            Helper.DisplayHeading("Patient Details");
            Console.Write("Please enter the ID of the patient whose details you are checking or type \"n\" to return to the menu: ");

            string patientID = Console.ReadLine();

            if (patientID.Equals("n", StringComparison.OrdinalIgnoreCase))
            {
                return;
            }

            Console.WriteLine();

            var patient = TxtHandler.GetPatientDetails(patientID);

            if (patient != null)
            {
                Console.WriteLine("Details for " + TxtHandler.GetPatientName(patientID) + "\n");
                Console.WriteLine($"{Helper.Padding("Patient", 15)}| {Helper.Padding("Doctor", 20)}| {Helper.Padding("Email Address", 25)}| {Helper.Padding("Phone", 11)}| Address");

                for (int i = 0; i < Console.WindowWidth / 2; i++)
                {
                    Console.Write("- ");
                }
                Console.WriteLine(patient.ToString());
            }
            else
            {
                Console.WriteLine("Patient not found.");
            }

            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(true);
        }

        // Add a new doctor to the system
        private static void AddDoctor()
        {
            Console.Clear();
            Helper.DisplayHeading("Add Doctor");
            Console.WriteLine("Registering a new doctor with the DOTNET Hospital Management System:");

            string firstName = Helper.CheckEmpty("First Name: ");
            string lastName = Helper.CheckEmpty("Last Name: ");
            string password = Helper.CheckEmptyPassword("Password: ");
            string email = Helper.CheckEmpty("Email: ");
            string phone = Helper.CheckEmpty("Phone: ");
            string streetNumber = Helper.CheckEmpty("Street Number: ");
            string street = Helper.CheckEmpty("Street: ");
            string city = Helper.CheckEmpty("City: ");
            string state = Helper.CheckEmpty("State: ");

            string newDoctorData = $"{password},{firstName},{lastName},{email},{phone},{streetNumber},{street},{city},{state}";

            int doctorId = TxtHandler.AddDoctor(newDoctorData);
            Console.WriteLine($"Doctor {firstName} {lastName} added to the system! Doctor ID: {doctorId}");
            Console.WriteLine("\nPlease wait while the registration email is being sent...");
            try
            {
                Helper.SendEmail(email, "Registration Successful", $"Dear Dr {firstName},\n\nYour Doctor ID is: {doctorId}.\nThank you for registering with our hospital!\n\nKind regards,\nDOTNET Hospital");
                Console.WriteLine("Registration confirmation email sent successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send registration confirmation email.\nError: {ex.Message}");
            }
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(true);
        }

        // Add a new patient to the system
        private static void AddPatient()
        {
            Console.Clear();
            Helper.DisplayHeading("Add Patient");
            Console.WriteLine("Registering a new patient with the DOTNET Hospital Management System:");

            string firstName = Helper.CheckEmpty("First Name: ");
            string lastName = Helper.CheckEmpty("Last Name: ");
            string password = Helper.CheckEmptyPassword("Password: ");
            string email = Helper.CheckEmpty("Email: ");
            string phone = Helper.CheckEmpty("Phone: ");
            string streetNumber = Helper.CheckEmpty("Street Number: ");
            string street = Helper.CheckEmpty("Street: ");
            string city = Helper.CheckEmpty("City: ");
            string state = Helper.CheckEmpty("State: ");

            string newPatientData = $"{password},{firstName},{lastName},{email},{phone},{streetNumber},{street},{city},{state}";
            int patientId = TxtHandler.AddPatient(newPatientData);
            Console.WriteLine($"{firstName} {lastName} added to the system! Patient ID: {patientId}");
            Console.WriteLine("\nPlease wait while the registration email is being sent...");
            try
            {
                Helper.SendEmail(email, "Registration Successful", $"Dear {firstName},\n\nYour Patient ID is: {patientId}.\nThank you for registering with our hospital!\n\nKind regards,\nDOTNET Hospital");
                Console.WriteLine("Registration confirmation email sent successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send registration confirmation email.\nError: {ex.Message}");
            }
            Console.WriteLine("\nPress any key to continue...");
            Console.ReadKey(true);
        }

    }
}
