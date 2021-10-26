using FluentAssertions;
using Game.Core.Board;
using Game.Core.Language;
using Game.Core.Resources;
using Game.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;

namespace Game.Core.Resources.Tests
{
    [TestClass]
    public class TileSchemaTests
    {
        private readonly string _tileschemaPath = Path.Combine(TestResourceLocator.Location, "tileschema_test.txt");
        private readonly string _incorrecttileschemaPath = Path.Combine(TestResourceLocator.Location, "tileschema_test_shouldFail.txt");

        [TestMethod]
        public void GetAllTilesTest()
        {
            var tileSchema = new TileSchemaLoader().LoadFromFile(_tileschemaPath);
            var actual = tileSchema.Tiles;
            var expected = new List<Tile>()
            {
                new Tile('e', 1),
                new Tile('i', 3),
                new Tile('a', 2),
            };
            actual.Should().BeEquivalentTo(expected);
        }

        [TestMethod]
        public void LoadFromFileTest()
        {
            var tileSchema = new TileSchemaLoader().LoadFromFile(_tileschemaPath);

            tileSchema.NumberOfTiles.Should().Be(3);
            tileSchema.TileExist('E').Should().BeTrue();
            tileSchema.TileExist('A').Should().BeTrue();
            tileSchema.TileExist('I').Should().BeTrue();
        }

        [TestMethod]
        public void LoadFromFileTest_ShouldThrowException_WrongFormat()
        {
            void loadFromFile() => _ = new TileSchemaLoader().LoadFromFile(_incorrecttileschemaPath);
            Assert.ThrowsException<FileLoadException>(loadFromFile);
        }

        [TestMethod]
        public void LoadFromFileTest_ShouldThrowException_NonExistingFile()
        {
            void loadFromFile() => _ = new TileSchemaLoader().LoadFromFile(_tileschemaPath + ".error");
            Assert.ThrowsException<FileLoadException>(loadFromFile);
        }
    }
}