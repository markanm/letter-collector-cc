using System;
using NUnit.Framework;
using Moq;
using Microsoft.AspNetCore.Mvc;
using LetterCollector.Api.Services.Interfaces;
using LetterCollector.Api.Controllers;
using LetterCollector.Api.Models;

namespace LetterCollector.Tests
{
    public class PathAnalysisControllerTests
    {
        private Mock<IPathAnalyzerService>? _mockPathAnalyzerService;
        private PathAnalysisController? _controller;

        [SetUp]
        public void Setup()
        {
            _mockPathAnalyzerService = new Mock<IPathAnalyzerService>();

            _mockPathAnalyzerService?.Setup(service => service.AnalyzePath(It.IsAny<string[]>()))
                .Returns(new PathAnalysisResultDto { Letters = "ACB", PathAsCharacters = "@---A---+|C|+---+|+-B-x" });

            if (_mockPathAnalyzerService != null)
            {
                _controller = new PathAnalysisController(_mockPathAnalyzerService.Object);
            }
        }

        [Test]
        public void AnalyzePath_ValidInput_ReturnsOkObject()
        {
            // Arrange
            var sampleMap = new string[] { "@---A---+", "        |", "x-B-+   C", "x-B-+   C", "    +---+" };

            // Act
            var result = _controller?.AnalyzePath(sampleMap);

            // Assert
            Assert.IsInstanceOf<OkObjectResult>(result);
            var okResult = result as OkObjectResult;
            var analysisResult = okResult?.Value as PathAnalysisResultDto;
            Assert.AreEqual("ACB", analysisResult?.Letters);
            Assert.AreEqual("@---A---+|C|+---+|+-B-x", analysisResult?.PathAsCharacters);
        }

        [Test]
        public void AnalyzePath_InvalidInput_ReturnsBadRequest()
        {
            // Arrange
            _mockPathAnalyzerService?.Setup(service => service.AnalyzePath(It.IsAny<string[]>()))
                .Throws(new ArgumentException("Invalid map: Missing start character"));

            // Act
            var result = _controller?.AnalyzePath(new string[] { " --A-x" });

            // Assert
            Assert.IsInstanceOf<BadRequestObjectResult>(result);
            var badRequestResult = result as BadRequestObjectResult;
            Assert.AreEqual("Invalid map: Missing start character", badRequestResult?.Value);
        }
    }
}