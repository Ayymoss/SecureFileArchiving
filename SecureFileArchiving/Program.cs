using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SecureFileArchiving.Data;
using FileCryption;

namespace SecureFileArchiving;

public static class SecureFileArchiving
{
    public static void Main(string[] args)
    {
        //StartWebFront(args); 

        Run();
    }

    private static void Run()
    {
        {
            // Store the ivBase64 and Encrypted Text into the Database
            // Key is the user's password. Do not store.
            const string text = "this is something else";
            const string userKey = "test2"; // For security, password/key should be longer than 20 characters.


            Console.WriteLine("-- Encrypt Decrypt symmetric --");
            
            var (key, ivBase64) = KeyConversion.SymmetricEncryptDecrypt(userKey);
            var encryptedText = AesCryption.Encrypt(text, ivBase64, key);

            Console.WriteLine("-- Salt --");
            Console.WriteLine(key);
            Console.WriteLine("-- IVBase64 --");
            Console.WriteLine(ivBase64);
            Console.WriteLine("-- Encrypted Text --");
            Console.WriteLine(encryptedText);

            var decryptedText = AesCryption.Decrypt(encryptedText, ivBase64, key);

            Console.WriteLine("-- Decrypted Text --");
            Console.WriteLine(decryptedText);
        }
    }

    private static void StartWebFront(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseSqlite(connectionString));
        builder.Services.AddDatabaseDeveloperPageExceptionFilter();

        builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
            .AddEntityFrameworkStores<ApplicationDbContext>();
        builder.Services.AddRazorPages();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseMigrationsEndPoint();
        }
        else
        {
            app.UseExceptionHandler("/Error");
            // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseAuthentication();
        app.UseAuthorization();

        app.MapRazorPages();

        app.Run();
    }
}
