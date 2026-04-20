using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace CelSyte.Models
{
    public class Canvas
    {

        [Key]
        public int Id { get; set; }

        public string Name { get; set; }

        public List<CompositionElement> CompositionElements { get; set; } = new();

        public string UserId { get; set; }

        [ValidateNever]
        public User User { get; set; } = null!;

    }
}
