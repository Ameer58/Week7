using Microsoft.EntityFrameworkCore;
using SpartaToDo.Models;
using System.Data.Common;

namespace SpartaToDo.Data
{
    public class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using(var db = new SpartaToDoContext(serviceProvider.GetRequiredService<DbContextOptions<SpartaToDoContext>>()))
            {
                if (db.ToDos.Any())
                {
                    return;
                }

                db.ToDos.AddRange(
                    new Todo
                    {
                        Title = "Teach C#",
                        Description = "Teach Eng 128",
                        Complete = true,
                        Date = new DateTime(2022,10,20)
                    },
                    new Todo
                    {
                        Title = "Sleep",
                        Description= "Early",
                        Complete = true
                    }
                    );
                db.SaveChanges();
            }
        }
    }
}
