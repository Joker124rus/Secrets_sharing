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

namespace Secrets_sharing.Tests
{
    public class LoginTests
    {
        [Fact]
        public async Task OnPostAsync_AnIncorrectModel_ReturnsAPageResult()
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
            var pageModel = new LoginModel(signInManager);
            pageModel.ModelState.AddModelError("Password", "Password is required");

            // Act
            var result = await pageModel.OnPostAsync("Test");

            // Assert
            Assert.IsType<PageResult>(result);

        }

        [Fact]
        public async Task OnPostAsync_ACorrectModel_UserSignedIn_ReturnsARedirectToPageIndex()
        {
            // Arrange
            var mockStore = new Mock<IUserStore<User>>();
            var userManager = new UserManager<User>(mockStore.Object, null, null, null, null, null, null, null, null);
            var mockHttpContextAccessor = new Mock<IHttpContextAccessor>();
            var mockClaimsFactory = new Mock<IUserClaimsPrincipalFactory<User>>();
            var mockSignInManager = new Mock<SignInManager<User>>(userManager,
                                                                  mockHttpContextAccessor.Object,
                                                                  mockClaimsFactory.Object,
                                                                  null,
                                                                  null,
                                                                  null,
                                                                  null);
            var pageModel = new LoginModel(mockSignInManager.Object)
            {
                Input = new LoginModel.InputModel { Email = "Email@email.com", Password = "1234" }
            };
            var signInResult = Microsoft.AspNetCore.Identity.SignInResult.Success;
            mockSignInManager.Setup(x => x.PasswordSignInAsync(pageModel.Input.Email, pageModel.Input.Password, false, false))
                                          .Returns(Task.FromResult(signInResult));

            // Act
            var result = await pageModel.OnPostAsync("Test");

            // Assert
            // mockSignInManager.Verify(x => x.PasswordSignInAsync(pageModel.Input.Email, pageModel.Input.Password, false, false));
            Assert.IsType<LocalRedirectResult>(result);
            Assert.Equal("Test", (result as LocalRedirectResult).Url);

        }
    }
}
