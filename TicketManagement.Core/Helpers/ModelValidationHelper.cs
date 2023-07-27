using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace Services.Helpers
{
    public static class ModelValidationHelper
    {
        internal static void Validate(object obj)
        {
            ValidationContext validationContext = new ValidationContext(obj);
            List<ValidationResult> validationResults = new List<ValidationResult>();

            bool isValid = Validator.TryValidateObject(obj, validationContext, validationResults, true);
            if (!isValid)
            {
                throw new ArgumentException(validationResults.FirstOrDefault()?.ErrorMessage);
            }
        }

        public static bool ValidateTicketFromStringArray(string[] ticketFct)
        {
            //Validate fields number
            if (ticketFct.Length != 8)
            {
                return false;
            }

            //Validate [Id_Tienda]
            if (!ticketFct[0].StartsWith("-->"))
            {
                return false;
            }

            if (!Regex.Match(ticketFct[0].Substring(3), "^\\d*$", RegexOptions.IgnoreCase).Success)
            {
                return false;
            }

            //Validate [Id_Registradora]
            if (!Regex.Match(ticketFct[1], "^\\d*$", RegexOptions.IgnoreCase).Success)
            {
                return false;
            }

            //Validate [FechaHora]
            if (!Regex.Match(ticketFct[2], "^\\d{8}$", RegexOptions.IgnoreCase).Success)
            {
                return false;
            }

            //Validate [Hora del ticket]
            if (!Regex.Match(ticketFct[3], "^\\d{6}$", RegexOptions.IgnoreCase).Success)
            {
                return false;
            }

            //Validate [Ticket]
            if (!Regex.Match(ticketFct[4], "^\\d*$", RegexOptions.IgnoreCase).Success)
            {
                return false;
            }

            //Validate [Importe Impuesto]
            if (!Regex.Match(ticketFct[5].Trim(), "^^\\d*\\.\\d+$", RegexOptions.IgnoreCase).Success)
            {
                return false;
            }

            //Validate [Total]
            if (!Regex.Match(ticketFct[6].Trim(), "^^\\d*\\.\\d+$", RegexOptions.IgnoreCase).Success)
            {
                return false;
            }

            return true;
        }
    }
}
