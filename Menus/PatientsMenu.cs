using System;
using System.Collections.Generic;
using System.Linq;

namespace HospitalManagementSystem
{
    public static class PatientsMenu
    {
        public static void ShowPatientMenu(string patientID)
        {

            while (true)
            {
                DisplayMenu(TxtHandler.GetPatientName(patientID));

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                char choice = keyInfo.KeyChar;

                switch (choice)
                {
                    case '1': // List patient details function
                        ListPatientDetails(patientID);
                        Console.WriteLine("\nPress any key to go back to menu...");
                        Console.ReadKey(true);
                        break;
                    case '2': // List my doctors function
                        ListMyDoctors(patientID);
                        Console.WriteLine("\nPress any key to go back to menu...");
                        Console.ReadKey(true);
                        break;
                    case '3': // List my appointments function
                        ListMyAppointments(patientID);
                        Console.WriteLine("\nPress any key to go back to menu...");
                        Console.ReadKey(true);
                        break;
                    case '4': // Book an appointment function
                        BookAppointment(patientID);
                        break;
                    case '5': // Logout
                        Console.Clear();
                        Login.ShowLoginMenu();
                        return;
                    case '6': // Exit application
                        Environment.Exit(0);
                        break;
                    default: // Error handling for wrong input
                        Console.WriteLine("Invalid choice. Press any key to try again...");
                        Console.ReadKey(true);
                        break;
                }
            }
        }

        // Displays the patient menu
        private static void DisplayMenu(string patientName)
        {
            Console.Clear();
            Helper.DisplayHeading("Patient Menu");
            Console.WriteLine($"Welcome to DOTNET Hospital Management System, {patientName}\n");
            Console.WriteLine("Please choose an option:");
            Console.WriteLine("1. List patient details");
            Console.WriteLine("2. List my doctor details");
            Console.WriteLine("3. List all appointments");
            Console.WriteLine("4. Book appointment");
            Console.WriteLine("5. Exit to login");
            Console.WriteLine("6. Exit system");
        }

        // List the details of a patient through the Patient ID
        private static void ListPatientDetails(string patientID)
        {
            Console.Clear();
            Helper.DisplayHeading("My Details");

            Patient patient = TxtHandler.GetPatientDetails(patientID);

            if (patient != null)
            {
                Console.WriteLine(patient.FirstName + " " + patient.LastName + "'s Details\n");
                Console.WriteLine("Patient ID: " + patient.PatientID);
                Console.WriteLine("Full Name: " + patient.FirstName + " " + patient.LastName);
                Console.WriteLine("Address: " + patient.StreetNumber + " " + patient.Street + ", " + patient.City + ", " + patient.State);
                Console.WriteLine("Email: " + patient.Email);
                Console.WriteLine("Phone: " + patient.Phone);
            }
            else
            {
                Console.WriteLine("Details for the provided Patient ID were not found.");
            }
        }

        // List the doctor associated with the patient ID
        private static void ListMyDoctors(string patientID)
        {
            Console.Clear();
            Helper.DisplayHeading("My Doctor");
            Console.WriteLine("Your doctor:\n");

            // Column headers
            Console.WriteLine(Helper.Padding("Name", 20) + "| " + Helper.Padding("Email Address", 25) + "| " + Helper.Padding("Phone", 15) + "| Address");
            for (int i = 0; i < Console.WindowWidth / 2; i++)
            {
                Console.Write("- ");
            }
            Console.WriteLine();

            var doctorIDs = TxtHandler.GetDoctorIDsForPatient(patientID);

            if (!doctorIDs.Any())
            {
                Console.WriteLine("You're not registered to a doctor.");
                return;
            }

            foreach (var doctorID in doctorIDs)
            {
                Doctor doctor = TxtHandler.GetDoctorDetails(doctorID);
                if (doctor != null)
                {
                    Console.WriteLine(doctor.ToString());
                }
            }
        }

        // List all appointments for a patient ID
        private static void ListMyAppointments(string patientID)
        {
            Console.Clear();
            Helper.DisplayHeading("My Appointments");
            Console.WriteLine("Appointments for " + TxtHandler.GetPatientName(patientID) + "\n");

            // Column headers
            Console.WriteLine(Helper.Padding("Doctor", 20) + "| " + Helper.Padding("Patient", 20) + "| Description");
            for (int i = 0; i < Console.WindowWidth / 2; i++)
            {
                Console.Write("- ");
            }
            Console.WriteLine();

            string patientName = TxtHandler.GetPatientName(patientID);
            var doctorIDs = TxtHandler.GetDoctorIDsForPatient(patientID);

            if (!doctorIDs.Any())
            {
                Console.WriteLine("You have no appointments booked.");
                return;
            }

            foreach (var doctorID in doctorIDs)
            {
                Doctor doctor = TxtHandler.GetDoctorDetails(doctorID);
                var descriptions = TxtHandler.GetAppointmentDescriptions(doctorID, patientID);
                if (doctor != null)
                {
                    string doctorFullName = doctor.FirstName + " " + doctor.LastName;
                    foreach (var description in descriptions)
                    {
                        Console.WriteLine($"{Helper.Padding(doctorFullName, 20)}| {Helper.Padding(patientName, 20)}| {description}");

                    }
                }
            }

        }

        // Books an appointment for a patient
        private static void BookAppointment(string patientID)
        {
            Console.Clear();
            Helper.DisplayHeading("Book Appointment");

            string doctorID = TxtHandler.GetDoctorForPatient(patientID);

            if (string.IsNullOrEmpty(doctorID))
            {
                var doctors = ListAllDoctors();  // Updated to return the list of doctors for validation

                int option;
                bool isValidOption;
                do
                {
                    string optionStr = Helper.CheckEmpty("\nPlease choose a doctor: ");
                    isValidOption = int.TryParse(optionStr, out option);

                    if (!isValidOption || option < 1 || option > doctors.Count)
                    {
                        Console.WriteLine("Invalid choice. Please enter a valid option number from the list.");
                        isValidOption = false;  // to continue prompting the user
                    }
                }
                while (!isValidOption);

                doctorID = TxtHandler.GetDoctorIDByOption(option);
                TxtHandler.RegisterDoctorForPatient(patientID, doctorID);

                Console.WriteLine($"You are booking a new appointment with {TxtHandler.GetDoctorName(doctorID)}");
            }
            else
            {
                Console.WriteLine($"You are booking a new appointment with {TxtHandler.GetDoctorName(doctorID)}");
            }

            Console.Write("Description of the appointment: ");
            string appointmentDescription = Console.ReadLine();

            TxtHandler.AddAppointment(doctorID, patientID, appointmentDescription);

            Console.WriteLine("The appointment has been booked successfully");
            Console.WriteLine("\nPlease wait while the confirmation email is being sent...");
            try
            {
                Helper.SendEmail(TxtHandler.GetPatientEmail(patientID), "Booking Successful", $"Dear {TxtHandler.GetPatientName(patientID)},\n\nYou will be seeing Doctor {TxtHandler.GetDoctorName(doctorID)} for {appointmentDescription}.\nThank you for booking with our hospital!\n\nKind regards,\nDOTNET Hospital");
                Console.WriteLine("Booking confirmation email sent successfully!");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to send booking confirmation email.\nError: {ex.Message}");
            }
            Console.WriteLine("\nPress any key to return to menu...");
            Console.ReadKey(true);
        }

        // List all doctors and prompts the patient to choose one to register with
        private static List<Doctor> ListAllDoctors()
        {
            Console.WriteLine("You are not registered with any doctor | Please choose which doctor you would like to register with:");

            var doctors = TxtHandler.ListAllDoctors();
            Console.WriteLine($"\n{Helper.Padding("#", 3)}| {Helper.Padding("Doctor", 20)}| {Helper.Padding("Email Address", 25)}| {Helper.Padding("Phone", 15)}| Address");

            for (int i = 0; i < Console.WindowWidth / 2; i++)
            {
                Console.Write("- ");
            }

            int optionNumber = 1;
            foreach (var doctor in doctors)
            {
                Console.WriteLine($"{Helper.Padding(optionNumber.ToString(), 3)}| {Helper.Padding(doctor.FirstName + " " + doctor.LastName, 20)}| {Helper.Padding(doctor.Email, 25)}| {Helper.Padding(doctor.Phone, 15)}| {doctor.StreetNumber} {doctor.Street}, {doctor.City}, {doctor.State}");
                optionNumber++;
            }
            return doctors;
        }
    }
}
