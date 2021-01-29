using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CookTool.Shared.Authentication
{
    //this code was taken from www.codeproject.com/Questions/614644/MVC-Data-Annotation-For-Not-Equal
    public class NotEqual : ValidationAttribute
    {
        private string OtherProperty { get; set; }

        public NotEqual(string otherProperty)
        {
            OtherProperty = otherProperty;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            // get other property value
            var otherPropertyInfo = validationContext.ObjectType.GetProperty(OtherProperty);
            var otherValue = otherPropertyInfo.GetValue(validationContext.ObjectInstance);

            // verify values
            if (value.ToString().Equals(otherValue.ToString()))
                return new ValidationResult(string.Format("{0} should not be equal to {1}.", validationContext.MemberName, OtherProperty));
            else
                return ValidationResult.Success;
        }
    }
}
