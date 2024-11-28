using CafeEmployeeManagement.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace CafeEmployeeManagement.Domain.Entities
{
    public class Employee : BaseEntity
    {
        public Employee()
        {
            Id = GenerateUniqueId();
        }

        [Key]
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^[89]\d{7}$")]
        public int PhoneNumber { get; set; }

        [Required]
        public Gender Gender { get; set; }

        // Foreign key to the Cafe entity
        public Guid CafeId { get; set; }
        // Navigation property
        public Cafe Cafe { get; set; }


        private string GenerateUniqueId()
        {
            const string prefix = "UI";
            const string alphaNumericChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890"; 
            const int length = 7;

            Random random = new();

            string randomString = new string(Enumerable.Range(0, length)
                .Select(_ => alphaNumericChars[random.Next(alphaNumericChars.Length)]).ToArray());

            return string.Concat(prefix, randomString);
        }
    }
}
