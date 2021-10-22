using SharpCompress.Archives;
using SharpCompress.Common;
using System;
using System.Linq;

namespace _7ZipDecoder
{
    class Program
    {
        static void Main(string[] args)
        {
            Decrypter fileDecrypter = new Decrypter();
            //path variable => easy implementation of user input file path
            fileDecrypter.TryBruteForceFile();
            Console.ReadLine();
        }
    }
}
