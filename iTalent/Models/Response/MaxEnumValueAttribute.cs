namespace iTalent.Models.Response
{
    using System;
    using System.ComponentModel.DataAnnotations;

    public class MaxEnumValueAttribute : ValidationAttribute
    {
        private readonly int maxValue;

        public MaxEnumValueAttribute(int maxValue)
        {
            this.maxValue = maxValue;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value != null)
            {
                if (Enum.TryParse(validationContext.ObjectType, value.ToString(), out object parsedValue))
                {
                    int intValue = (int)parsedValue;
                    if (intValue > maxValue)
                    {
                        return new ValidationResult($"The field {validationContext.DisplayName} cannot exceed {maxValue}.");
                    }
                }
            }

            return ValidationResult.Success;
        }
    }

}
