﻿using CommandLineParser.Arguments;
using System;
using System.Diagnostics;
using VSTSShared.BaseClasses;
using VSTSShared.Helpers;

namespace VSTSKeep
{
    class Program
    {
        static void Main(string[] args)
        {
            // Setup parser and extract command line arguments
            var parser = new CommandLineParser.CommandLineParser();
            var cmdLineArgs = new CommandLineArgs();

            try
            {
                // Add the verbose switch programmatically because I am having issues with the attributes
                var verbose = new SwitchArgument('v', "verbose", "Turns on verbose output.", false);
                parser.Arguments.Add(verbose);

                parser.ShowUsageHeader = "Sets or removes \"keep forever\" retention for the specified build number in VSTS.\r\n\r\n" +
                    "VSTS-KEEP -a <Account> [-u <User ID>] -p <Password> -t <Project> -b <BuildNumber> [-k] <0|1>";
                parser.ShowUsageOnEmptyCommandline = true;

                parser.ExtractArgumentAttributes(cmdLineArgs);
                parser.ParseCommandLine(args);

                var authentication = new BasicAuthentication(cmdLineArgs.Account, cmdLineArgs.UserId, cmdLineArgs.Password);
                var helper = new VstsHelper();

                Console.WriteLine(helper.KeepForever(authentication, cmdLineArgs.Project, cmdLineArgs.BuildNumber,
                    cmdLineArgs.KeepForever == 1, verbose.Value)
                    ? "    Retention set successfully."
                    : "    Failed to set retention.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                parser.ShowUsage();
            }

            if (Debugger.IsAttached)
            {
                // Keep the console window from closing when running from IDE
                Console.WriteLine("\r\nPress any key to close command window.");
                Console.ReadKey();
            }
        }
    }
}