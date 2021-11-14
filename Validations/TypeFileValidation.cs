using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsApi.Validations
{
    public class TypeFileValidation : ValidationAttribute
    {
        private readonly string[] validTypes;

        public TypeFileValidation(string[] ValidTypes)
        {
            validTypes = ValidTypes;
        }

        public TypeFileValidation(TypesFilesGroup typesFilesGroup)
        {
            if (typesFilesGroup == TypesFilesGroup.Image)
            {
                validTypes = new string[] { "image/jpeg", "image/png", "image/gif" };
            }
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;

            }

            IFormFile formFile = value as IFormFile;

            if (formFile == null)
            {
                return ValidationResult.Success;
            }

           if(!validTypes.Contains(formFile.ContentType))
            {
                return new ValidationResult($"The type of the file must be: {string.Join(", ", validTypes)}");
            }

            return ValidationResult.Success;
        }


    }

}

