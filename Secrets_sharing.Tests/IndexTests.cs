using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Microsoft.EntityFrameworkCore;
using Moq;
using Secrets_sharing.Pages;
using Xunit;

namespace Secrets_sharing.Tests
{
    public class IndexTests
    {
        [Fact]
        public void OnGet_ReturnsAPageResult()
        {
            // Arrange
            var pageModel = new IndexModel();

            // Act
            var result = pageModel.OnGet();

            // Assert
            Assert.IsType<PageResult>(result);
        }
        [Fact]
        public void OnGet_ReturnsNotNull()
        {
            // Arrange
            IndexModel model = new IndexModel();

            // Act
            var result = model.OnGet();

            // Assert
            Assert.NotNull(result);
        }
    }
}
