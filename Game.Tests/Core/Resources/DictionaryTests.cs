using FluentAssertions;
using Game.Core.Board;
using Game.Core.Language;
using Game.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;

namespace Game.Core.Resources.Tests
{
    [TestClass]
    public class DictionaryTests
    {

        static List<string> TestDictionary
        {
            get => new List<string>() { "Lorem", "ipsum", "dolor" };
        }

        static object[] WordExistTestData
        {
            get
            {
                return new[]
                {
                    new object[]{ "lorem", true },
                    new object[]{ "LOREM", true },
                    new object[]{ "orem", false },
                    new object[]{ "", false },
                };
            }
        }

        static object[] SquareSequenceExistTestData
        {
            get
            {
                return new[]
                {
                    new object[]{ new List<Square>() {
                        new(default, new('l', 1)),
                        new(default, new('o', 1)),
                        new(default, new('r', 1)),
                        new(default, new('e', 1)),
                        new(default, new('m', 1)),
                    }, true },

                    new object[]{ new List<Square>() {
                        new(default, new('L', 1)),
                        new(default, new('O', 1)),
                        new(default, new('R', 1)),
                        new(default, new('E', 1)),
                        new(default, new('M', 1)),
                    }, true },

                    new object[]{ new List<Square>() {
                        new(default, new('o', 1)),
                        new(default, new('r', 1)),
                        new(default, new('e', 1)),
                        new(default, new('m', 1)),
                    }, false },

                    new object[]{ new List<Square>() {
                        new(default),
                        new(default, new('O', 1)),
                        new(default, new('R', 1)),
                        new(default, new('E', 1)),
                        new(default, new('M', 1)),
                    }, false },
                };
            }
        }

        [TestMethod]
        public void SetDictionaryTest()
        {
            var dictionary = new Dictionary("Test", TestDictionary);
            dictionary.WordCount.Should().Be(TestDictionary.Count);
        }

        [DataTestMethod]
        [DynamicData(nameof(SquareSequenceExistTestData))]
        public void SquareSequenceExistTest(List<Square> squareSequence, bool exists)
        {
            var dictionary = new Dictionary("Test", TestDictionary);
            dictionary.ContainsSquareSequence(squareSequence).Should().Be(exists);
        }

        [DataTestMethod]
        [DynamicData(nameof(WordExistTestData))]
        public void WordExistTest(string word, bool exists)
        {
            var dictionary = new Dictionary("Test", TestDictionary);
            dictionary.ContainsWord(word).Should().Be(exists);
        }

        [TestMethod]
        public void LoadFromFileTest()
        {
            var dictionaryPath = Path.Combine(TestResourceLocator.Location, "dictionary_test.txt");
            var dictionary = new DictionaryLoader().LoadFromFile(dictionaryPath);

            dictionary.ContainsWord("lorem").Should().BeTrue();
            dictionary.WordCount.Should().Be(5);
            dictionary.Name.Should().Be("dictionary_test");
        }

        [TestMethod]
        public void LoadFromFileTest_ShouldThrowException()
        {
            var dictionaryPath = Path.Combine(TestResourceLocator.Location, "NO_FILE.txt");
            Assert.ThrowsException<FileLoadException>(() => _ = new DictionaryLoader().LoadFromFile(dictionaryPath));
        }
    }
}