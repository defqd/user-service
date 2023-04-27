using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace UserWebAPI.Helper.Validation
{
    sealed public class LatinaLettersAndNumbersAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            string? text = (string?)value;

            if (text == null)
                return false;

            bool result = Regex.IsMatch(text, @"^[0-9a-zA-Zа]+$");

            ErrorMessage = "Поле должно содержать только латинские буквы или цифры";

            return result;
        }
    }
}