using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace CelSyte.Models
{
    public class Image
    {
        [Key]
        public int Id { get; set; }

        public string FileName { get; set; }

        public string Name { get; set; }

        public string Tags { get; set; }

        public string UserId { get; set; }

        [ValidateNever]
        public User User { get; set; } = null!;
    }
}
