using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

namespace ImplementingLocalization.Validation
{
    public class CustomValidationMetadataProvider : IValidationMetadataProvider
    {
        private Dictionary<Type, string> _errorMessageMap;

        public CustomValidationMetadataProvider()
        {
            _errorMessageMap= new Dictionary<Type, string>
            {
                {typeof(RequiredAttribute),"RequiredError" },
                {typeof(MaxLengthAttribute),"MaxLengthError" }
            };
        }
        public void CreateValidationMetadata(ValidationMetadataProviderContext context)
        {
            foreach (object attribute in context.Attributes)
            {
                ValidationAttribute validationAttribute =
                    attribute as ValidationAttribute;

                if(validationAttribute != null)
                {
                    Type type = attribute.GetType();

                    string key;

                    if(_errorMessageMap.TryGetValue(type,out key))
                    {
                        validationAttribute.ErrorMessage = key;
                    }
                }
            }
        }
    }
}
