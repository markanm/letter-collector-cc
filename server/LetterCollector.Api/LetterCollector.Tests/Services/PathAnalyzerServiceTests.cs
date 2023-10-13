using LetterCollector.Api.Services;
using LetterCollector.Api.Services.Interfaces;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LetterCollector.Tests.Services
{
    internal class PathAnalyzerServiceTests
    {
        private IPathAnalyzerService? _service;

        [SetUp]
        public void Setup()
        {
            _service = new PathAnalyzerService();
        }

        [Test]
        public void AnalyzePath_ValidMapBasic_ReturnsCorrectResult()
        {
            // Arrange
            var sampleMap = new[]
            {
                "@---A---+",
                "        |",
                "x-B-+   C",
                "    |   |",
                "    +---+"
            };

            // Act
            var result = _service?.AnalyzePath(sampleMap);

            // Assert
            Assert.AreEqual("ACB", result?.Letters);
            Assert.AreEqual("@---A---+|C|+---+|+-B-x", result?.PathAsCharacters);
        }


        [Test]
        public void AnalyzePath_ValidMapGoThroughIntersections_ReturnsCorrectResult()
        {
            // Arrange
            var sampleMap = new[]
            {
                "  @ ",
                "  | +-C--+ ",
                "  A |    | ",
                "  +---B--+ ",
                "    |      x ",
                "    |      | ",
                "    +---D--+ "
            };

            // Act
            var result = _service?.AnalyzePath(sampleMap);

            // Assert
            Assert.AreEqual("ABCD", result?.Letters);
            Assert.AreEqual("@|A+---B--+|+--C-+|-||+---D--+|x", result?.PathAsCharacters);
        }

        [Test]
        public void AnalyzePath_ValidMapLettersOnTurns_ReturnsCorrectResult()
        {
            // Arrange
            var sampleMap = new[]
            {
                "  @---A---+ ",
                "          | ",
                "  x-B-+   | ",
                "      |   | ",
                "      +---C "
            };

            // Act
            var result = _service?.AnalyzePath(sampleMap);

            // Assert
            Assert.AreEqual("ACB", result?.Letters);
            Assert.AreEqual("@---A---+|||C---+|+-B-x", result?.PathAsCharacters);
        }

        [Test]
        public void AnalyzePath_ValidMapDoNotCollectSameLettersTwice_ReturnsCorrectResult()
        {
            // Arrange
            var sampleMap = new[]
            {
                "     +-O-N-+ ",
                "     |     | ",
                "     |   +-I-+ ",
                " @-G-O-+ | | | ",
                "     | | +-+ E ",
                "     +-+     S ",
                "             | ",
                "             x "
            };

            // Act
            var result = _service?.AnalyzePath(sampleMap);

            // Assert
            Assert.AreEqual("GOONIES", result?.Letters);
            Assert.AreEqual("@-G-O-+|+-+|O||+-O-N-+|I|+-+|+-I-+|ES|x", result?.PathAsCharacters);
        }

        [Test]
        public void AnalyzePath_ValidMapKeepDirections_ReturnsCorrectResult()
        {
            // Arrange
            var sampleMap = new[]
            {
                " +-L-+ ",
                " |  +A-+ ",
                "@B+ ++ H ",
                " ++    x "
            };

            // Act
            var result = _service?.AnalyzePath(sampleMap);

            // Assert
            Assert.AreEqual("BLAH", result?.Letters);
            Assert.AreEqual("@B+++B|+-L-+A+++A-+Hx", result?.PathAsCharacters);
        }

        [Test]
        public void AnalyzePath_ValidMapIgnoreAfterEndOfPath_ReturnsCorrectResult()
        {
            // Arrange
            var sampleMap = new[]
            {
                " @-A--+ ",
                "      | ",
                "      +-B--x-C--D "
            };

            // Act
            var result = _service?.AnalyzePath(sampleMap);

            // Assert
            Assert.AreEqual("AB", result?.Letters);
            Assert.AreEqual("@-A--+|+-B--x", result?.PathAsCharacters);
        }

        [Test]
        public void AnalyzePath_InvalidMapNoStartingChar_ThrowsArgumentException()
        {
            // Arrange
            var sampleMap = new[]
            {
                "     -A---+ ",
                "          | ",
                "  x-B-+   C ",
                "      |   | ",
                "      +---+ "
            };

            // Assert
            var ex = Assert.Throws<ArgumentException>(() => _service?.AnalyzePath(sampleMap), "Invalid Map");
            Assert.AreEqual("Invalid map: Missing start character.", ex?.Message);
        }

        [Test]
        public void AnalyzePath_InvalidMapNoEndChar_ThrowsArgumentException()
        {
            // Arrange
            var sampleMap = new[]
            {
                " @--A---+ ",
                "        | ",
                "  B-+   C ",
                "    |   | ",
                "    +---+ "
            };

            // Assert
            var ex = Assert.Throws<ArgumentException>(() => _service?.AnalyzePath(sampleMap), "Invalid Map");
            Assert.AreEqual("Invalid map: Cannot find next direction.", ex?.Message);
        }

        [Test]
        public void AnalyzePath_InvalidMapMultipleStarts2_ThrowsArgumentException()
        {
            // Arrange
            var sampleMap = new[]
            {
                "  @--A---+ ",
                "         | ",
                "         C ",
                "         x ",
                "     @-B-+ "
            };

            // Assert
            var ex = Assert.Throws<ArgumentException>(() => _service?.AnalyzePath(sampleMap), "Invalid Map");
            Assert.AreEqual("Invalid map: Multiple starts.", ex?.Message);
        }

        [Test]
        public void AnalyzePath_InvalidMapMultipleStarts3_ThrowsArgumentException()
        {
            // Arrange
            var sampleMap = new[]
            {
                "  @--A--x ",
                "  ",
                " x-B-+ ",
                "     | ",
                "     @ "
            };

            // Assert
            var ex = Assert.Throws<ArgumentException>(() => _service?.AnalyzePath(sampleMap), "Invalid Map");
            Assert.AreEqual("Invalid map: Multiple starts.", ex?.Message);
        }

        [Test]
        public void AnalyzePath_InvalidMapForkInPath_ThrowsArgumentException()
        {
            // Arrange
            var sampleMap = new[]
            {
                "       x-B ",
                "         | ",
                "  @--A---+ ",
                "         | ",
                "    x+   C ",
                "     |   | ",
                "     +---+ "
            };

            // Assert
            var ex = Assert.Throws<ArgumentException>(() => _service?.AnalyzePath(sampleMap), "Invalid Map");
            Assert.AreEqual("Invalid map: Fork in path.", ex?.Message);
        }

        [Test]
        public void AnalyzePath_InvalidMapBrokenPath_ThrowsArgumentException()
        {
            // Arrange
            var sampleMap = new[]
            {
                " @--A-+ ",
                "      | ",
                "        ",
                "      B-x ",
            };

            // Assert
            var ex = Assert.Throws<ArgumentException>(() => _service?.AnalyzePath(sampleMap), "Invalid Map");
            Assert.AreEqual("Invalid map: Broken path.", ex?.Message);
        }

        [Test]
        public void AnalyzePath_InvalidMapMultipleStartPaths_ThrowsArgumentException()
        {
            // Arrange
            var sampleMap = new[]
            {
                " x-B-@-A-x "
            };

            // Assert
            var ex = Assert.Throws<ArgumentException>(() => _service?.AnalyzePath(sampleMap), "Invalid Map");
            Assert.AreEqual("Invalid map: Fork in path.", ex?.Message);
        }

        [Test]
        public void AnalyzePath_InvalidMapFakeTurn_ThrowsArgumentException()
        {
            // Arrange
            var sampleMap = new[]
            {
                " @-A-+-B-x "
            };

            // Assert
            var ex = Assert.Throws<ArgumentException>(() => _service?.AnalyzePath(sampleMap), "Invalid Map");
            Assert.AreEqual("Invalid map: Fake turn.", ex?.Message);
        }
    }
}
