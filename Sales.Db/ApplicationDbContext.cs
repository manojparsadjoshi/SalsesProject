using Microsoft.EntityFrameworkCore;
using Sales.Entity;
using SalsesProject.Models;

namespace Sales.Db
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
        public DbSet<LogInModel> LogInModel { get; set; }
        public DbSet<CategoryModel> Categories { get; set; }
        public DbSet<VenderModel> venders { get; set; }

    }
}
