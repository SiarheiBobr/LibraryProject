using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using Library.Domain.Abstract;
using Library.Domain.Entities;
using Library.WebUI.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace SportsStore.UnitTests
{
    [TestClass]
    public class AdminTests
    {

        [TestMethod]
        public void Index_Contains_All_Books()
        {
            // Arrange - create the mock repository
            Mock<IBookRepository> mock = new Mock<IBookRepository>();
            mock.Setup(m => m.Books).Returns(new Book[] {
                new Book {BookID = 1, Title = "P1"},
                new Book {BookID = 2, Title = "P2"},
                new Book {BookID = 3, Title = "P3"},
            }.AsQueryable());

            // Arrange - create a controller 
            AdminController target = new AdminController(mock.Object);


            // Action
            Book[] result = new Book[1];// ((IEnumerable<Book>)target.Index().ViewData.Model).ToArray();

            // Assert
            Assert.AreEqual(result.Length, 3);
            Assert.AreEqual("P1", result[0].Title);
            Assert.AreEqual("P2", result[1].Title);
            Assert.AreEqual("P3", result[2].Title);
        }
    }
}