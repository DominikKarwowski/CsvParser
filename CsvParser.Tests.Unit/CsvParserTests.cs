using System;
using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;
using DjK.Utilities;
using System.IO;

namespace Utilities.Tests.Unit
{
    [TestFixture]
    public class CsvParserTests
    {
        [Test]
        public void CsvParser_ConstructedWithoutSpecifyingSeparator_CreatesInstanceWithSemicolonSeparatorByDefault()
        {
            // Arrange
            const string DUMMY_FILE_PATH = @"C:\dummyPath\dummyFile.csv";
            const char DEFAULT_SEPARATOR = ';';

            // Act
            var csvParserSut = new CsvParser(DUMMY_FILE_PATH);

            // Assert
            Assert.AreEqual(DEFAULT_SEPARATOR, csvParserSut.Separators[0]);
        }

        [Test]
        public void CsvParser_SourceFilePropertyValue_IsSetFromConstructor()
        {
            // Arrange
            const string DUMMY_FILE_PATH = @"C:\dummyPath\dummyFile.csv";

            // Act
            var csvParserSut = new CsvParser(DUMMY_FILE_PATH);

            // Assert
            Assert.AreEqual(DUMMY_FILE_PATH, csvParserSut.SourceFile);
        }

        [Test]
        public void Separators_NullValueAssigned_ThrowsArgumentNullExceptio()
        {
            // Arrange
            const string DUMMY_FILE_PATH = @"C:\dummyPath\dummyFile.csv";
            var csvParserSut = new CsvParser(DUMMY_FILE_PATH);

            // Act
            void CodeToTest() => csvParserSut.Separators = null;

            // Arrange
            Assert.Throws(
                Is.TypeOf<ArgumentNullException>()
                .And.Property("ParamName").EqualTo(nameof(csvParserSut.Separators)),
                CodeToTest);
        }

        [Test]
        public void Separators_EmptyArrayAssigned_ThrowsArgumentNullExceptio()
        {
            // Arrange
            const string DUMMY_FILE_PATH = @"C:\dummyPath\dummyFile.csv";
            var csvParserSut = new CsvParser(DUMMY_FILE_PATH);

            // Act
            void CodeToTest() => csvParserSut.Separators = new char[] { };

            // Arrange
            Assert.Throws(
                Is.TypeOf<ArgumentException>().
                And.Property("ParamName").EqualTo(nameof(csvParserSut.Separators)),
                CodeToTest);
        }

        [Test]
        public void LinesToListOfT_IfSourceFileCannotBeFound_ThrowsFileNotFoundException()
        {
            // Arrange
            const string DUMMY_FILE_PATH = @"dummyPath\dummyFile.csv";
            var csvParserSut = new CsvParser(DUMMY_FILE_PATH);

            // Act
            void CodeToTest() => csvParserSut.LinesToList(l => l);

            // Assert
            Assert.Throws(
                Is.TypeOf<FileNotFoundException>()
                .And.Property("FileName").EqualTo(csvParserSut.SourceFile),
                CodeToTest);
        }
    }
}
