using Microsoft.EntityFrameworkCore;
using Warehouse.DB.Entities;

namespace Warehouse.DB.Contexts
{
    public class WarehouseDbContext : DbContext
    {
        public WarehouseDbContext(DbContextOptions<WarehouseDbContext> options)
            : base(options)
        {
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
             .HasData(
              new User
              {
                  UserID = 1,
                  FullName = "Test Admin",
                  UserName = "Admin",
                  Email = "admin@gmail.com",
                  ISActive = true,
                  Password = "pass1234",
              }
              );

            modelBuilder.Entity<Category>()
              .HasData(
               new Category
               {
                   CategoryID = 1,
                   CategoryName = "Computer Keyboard",
               },
               new Category
               {
                   CategoryID = 2,
                   CategoryName = "Computer Mouse",
               },
               new Category
               {
                   CategoryID = 3,
                   CategoryName = "Computer Motherboard",
               },
               new Category
               {
                   CategoryID = 4,
                   CategoryName = "Computer Ram",
               }

               );

            modelBuilder.Entity<Product>()
                .HasData(
                 new Product
                 {
                     ProductID = 1,
                     Name = "MK235 Wireless Keyboard",
                     Description = "Logitech MK235 Wireless Keyboard and Mouse Combo for Windows, 2.4 GHz Wireless with Unifying USB-Receiver, Wireless Mouse, 15 FN Keys, 3-Year",
                     CategoryID = 1,
                     Price = 275,
                     Quantity = 50
                 },
                     new Product
                     {
                         ProductID = 2,
                         Name = "SteelSeries Gaming Keyboard",
                         Description = "SteelSeries Apex Pro TKL HyperMagnetic Gaming Keyboard - World's Fastest Keyboard - Adjustable Actuation - Esports TKL - OLED Screen - PBT Keycaps",
                         CategoryID = 1,
                         Price = 820,
                         Quantity = 20
                     },
                     new Product
                     {
                         ProductID = 3,
                         Name = "Logitech G305 Wireless Mouse",
                         Description = "Logitech G305 Lightspeed Wireless Gaming Mouse, HERO Sensor, 12,000 DPI, Lightweight, 6 Programmable Buttons, 250h Battery Life, On-Board Memory,",
                         CategoryID = 2,
                         Price = 248,
                         Quantity = 25
                     },
                     new Product
                     {
                         ProductID = 4,
                         Name = "MSI PRO B650-S WiFi ProSeries Motherboard",
                         Description = "MSI PRO B650-S WiFi ProSeries Motherboard (Supports AMD Ryzen 7000 Series Processors, AM5, DDR5, PCIe 4.0, M.2 Slots, SATA 6Gb/s, USB 3.2 Gen 2,",
                         CategoryID = 3,
                         Price = 700,
                         Quantity = 30
                     },
                     new Product
                     {
                         ProductID = 5,
                         Name = "ASRock B660M Pro RS Intel",
                         Description = "ASRock B660M Pro RS Intel B660 Series CPU (LGA1700) Compatible B660M MicroATX Motherboard",
                         CategoryID = 3,
                         Price = 248,
                         Quantity = 45
                     },
                     new Product
                     {
                         ProductID = 6,
                         Name = "Intel Core i7-14700K Motherboard",
                         Description = "Intel Core i7-14700K New Gaming Desktop Processor 20 cores (8 P-cores + 12 E-cores) with Integrated Graphics - Unlocked",
                         CategoryID = 3,
                         Price = 700,
                         Quantity = 50
                     },
                     new Product
                     {
                         ProductID = 7,
                         Name = "HPE DDR4 RAM 32GB",
                         Description = "HPE P00924-B21 memory module 32 GB DDR4 2933 MHz",
                         CategoryID = 4,
                         Price = 900,
                         Quantity = 20
                     },
                     new Product
                     {
                         ProductID = 8,
                         Name = "CORSAIR DDR5 RAM 32GB",
                         Description = "CORSAIR VENGEANCE RGB DDR5 RAM 32GB (2x16GB) 6400MHz CL36 Intel XMP iCUE Compatible Computer Memory - Black (CMH32GX5M2B6400C36)",
                         CategoryID = 4,
                         Price = 75
                     });



            base.OnModelCreating(modelBuilder);

        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<User> User { get; set; }
    }
}
