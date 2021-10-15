using Microsoft.EntityFrameworkCore;
using PosthumanWebApi.Models.Entities;

namespace PosthumanWebApi.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new PosthumanContext(
                serviceProvider.GetRequiredService<DbContextOptions<PosthumanContext>>()))
            {
                // Database has been seed - entries present
                if (context.Projects.Any())
                {
                    return;
                }
                else
                {
                    for (int i = 0; i < 3; i++)
                    {
                        var project = new Project(
                            i,
                            $"Tytul {i}",
                            $"Opis {i}",
                            false,
                            DateTime.Now);

                        context.Projects.Add(project);
                    }
                }
            }
        }
    }
}
