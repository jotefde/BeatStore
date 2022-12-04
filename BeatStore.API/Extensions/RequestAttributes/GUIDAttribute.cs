using BeatStore.API.Entities;
using Minio.DataModel;
using System.ComponentModel.DataAnnotations;

namespace BeatStore.API.Extensions.RequestAttributes
{
    public class GUIDAttribute : ValidationAttribute
    {
        private readonly bool _isList;
        public GUIDAttribute(bool isList = false)
        {
            _isList = isList;
        }

        protected override ValidationResult IsValid(object? value, ValidationContext validationContext)
        {
            if(_isList)
            {
                var list = value as List<string>;
                if(list != null)
                {
                    foreach(var item in list)
                    {
                        var isGUIDValid = Guid.TryParse(item, out _);
                        if (!isGUIDValid)
                            return new ValidationResult($"'{item}' is not a valid Id format");
                    }
                    return ValidationResult.Success;
                }
                return new ValidationResult($"The Id list is invalid");
            }
            else
            {
                var id = value as string;
                if(id != null)
                {
                    var isGUIDValid = Guid.TryParse(id, out _);
                    if (isGUIDValid)
                        return ValidationResult.Success;
                }
                return new ValidationResult($"'{id}' is not a valid Id format");
            }
        }
    }
}
