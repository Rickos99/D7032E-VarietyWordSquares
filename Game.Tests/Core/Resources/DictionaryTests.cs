using FluentAssertions;
using Game.Tests;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
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

        [TestMethod]
        public void SetDictionaryTest()
        {
            var dictionary = new Dictionary("Test", TestDictionary);
            dictionary.WordCount.Should().Be(TestDictionary.Count);
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
            var dictionary = Dictionary.LoadFromFile(dictionaryPath);

            dictionary.ContainsWord("lorem").Should().BeTrue();
            dictionary.WordCount.Should().Be(5);
            dictionary.Name.Should().Be("dictionary_test");
        }

        [TestMethod]
        public void LoadFromFileTest_ShouldThrowException()
        {
            var dictionaryPath = Path.Combine(TestResourceLocator.Location, "NO_FILE.txt");
            Assert.ThrowsException<FileLoadException>(() => _ = Dictionary.LoadFromFile(dictionaryPath));
        }
    }
}