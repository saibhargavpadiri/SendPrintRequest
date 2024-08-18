using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Diagnostics;
namespace PrintRequest
{
    class Class1
    {
        public static void SendPrintRequest(string PrinterName,string DocumentPath)
        {
            string command = String.Empty;
            try
            {
                if (!(String.IsNullOrEmpty(PrinterName))) 
                {
                    command = String.Format($"rundll32 printui.dll,PrintUIEntry /y /n \'{0}\'", PrinterName);
                }
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = DocumentPath,
                    Arguments = $"/c {command}",
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = true,
                    CreateNoWindow = true,
                    WindowStyle = ProcessWindowStyle.Hidden,
                    Verb="print",
                };
                using (Process process = Process.Start(startInfo))
                {
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();
                    process.WaitForExit();

                    // Print the output
                    Console.WriteLine("Output:");
                    Console.WriteLine(output);

                    // Print any errors
                    if (!string.IsNullOrEmpty(error))
                    {
                        Console.WriteLine("Error:");
                        Console.WriteLine(error);
                    }
                }
                }
            catch(Exception ex)
            {

            }

        }
    }
}
