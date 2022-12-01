using BeatStore.API.Helpers.Enums;
using System.ComponentModel.DataAnnotations;

namespace BeatStore.API.Helpers.Validation
{
    public class PaymentMethodValidator : ValidationAttribute
    {
        public PaymentMethodValidator() { }
        public string Method { get; }

        public string GetErrorMessage() =>
            $"Invalid payment method";

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            try
            {
                string stringValue = (string)value;
                var isValid = Enum.TryParse<PaymentMethod>(stringValue, true, out _);

                if (isValid)
                    return ValidationResult.Success;
            }
            catch { }

            return new ValidationResult(GetErrorMessage());
        }
    }
}
