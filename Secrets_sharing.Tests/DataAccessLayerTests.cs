using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Secrets_sharing.Models;
using Secrets_sharing.Tests.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Secrets_sharing.Tests
{
    public class DataAccessLayerTests
    {
        [Fact]
        public void Delete_FileIsDeleted()
        {
            // Arrange
            using (var db = new ApplicationContext(TestDbConnection.TestDbContextOptions()))
            {
                var files = ApplicationContext.AddTestFiles();
                db.AddRange(files);
                db.SaveChanges();
                var id = 1;
                var expectedFiles = db.Files.Where(f => f.Id != id).ToList();

                // Act
                db.Delete<File>(id);

                // Assert
                var actualFiles = db.Files.AsNoTracking().ToList();
                Assert.Equal(
                    expectedFiles.OrderBy(f => f.Id).Select(f => f.Name),
                    actualFiles.OrderBy(f => f.Id).Select(f => f.Name));
            }
        }
        [Fact]
        public void GetFileByUrl_ReturnFile()
        {
            // Arrange
            using (var db = new ApplicationContext(TestDbConnection.TestDbContextOptions()))
            {
                var files = ApplicationContext.AddTestFiles();
                db.AddRange(files);
                db.SaveChanges();
                var url = "234";
                var expectedFile = new File { Id = 2, Name = "Solution", Url = "234" };

                // Act
                var actualFile = db.GetFileByUrl(url);

                // Assert
                Assert.Equal(expectedFile.Name, actualFile.Name);
            }
        }
    }
}
