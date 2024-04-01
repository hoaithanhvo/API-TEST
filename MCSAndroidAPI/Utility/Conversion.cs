using System.Configuration;
using System.Globalization;
using System.Security.Cryptography;
using System.Text;

namespace MCSAndroidAPI.Utility
{
    public static class Conversion
    {
        public static string NumberToRoman(int number)
        {
            // Validate
            if (number < 0 || number > 3999)
            {
                return String.Empty;
            }

            if (number == 0) return "N";

            // Set up key numerals and numeral pairs
            int[] values = new int[] { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };
            string[] numerals = new string[] { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };

            // Initialise the string builder
            StringBuilder result = new StringBuilder();

            // Loop through each of the values to diminish the number
            for (int i = 0; i < 13; i++)
            {
                // If the number being converted is less than the test value, append
                // the corresponding numeral or numeral pair to the resultant string
                while (number >= values[i])
                {
                    number -= values[i];
                    result.Append(numerals[i]);
                }
            }

            // Done
            return result.ToString();
        }

        public static string parsePeriod(int year, int month)
        {
            return string.Concat(year, "-", month.ToString().Length == 1 ? month.ToString().Insert(0, "0") : month.ToString());
        }

        public static DateTime parseDateTime(object obj)
        {
            return Convert.ToDateTime(obj);
        }

        public static DateTime parseDateTime(object obj, string format)
        {
            DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
            dtfi.ShortDatePattern = format;
            dtfi.DateSeparator = "/";

            return Convert.ToDateTime(obj, dtfi);
        }

        public static TimeSpan parseTime(object obj)
        {
            try
            {
                DateTime d = DateTime.Parse(obj.ToString());
                return d.TimeOfDay;
            }
            catch
            {
                return new TimeSpan();
            }
        }


        public static TimeSpan parseTime(object obj, string format)
        {
            try
            {
                DateTime d = DateTime.ParseExact(obj.ToString(), format, CultureInfo.InvariantCulture);
                return d.TimeOfDay;
            }
            catch
            {
                return new TimeSpan();
            }
        }

        public static DateTime? parseNullableDateTime(object obj, string format)
        {
            DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
            dtfi.ShortDatePattern = format;
            dtfi.DateSeparator = "/";
            if (Validation.isNullOrEmpty(obj))
                return null;
            else if (!Validation.isDateTime(obj.ToString(), format))
                return null;
            return Convert.ToDateTime(obj, dtfi);
        }

        public static DateTime parseNotNullableDateTime(object obj, string format)
        {
            DateTimeFormatInfo dtfi = new DateTimeFormatInfo();
            dtfi.ShortDatePattern = format;
            dtfi.DateSeparator = "/";
            if (Validation.isNullOrEmpty(obj))
                return DateTime.Now;
            else if (!Validation.isDateTime(obj.ToString(), format))
                return DateTime.Now;
            return Convert.ToDateTime(obj, dtfi);
        }

        public static long parseLong(object obj)
        {
            try
            {
                if (Validation.isNullOrEmpty(obj))
                    return 0;
                return long.Parse(obj.ToString());
            }
            catch
            {
                return 0;
            }
        }

        public static short parseShort(object obj)
        {
            try
            {
                if (Validation.isNullOrEmpty(obj))
                    return 0;
                return short.Parse(obj.ToString());
            }
            catch
            {
                return 0;
            }
        }
        public static int parseInt(object obj)
        {
            try
            {
                if (Validation.isNullOrEmpty(obj))
                    return 0;
                if (parseString(obj).Equals("True", StringComparison.OrdinalIgnoreCase))
                {
                    return 1;
                }
                else if (parseString(obj).Equals("False", StringComparison.OrdinalIgnoreCase))
                {
                    return 0;
                }
                return int.Parse(obj.ToString());
            }
            catch
            {
                return 0;
            }
        }

        public static short parseBoolToShort(object obj)
        {
            try
            {
                string temp = string.Empty;
                if (Validation.isNullOrEmpty(obj))
                    return 0;
                else
                {
                    temp = (bool)obj == true ? "1" : "0";
                }
                return short.Parse(temp);
            }
            catch
            {
                return 0;
            }
        }

        public static double parseDouble(object obj)
        {
            try
            {
                if (Validation.isNullOrEmpty(obj))
                    return 0;
                return double.Parse(obj.ToString());
            }
            catch
            {
                return 0;
            }
        }
        public static float parseFloat(object obj)
        {
            try
            {
                if (Validation.isNullOrEmpty(obj))
                    return 0;
                return Convert.ToSingle(obj.ToString());
            }
            catch
            {
                return 0;
            }
        }
        public static decimal parseDecimal(object obj)
        {
            try
            {
                if (Validation.isNullOrEmpty(obj))
                    return 0;
                return decimal.Parse(obj.ToString());
            }
            catch
            {
                return 0;
            }
        }
        public static decimal parseDecimal(object obj, NumberStyles style)
        {
            try
            {
                if (Validation.isNullOrEmpty(obj))
                    return 0;
                return decimal.Parse(obj.ToString(), style);
            }
            catch
            {
                return 0;
            }
        }
        public static string parseString(object obj)
        {
            try
            {
                if (Validation.isNullOrEmpty(obj))
                    return "";
                return obj.ToString().Trim();
            }
            catch
            {
                return "";
            }
        }

        public static decimal parseInternalDecimal(object obj, string currency)
        {
            decimal result = 0;
            if (currency.Equals("USD"))
            {
                result = parseDecimal(obj);
            }
            else
            {
                result = parseDecimal(parseDecimal(obj).ToString("N0"));
            }
            return result;
        }


        public static bool parseBoolean(object obj)
        {
            try
            {
                if (Validation.isNullOrEmpty(obj))
                    return false;
                return Convert.ToBoolean(obj);
            }
            catch
            {
                return false;
            }
        }
        //Convert Datetime to String with specific format
        public static string parseString(DateTime? date, string format)
        {
            try
            {
                if (Validation.isNullOrEmpty(date))
                    return "";
                return date.Value.ToString(format);
            }
            catch
            {
                return "";
            }
        }

        public static string ConvertCurrencyToWord(decimal? value, string currency)
        {
            string result = string.Empty;
            try
            {
                if (Validation.isNullOrEmpty(value))
                    return result;
                switch (currency)
                {
                    case "USD":
                        result = value.Value.ToString("N2");
                        //result = String.Format("{0:#,###,###,##0.##}", value.Value);
                        break;
                    case "JPY":
                        result = value.Value.ToString("N0");
                        //result = String.Format("{0:#,###,###,##0}", value.Value);
                        break;
                    case "VND":
                        result = value.Value.ToString("N0");
                        //result = String.Format("{0:#,###,###,##0}", value.Value);
                        break;
                    default:
                        result = value.Value.ToString("N2");
                        //result = String.Format("{0:#,###,###,##0.##}", value.Value);
                        break;
                }
            }
            catch
            {
                return result;
            }
            return result;
        }

        public static string ConvertNumberToWord(string str)
        {
            string strNum1 = "";
            string strNum2 = "";
            string total = "";
            string strTemp = "";
            string strTemp2 = "";
            string result = "";
            str = str.Replace(",", "");

            if (str.Contains('.'))
            {
                strNum1 = ConvertToWord(Convert.ToInt64(str.Substring(str.IndexOf('.') + 1, str.Length - str.IndexOf('.') - 1)));
                if (str.Contains(','))
                {
                    strTemp = str.Substring(0, str.IndexOf(','));
                    strTemp2 = str.Substring(strTemp.ToString().Length + 1, str.Length - (strTemp.ToString().Length + 4));
                    total = strTemp.ToString() + strTemp2.ToString();
                }
                else
                {
                    strTemp2 = str.Substring(0, str.IndexOf('.'));
                    total = strTemp2.ToString();
                }
                strNum2 = ConvertToWord(Convert.ToInt64(total));

                if (strNum1 != "")
                {
                    return result = strNum2 + " USD AND " + strNum1 + " PENCES";
                }
                else
                {
                    return result = strNum2 + " USD";
                }
            }
            else
            {
                return result = ConvertToWord(Convert.ToInt64(str)) + " USD ";
            }
        }
        public static string ConvertToWord(long nNumber)
        {
            long CurrentNumber = nNumber;
            string sReturn = "";

            if (CurrentNumber >= 1000000000)
            {
                sReturn = sReturn + " " + GetWord(CurrentNumber / 1000000000, "BILLION");
                CurrentNumber = CurrentNumber % 1000000000;
            }
            if (CurrentNumber >= 1000000)
            {
                sReturn = sReturn + " " + GetWord(CurrentNumber / 1000000, "MILLION");
                CurrentNumber = CurrentNumber % 1000000;
            }
            if (CurrentNumber >= 1000)
            {
                sReturn = sReturn + " " + GetWord(CurrentNumber / 1000, "THOUSAND");
                CurrentNumber = CurrentNumber % 1000;
            }
            if (CurrentNumber >= 100)
            {
                sReturn = sReturn + " " + GetWord(CurrentNumber / 100, "HUNDRED");
                CurrentNumber = CurrentNumber % 100;
            }
            if (CurrentNumber >= 20)
            {
                sReturn = sReturn + " " + GetWord(CurrentNumber, "");
                CurrentNumber = CurrentNumber % 10;
            }
            else if (CurrentNumber > 0)
            {
                sReturn = sReturn + " " + GetWord(CurrentNumber, "");
                CurrentNumber = 0;
            }
            return sReturn.Replace("  ", " ").Trim();
        }
        private static string GetWord(long nNumber, string sPrefix)
        {
            long nCurrentNumber = nNumber;
            string sReturn = "";
            while (nCurrentNumber > 0)
            {
                if (nCurrentNumber > 100)
                {
                    sReturn = sReturn + " " + GetWord(nCurrentNumber / 100, "HUNDRED");
                    nCurrentNumber = nCurrentNumber % 100;
                }
                else if (nCurrentNumber > 20)
                {
                    sReturn = sReturn + " " + GetTwentyWord(nCurrentNumber / 10);
                    nCurrentNumber = nCurrentNumber % 10;
                }
                else
                {
                    sReturn = sReturn + " " + GetLessThanTwentyWord(nCurrentNumber);
                    nCurrentNumber = 0;
                }
            }
            sReturn = sReturn + " " + sPrefix;
            return sReturn;
        }
        private static string GetTwentyWord(long nNumber)
        {
            string sReturn = "";
            switch (nNumber)
            {
                case 2:
                    sReturn = "TWENTY";
                    break;
                case 3:
                    sReturn = "THIRTY";
                    break;
                case 4:
                    sReturn = "FORTY";
                    break;
                case 5:
                    sReturn = "FIFTY";
                    break;
                case 6:
                    sReturn = "SIXTY";
                    break;
                case 7:
                    sReturn = "SEVENTY";
                    break;
                case 8:
                    sReturn = "EIGHTY";
                    break;
                case 9:
                    sReturn = "NINETY";
                    break;
            }
            return sReturn;
        }
        private static string GetLessThanTwentyWord(long nNumber)
        {
            string sReturn = "";
            switch (nNumber)
            {
                case 1:
                    sReturn = "ONE";
                    break;
                case 2:
                    sReturn = "TWO";
                    break;
                case 3:
                    sReturn = "THREE";
                    break;
                case 4:
                    sReturn = "FOUR";
                    break;
                case 5:
                    sReturn = "FIVE";
                    break;
                case 6:
                    sReturn = "SIX";
                    break;
                case 7:
                    sReturn = "SEVEN";
                    break;
                case 8:
                    sReturn = "EIGHT";
                    break;
                case 9:
                    sReturn = "NINE";
                    break;
                case 10:
                    sReturn = "TEN";
                    break;
                case 11:
                    sReturn = "ELEVEN";
                    break;
                case 12:
                    sReturn = "TWELVE";
                    break;
                case 13:
                    sReturn = "THIRTEEN";
                    break;
                case 14:
                    sReturn = "FORTEEN";
                    break;
                case 15:
                    sReturn = "FIFTEEN";
                    break;
                case 16:
                    sReturn = "SIXTEEN";
                    break;
                case 17:
                    sReturn = "SEVENTEEN";
                    break;
                case 18:
                    sReturn = "EIGHTEEN";
                    break;
                case 19:
                    sReturn = "NINETEEN";
                    break;
            }
            return sReturn;
        }
        public static string splipString(string value, char keySplip)
        {
            if (value.Contains(keySplip))
            {
                string[] s = value.Split(keySplip);

                return s[0].ToString();
            }
            else
            {
                return value;
            }


        }
        public static string Md5Encrypt(string toEncrypt, bool useHashing)
        {
            byte[] keyArray;
            byte[] toEncryptArray = UTF8Encoding.UTF8.GetBytes(toEncrypt);

            System.Configuration.AppSettingsReader settingsReader =
                                                new AppSettingsReader();
            // Get the key from config file

            //string key = (string)settingsReader.GetValue("SecurityKey",
            //                                                 typeof(String));
            string key = "abcdef";
            //System.Windows.Forms.MessageBox.Show(key);
            //If hashing use get hashcode regards to your key
            if (useHashing)
            {
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //Always release the resources and flush data
                // of the Cryptographic service provide. Best Practice

                hashmd5.Clear();
            }
            else
                keyArray = UTF8Encoding.UTF8.GetBytes(key);

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes.
            //We choose ECB(Electronic code Book)
            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)

            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateEncryptor();
            //transform the specified region of bytes array to resultArray
            byte[] resultArray =
              cTransform.TransformFinalBlock(toEncryptArray, 0,
              toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor
            tdes.Clear();
            //Return the encrypted data into unreadable string format
            return Convert.ToBase64String(resultArray, 0, resultArray.Length);
        }
        public static string Md5Decrypt(string cipherString, bool useHashing)
        {
            byte[] keyArray;
            //get the byte code of the string

            byte[] toEncryptArray = Convert.FromBase64String(cipherString);

            System.Configuration.AppSettingsReader settingsReader =
                                                new AppSettingsReader();
            //Get your key from config file to open the lock!
            // string key = (string)settingsReader.GetValue("SecurityKey",
            //                                             typeof(String));
            string key = "abcdef";
            if (useHashing)
            {
                //if hashing was used get the hash code with regards to your key
                MD5CryptoServiceProvider hashmd5 = new MD5CryptoServiceProvider();
                keyArray = hashmd5.ComputeHash(UTF8Encoding.UTF8.GetBytes(key));
                //release any resource held by the MD5CryptoServiceProvider

                hashmd5.Clear();
            }
            else
            {
                //if hashing was not implemented get the byte code of the key
                keyArray = UTF8Encoding.UTF8.GetBytes(key);
            }

            TripleDESCryptoServiceProvider tdes = new TripleDESCryptoServiceProvider();
            //set the secret key for the tripleDES algorithm
            tdes.Key = keyArray;
            //mode of operation. there are other 4 modes. 
            //We choose ECB(Electronic code Book)

            tdes.Mode = CipherMode.ECB;
            //padding mode(if any extra byte added)
            tdes.Padding = PaddingMode.PKCS7;

            ICryptoTransform cTransform = tdes.CreateDecryptor();
            byte[] resultArray = cTransform.TransformFinalBlock(
                                 toEncryptArray, 0, toEncryptArray.Length);
            //Release resources held by TripleDes Encryptor                
            tdes.Clear();
            //return the Clear decrypted TEXT
            return UTF8Encoding.UTF8.GetString(resultArray);
        }
        public static string MD5Encrypt(string value)
        {
            MD5CryptoServiceProvider s_md5 = null;
            if (s_md5 == null) //creating only when needed
                s_md5 = new MD5CryptoServiceProvider();
            Byte[] newdata = Encoding.Default.GetBytes(value);
            Byte[] encrypted = s_md5.ComputeHash(newdata);
            return BitConverter.ToString(encrypted).Replace("-", "");
        }
        public static string filterCharSQl(string text)
        {

            string result = text;
            if (text.Contains("\\"))
            {
                result = text.Replace("\\", "\\");
            }

            if (text.Contains("'"))
            {
                result = text.Replace("'", "\\'");
            }

            return result;
        }
        public static void LogError(string error)
        {
            try
            {
                string pathServer = Environment.CurrentDirectory;
                string path = "";
                if (pathServer.Contains("Template"))
                    path = Environment.CurrentDirectory + "\\FileError.txt";
                else
                    path = Environment.CurrentDirectory + "\\Template\\FileError.txt";
                if (!System.IO.File.Exists(path))
                {
                    System.IO.File.Create(path);
                }
                System.IO.StreamWriter Tex = new System.IO.StreamWriter(path, true);
                Tex.WriteLine(DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss").ToUpper() + " : " + error);
                Tex.Write(Tex.NewLine);
                Tex.Close();
            }
            catch { }
        }

        public static int GetNumberOfRow()
        {
            try
            {
                return 15;
            }
            catch
            {
                return 15;
            }
        }

        public static string TruncateURL(string url)
        {
            if (url.IndexOf("/") == 0)
            {
                url = url.Remove(0, 1);
            }

            if (url.LastIndexOf("/") == url.Length - 1)
            {
                url = url.Remove(url.Length - 1, 1);
            }
            return url;
        }
    }
}
