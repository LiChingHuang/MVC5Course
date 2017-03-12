using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace MVC5Course.Models
{
    public class LoginVM:IValidatableObject
    {
        [Required]
        [MinLength(3)]
        public String Username { get; set; }
        [Required]
        [MinLength(6)]
        public String Password { get; set; }

        public bool Logincheck() { return this.Username == "HLC" && this.Password == "123456"; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (!this.Logincheck())
            {
                yield return new ValidationResult("登入失敗!", new String[] {"Username"});
                yield break;
            }
            yield return ValidationResult.Success;
        }
    }
}