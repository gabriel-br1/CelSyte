using System.ComponentModel.DataAnnotations;

namespace CelSyte.Models
{
    public class CompositionElement
    {

        [Key]
        public int Id { get; set; }

        public int ImageId { get; set; }

        public int CanvasId { get; set; }

        public double XCoord { get; set; }

        public double YCoord { get; set; }

        public int Opacity { get; set; }

        public double Scale { get; set; }

        public double RotationAngle { get; set; }

        public int OrderPlacement { get; set; }

        public Image Image { get; set; }

        public Canvas Canvas { get; set; }

    }
}
