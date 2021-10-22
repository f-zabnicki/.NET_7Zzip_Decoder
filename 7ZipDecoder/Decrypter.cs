using SharpCompress.Archives;
using SharpCompress.Common;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _7ZipDecoder
{
    class Decrypter
    {
        private string password = "Tir";
        private static char[] charsOfPassword =
        {
        'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j',
        'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
        'u', 'v', 'w', 'x', 'y', 'z','A','B','C','D','E',
        'F','G','H','I','J','K','L','M','N','O','P','Q','R',
        'S','T','U','V','W','X','Y','Z','1','2','3','4','5',
        '6','7','8','9','0'
        };
        private static int passwordLenght = 0;
        private static bool opened = false;
        private static int counter = 0;

        public void TryBruteForceFile()
        {
            Stopwatch watch = new Stopwatch();
            watch.Start();
            while (!opened)
            {
                passwordLenght++;
                BruteForce(passwordLenght);
            }
            watch.Stop();
            Console.WriteLine(watch.Elapsed);
        }
        [Conditional("RELEASE")]
        private void ShowCounter()
        {
            counter++;
            Console.Clear();
            Console.WriteLine($"Tried: {counter} times");
        }

        private void BruteForce(int passwordLenght)
        {
            var charArray = createCharArray(passwordLenght, charsOfPassword[0]);
            var lastCharIndex = charArray.Length - 1;
            CreatePassword(0, charArray, passwordLenght, lastCharIndex);
        }
        private char[] createCharArray(int length, char defaultChar)
        {
            char[] output = new char[length];
            for (int i = 0; i < length; i++)
            {
                output[i] = defaultChar;
            }
            return output;
        }
        private void CreatePassword(int currentPosition, char[] chars, int lenght, int index)
        {
            var nextPosition = currentPosition + 1;
            for (int i = 0; i < charsOfPassword.Length; i++)
            {
                chars[currentPosition] = charsOfPassword[i];
                if (currentPosition < index)
                {
                    CreatePassword(nextPosition, chars, lenght, index);
                }
                else if (opened) { return; }
                else
                {
                    ShowCounter();
                    var output = new String(chars);
                    if (SevenZipOperator.DecryptFile(output))
                    {
                        opened = true;
                        Console.WriteLine("Correct password is: " + output);
                        return;
                    }
                }
            }
        }
    }
}
