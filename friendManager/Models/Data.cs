using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace friendManager.Models
{
    public class Data
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new friendManagerContext(
                serviceProvider.GetRequiredService<DbContextOptions<friendManagerContext>>()))
            {
                if (context.Friend.Any())
                {
                    return;   // DB has been seeded
                }

                context.Friend.AddRange(
                     new Friend
                     {
                         Name = "Maciej Maciejowski",
                         FirstMeetDate = DateTime.Parse("2012-12-12 14:53:00"),
                         BiggestSecret = "Dislikes Charmander",
                         Likeability = 12.12M
                     },

                     new Friend
                     {
                         Name = "Marcin Marcinowski",
                         FirstMeetDate = DateTime.Parse("2001-06-11 12:34:00"),
                         BiggestSecret = "Learned Javascript from Youtube tutorials",
                         Likeability = -100.00M
                     },

                     new Friend
                     {
                         Name = "Jan Janowski",
                         FirstMeetDate = DateTime.Parse("1999-01-11 21:37:00"),
                         BiggestSecret = "His biggest fear is a missing semicolon",
                         Likeability = 35.00M
                     },

                   new Friend
                   {
                       Name = "Marcin Domarski",
                       FirstMeetDate = DateTime.Parse("2009-09-29 17:14:00"),
                       BiggestSecret = "Is imaginary self-friend of real himself",
                       Likeability = 1000000.00M
                   }
                );
                context.SaveChanges();
            }
        }
    }
}
