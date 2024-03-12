using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace HospitalManagementSystem
{
    public static class TxtHandler
    {
        // Sets txt file names to readonly variables
        private static readonly string PatientsFileName = "Patients.txt";
        private static readonly string DoctorsFileName = "Doctors.txt";
        private static readonly string AppointmentsFileName = "Appointments.txt";
        private static readonly string AdministratorsFileName = "Administrators.txt";

        private static readonly string[] fileNames =
        {
            PatientsFileName,
            DoctorsFileName,
            AppointmentsFileName,
            AdministratorsFileName
        };

        // Function that returns an array of strings split by ','
        private static IEnumerable<string[]> SplitLines(string fileName)
        {
            return File.ReadLines(fileName).Select(line => line.Split(','));
        }

        // Creates Doctor object
        private static Doctor CreateDoctor(string[] parts)
        {
            return new Doctor
            {
                DoctorID = parts[0],
                FirstName = parts[2],
                LastName = parts[3],
                Email = parts[4],
                Phone = parts[5],
                StreetNumber = parts[6],
                Street = parts[7],
                City = parts[8],
                State = parts[9]
            };
        }

        // Creates Patient object
        private static Patient CreatePatient(string[] parts)
        {
            return new Patient
            {
                PatientID = parts[0],
                FirstName = parts[2],
                LastName = parts[3],
                Email = parts[4],
                Phone = parts[5],
                StreetNumber = parts[6],
                Street = parts[7],
                City = parts[8],
                State = parts[9]
            };
        }

        // Creates Appointment object
        private static Appointment CreateAppointment(string[] parts)
        {
            return new Appointment
            {
                DoctorID = parts[0],
                PatientID = parts[1],
                Description = parts[2]
            };
        }

        // Login function by that takes id, password and filename from validation function loop in Login.cs
        public static bool ValidateUser(string id, string password, string fileName)
        {
            return SplitLines(fileName).Any(parts => parts.Length >= 2 && parts[0] == id && parts[1] == password);
        }

        // Validate credentials against all files to find a match and determine which role
        public static string ValidateCredentials(string id, string password)
        {
            foreach (string fileName in fileNames)
            {
                if (ValidateUser(id, password, fileName))
                {
                    return fileName;
                }
            }
            return null;
        }

        // Get patient name for menus and emails
        public static string GetPatientName(string patientID)
        {
            foreach (var parts in SplitLines(PatientsFileName))
            {
                if (parts.Length >= 2 && parts[0] == patientID)
                {
                    return parts[2] + " " + parts[3];
                }
            }
            return "Unknown";
        }

        // Bonus mark function - Email system
        public static string GetPatientEmail(string patientID)
        {
            foreach (var parts in SplitLines(PatientsFileName))
            {
                if (parts.Length >= 4 && parts[0] == patientID)
                {
                    return parts[4];
                }
            }
            return "Unknown";
        }

        // Gets the doctor's ID for a patient from the appointments file by Patient ID
        public static List<string> GetDoctorIDsForPatient(string patientID)
        {
            return SplitLines(AppointmentsFileName)
                .Where(parts => parts.Length >= 2 && parts[1] == patientID)
                .Select(parts => parts[0])
                .Distinct()
                .ToList();
        }

        // Gets the Doctors details from the doctors file
        public static Doctor GetDoctorDetails(string doctorID)
        {
            foreach (var parts in SplitLines(DoctorsFileName))
            {
                if (parts.Length >= 9 && parts[0] == doctorID)
                {
                    return CreateDoctor(parts);
                }
            }
            return null;
        }

        // Gets the patient's details from the patients file by Patient ID
        public static Patient GetPatientDetails(string patientID)
        {
            foreach (var parts in SplitLines(PatientsFileName))
            {
                if (parts.Length >= 9 && parts[0] == patientID)
                {
                    return CreatePatient(parts);
                }
            }
            return null;
        }

        // Gets the appointment's description by finding a matching Doctor ID and Patient ID
        public static List<string> GetAppointmentDescriptions(string doctorID, string patientID)
        {
            var descriptions = SplitLines(AppointmentsFileName)
                .Where(parts => parts.Length == 3 && parts[0] == doctorID && parts[1] == patientID)
                .Select(parts => parts[2])
                .ToList();

            return descriptions.Count > 0 ? descriptions : new List<string> { "No description available." };
        }

        // Appends a line with a new appointment to the appointments file
        public static void AddAppointment(string doctorID, string patientID, string description)
        {
            File.AppendAllText(AppointmentsFileName, $"{doctorID},{patientID},{description}\n");
        }

        // Retrieves the doctor that is registered to a patient via Patient ID
        public static string GetDoctorForPatient(string patientID)
        {
            return SplitLines(PatientsFileName)
                .Where(parts => parts.Length > 10 && parts[0] == patientID)
                .Select(parts => parts[10])
                .FirstOrDefault();
        }

        // Registers a doctor to a patient by assigning a doctor ID to the last "part" of the line matching the Patient ID in the patients text file
        public static void RegisterDoctorForPatient(string patientID, string doctorID)
        {
            var lines = File.ReadAllLines(PatientsFileName).ToList();

            for (int i = 0; i < lines.Count; i++)
            {
                var parts = lines[i].Split(',');
                if (parts[0] == patientID)
                {
                    lines[i] += "," + doctorID;
                    break;
                }
            }

            File.WriteAllLines(PatientsFileName, lines);
        }

        // Retrieves a doctor's ID by the option the user chooses
        public static string GetDoctorIDByOption(int option)
        {
            int currentOption = 1;
            foreach (var parts in SplitLines(DoctorsFileName))
            {
                if (currentOption == option)
                {
                    return parts[0];
                }
                currentOption++;
            }
            return null;
        }

        // Gets the administrator who is logged on's name
        public static string GetAdministratorsName(string administratorID)
        {
            return SplitLines(AdministratorsFileName)
                .Where(parts => parts[0] == administratorID)
                .Select(parts => parts[2] + " " + parts[3])
                .FirstOrDefault();
        }

        // Gets doctor's name from the doctor's file via their Doctor ID
        public static string GetDoctorName(string doctorID)
        {
            return SplitLines(DoctorsFileName)
                .Where(parts => parts[0] == doctorID)
                .Select(parts => parts[2] + " " + parts[3])
                .FirstOrDefault();
        }

        // Returns a list of all doctors
        public static List<Doctor> ListAllDoctors()
        {
            return SplitLines(DoctorsFileName)
                .Where(parts => parts.Length >= 9)
                .Select(CreateDoctor)
                .ToList();
        }

        // Returns a list of all patients
        public static List<Patient> ListAllPatients()
        {
            return SplitLines(PatientsFileName)
                .Where(parts => parts.Length >= 10)
                .Select(CreatePatient)
                .ToList();
        }

        // Returns a list of all appointments booked
        public static List<Appointment> ListAllAppointments()
        {
            return SplitLines(AppointmentsFileName)
                .Where(parts => parts.Length >= 3)
                .Select(CreateAppointment)
                .ToList();
        }

        // Takes new doctor data and appends it to the doctor file and adds a new line
        public static int AddDoctor(string newDoctorData)
        {
            int nextId = GetNextUserId();
            string formattedData = $"{nextId},{newDoctorData}";
            File.AppendAllText(DoctorsFileName, formattedData + Environment.NewLine);
            return nextId;
        }

        // Takes new patient data and appends it to the patient file and adds a new line
        public static int AddPatient(string newPatientData)
        {
            int nextId = GetNextUserId();
            string formattedData = $"{nextId},{newPatientData}";
            File.AppendAllText(PatientsFileName, formattedData + Environment.NewLine);
            return nextId;
        }

        // Loops through all file names, and finds the highest user ID and returns max + 1
        private static int GetNextUserId()
        {
            string[] userFiles = { DoctorsFileName, PatientsFileName, AdministratorsFileName };

            int maxId = 0;

            foreach (var fileName in userFiles)
            {
                if (!File.Exists(fileName))
                {
                    continue;
                }

                var lines = File.ReadAllLines(fileName);
                foreach (var line in lines)
                {
                    var columns = line.Split(',');
                    if (columns.Length > 0 && int.TryParse(columns[0], out int currentId))
                    {
                        maxId = Math.Max(maxId, currentId);
                    }
                }
            }

            return maxId + 1;
        }

    }
}
