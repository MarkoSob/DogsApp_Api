using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using static DogsApp_Core.AppConstants;

namespace DogsApp_DAL.Entities
{
    [Index(nameof(Name), IsUnique = true)]
    public class Dog : Entity
    {
        [Required]
        [MinLength(3 , ErrorMessage = ErrorMessages.FieldLengthErrorMessage)]
        public string? Name { get; set; }

        [Required]
        [MinLength(1, ErrorMessage = ErrorMessages.FieldLengthErrorMessage)]
        public string? Color { get; set; }

        [Required]
        [Range(typeof(double), "1", "999", ErrorMessage = ErrorMessages.TailLengthErrorMessage)]
        public double TailLength { get; set; }

        [Required]
        [Range(typeof(double), "1", "999", ErrorMessage = ErrorMessages.WeighthErrorMessage)]
        public double Weight { get; set; }
    }
}
