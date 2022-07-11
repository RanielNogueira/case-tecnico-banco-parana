using Microsoft.AspNetCore.Mvc;
using PBTech.APIv1.Controllers;
using PBTech.APIv1.ViewModel;
using PBTech.Domain.Interfaces;
using PBTech.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace PBTech.APIv1Test
{
    public class NewsLetterControllerTest
    {
        NewsLetterController _controller;
        INewsLetterRepository _service;

        public NewsLetterControllerTest()
        {
            _service = new NewsLetterServiceFake();
            _controller = new NewsLetterController(_service);
        }

        [Fact]
        public async Task Post_CreatedResult()
        {
            var newReceive = new NewReceive("Full Name Teste 6", "pessoa6@teste.com.br");
            ReceiveNews receiveNews = new ReceiveNews { FullName = newReceive.FullName, Email = newReceive.Email };
            var result = _controller.Post(receiveNews).Result as CreatedAtActionResult;
            Assert.IsType<ReceiveNews>(result.Value);
        }

        [Fact]
        public async Task Put_CreatedResult()
        {
            var newReceive = new ReceiveNews(3, "Teste de Update", "pessoa3@teste.com.br");
            var result = _controller.Put(3,newReceive).Result as NoContentResult;
            Assert.Equal(204, result.StatusCode);
        }

        [Fact]
        public void Get_OkResult()
        {
            var okResult = _controller.Get().Result as OkObjectResult;
            var items = Assert.IsType<List<ReceiveNews>>(okResult.Value);
            Assert.Equal(5, items.Count);
        }

        [Fact]
        public async Task GetByEmail_NotNull_OkResult()
        {
            var result = _controller.GetByEmail("pessoa2@teste.com.br").Result as OkObjectResult;
            Assert.IsType<ReceiveNews>(result.Value);
        }

        [Fact]
        public void GetByEmail_Null_OkResult()
        {
            var okResult = _controller.GetByEmail("pessoa12345@teste.com.br").Result as ReceiveNews;
            Assert.Null(okResult);
        }

        [Fact]
        public async Task DeleteByEmail_FoundResult()
        {
            var foundResult = _controller.Delete("pessoa1@teste.com.br").Result as OkResult;
            Assert.Equal(200, foundResult.StatusCode);
        }

        [Fact]
        public async Task DeletetByEmail_NotFoundResult()
        {
            ErrorMessage errorMessage = new ErrorMessage("Register not found!");
            var notFoundResult = await _controller.Delete("pessoa3333@teste.com.br") as NotFoundObjectResult;
            Assert.IsType<NotFoundObjectResult>(notFoundResult);
            Assert.Equal(errorMessage.Message,notFoundResult.Value);
        }
    }
}
