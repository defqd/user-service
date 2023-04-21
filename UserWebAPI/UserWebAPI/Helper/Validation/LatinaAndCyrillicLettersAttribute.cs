using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace UserWebAPI.Helper.Validation
{
    sealed public class LatinaAndCyrillicLettersAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            string? text = (string?)value;

            if (text == null)
                return false;

            var result = Regex.IsMatch(text, @"^[a-zA-Zа-яёА-ЯЁ]+$");

            return result;
        }
    }
}