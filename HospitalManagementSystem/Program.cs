﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalManagementSystem
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Login.ShowLoginMenu();
            Console.WriteLine("Press any key to exit...");
            Console.ReadKey();
        }
    }
}