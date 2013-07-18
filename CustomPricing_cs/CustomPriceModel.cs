//  Written By: Justin Braun
//  May 12, 2009
//  Trading Technologies International

using System;
using System.IO;
using System.Text;
using TTPriceModelLib;

namespace CustomPricing_CS
{
    public class CustomPriceModel : IPriceModel
    {

        static readonly string assembleDirectory = Path.GetDirectoryName(System.Reflection.Assembly.GetAssembly(typeof(CustomPriceModelFactory)).Location);
        private string _iniPath = null;
        
        #region IPriceModel Members
        object[] param = new object[10];
        int modelIndex;

        public CustomPriceModel() { }

        public CustomPriceModel(int index)
        {
            modelIndex = index;
        }

        public void Init(object priceParams)
        {
            bool debugMode = true;
            bool eomConversion = false;
            bool optionalValues = true;
            DateTime settlementDate = DateTime.MinValue;
            DateTime maturityDate = DateTime.MinValue;
            DateTime firstCouponDate = DateTime.MinValue;
            DateTime datedDate = DateTime.MinValue;
            float coupon = 0;
            float conversionFactor = 0;
            int initSource = 0;
            int dayCountType = 0;
            int couponFrequency = 0;
            object[,] priceParam = (object[,])priceParams;
            string couponsFrequencyString = "";
            string dayCountTypeString = "";

            try
            {
                this._iniPath = Path.Combine(assembleDirectory, "OutsideYield.ini");
                TextReader sr = new StreamReader(this._iniPath);
                string identifier = "[" + modelIndex + "]";
                string currentLine = sr.ReadLine();

                while (currentLine != null)
                {
                    if (currentLine.Equals("[Settings]"))
                    {
                        while (currentLine != null)
                        {
                            if (currentLine.ToLower().StartsWith("debugmode="))
                            {
                                currentLine = currentLine.Trim().Substring(10);
                                debugMode = bool.Parse(currentLine);                                    
                            }
                            else if (currentLine.ToLower().StartsWith("initsource="))
                            {
                                currentLine = currentLine.Trim().Substring(11);
                                initSource = int.Parse(currentLine);
                            }
                        currentLine = sr.ReadLine();
                        }
                    }
                    currentLine = sr.ReadLine();
                }
            }
            catch (Exception ex)
            {
                if (debugMode)
                {
                    debugWrite("Could not initialize settings from OutsideYield.ini, " + ex.ToString());
                }
            }
            if (initSource ==0)
            {
                couponsFrequencyString = priceParam[0, 1].ToString();
                switch (couponsFrequencyString)
                {
                    case "Semi Annual":
                        couponFrequency = 2;
                        break;
                    case "Annual":
                        couponFrequency = 1;
                        break;
                    case "Quarterly":
                        couponFrequency = 4;
                        break;
                    case "Monthly":
                        couponFrequency = 12;
                        break;
                    default:
                        couponFrequency = 0;
                        break;
                } 
                
                coupon = float.Parse(priceParam[1, 1].ToString());
                dayCountTypeString =priceParam[2, 1].ToString();
                settlementDate = DateTime.Parse(priceParam[3, 1].ToString());
                maturityDate = DateTime.Parse(priceParam[4, 1].ToString());
                conversionFactor = float.Parse(priceParam[5, 1].ToString());
                eomConversion = bool.Parse(priceParam[6, 1].ToString());
                optionalValues = bool.Parse(priceParam[7, 1].ToString());

                if (optionalValues)
                {
                    firstCouponDate = DateTime.Parse(priceParam[8, 1].ToString());
                    datedDate = DateTime.Parse(priceParam[9, 1].ToString());
                }
            }
            else if (initSource == 1)
            {
                try
                {
                    this._iniPath = Path.Combine(assembleDirectory, "OutsideYield.ini");
                    TextReader sr = new StreamReader(this._iniPath);
                    string identifier = "[" + modelIndex + "]";
                    string nextIdentifier = "[" + (modelIndex+1) + "]";
                    string currentLine = sr.ReadLine();
                    while (currentLine != null)
                    {

                        if (currentLine.Equals(identifier))
                        {
                            while (currentLine != null)
                            {
                                if (currentLine.Equals(nextIdentifier))
                                {
                                    break;
                                }
                                if (currentLine.StartsWith("MaturityDate="))
                                {
                                    maturityDate = DateTime.Parse(currentLine.Trim().Substring(13).Trim());
                                }
                                else if (currentLine.StartsWith("DayCountMethod="))
                                {
                                    dayCountTypeString = currentLine.Trim().Substring(15).Trim();
                                }
                                else if (currentLine.StartsWith("Coupon="))
                                {
                                    coupon = float.Parse(currentLine.Trim().Substring(7).Trim());
                                }
                                else if (currentLine.StartsWith("SettlementDate="))
                                {
                                    settlementDate = DateTime.Today.AddDays(1);
                                    currentLine = currentLine.Trim().Substring(15).Trim();
                                    if (currentLine.ToLower().Equals("today"))
                                    {
                                        settlementDate = DateTime.Today;
                                    }
                                    else if (currentLine.ToLower().StartsWith("today+"))
                                    {
                                        settlementDate = DateTime.Today.AddDays(double.Parse(currentLine.Trim().Substring(6).Trim()));
                                        if (settlementDate.DayOfWeek == DayOfWeek.Saturday)
                                        {
                                            settlementDate = settlementDate.AddDays(2);
                                        }
                                        else if (settlementDate.DayOfWeek == DayOfWeek.Sunday)
                                        {
                                            settlementDate = settlementDate.AddDays(1);
                                        }
                                    }
                                    else
                                    {
                                        settlementDate = DateTime.Parse(currentLine);
                                        if (settlementDate.DayOfWeek == DayOfWeek.Saturday)
                                        {
                                            settlementDate = settlementDate.AddDays(2);
                                        }
                                        else if (settlementDate.DayOfWeek == DayOfWeek.Sunday)
                                        {
                                            settlementDate = settlementDate.AddDays(1);
                                        }
                                    }



                                }
                                else if (currentLine.StartsWith("ConversionFactor="))
                                {
                                    conversionFactor = float.Parse(currentLine.Trim().Substring(17).Trim());
                                }
                                else if (currentLine.StartsWith("EndOfMonthFlag="))
                                {
                                    eomConversion = bool.Parse(currentLine.Trim().Substring(15).Trim());
                                }
                                else if (currentLine.StartsWith("DatedDate="))
                                {
                                    string dd = currentLine.Trim().Substring(10).Trim();
                                    if (dd.Equals(string.Empty))
                                    {
                                        datedDate = DateTime.MinValue;
                                    }
                                    else
                                    {
                                        datedDate = DateTime.Parse(dd);
                                    }
                                }
                                else if (currentLine.StartsWith("FirstCouponDate="))
                                {
                                    string fcd = currentLine.Trim().Substring(16).Trim();
                                    if (fcd.Equals(string.Empty))
                                    {
                                        firstCouponDate = DateTime.MinValue;
                                    }
                                    else
                                    {
                                        firstCouponDate = DateTime.Parse(fcd);
                                    }
                                }
                                else if (currentLine.StartsWith("CouponsPerYear="))
                                {
                                    couponFrequency = int.Parse(currentLine.Trim().Substring(15).Trim());
                                }

                                currentLine = sr.ReadLine();
                            }
                        }
                        currentLine = sr.ReadLine();
                    }
                }
                catch(Exception ex)
                {
                    if (debugMode)
                    {
                        debugWrite("Could not read parameters from OutsideYield.ini, " + ex.ToString());
                    }
                }
            }
             
            switch (dayCountTypeString)
            {
                case "30/360":
                    dayCountType = 1;
                    break;
                case "30/365":
                    dayCountType = 2;
                    break;
                case "30E/360":
                    dayCountType = 3;
                    break;
                case "Actual/360":
                    dayCountType = 4;
                    break;
                case "Actual/365":
                    dayCountType = 5;
                    break;
                case "Actual/Actual":
                    dayCountType = 6;
                    break;
                default:
                    dayCountType = 1;
                    break;
            }

            param[0] = couponFrequency;
            param[1] = coupon;
            param[2] = dayCountType;
            param[3] = settlementDate;
            param[4] = maturityDate;
            param[5] = conversionFactor;
            param[6] = eomConversion;
            param[7] = optionalValues;
            param[8] = firstCouponDate;
            param[9] = datedDate;
        }

        public double PriceToYield(double price)
        {
            return priceYield(price, 1);
        }

        public double YieldToPrice(double yield)
        {
            return priceYield(yield, 0);
        }

        #endregion

        #region debug
        int debugWrite(string errorString)
        {
            TextWriter db = new StreamWriter("debug.txt");
            errorString = "Error Event with ErrorString: " + errorString + " occured: " +DateTime.Now.ToString();
            db.WriteLine(errorString);

            db.WriteLine("Parameters dump:");
            db.WriteLine("CouponsPerYear: " + param[0].ToString());
            db.WriteLine("Coupon: " + param[1].ToString());
            db.WriteLine("DayCountType: " + param[2].ToString());
            db.WriteLine("SettlementDate: " + param[3].ToString());
            db.WriteLine("MaturityDate: " + param[4].ToString());
            db.WriteLine("ConversionFactor: " + param[5].ToString());
            db.WriteLine("EOM Conversion: " + param[6].ToString());
            db.WriteLine("Additional Dates Flag: " + param[7].ToString());
            db.WriteLine("FirstCouponDate: " + param[8].ToString());
            db.WriteLine("DatedDate: " + param[9].ToString());

            int couponsPerYear = int.Parse(param[0].ToString());
            double coupon = double.Parse(param[1].ToString());
            int dayCountType = int.Parse(param[2].ToString());
            DateTime settlementDate = (DateTime)param[3];
            DateTime maturityDate = (DateTime)param[4];
            double conversionFactor = double.Parse(param[5].ToString());
            bool eomFlag = (bool)param[6];
            DateTime firstCouponDate = DateTime.MinValue;
            DateTime issueDate = DateTime.MinValue;
            if ((bool)param[7])
            {
                if (param[8] != null)
                {
                    firstCouponDate = (DateTime)param[8];
                }
                if (param[9] != null)
                {
                    issueDate = (DateTime)param[9];
                }
            }
            else
            {
                firstCouponDate = DateTime.MinValue;
                issueDate = DateTime.MinValue;
            }

            //tests daysInPeriod()
            int testDate = daysInPeriod(settlementDate, maturityDate, dayCountType, couponsPerYear);
            db.WriteLine("\ndaysInPeriod() result: " + testDate.ToString());
            //testsDaysBetween()
            int testDaysBetween = daysBetween(settlementDate, maturityDate, dayCountType, eomFlag);
            db.WriteLine("\ndaysBetween() result: " + testDaysBetween.ToString());
            db.Close();
            return 0;
        }
        #endregion

        #region OtherCalcs

        int daysInPeriod(DateTime dt1, DateTime dt2, int dayCountType, int m)
        {
            switch (dayCountType)
            {
                case 1:
                    //30/360
                    return 360 / m;
                case 2:
                    //30/365
                    return 365 / m;
                case 3:
                    //30E/360
                    return 360 / m;
                case 4:
                    //actual/360
                    return 360 / m;
                case 5:
                    //actual/365
                    return 365 / m;
                case 6:
                    //actual/actual
                    TimeSpan difference = dt2.Subtract(dt1);
                    return difference.Days;
                default:
                    return 0;
            }
        }

        int daysBetween(DateTime dt1, DateTime dt2, int dayCountType, bool eomFlag)
        {
            try
            {
                int d1 = 0;
                int d2 = 0;
                int m1 = 0;
                int m2 = 0;
                int y1 = 0;
                int y2 = 0;
                DateTime tmpDt1;
                DateTime tmpDt2;

                if (dayCountType == 4 || dayCountType == 5 || dayCountType == 6)
                {
                    TimeSpan difference = dt2.Subtract(dt1);
                    return difference.Days;
                }
                else if (dayCountType == 1 || dayCountType == 2)
                {
                    d1 = dt1.Day;
                    m1 = dt1.Month;
                    y1 = dt1.Year;
                    d2 = dt2.Day;
                    m2 = dt2.Month;
                    y2 = dt2.Year;

                    string dateString = "2/" + DateTime.DaysInMonth(dt1.Year, 2).ToString() +"/"+ dt1.Year.ToString();
                    tmpDt1 = DateTime.Parse(dateString);
                    dateString = "2/" + DateTime.DaysInMonth(dt2.Year, 2).ToString() +"/"+ dt2.Year.ToString();
                    tmpDt2 = DateTime.Parse(dateString);

                    if (eomFlag)
                    {
                        if (tmpDt1 == dt1 && tmpDt2 == dt2)
                        {
                            d2 = 30;
                        }
                        if (tmpDt1 == dt1)
                        {
                            d1 = 30;
                        }
                    }
                    if (d2 == 31 && (d1 == 30 || d1 == 31))
                    {
                        d2 = 30;
                    }
                    if (d1 == 31)
                    {
                        d1 = 30;
                    }
                    return ((y2 - y1) * 360) + ((m2 - m1) * 30) + (d2 - d1);
                }
                else if (dayCountType == 3)
                {
                    d1 = dt1.Day;
                    m1 = dt1.Month;
                    y1 = dt1.Year;
                    d2 = dt2.Day;
                    m2 = dt2.Month;
                    y2 = dt2.Year;

                    if (d1 == 31)
                    {
                        d2 = 30;
                    }
                    if (d2 == 31)
                    {
                        d2 = 30;
                    }
                    return ((y2 - y1) * 360) + ((m2 - m1) * 30) + (d2 - d1);
                }
                else
                {
                    return 0;
                }
            }
            catch
            {
                return 0;
            }
        }

        DateTime adjustForEOM(DateTime maturityDate, DateTime tmpDt, bool eomFlag)
        {
            DateTime eomDate;
            //If the maturity date falls on the last day of the month, 
            //make sure all coupon dates also fall on the last day of the month

            if (eomFlag == false)
            {
                return tmpDt;
            }
            string dateString =maturityDate.Month.ToString() + "/" + DateTime.DaysInMonth(maturityDate.Year, maturityDate.Month).ToString() + "/" + maturityDate.Year.ToString();
            eomDate = DateTime.Parse(dateString);
            

            if (eomDate == maturityDate)
            {
                dateString = tmpDt.Month.ToString() + "/" + DateTime.DaysInMonth(tmpDt.Year, tmpDt.Month).ToString() + "/" + tmpDt.Year.ToString();
                return DateTime.Parse(dateString);
            }
            else
            {
                return tmpDt;
            }
        }

        bool isValidFirstCouponDate(DateTime maturityDate, DateTime issueDate, DateTime couponDate, int mm, bool eomFlag)
        {
            DateTime tmpDate;
            int i = 1;

            if ((issueDate == DateTime.MinValue && couponDate != DateTime.MinValue) || (issueDate != DateTime.MinValue && couponDate == DateTime.MinValue))
            {
                return false;
            }

            tmpDate = maturityDate;

            while (tmpDate > issueDate)
            {
                if (tmpDate.Equals(couponDate))
                {
                    return true;
                }
                tmpDate = maturityDate.AddMonths(-1 * mm * i);
                tmpDate = adjustForEOM(maturityDate, tmpDate, eomFlag);
                i++;
            }

            return false;
        }

        double priceYield(double priceYield, int calcType)
        {
            /*
            param[0] = couponFrequency;
            param[1] = coupon;
            param[2] = DayCountType;
            param[3] = settlementDate;
            param[4] = maturityDate;
            param[5] = conversionFactor;
            param[6] = eomConversion;
            param[8] = firstCouponDate;
            param[9] = datedDate;
            */
            int m = (int)param[0];
            double coupon = double.Parse(param[1].ToString());
            int dayCountType = int.Parse(param[2].ToString());
            DateTime settlementDate = (DateTime)param[3];
            DateTime maturityDate = (DateTime)param[4];
            double conversionFactor = double.Parse(param[5].ToString());
            bool eomFlag = (bool)param[6];
            DateTime firstCouponDate = DateTime.MinValue;
            DateTime issueDate = DateTime.MinValue;
            if ((bool)param[7])
            {
                if (param[8] != null)
                {
                    firstCouponDate = (DateTime)param[8];
                }
                if (param[9] != null)
                {
                    issueDate = (DateTime)param[9];
                }
            }
            else
            {
                firstCouponDate = DateTime.MinValue;
                issueDate = DateTime.MinValue;
            }

            DateTime q1Date;
            
            double py;
            int rv = 100;
            int mm = 12 / m;
            double r = coupon / 100;

            if (calcType == 0)
            {
                py = priceYield / 100;
            }
            else if (calcType == 1)
            {
                if (priceYield < 1)
                {
                    return 0;
                }
                else
                {
                    py = priceYield * conversionFactor;
                }
            }
            else
            {
                return 0;
            }

            if (issueDate == DateTime.MinValue && firstCouponDate == DateTime.MinValue)
            {
                if (calcType == 0)
                {
                    return Math.Round(priceYieldForRegularCoupon(settlementDate, maturityDate, m, mm, rv, r, py, dayCountType, eomFlag, calcType), 6) / conversionFactor;
                }
                else
                {
                    double output = Math.Round(priceYieldForRegularCoupon(settlementDate, maturityDate, m, mm, rv, r, py, dayCountType, eomFlag, calcType), 6);
                    return output;
                }
            }
            else
            {
                if (isValidFirstCouponDate(maturityDate, issueDate, firstCouponDate, mm, eomFlag) == false)
                {
                    return 0;
                }
                if (settlementDate >= firstCouponDate)
                {
                    if (calcType == 0)
                    {
                        return Math.Round(priceYieldForRegularCoupon(settlementDate, maturityDate, m, mm, rv, r, py, dayCountType, eomFlag, calcType), 6) / conversionFactor;
                    }
                    else
                    {
                        return Math.Round(priceYieldForRegularCoupon(settlementDate, maturityDate, m, mm, rv, r, py, dayCountType, eomFlag, calcType), 6);
                    }
                }
                else
                {
                    q1Date = firstCouponDate.AddMonths(-1 * mm);
                    q1Date = adjustForEOM(maturityDate, q1Date, eomFlag);

                    if (issueDate >= q1Date)
                    {
                        //short first coupon period
                        if (calcType == 0)
                        {
                            return Math.Round(priceYieldForShortFirstCoupon(settlementDate, maturityDate, m, mm, rv, r, py, issueDate, firstCouponDate, dayCountType, eomFlag, calcType), 6) / conversionFactor;
                        }
                        else
                        {
                            return Math.Round(priceYieldForShortFirstCoupon(settlementDate, maturityDate, m, mm, rv, r, py, issueDate, firstCouponDate, dayCountType, eomFlag, calcType), 6);
                        }
                    }
                    else
                    {
                        if (calcType == 0)
                        {
                            return Math.Round(priceYieldForLongFirstCoupon(settlementDate, maturityDate, m, mm, rv, r, py, issueDate, firstCouponDate, dayCountType, eomFlag, calcType), 6);
                        }
                        else
                        {
                            return Math.Round(priceYieldForLongFirstCoupon(settlementDate, maturityDate, m, mm, rv, r, py, issueDate, firstCouponDate, dayCountType, eomFlag, calcType), 6);
                        }
                    }
                }    
            }
        }

        double priceYieldForRegularCoupon(DateTime settlementDate, DateTime maturityDate,
            int m, int mm, int rv, double r, double py, int dayCountType, bool eomFlag,
            int calcType)
        {
            int n = 0;
            DateTime previousCouponDate = maturityDate;
            DateTime tmpFirstCouponDate = DateTime.MinValue;
            do
            {
                n++;
                tmpFirstCouponDate = previousCouponDate;
                previousCouponDate = maturityDate.AddMonths(-1 * mm * n);
                previousCouponDate = adjustForEOM(maturityDate, previousCouponDate, eomFlag);
            } while (previousCouponDate > settlementDate);

            Single e = daysInPeriod(previousCouponDate, tmpFirstCouponDate, dayCountType, m);
            int a = daysBetween(previousCouponDate, settlementDate, dayCountType, eomFlag);
            int dsc = daysBetween(settlementDate, tmpFirstCouponDate, dayCountType, eomFlag);



            double fv;
            double v1;
            double v;
            double fvp;
            double z = 100 * r/m;
            double t = dsc / e;
            double ai = z * a / e;

            if (calcType == 0)
            {
                //price
                v = 1 / (1 + (py / m));
                
                return (rv*Math.Pow((v),(n-1+t))) + (z * ((Math.Pow(v,(n + t)) - Math.Pow(v , t)) / (v - 1))) - ai;
            }
            else if (calcType == 1)
            {
                //yield
                v1 = 1 / (1 + (r / m));

                int counter = 0;
                do
                {
                    counter++;
                    if (counter > 500)
                    {
                        return 0;
                    }
                    v = v1;
                    fv = (rv * Math.Pow(v, (n - 1 + t))) + (z * ((Math.Pow(v, (n + t)) - Math.Pow(v, t)) / (v - 1))) - ai - py;
                    fvp = (rv * (n - 1 + t) * Math.Pow(v, (n - 2 + t))) + (z * ((((n + t) * Math.Pow(v, (n + t - 1)) - t * Math.Pow(v, (t - 1))) * (v - 1)) - (Math.Pow(v, (n + t)) - Math.Pow(v, t))) / (Math.Pow((v - 1), 2)));
                    v1 = v - (fv / fvp);
                } while (Math.Abs(v - v1) > .00000005);
                double output = 100 * m * ((1 / v1) - 1);
                return output;
            }
            else
            {
                return 0;
            }                    
        }

        double priceYieldForShortFirstCoupon(DateTime settlementDate, DateTime maturityDate,
            int m, int mm, int rv, double r, double py, DateTime issueDate, DateTime firstCouponDate,
            int dayCountType, bool eomFlag, int calcType)
        {
            int n=0;
            int a;
            int dsc;
            int dfc;
            int counter;
            DateTime previousCouponDate;
            double v;
            double z;
            double t;
            double w;
            double q;
            double ai;
            double v1;
            double fv;
            double fvp;
            Single e;

            do
            {
                n++;
                previousCouponDate = maturityDate.AddMonths(-1*mm*n);
                previousCouponDate = adjustForEOM(maturityDate, previousCouponDate, eomFlag);
            } while (previousCouponDate > settlementDate);

            e = daysInPeriod(previousCouponDate, firstCouponDate, dayCountType, m);
            dfc = daysBetween(issueDate, firstCouponDate, dayCountType, eomFlag);
            a = daysBetween(issueDate, settlementDate, dayCountType, eomFlag);
            dsc = dfc - a;

            z = 100 * r / m;
            t = dsc / e;
            w = dfc / e;
            q = z * w;
            ai = z * a / 3;

            if (calcType == 0)
            {//Price
                v = 1 / (1 + (py / m));
                return (rv * Math.Pow(v, (n - 1 + t))) + (q * Math.Pow(v, t)) + (z * ((Math.Pow(v, (n + t)) - Math.Pow(v, t)) / (v - 1))) - (z * Math.Pow(v, t) - ai);
            }
            else if (calcType == 1)
            {//Yield
                v1 = 1 / (1 + (r / m));
                counter = 0;
                do
                {
                    counter++;
                    if (counter > 500)
                    {
                        return 0;
                    }

                    v = v1;
                    fv = (rv * Math.Pow(v, (n - 1 + t))) + (q * Math.Pow(v, t)) + (z * ((Math.Pow(v, (n + t)) - Math.Pow(v, t)) / (v - 1))) - (z * Math.Pow(v, t)) - ai - py;
                    fvp = (rv * (n - 1 + t) * Math.Pow(v, (n - 2 + t))) + (q * t * Math.Pow(v, (t - 1))) + (z * ((((n + t) * Math.Pow(v, (n + t - 1)) - t * Math.Pow(v, (t - 1))) * (v - 1)) - (Math.Pow(v, (n + t)) - Math.Pow(v, t))) / (Math.Pow((v - 1), 2))) - (z * t * Math.Pow(v, (t - 1)));
                    v1 = v - (fv / fvp);
                } while (Math.Abs(v - v1) > 0.00000005);
                return 100 * m * ((1 / v1) - 1);
            }
            else
            {
                return 0;
            }
        }

        double priceYieldForLongFirstCoupon(DateTime settlementDate, DateTime maturityDate, int m, int mm, int rv, double r, double py, DateTime issueDate, DateTime firstCouponDate, int dayCountType, bool eomFlag, int calcType)
        {
            int n = 0;
            int nqf = 0;
            DateTime previousCouponDate;
            DateTime nextCouponDate;
            do
            {
                n++;
                previousCouponDate = maturityDate.AddMonths(-1 * mm * n);
                previousCouponDate = adjustForEOM(maturityDate, previousCouponDate, eomFlag);
            } while (previousCouponDate > firstCouponDate);

            do
            {
                nqf++;
                nextCouponDate = previousCouponDate;
                previousCouponDate = firstCouponDate.AddMonths(-1 * mm * nqf);
                previousCouponDate = adjustForEOM(maturityDate, previousCouponDate, eomFlag);
            } while (previousCouponDate > settlementDate);
            nqf--;

            Single e = daysInPeriod(previousCouponDate, nextCouponDate, dayCountType, m);
            int dsc = daysBetween(settlementDate, nextCouponDate, dayCountType, eomFlag);
            double z = 100 * r / m;
            double t = nqf + (dsc / e);

            int ncf = 0;
            double d = 0;
            double ai = 0;
            previousCouponDate = firstCouponDate;

            Single nlf;
            int a = 0;
            int dfc =0;
            do
            {
                ncf++;
                nextCouponDate = previousCouponDate;
                previousCouponDate = firstCouponDate.AddMonths(-1 * mm * ncf);
                previousCouponDate = adjustForEOM(maturityDate, previousCouponDate, eomFlag);
                nlf = daysInPeriod(previousCouponDate, nextCouponDate, dayCountType, m);

                if (issueDate < previousCouponDate && settlementDate < previousCouponDate)
                {
                    //Neither the issue date nor the settlement date are in this period
                    a = 0;
                    dfc = daysBetween(previousCouponDate, nextCouponDate, dayCountType, eomFlag);
                }
                else if (issueDate >= previousCouponDate && issueDate < nextCouponDate && settlementDate >= nextCouponDate)
                {
                    //The issue date falls in this period but the settlement date does not
                    a = daysBetween(issueDate, nextCouponDate, dayCountType, eomFlag);
                    dfc = a;
                }
                else if (issueDate >= previousCouponDate && issueDate < nextCouponDate && settlementDate < nextCouponDate)
                {
                    //Both the issue date and settlement date fall in this period
                    a = daysBetween(issueDate, settlementDate, dayCountType, eomFlag);
                    dfc = daysBetween(issueDate, nextCouponDate, dayCountType, eomFlag);
                }
                else if (issueDate < previousCouponDate && settlementDate >= previousCouponDate && settlementDate < nextCouponDate)
                {
                    a = daysBetween(previousCouponDate, settlementDate, dayCountType, eomFlag);
                    dfc = daysBetween(previousCouponDate, nextCouponDate, dayCountType, eomFlag);
                }

                d = d + (z * dfc / nlf);
                ai = ai + (z * a / nlf);
            } while (previousCouponDate > issueDate);

            double v;

            double v1;
            double fv;
            double fvp;

            if (calcType == 0)
            {//price
                v = 1 / (1 + (py / m));
                return (rv * Math.Pow(v, (n + t))) + (d * Math.Pow(v, t)) + (z * ((Math.Pow(v, (n + t + 1)) - Math.Pow(v, (t + 1))) / (v - 1))) - ai;
            }
            else if (calcType == 1)
            {//yield
                v1 = 1 / (1 + (r / m));
                int counter = 0;
                do
                {
                    counter++;
                    if (counter > 500)
                    {
                        return 0;
                    }

                    v = v1;
                    fv = (rv * Math.Pow(v, (n + t))) + (d * Math.Pow(v, t)) + (z * (( Math.Pow(v, (n + t + 1)) - Math.Pow(v, (t + 1))) / (v - 1))) - ai - py;
                    fvp = (rv * (n + t) * Math.Pow(v, (n + t - 1))) + (d * t * Math.Pow(v, (t - 1))) + (z * ((((n + t + 1) * Math.Pow(v, (n + t)) - (t + 1) * Math.Pow(v, t)) * (v - 1)) - (Math.Pow(v, (n + t + 1)) - Math.Pow(v, (t + 1)))) / (Math.Pow((v - 1), 2)));
                    v1 = v - (fv / fvp);
                } while (Math.Abs(v-v1) > 0.00000005);

                return 100 * m * ((1 / v1) - 1);
            }
            else
            {
                return 0;
            }
            
        }
        #endregion
    }
}