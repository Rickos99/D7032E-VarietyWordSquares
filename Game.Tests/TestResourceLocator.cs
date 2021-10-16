using System;
using System.IO;

namespace Game.Tests
{
    /// <summary>
    /// Locate resources used in testing
    /// </summary>
    static class TestResourceLocator
    {
        /// <summary>
        /// Get location of the resource folder which contains test text files
        /// </summary>
        public static string Location
        {
            get => Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "TestResources/");
        }
    }
}
