using System;
using System.Collections.Generic;
using System.Linq;

namespace HospitalManagementSystem
{
    public static class DoctorsMenu
    {
        public static void ShowDoctorsMenu(string doctorID)
        {
            while (true)
            {
                DisplayMenu(TxtHandler.GetDoctorName(doctorID));

                ConsoleKeyInfo keyInfo = Console.ReadKey(true);
                char choice = keyInfo.KeyChar;

                switch (choice)
                {
                    case '1': // List doctor details function
                        ListDoctorDetails(doctorID);
                        break;
                    case '2': // List patients function
                        ListPatients(doctorID);
                        break;
                    case '3': // List appointment function
                        ListAppointments(doctorID);
                        break;
                    case '4': // Check particular patient function
                        CheckParticularPatient();
                        break;
                    case '5': // List appointments with patient function
                        ListAppointmentsWithPatient(doctorID);
                        break;
                    case '6': // Logout
                        Console.Clear();
                        Login.ShowLoginMenu();
                        return;
                    case '7': // Exit the application
                        Environment.Exit(0);
                        break;
                    default: // Error handling for wrong input
                        Console.WriteLine("Invalid choice. Press any key to try again...");
                        Console.ReadKey(true);
                        break;
                }
            }
        }

        // Displays the doctor menu
        private static void DisplayMenu(string doctorName)
        {
            Console.Clear();
            Helper.DisplayHeading("Doctor Menu");
            Console.WriteLine($"Welcome to DOTNET Hospital Management System, {doctorName}\n");
            Console.WriteLine("Please choose an option:");
            Console.WriteLine("1. List doctor details");
            Console.WriteLine("2. List patients");
            Console.WriteLine("3. List appointments");
            Console.WriteLine("4. Check particular patient");
            Console.WriteLine("5. List appointments with patient");
            Console.WriteLine("6. Logout");
            Console.WriteLine("7. Exit");
        }

        // Display the details of a doctor from their Doctor ID
        public static void ListDoctorDetails(string doctorID)
        {
            Console.Clear();
            Helper.DisplayHeading("My Details");
            Doctor doctor = TxtHandler.GetDoctorDetails(doctorID);
            if (doctor != null)
            {
                Console.WriteLine($"{Helper.Padding("Name", 20)}| {Helper.Padding("Email Address", 25)}| {Helper.Padding("Phone", 15)}| Address");

                for (int i = 0; i < Console.WindowWidth / 2; i++)
                {
                    Console.Write("- ");
                }
                Console.WriteLine();
                Console.WriteLine(doctor.ToString());
            }
            else
            {
                Console.WriteLine("Details for the provided Doctor ID were not found.\n");
            }

            Console.WriteLine("\nPress any key to go back to menu...");
            Console.ReadKey(true);
        }

        // Displays a list of patients assigned to a doctor
        public static void ListPatients(string doctorID)
        {
            Console.Clear();
            Helper.DisplayHeading("My Patients");

            List<Patient> patients = TxtHandler.ListAllPatients();

            if (patients != null && patients.Count > 0)
            {

                var filteredPatients = patients.Where(p => TxtHandler.GetDoctorForPatient(p.PatientID) == doctorID).ToList();

                if (filteredPatients.Count > 0)
                {
                    Console.WriteLine($"{Helper.Padding("Patient", 15)}| {Helper.Padding("Doctor", 20)}| {Helper.Padding("Email Address", 25)}| {Helper.Padding("Phone", 11)}| Address");
                    for (int i = 0; i < Console.WindowWidth / 2; i++)
                    {
                        Console.Write("- ");
                    }
                    Console.WriteLine();

                    foreach (var patient in filteredPatients)
                    {
                        Console.WriteLine(patient.ToString());
                    }
                }
                else
                {
                    Console.WriteLine($"No patients found for Doctor ID: {doctorID}.\n");
                }
            }
            else
            {
                Console.WriteLine("No patients found in the system.\n");
            }

            Console.WriteLine("\nPress any key to go back to menu...");
            Console.ReadKey(true);
        }

        // Display a list of appointments assigned to a particular Doctor ID
        public static void ListAppointments(string doctorID)
        {
            Console.Clear();
            Helper.DisplayHeading("All Appointments");

            List<Appointment> appointments = TxtHandler.ListAllAppointments();

            if (appointments != null && appointments.Count > 0)
            {
                var filteredAppointments = appointments.Where(a => a.DoctorID == doctorID).ToList();

                if (filteredAppointments.Count > 0)
                {
                    Console.WriteLine($"{Helper.Padding("Doctor", 20)}| {Helper.Padding("Patient", 20)}| {Helper.Padding("Description", 40)}");
                    for (int i = 0; i < Console.WindowWidth / 2; i++)
                    {
                        Console.Write("- ");
                    }
                    Console.WriteLine();

                    foreach (var appointment in filteredAppointments)
                    {
                        Console.WriteLine(appointment.ToString());
                    }
                }
                else
                {
                    Console.WriteLine($"No appointments found for Doctor ID: {doctorID}.\n");
                }
            }
            else
            {
                Console.WriteLine("No appointments found in the system.\n");
            }

            Console.WriteLine("\nPress any key to go back to menu...");
            Console.ReadKey(true);
        }

        // Check and show details of a patient based on their patient ID
        public static void CheckParticularPatient()
        {
            Console.Clear();
            Helper.DisplayHeading("Check Patient Details");

            Console.Write("Enter the ID of the patient to check: ");
            string patientID = Console.ReadLine();
            Patient patient = TxtHandler.GetPatientDetails(patientID);
            Console.WriteLine();
            if (patient != null)
            {
                Console.WriteLine($"{Helper.Padding("Patient", 15)}| {Helper.Padding("Doctor", 20)}| {Helper.Padding("Email Address", 25)}| {Helper.Padding("Phone", 11)}| Address");

                for (int i = 0; i < Console.WindowWidth / 2; i++)
                {
                    Console.Write("- ");
                }
                Console.WriteLine();

                string doctorName = TxtHandler.GetDoctorName(patient.DoctorID);
                Console.WriteLine(patient.ToString());
            }
            else
            {
                Console.WriteLine("No details found for the provided Patient ID.");
            }

            Console.WriteLine("\nPress any key to go back to menu...");
            Console.ReadKey(true);
        }

        // Display appointments between the authenticated doctor and a patient
        public static void ListAppointmentsWithPatient(string doctorID)
        {
            Console.Clear();
            Helper.DisplayHeading("Appointments With");

            Console.Write("Enter the ID of the patient you would like to view appointments for: ");
            string patientID = Console.ReadLine();

            List<Appointment> appointments = TxtHandler.ListAllAppointments();
            Console.WriteLine();
            if (appointments != null && appointments.Count > 0)
            {
                var filteredAppointments = appointments.Where(a => a.DoctorID == doctorID && a.PatientID == patientID).ToList();

                if (filteredAppointments.Count > 0)
                {
                    Console.WriteLine($"{Helper.Padding("Doctor", 20)}| {Helper.Padding("Patient", 20)}| {Helper.Padding("Description", 40)}");
                    for (int i = 0; i < Console.WindowWidth / 2; i++)
                    {
                        Console.Write("- ");
                    }
                    Console.WriteLine();

                    foreach (var appointment in filteredAppointments)
                    {
                        Console.WriteLine(appointment.ToString());
                    }
                }
                else
                {
                    Console.WriteLine($"No appointments found with Patient ID: {patientID}.");
                }
            }
            else
            {
                Console.WriteLine("No appointments found in the system.");
            }

            Console.WriteLine("\nPress any key to go back to menu...");
            Console.ReadKey(true);
        }
    }
}
