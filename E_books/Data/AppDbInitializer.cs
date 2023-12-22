using E_books.Data.Static;
using E_books.Models;
using Microsoft.AspNetCore.Identity;

namespace E_books.Data
{
    public class AppDbInitializer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        { 
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                context.Database.EnsureCreated();

                //Categories
                if (!context.Categories.Any())
                {
                    context.Categories.AddRange(new List<Category>()
                    {
                        new Category()
                        {
                            Title = "Horror",
                            ImageUrl = "http://dotnethow.net/images/cinemas/cinema-1.jpeg",
                            Description = "This is the description of the Horror category"
                        },
                        new Category()
                        {
                            Title = "Fantasy",
                            ImageUrl = "http://dotnethow.net/images/cinemas/cinema-2.jpeg",
                            Description = "This is the description of the Fantasy category"
                        },
                        new Category()
                        {
                            Title = "Historical",
                            ImageUrl = "http://dotnethow.net/images/cinemas/cinema-3.jpeg",
                            Description = "This is the description of the Historical category"
                        }
                    });
                    context.SaveChanges();
                }
                //Suppliers
                if (!context.Suppliers.Any())
                {
                    context.Suppliers.AddRange(new List<Supplier>()
                    {
                        new Supplier()
                        {
                            Name = "Amazon",
                            ImageUrl = "data:image/png;base64,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",
                            Description = "This is the description of Amazon"
                        },
                        new Supplier()
                        {
                            Name = "Wizi Shop",
                            ImageUrl = "data:image/png;base64,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",
                            Description = "This is the description of the Wizi Shop"
                        },
                        new Supplier()
                        {
                            Name = "the book edition",
                            ImageUrl = "data:image/png;base64,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",
                            Description = "This is the description of the  book edition"
                        }
                    });
                    context.SaveChanges();
                }
                //Products
                if (!context.Products.Any())
                {
                    context.Products.AddRange(new List<Product>()
                    {
                        new Product()
                        {
                            Title = "Mothtown",
                            Description = "David is growing up in a world where something is very badly wrong but everyone is protecting David from knowing what it is. ",
                            Price = 39.50,
                            ImageUrl = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1684797278i/80328993.jpg",
                            createdAt = DateTime.Now.AddDays(-10),
                            CategoryId = 7,
                            SupplierId = 7
                        },
                        new Product()
                        {
                            Title = "One Dark Window",
                            Description = "Elspeth needs a monster. The monster might be her.Elspeth Spindle needs more than luck to stay safe in the eerie...",
                            Price = 49.50,
                            ImageUrl = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1659411218i/58340706.jpg",
                            createdAt = DateTime.Now.AddDays(-5),
                            CategoryId = 7,
                            SupplierId = 7
                        },
                        new Product()
                        {
                            Title = "Passion danse : l'encyclo",
                            Description = "Une première approche de la danse en six thématiques : le mouvement, le ballet classique, la danse à travers le monde ",
                            Price = 59.50,
                            ImageUrl = "https://www.leslibraires.ca/_next/image?url=https%3A%2F%2Fimages.leslibraires.ca%2Fbooks%2F9791036360541%2Ffront%2F9791036360541_large.jpg&w=1920&q=100",
                            createdAt = DateTime.Now.AddDays(-2),
                            CategoryId = 8,
                            SupplierId = 8
                        },
                        new Product()
                        {
                            Title = "Nos fleurs",
                            Description = "Parce qu’il est petit, et pour ne pas disparaître, le myosotis s’appelle « ne m’oublie pas » en anglais, en allemand et en espagnol.",
                            Price = 519.50,
                            ImageUrl = "https://www.leslibraires.ca/_next/image?url=https%3A%2F%2Fimages.leslibraires.ca%2Fbooks%2F9782925059325%2Ffront%2F9782925059325_large.jpg&w=1920&q=100",
                            createdAt = DateTime.Now.AddDays(-19),
                            CategoryId = 8,
                            SupplierId = 7
                        },
                        new Product()
                        {
                            Title = "Product 5",
                            Description = "This is the Product 5 movie description",
                            Price = 0.50,
                            ImageUrl = "http://dotnethow.net/images/movies/movie-5.jpeg",
                            createdAt = DateTime.Now.AddDays(-1),
                            CategoryId = 9,
                            SupplierId = 9
                        },
                    });
                    context.SaveChanges();
                }
              
               
            }

        }
        public static async Task SeedUsersAndRolesAsync(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {

                //Roles
                var roleManager = serviceScope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

                if (!await roleManager.RoleExistsAsync(UserRoles.Admin))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.Admin));
                if (!await roleManager.RoleExistsAsync(UserRoles.User))
                    await roleManager.CreateAsync(new IdentityRole(UserRoles.User));

                //Users
                var userManager = serviceScope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
                string adminUserEmail = "admin@eproducts.com";

                var adminUser = await userManager.FindByEmailAsync(adminUserEmail);
                if (adminUser == null)
                {
                    var newAdminUser = new ApplicationUser()
                    {
                        FullName = "Admin User",
                        UserName = "admin-user",
                        Email = adminUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAdminUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAdminUser, UserRoles.Admin);
                }


                string appUserEmail = "user@eproducts.com";

                var appUser = await userManager.FindByEmailAsync(appUserEmail);
                if (appUser == null)
                {
                    var newAppUser = new ApplicationUser()
                    {
                        FullName = "Application User",
                        UserName = "app-user",
                        Email = appUserEmail,
                        EmailConfirmed = true
                    };
                    await userManager.CreateAsync(newAppUser, "Coding@1234?");
                    await userManager.AddToRoleAsync(newAppUser, UserRoles.User);
                }
            }
        }
    }
}
