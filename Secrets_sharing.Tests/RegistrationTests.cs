using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Moq;
using Microsoft.AspNetCore.Identity;
using Secrets_sharing.Models;
using Secrets_sharing.Pages;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Secrets_sharing.Tests.Utility;

namespace Secrets_sharing.Tests
{
    public class RegistrationTests
    {
        [Fact]
        public async Task OnPostAsync_TheEmailAlreadyExist_ReturnsAPageResult()
        {
            // Arrange
            var mockStore = new Mock<IUserStore<User>>();
            var userManager = new UserManager<User>(mockStore.Object, null, null, null, null, null, null, null, null);
            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            var mockClaimsFactory = new Mock<IUserClaimsPrincipalFactory<User>>();
            var signInManager = new SignInManager<User>(userManager,
                                                        mockHttpContextAccessor.Object,
                                                        mockClaimsFactory.Object,
                                                        null,
                                                        null,
                                                        null,
                                                        null);
            var testUser = new User { UserName = "Email@email.com", Email = "Email@email.com" };
            var db = new ApplicationContext(TestDbConnection.TestDbContextOptions());
            db.Add(testUser);
            db.SaveChanges();
            var pageModel = new RegistrationModel(db, signInManager, userManager);
            pageModel.Input = new RegistrationModel.InputModel
            {
                Email = "Email@email.com",
                Password = "123",
                PasswordConfirm = "123"
            };

            // Act
            var result = await pageModel.OnPostAsync();

            // Assert
            Assert.Equal(1, pageModel.ModelState.ErrorCount);
            Assert.IsType<PageResult>(result);

        }
    }
}
