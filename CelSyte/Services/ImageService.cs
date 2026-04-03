using CelSyte.Data;
using CelSyte.Models;
using Microsoft.EntityFrameworkCore;
namespace CelSyte.Services
{
    public class ImageService
    {

        CelSyteContext _dbContext = null;

        public ImageService(CelSyteContext dbContext)
        {
            _dbContext = dbContext;
        }

        public List<Image> findUserImages(string userId)
        {
            List<Image> images = new List<Image>();
            DbSet<Image> dbImages = _dbContext.Set<Image>();
            foreach (Image image in dbImages) 
            { 
                if(image.UserId == userId)
                {
                    images.Add(image);
                }
            }
            return images;
        }

    }
}
