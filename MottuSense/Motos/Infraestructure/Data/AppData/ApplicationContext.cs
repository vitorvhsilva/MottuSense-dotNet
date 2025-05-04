using Microsoft.EntityFrameworkCore;

namespace Motos.Infraestructure.Data.AppData
{
    public class ApplicationContext: DbContext
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options){ }

        
    }
}
