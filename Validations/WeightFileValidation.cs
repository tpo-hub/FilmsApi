using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsApi.Validations
{
    public class WeightFileValidation: ValidationAttribute
    {
        private readonly int maxWeightMb;

        public WeightFileValidation(int MaxWeightMb)
        {
            maxWeightMb = MaxWeightMb;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if(value == null)
            {
                return ValidationResult.Success;

            }

            IFormFile formFile = value as IFormFile;

            if(formFile == null)
            {
                return ValidationResult.Success;
            }

            if(formFile.Length > maxWeightMb * 1024 * 1024)
            {
                return new ValidationResult($"The weight of the file should not be more of {maxWeightMb} mb");
            }

            return ValidationResult.Success;
        }

    }
}
