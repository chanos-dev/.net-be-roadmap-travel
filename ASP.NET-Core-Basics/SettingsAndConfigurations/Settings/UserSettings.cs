using System.ComponentModel.DataAnnotations;

namespace SettingsAndConfigurations.Settings
{
    public class UserSettings : IValidatableObject
    {
        public string Id { get; set; }
        public string Password { get; set; }

        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            if (string.IsNullOrEmpty(Id))
                yield return new ValidationResult("id is empty.");

            if (string.IsNullOrEmpty(Password))
                yield return new ValidationResult("password is empty.");
        }
    }
}
