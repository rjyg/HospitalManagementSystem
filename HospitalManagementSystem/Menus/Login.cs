using System;

namespace HospitalManagementSystem
{
    public static class Login
    {
        // Displays login menu
        public static void ShowLoginMenu()
        {
            bool isAuthenticated = false; // Authentication flag
            while (!isAuthenticated)
            {
                Console.Clear();
                Helper.DisplayHeading("Login");
                // Prompt user for details
                string userID = Helper.CheckEmpty("ID: ");
                string password = Helper.CheckEmptyPassword("Password: ");

                // Validate user credentials against all three .txt files
                string authenticatedRole = TxtHandler.ValidateCredentials(userID, password);

                // If credentials are valid
                if (!string.IsNullOrEmpty(authenticatedRole))
                {
                    isAuthenticated = true; // Change authentication flag to true
                    Console.Clear();

                    // Navigate to the respective menu of the authenticated role
                    switch (authenticatedRole)
                    {
                        case "Patients.txt":
                            PatientsMenu.ShowPatientMenu(userID);
                            break;
                        case "Doctors.txt":
                            DoctorsMenu.ShowDoctorsMenu(userID);
                            break;
                        case "Administrators.txt":
                            AdministratorsMenu.ShowAdministratorsMenu(userID);
                            break;
                    }
                }
                else // If credentials are invalid
                {
                    Console.WriteLine("Invalid credentials. Please try again.");
                    Console.WriteLine("Press any key to retry...");
                    Console.ReadKey();
                }
            }
        }
    }
}
