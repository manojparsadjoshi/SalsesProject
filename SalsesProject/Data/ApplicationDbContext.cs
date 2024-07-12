using Microsoft.EntityFrameworkCore;
using SalsesProject.Models;

namespace SalsesProject.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }
        public DbSet<CustomerModel> Customers { get; set; }
        public DbSet<ItemsModel> Items { get; set; }
        public DbSet<SalesMasterModel> masterModels { get; set; }
        public DbSet<SalesDetailsModel> DetailsModels { get; set; }
        public DbSet<UserModel> Users { get; set; }
        public DbSet<SalsesProject.Models.LogInModel> LogInModel { get; set; } = default!;

    }
}
