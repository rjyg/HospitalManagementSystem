using System;
using System.Net;
using System.Net.Mail;

namespace HospitalManagementSystem
{
    public static class Helper
    {
        // Captures user input as a password, masking each character with "*"
        public static string ReadPassword()
        {
            string password = "";
            ConsoleKeyInfo info = Console.ReadKey(true);
            while (info.Key != ConsoleKey.Enter)
            {
                if (info.Key != ConsoleKey.Backspace)
                {
                    Console.Write("*");
                    password += info.KeyChar;
                }
                else if (info.Key == ConsoleKey.Backspace)
                {
                    if (!string.IsNullOrEmpty(password))
                    {
                        password = password.Substring(0, password.Length - 1);
                        Console.Write("\b \b");
                    }
                }
                info = Console.ReadKey(true);
            }
            Console.WriteLine();
            return password;
        }

        // Pads a string with spaces on the right to reach a specified total length
        public static string Padding(string input, int maxLength)
        {
            if (input.Length >= maxLength) return input;
            return input + new string(' ', maxLength - input.Length);
        }

        // Displays a formatted heading within borders in the console
        public static void DisplayHeading(string title)
        {
            string topBorder = "┌───────────────────────────────────┐";
            string titlePrefix = "│ DOTNET Hospital Management System │";
            string middleBorder = "├───────────────────────────────────┤";
            string bottomBorder = "└───────────────────────────────────┘";

            int maxTitleLength = topBorder.Length - 4;
            string paddedTitle = CenterPadding(title, maxTitleLength);

            Console.WriteLine(topBorder);
            Console.WriteLine(titlePrefix);
            Console.WriteLine(middleBorder);
            Console.WriteLine($"│ {paddedTitle} │");
            Console.WriteLine(bottomBorder);
            Console.WriteLine();
        }

        // Centers a string within a specified total length by padding spaces equally on both sides
        public static string CenterPadding(string input, int maxLength)
        {
            if (input.Length >= maxLength) return input;

            int paddingSize = maxLength - input.Length;
            int paddingLeftSize = paddingSize / 2;
            int paddingRightSize = paddingLeftSize;

            // Adjust if difference between padding left and right is odd
            if (paddingSize % 2 != 0) paddingLeftSize++;

            return new string(' ', paddingLeftSize) + input + new string(' ', paddingRightSize);
        }

        // Prompts the user for input and checks if the input is empty or whitespace
        public static string CheckEmpty(string prompt)
        {
            string result;
            do
            {
                Console.Write(prompt);
                result = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(result))
                {
                    Console.WriteLine("Input cannot be empty. Please try again.");
                }
            } while (string.IsNullOrWhiteSpace(result));
            return result;
        }

        // Prompts the user for a password and checks if the password is empty
        public static string CheckEmptyPassword(string prompt)
        {
            string password;
            do
            {
                Console.Write(prompt);
                password = ReadPassword();
                if (string.IsNullOrEmpty(password))
                {
                    Console.WriteLine("Input cannot be empty. Please enter again.");
                }
            }
            while (string.IsNullOrEmpty(password));
            return password;
        }

        // Bonus mark: Sends an email using SMTP with specified recipient, subject, and body
        public static void SendEmail(string toEmail, string subject, string body)
        {
            SmtpClient smtpClient = new SmtpClient("smtp.gmail.com")
            {
                Port = 587,
                EnableSsl = true,
                Credentials = new NetworkCredential("rjyg98@gmail.com", "nhil bvnh dodk tnis")
            };

            smtpClient.Send("rjyg98@gmail.com", toEmail, subject, body);
        }
    }
}
