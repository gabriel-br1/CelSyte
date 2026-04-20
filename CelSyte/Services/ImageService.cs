using Microsoft.EntityFrameworkCore;
using Image = CelSyte.Models.Image;
namespace CelSyte.Services
{
    public class ImageService
    {

        public ImageService()
        {
        }

        public List<Image> findUserImages(string userId, IDbContextFactory<CelSyte.Data.CelSyteContext> DbFactory)
        {
            List<Image> images = new List<Image>();
            List<Image> returnImages = new List<Image>();
            using var context = DbFactory.CreateDbContext();
            images = context.Set<Image>().ToList();

            System.Diagnostics.Debug.WriteLine(images.Count());

            foreach (Image image in images) 
            { 
                if(image.UserId == userId)
                {
                    returnImages.Add(image);
                }
            }
            return returnImages;
        }

        public Image findImageById(int imageId, IDbContextFactory<CelSyte.Data.CelSyteContext> DbFactory)
        {
            using var context = DbFactory.CreateDbContext();
            List<Image> images = context.Set<Image>().ToList();

            foreach(Image image in images)
            {
                if(image.Id == imageId)
                {
                    return image;
                }
            }
            return null;
        }

    }
}
