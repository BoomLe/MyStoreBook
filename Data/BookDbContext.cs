using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Book.Models;
using Microsoft.EntityFrameworkCore;
using Book.Areas.Admin.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Book.Areas.Contact.Models;

namespace Book.Data
{
    public class BookDbContext : IdentityDbContext<IdentityUser>
    {
        public DbSet<Categorys> Categorys{set;get;}

        public DbSet<Products> Products{set;get;}

        public DbSet<ContactModel> Contact{set;get;}

        public DbSet<ApplicationUser> ApplicationUser{set;get;}
        public BookDbContext(DbContextOptions options) : base(options)
        {
        }



        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            
            modelBuilder.Entity<Categorys>().HasData(
                new Categorys(){Id = 1, Name = "Anime", DisplayOrder =1},
                new Categorys(){Id = 2, Name = "Horror", DisplayOrder =2},
                new Categorys(){Id = 3, Name = "Action", DisplayOrder =3}
            );

            modelBuilder.Entity<Products>().HasData(
                new Products { 
                    Id = 1, 
                    Title = "Anime and the Art of Adaptation", 
                    Author="Dani Cavallaro", 
                    Description= @"Exploring a selection of anime adaptations of famous works of both Eastern and Western provenance, 
                    this book is concerned with appreciating their significance and appeal as independent texts. The author evaluates 
                    three aspects of anime adaptation--how anime adaptations develop their original sources in stylistic, aesthetic, 
                    and psychological terms; how specific features of the anime medium impact alchemically on the original sources to 
                    bring into being imaginative works of an autonomous nature; and which qualities render an adaptation in anime form 
                    a distinctly unique artistic creation.",
                    ISBN="SWD9999001",
                    ListPrice=99,
                    Price=90,
                    Price50=85,
                    Price100=80,
                    CategoryId = 1,
                    ImageUrl = ""
                },
                new Products
                {
                    Id = 2,
                    Title = "Attack on Titan",
                    Author = "Hajime Isayama",
                    Description = @"The definitive guide to the Attack on Titan anime, 
                    just in time for season two! The creators behind the hit anime tell it all! Colossal-sized and in full color, 
                    this official guidebook includes: • An in-depth interview with Hajime Isayama and director Tetsuro Araki, 
                    where a surprise about Eren and Armin is revealed! • A detailed look at the world of Attack on Titan from side 
                    characters, to weaponry, and even all the Titans • Exclusive interviews with the unforgettable voice actors, 
                    and the musicians behind the iconic theme songs • Four special-edition postcards you cant get anywhere else!",
                    ISBN = "CAW777777701",
                    ListPrice = 40,
                    Price = 30,
                    Price50 = 25,
                    Price100 = 20,
                    CategoryId = 1,
                    ImageUrl = ""
                },
                new Products
                {
                    Id = 3,
                    Title = "Dracula",
                    Author = "Bram Stoker",
                    Description = @"Bram Stoker has written his name deeply into history. His most illustrious creation, 
                    Count Dracula, has gone beyond the universe of literature and has become an integral part of our culture. 
                    There is no one who does not know the vampire and, in some way, has not been terrified by him.",
                    ISBN = "RITO5555501",
                    ListPrice = 55,
                    Price = 50,
                    Price50 = 40,
                    Price100 = 35,
                    CategoryId = 2,
                    ImageUrl = ""
                },
                new Products
                {
                    Id = 4,
                    Title = "Horror In The Wax Museum",
                    Author = "Drac Von Stoller",
                    Description = @"It was 1863 in a town called Cactus Jack and Dr. Henry Corbin who was the towns doctor 
                    had grown tired of practicing medicine and was really interested in opening his own wax museum. Money was no 
                    object so he called on some of the best carpenters in town to begin the process. The wax museum was completed 
                    within a year and the only thing missing was the wax figures. Henry sat at his desk in his wax museum almost 
                    in tears. He said, to himself, What am I going to do? I have no wax figures to put in my wax museum. I'll be out of business even before I open the doors.",
                    ISBN = "WS3333333301",
                    ListPrice = 70,
                    Price = 65,
                    Price50 = 60,
                    Price100 = 55,
                    CategoryId = 2,
                    ImageUrl = ""
                },
                new Products
                {
                    Id = 5,
                    Title = "NDA Action Sports Digital Journal: Book One",
                    Author = "NDigitalArts",
                    Description = " A collection of photos and articles from projects that NDA Action Sports has produced. ",
                    ISBN = "NDA000000001",
                    ListPrice = 30,
                    Price = 27,
                    Price50 = 25,
                    Price100 = 20,
                    CategoryId = 3,
                    ImageUrl = ""
                },
                new Products
                {
                    Id = 6,
                    Title = "Claiming His Treasure",
                    Author = "Ember Casey",
                    Description =  @"
                    Action-adventure romance from USA Today bestseller Ember Casey! Thrilling adventure meets a gripping second-chance love story in this exciting romantic suspense.
                    Holy amazeballs; Ember Casey rocked Jackson's story. This is not your average romance; this is a romance full of action, anticipation and thrill. Alison S. Parkins Book Reviews",
                    ISBN = "FOT000000001",
                    ListPrice = 25,
                    Price = 23,
                    Price50 = 22,
                    Price100 = 20,
                    CategoryId = 3,
                    ImageUrl = ""
                }
            );

              foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                var tables = entity.GetTableName();
                if(tables.StartsWith("AspNet"))
                {
                   entity.SetTableName(tables.Substring(6));
                }
                
            }
        }
    }

}