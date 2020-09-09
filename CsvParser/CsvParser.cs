using System;
using System.Collections.Generic;
using System.IO;

namespace DjK.Utilities
{
    /// <summary>
    /// Provides a functionality to parse csv file and convert text data to specified data object.
    /// </summary>
    public class CsvParser
    {
        private char[] _Separators;

        /// <summary>
        /// Path to csv file.
        /// </summary>
        public string SourceFile { get; set; }

        /// <summary>
        /// Separators used in csv file. Cannot be empty.
        /// </summary>
        /// <exception cref="ArgumentNullException">Thrown on attempt to assign null reference.</exception>
        /// <exception cref="ArgumentException">Thrown on attempt to assign an empty array.</exception>
        public char[] Separators
        {
            get { return _Separators; }
            set
            {
                if (value == null)
                    throw new ArgumentNullException(nameof(Separators), "Separators array reference cannot be null.");
                else if (value.Length == 0)
                    throw new ArgumentException("At least one separator must be specified.", nameof(Separators));
                else
                    _Separators = value;
            }
        }

        /// <summary>
        /// Specify path to csv file. Semicolon (;) will be set as a default separator.
        /// </summary>
        /// <param name="file">Path to csv file.</param>
        public CsvParser(string file)
            : this(file, ';')
        { }

        /// <summary>
        /// Specify path to csv file and a csv separator.
        /// </summary>
        /// <param name="file"></param>
        /// <param name="separator"></param>
        public CsvParser(string file, char separator)
            : this(file, new char[] { separator })
        { }

        /// <summary>
        /// Specify path to csv file and multiple csv separators.
        /// </summary>
        /// <param name="file">Path to csv file.</param>
        /// <param name="separators">Separators used in csv file.</param>
        public CsvParser(string file, char[] separators)
        {
            SourceFile = file;
            Separators = separators;
        }

        /// <summary>
        /// Converts csv line to data object of type T based on provided mapping function.
        /// FileNotFoundException is thrown when the file specified in SourceFile property does not exist.
        /// </summary>
        /// <typeparam name="T">Data object to be created from the data in csv file.</typeparam>
        /// <param name="lineToObjectMapper">Function mapping csv line to a specified object.</param>
        /// <returns>List of data objects created from csv data.</returns>
        /// <exception cref="FileNotFoundException">Thrown if the file specified in the SourceFile property cannot be found.</exception>
        public List<T> LinesToList<T>(Func<string[], T> lineToObjectMapper)
        {
            List<T> retrievedData = new List<T>() { };

            if (File.Exists(SourceFile))
            {
                using (StreamReader file = new StreamReader(SourceFile))
                {
                    string line;
                    while((line = file.ReadLine()) != null)
                    {
                        string[] lineElements = line.Split(Separators);
                        T objectFromLine = lineToObjectMapper(lineElements);
                        if (objectFromLine != null) retrievedData.Add(objectFromLine);
                    }
                }
            }
            else
            {
                throw new FileNotFoundException($"Input file specified by the {nameof(SourceFile)} property not found.", SourceFile);
            }
            
            return retrievedData;
        }

        
    }
}
