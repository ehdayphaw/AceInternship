﻿// See https://aka.ms/new-console-template for more information
using AceInternship.ConsoleApp;
using System.Data;
using System.Data.SqlClient;

Console.WriteLine("Hello, World!");

AdoDotNetExample adoDotNetExample = new AdoDotNetExample();
//adoDotNetExample.Read();

//adoDotNetExample.Create("eheh", "day", "phaw");
//adoDotNetExample.Update(38, "pyoneplay", "Eh", "pyone nay lite");
adoDotNetExample.Delete(38);

Console.ReadKey();