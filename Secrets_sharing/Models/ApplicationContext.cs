using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Secrets_sharing.Models
{
    public class ApplicationContext : IdentityDbContext<User>
    {
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options)
        {
            // Database.EnsureCreated(); // Create database if it doesn't exist
        }
        public DbSet<File> Files { get; set; }
        public DbSet<Text> Texts { get; set; }

        public File GetFileByUrl(string Url)
        {
            var file = Files.First(f => f.Url == Url);
            return file;
        }
        public Text GetTextByUrl(string Url)
        {
            var text = Texts.First(f => f.Url == Url);
            return text;
        }

        public void Delete<T>(int id) where T: class
        {
            var obj = Find<T>(id);

            if (obj != null)
            {
                Remove(obj);
                SaveChanges();
            }
        }
        public static List<File> AddTestFiles()
        {
            return new List<File>
            {
                new File { Id = 1, Name = "Resolution", Url = "123" },
                new File { Id = 2, Name = "Solution", Url = "234" },
                new File { Id = 3, Name = "Salvation", Url = "345" }
            };
        }
        public static List<Text> AddTestTexts()
        {
            return new List<Text>
            {
                new Text { Id = 1, Name = "War" },
                new Text { Id = 2, Name = "Never" },
                new Text { Id = 3, Name = "Changes" }
            };
        }
    }
}
