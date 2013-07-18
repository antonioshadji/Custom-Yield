using System;
using TTPriceModelLib;
using System.Text;
using System.IO;

namespace CustomPricing_CS
{
    public class CustomPriceModelFactory : IPriceModelFactory
    {   
        static readonly string assembleDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetAssembly(typeof(CustomPriceModelFactory)).Location);
        private string _iniPath = null;

        #region IPriceModelFactory Members

        public object GetPriceModel(int index)
        {
            errorlog(new Exception("no error. Loaded :" + index.ToString() + " Index value"));
            return new CustomPriceModel(index);
        }

        public string GetPriceModelName(int index)
        {            
            if (!String.IsNullOrEmpty(this._iniPath))
            {
                string identifier = "[" + index + "]";
                string currentLine;

                try
                {
                    TextReader tr = new StreamReader(this._iniPath);
                    currentLine = tr.ReadLine();
                    currentLine = currentLine.Trim();
                    while (currentLine != null)
                    {
                        if (currentLine.Equals(identifier))
                        {
                            while (currentLine != null)
                            {
                                if (currentLine.StartsWith("Name="))
                                {
                                    currentLine = currentLine.Trim();
                                    currentLine = currentLine.Substring(5);
                                    tr.Close();
                                    errorlog(currentLine);
                                    //Returns out of Function!!!!!!
                                    return currentLine;
                                }
                                currentLine = tr.ReadLine();
                            }
                        }
                        currentLine = tr.ReadLine();
                    }
                }
                catch (Exception ex)
                {
                    errorlog(ex);
                    return "ERROR" + ex.ToString();
                }
            }

            return string.Empty;
        }

        public int NumberOfSupportedPricingModels()
        {
            this._iniPath = Path.Combine(assembleDirectory, "OutsideYield.ini");

            System.Diagnostics.EventLog.WriteEntry("Application", "Path " + this._iniPath, System.Diagnostics.EventLogEntryType.SuccessAudit);
            
            if (File.Exists(this._iniPath))
            {
                string currentLine;
                try
                {
                    TextReader tr = new StreamReader(this._iniPath);
                    currentLine = tr.ReadLine();
                    while (currentLine != null)
                    {
                        if (currentLine.StartsWith("Instruments="))
                        {
                            currentLine = currentLine.Trim().Substring(12).Trim();
                            tr.Close();
                            return int.Parse(currentLine);
                        }
                        currentLine = tr.ReadLine();
                    }
                    tr.Close();
                    return 0;
                }
                catch (Exception ex)
                {
                    errorlog(ex);
                    return 0;
                }
            }
            else
            {
                this._iniPath = null;
                System.Diagnostics.EventLog.WriteEntry("Application", "Path '" + this._iniPath + "' does not Exists!", System.Diagnostics.EventLogEntryType.Error);            
            }

            return 0;
        }

        #endregion



        #region Error log code

        void errorlog(Exception exception)
        {
            //             TextWriter tw = new StreamWriter("error.txt");
            //             tw.WriteLine("ERROR in Model Factory" + error);
            //             tw.Close();

            System.Diagnostics.EventLog.WriteEntry("Application", exception.ToString(), System.Diagnostics.EventLogEntryType.Error);
        }

        void errorlog(string informaiton)
        {
            //TextWriter tw = new StreamWriter("error.txt");
            //tw.WriteLine("ERROR in Model Factory" + informaiton);
            //tw.Close();

            System.Diagnostics.EventLog.WriteEntry("Application", informaiton, System.Diagnostics.EventLogEntryType.Information);
        }

        #endregion
    }
}
