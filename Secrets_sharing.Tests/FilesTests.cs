using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Moq;
using Secrets_sharing.Models;
using Secrets_sharing.Pages;
using Xunit;

namespace Secrets_sharing.Tests
{
    public class FilesTests
    {
        [Fact]
        public void OnPost_ReturnsARedirectToPageResult()
        {

            // Arrange
            var store = new Mock<IUserStore<User>>();
            var userManager = new UserManager<User>(store.Object, null, null, null, null, null, null, null, null);
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            var mockApplicationContext = new Mock<ApplicationContext>(optionsBuilder.Options);
            var pageModel = new FilesModel(userManager, mockApplicationContext.Object);
            var id = 1;

            // Act
            var result = pageModel.OnPost(id);

            // Assert
            Assert.IsType<RedirectToPageResult>(result);
        }

        [Fact]
        public void OnGet_PopulatesThePageModel_WithAListOfFiles()
        {
            // Arrange
            var user = new User { Id = "qwe", UserName = "em@mail.ru", Email = "em@mail.ru", Files = ApplicationContext.AddTestFiles() };
            var store = new Mock<IUserStore<User>>();
            var userManager = new Mock<UserManager<User>>(store.Object, null, null, null, null, null, null, null, null);
            var optionsBuilder = new DbContextOptionsBuilder<ApplicationContext>();
            var mockApplicationContext = new Mock<ApplicationContext>(optionsBuilder.Options);
            var expectedFiles = user.Files;
            var pageModel = new FilesModel(userManager.Object, mockApplicationContext.Object);
            userManager.Setup(x => x.GetUserAsync(pageModel.User).Result).Returns(user);

            // Act
            pageModel.OnGet();

            // Assert
            var actualFiles = Assert.IsAssignableFrom<List<File>>(pageModel.Files);
            Assert.Equal(
                expectedFiles.OrderBy(m => m.Id).Select(f => f.Name),
                actualFiles.OrderBy(m => m.Id).Select(f => f.Name));
        }
    }
}
