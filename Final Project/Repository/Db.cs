using Domain.Models;
using Domain.Models.Common;
using Final_Project.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Repository
{
    public class Db : IdentityDbContext<AppUser>
    {
        public Db(DbContextOptions<Repository.Db> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductImages> ProductImages { get; set; }
        public DbSet<Abt> Abts { get; set; }
        public DbSet<Brands> Brands { get; set; }
        public DbSet<BannerImage> BannerImage { get; set; }
        public DbSet<Basket> Basket { get; set; }
        public DbSet<Comment> Comments { get; set; }
        public DbSet<ContactDetail> ContactDetails { get; set; }

        public DbSet<HeroArea> HeroArea { get; set; }
        public DbSet<HeroUnderSection> HeroUnderSection { get; set; }
        public DbSet<News> News { get; set; }
        public DbSet<Employee> Employees { get; set; }
      
        public DbSet<Subscribe> Subscribe { get; set; }
       
        public DbSet<SendNotfication> SendNotfications { get; set; }
        

        public DbSet<Setting> Setting { get; set; }
        public DbSet<WhyFruitka> WhyFruitka { get; set; }
        public DbSet<Wishlist> Wishlist { get; set; }
        public DbSet<Contact> Contact { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(BaseEntity).Assembly);

            base.OnModelCreating(modelBuilder);
        }
    }
}
