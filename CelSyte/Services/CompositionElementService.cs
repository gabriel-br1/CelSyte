using CelSyte.Models;
using Microsoft.EntityFrameworkCore;

namespace CelSyte.Services
{
    public class CompositionElementService
    {

        public CompositionElementService()
        {
        }

        public List<CompositionElement> findCanvasCompElements(int canvasId, IDbContextFactory<CelSyte.Data.CelSyteContext> DbFactory)
        {
            List<CompositionElement> compositionElements = new List<CompositionElement>();
            List<CompositionElement> returnElements = new List<CompositionElement>();
            using var context = DbFactory.CreateDbContext();
            compositionElements = context.Set<CompositionElement>().ToList();

            foreach (CompositionElement element in compositionElements)
            {
                if (element.CanvasId == canvasId)
                {
                    returnElements.Add(element);
                }
            }
            return returnElements;
        }

    }
}
