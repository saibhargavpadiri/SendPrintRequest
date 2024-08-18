using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Printing;
using System.Drawing.Printing;
using System.Drawing;
using System.IO;
using System.Diagnostics;
using System.Threading;

namespace PrintRequest
{
    class Program
    {
        static void Main(string[] args)
        {
           
            PrinterSettings.StringCollection a = System.Drawing.Printing.PrinterSettings.InstalledPrinters;
            Console.WriteLine(a[2]);
            foreach(string printerName1 in a)
            {
                Console.WriteLine(printerName1);
            }

            Process oProcess = new Process();

            try
            {
                // Run the command to set the default printer
                string printerName11 = "Microsoft Print to PDF";
                string command = $"rundll32 printui.dll,PrintUIEntry /y /n \"{printerName11}\"";
                Process.Start("cmd.exe", $"/c {command}");

                // Set up process start information
                oProcess.StartInfo.CreateNoWindow = false;
                oProcess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                oProcess.StartInfo.Verb = "print";
                oProcess.StartInfo.Arguments = "Actual Size";
                oProcess.StartInfo.UseShellExecute = true;
                oProcess.StartInfo.FileName = @"C:\Users\saibh\OneDrive\Documents\Resume.pdf";
                
                //
                //C:\Users\saibh\OneDrive\Documents\RPA Developer.pdf
                // Start the process
                oProcess.Start();

                // Wait for the process to exit or timeout
                oProcess.WaitForExit(10000);

                // Check if the process is still responding
                while (!oProcess.Responding)
                {
                    Thread.Sleep(10000);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }


            //Shell(String.Format("rundll32 printui.dll,PrintUIEntry /y /n '{0}'", System.Printing.LocalPrintServer.GetDefaultPrintQueue().FullName;));
            string documentPath = @"C:\Users\saibh\OneDrive\Documents\RPA Developer.pdf";
            string printerName = System.Printing.LocalPrintServer.GetDefaultPrintQueue().FullName; // Optional, use default printer if empty

            PrintDocument printDocument = new PrintDocument();

            // Optionally set the printer name
            if (!string.IsNullOrEmpty(printerName))
            {
                printDocument.PrinterSettings.PrinterName = printerName;
            }

            // Set up the PrintPage event handler
            printDocument.PrintPage += (sender, e) =>
            {
                using (StreamReader reader = new StreamReader(documentPath))
                {
                    string text = reader.ReadToEnd();
                    e.Graphics.DrawString(text, new Font("Arial", 12), Brushes.Black, e.MarginBounds.Left, e.MarginBounds.Top);
                }
            };

            // Send the document to the printer
            try
            {
                printDocument.Print();
                Console.WriteLine("Print request sent successfully.");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            //PrinterSettings.StringCollection a = System.Drawing.Printing.PrinterSettings.InstalledPrinters;
            //foreach(string printerName1 in a)
            //{
            //    Console.WriteLine(printerName1);
            //}
            //string Name = System.Printing.LocalPrintServer.GetDefaultPrintQueue().FullName;
            //System.IO.File.Copy(@"C:\Users\saibh\Downloads\JPTrain_Ticket_27thMay2024.pdf", System.Printing.LocalPrintServer.GetDefaultPrintQueue().FullName);
            // PrintQueue printQueue = PrintQueue.GetPrintQueues()[0];
            //string printerName = Name;

            // Create a LocalPrintServer object
            //LocalPrintServer localPrintServer = new LocalPrintServer();
            
            // Get the print queue for the specified printer
           // PrintQueue printQueue = localPrintServer.GetPrintQueue(printerName);
        }


        

           
    }
}
