using MCSAndroidAPI.Constants;
using MCSAndroidAPI.Models;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.Configuration;
using System.Diagnostics;
using System.Globalization;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;


namespace MCSAndroidAPI.Utility
{
    public static class Validation
    {
        private readonly static ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
        private readonly static ILogger _logger = factory.CreateLogger(typeof(Validation));

        public static ClaimsPrincipal ValidateToken(string jwtToken)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            IdentityModelEventSource.ShowPII = true;

            SecurityToken validatedToken;
            TokenValidationParameters validationParameters = new TokenValidationParameters();

            validationParameters.ValidateLifetime = true;

            validationParameters.ValidAudience = configuration["JWT:ValidAudience"];
            validationParameters.ValidIssuer = configuration["JWT:ValidIssuer"];
            validationParameters.IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["JWT:Secret"]));

            ClaimsPrincipal principal = new JwtSecurityTokenHandler().ValidateToken(jwtToken, validationParameters, out validatedToken);


            return principal;
        }

        private static bool CheckModel<T>(T model, PropertyInfo[] properties, FieldInfo[] maxlengthFields, FieldInfo[] displayFields, ref string message, string[]? skipFields)
        {
            try
            {

                foreach (PropertyInfo property in properties)
                {
                    object value = property.GetValue(model)!;

                    // check required
                    // If it's an skipfield, skip it
                    if (isNullOrEmpty(value) && (isNullOrEmpty(skipFields) || !skipFields!.Contains(property.Name)))
                    {
                        var displayField = displayFields.First(x => x.Name == property.Name).GetValue(null)?.ToString();
                        message = SystemConstants.Message.FIELD_IS_REQUIRED.Replace("{0}", displayField);
                        return false;
                    }

                    // check maxlength
                    foreach (FieldInfo field in maxlengthFields)
                    {
                        int maxlength = 0;
                        // Check the field is constant
                        if (field.IsLiteral)
                        {
                            int.TryParse(field.GetValue(null)?.ToString(), out maxlength);
                        }

                        if (!isNullOrEmpty(value) && property.Name == field.Name && value.ToString()?.Length > maxlength)
                        {
                            var displayField = displayFields.First(x => x.Name == property.Name).GetValue(null)?.ToString();
                            message = SystemConstants.Message.MAXLENGTH.Replace("{0}", displayField).Replace("{1}", maxlength.ToString());
                            return false;
                        }
                    }
                }
            } catch(Exception ex)
            {
                _logger.LogError(ex.ToString());
                return false;
            }

            return true;
        }

        public static bool ValidateLotManModel(LotManModel model, out string message, string[]? skipFields = null)
        {
            message = string.Empty;

            try
            {
                // get properties of class LotManModel
                Type typeOfModel = typeof(LotManModel);
                PropertyInfo[] properties = typeOfModel.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);

                // get fields of class LotManMaxlength
                Type typeOfMaxlength = typeof(LotManMaxlength);
                FieldInfo[] maxlengthFields = typeOfMaxlength.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy | BindingFlags.Instance);

                // get fields of class LotManDisplay
                Type typeOfDisplay = typeof(LotManDisplay);
                FieldInfo[] displayFields = typeOfDisplay.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy | BindingFlags.Instance);

                return CheckModel(model, properties, maxlengthFields, displayFields, ref message, skipFields);

            } catch (Exception ex)
            {
                Debug.WriteLine(ex) ;
                return false;
            }
        }

        public static bool ValidateLotStartModel(LotStartModel model, out string message, string[]? skipFields = null)
        {
            message = string.Empty;

            try
            {
                // get properties of class LotStartModel
                Type typeOfModel = typeof(LotStartModel);
                PropertyInfo[] properties = typeOfModel.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);

                // get fields of class LotStartMaxlength
                Type typeOfMaxlength = typeof(LotStartMaxlength);
                FieldInfo[] maxlengthFields = typeOfMaxlength.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy | BindingFlags.Instance);

                // get fields of class LotStartDisplay
                Type typeOfDisplay = typeof(LotStartDisplay);
                FieldInfo[] displayFields = typeOfDisplay.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy | BindingFlags.Instance);

                return CheckModel(model, properties , maxlengthFields, displayFields, ref message, skipFields);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }

        public static bool ValidateLotDefectModel(LotDefectModel model, out string message, string[]? skipFields = null)
        {
            message = string.Empty;

            try
            {
                // get properties of class LotDefectModel
                Type typeOfModel = typeof(LotDefectModel);
                PropertyInfo[] properties = typeOfModel.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);

                // get fields of class LotDefectMaxlength
                Type typeOfMaxlength = typeof(LotDefectMaxlength);
                FieldInfo[] maxlengthFields = typeOfMaxlength.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy | BindingFlags.Instance);

                // get fields of class LotDefectDisplay
                Type typeOfDisplay = typeof(LotDefectDisplay);
                FieldInfo[] displayFields = typeOfDisplay.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy | BindingFlags.Instance);

                return CheckModel(model, properties, maxlengthFields, displayFields, ref message, skipFields);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }
        public static bool ValidateLotStopModel(LotStopModel model, out string message, string[]? skipFields = null)
        {
            message = string.Empty;

            try
            {
                // get properties of class LotStopModel
                Type typeOfModel = typeof(LotStopModel);
                PropertyInfo[] properties = typeOfModel.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);

                // get fields of class LotStopMaxlength
                Type typeOfMaxlength = typeof(LotStopMaxlength);
                FieldInfo[] maxlengthFields = typeOfMaxlength.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy | BindingFlags.Instance);

                // get fields of class LotStopDisplay
                Type typeOfDisplay = typeof(LotStopDisplay);
                FieldInfo[] displayFields = typeOfDisplay.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy | BindingFlags.Instance);

                return CheckModel(model, properties, maxlengthFields, displayFields, ref message, skipFields);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }

        public static bool ValidateLotScrapModel(LotScrapModel model, out string message, string[]? skipFields = null)
        {
            message = string.Empty;

            try
            {
                // get properties of class LotScrapModel
                Type typeOfModel = typeof(LotScrapModel);
                PropertyInfo[] properties = typeOfModel.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);

                // get fields of class LotScrapMaxlength
                Type typeOfMaxlength = typeof(LotScrapMaxlength);
                FieldInfo[] maxlengthFields = typeOfMaxlength.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy | BindingFlags.Instance);

                // get fields of class LotScrapDisplay
                Type typeOfDisplay = typeof(LotScrapDisplay);
                FieldInfo[] displayFields = typeOfDisplay.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy | BindingFlags.Instance);

                return CheckModel(model, properties, maxlengthFields, displayFields, ref message, skipFields);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }

        public static bool ValidateDefectDetailModel(DefectDetailModel model, out string message, string[]? skipFields = null)
        {
            message = string.Empty;

            try
            {
                // get properties of class DefectDetailModel
                Type typeOfModel = typeof(DefectDetailModel);
                PropertyInfo[] properties = typeOfModel.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.FlattenHierarchy);

                // get fields of class DefectDetailMaxlength
                Type typeOfMaxlength = typeof(DefectDetailMaxlength);
                FieldInfo[] maxlengthFields = typeOfMaxlength.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy | BindingFlags.Instance);

                // get fields of class DefectDetailDisplay
                Type typeOfDisplay = typeof(DefectDetailDisplay);
                FieldInfo[] displayFields = typeOfDisplay.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy | BindingFlags.Instance);

                return CheckModel(model, properties, maxlengthFields, displayFields, ref message, skipFields);
            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex);
                return false;
            }
        }
        public static bool isNullOrEmpty(object? obj)
        {
            try
            {
                if (obj == null)
                    return true;
                if (obj.ToString()!.Trim().Equals("") || obj.ToString()!.Trim().Equals("null"))
                    return true;
                return false;
            }
            catch
            {
                return false;
            }
        }
        public static bool IsAlphaNumeric(string pText)
        {
            Regex regex = new Regex(@"^[A-Za-z0-9_]+$");
            return regex.IsMatch(pText);
        }

        public static bool IsNumber(string pText)
        {
            Regex regex = new Regex(@"^[-+]?[0-9]*\.?[0-9]+$");
            return regex.IsMatch(pText);
        }

        public static bool IsPositiveNumber(string pText)
        {
            Regex regex = new Regex(@"^[0-9]*\.?[0-9]+$");
            return regex.IsMatch(pText);
        }

        public static bool IsPositiveInteger(string pValue)
        {
            foreach (Char c in pValue)
            {
                if (!Char.IsDigit(c))
                    return false;
            }
            return true;
        }
        public static bool IsNumeric(object Expression)
        {
            // Variable to collect the Return value of the TryParse method.
            bool isNum;

            // Define variable to collect out parameter of the TryParse method. If the conversion fails, the out parameter is zero.
            double retNum;

            // The TryParse method converts a string in a specified style and culture-specific format to its double-precision floating point number equivalent.
            // The TryParse method does not generate an exception if the conversion fails. If the conversion passes, True is returned. If it does not, False is returned.

            isNum = Double.TryParse(Convert.ToString(Expression), System.Globalization.NumberStyles.Any, System.Globalization.NumberFormatInfo.InvariantInfo, out retNum);
            return isNum;
        }
        public static bool IsInteger(string pValue)
        {
            try
            {
                int n = Convert.ToInt32(pValue);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool isValidMaxLength(object obj, long maxLength)
        {
            try
            {
                if (isNullOrEmpty(obj))
                    return true;
                if (obj.ToString().Trim().Length <= maxLength)
                    return true;

                return false;
            }
            catch
            {
                return false;
            }
        }
        public static bool IsValidEmailAddress(string sEmail)
        {
            if (sEmail == null)
            {
                return false;
            }

            int nFirstAT = sEmail.IndexOf('@');
            int nLastAT = sEmail.LastIndexOf('@');

            if ((nFirstAT > 0) && (nLastAT == nFirstAT) &&
            (nFirstAT < (sEmail.Length - 1)))
            {
                // address is ok regarding the single @ sign
                return (Regex.IsMatch(sEmail, @"(\w+)@([\w\-]+)\.(\w+)"));
            }
            else
            {
                return false;
            }
        }
        public static bool isDate(string strDate)
        {
            string valuePassed = string.Empty;
            valuePassed = strDate;
            DateTime dt = DateTime.MinValue;

            try
            {
                string strRegex = @"^\d{4}([/])\d{1,2}([/])\d{1,2}$";
                Regex re = new Regex(strRegex);
                if (!re.IsMatch(strDate))
                    return false;

                IFormatProvider format = new CultureInfo("en-US");

                dt = DateTime.Parse(valuePassed, format, DateTimeStyles.None);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool isDateTime(string pDate)
        {
            try
            {
                DateTime A = DateTime.Parse(pDate);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool isDateTime(string pDate, string pDateFormat)
        {
            try
            {
                DateTime A = Conversion.parseDateTime(pDate, pDateFormat);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool isTime(string pTime, string pTimeFormat)
        {
            try
            {
                TimeSpan A = Conversion.parseTime(pTime, pTimeFormat);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool isInteger(string pNum)
        {
            try
            {
                int A = Convert.ToInt32(pNum);
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool isDouble(string pNum)
        {
            try
            {
                double A = Convert.ToDouble(pNum);
                return true;
            }
            catch
            {
                return false;
            }
        }

    }
}
