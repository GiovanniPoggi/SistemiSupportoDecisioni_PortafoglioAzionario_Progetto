using System;
using System.Drawing;

namespace SsdWebApi 
{
    public class Forecast 
    {
        public Forecast() {

        }

        //Lettura e previsione di una serie di ordini
        public string forecastSARIMAindex(string attribute) {
            string res = "\"text\":\"";
            string interpreter = @"C:\Users\Giovanni\.conda\envs\opanalytics1\python.exe";
            string environment = "opanalytics1";
            int timeout = 10000;
            PythonRunner PR = new PythonRunner(interpreter, environment, timeout);
            Bitmap bmp = null;

            try {
                string command = $"Models/forecastStat.py {attribute}.csv";
                string list = PR.runDosCommands(command);

                if (string.IsNullOrWhiteSpace(list)) {
                    Console.WriteLine("Error in the script call");
                    goto lend;
                }

                string[] lines = list.Split(new[] {Environment.NewLine}, StringSplitOptions.None);
                string strBitmap = "";
                foreach (string s in lines) {
                    if(s.StartsWith("MAPE")) {
                        Console.WriteLine(s);
                        res += s;
                    }

                    if(s.StartsWith("b'")) {
                        strBitmap = s.Trim();
                        break;
                    }

                    if(s.StartsWith("Actual")) {
                        double fcast = Convert.ToDouble(s.Substring(s.LastIndexOf(" ")));
                        Console.WriteLine(fcast);
                    }
                }

                strBitmap = strBitmap.Substring(strBitmap.IndexOf("b'")); //begin of binary image
                //strBitmap = strBitmap.Remove(strBitmap.Length-4).Trim(); //removes "exit" at the end
                res += "\",\"img\":\""+strBitmap+"\"";
                try {
                    bmp = PR.FromPythonBase64String(strBitmap);
                }
                catch (Exception exception) {
                    throw new System.Exception(
                        "An error occured while trying to create an image from "+
                        "Python script output. See inner exception for details.",
                        exception);
                }
                goto lend;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                goto lend;
            }

            lend:
            return res;
        }
    }
}
