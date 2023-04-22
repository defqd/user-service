using System.ComponentModel.DataAnnotations;

namespace UserWebAPI.Helper.Validation
{
    sealed public class GenderAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            int? gender = (int?)value;

            if (gender == null)
                return false;

            if (gender >= 0 && gender <= 2) 
                return true;

            ErrorMessage = "Поле пол может содержать 0 - Женский, 1 - Мужской, 2 - Неизвестно";

            return false;
        }
    }
}