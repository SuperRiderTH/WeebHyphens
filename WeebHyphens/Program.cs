using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WeebHyphens
{
    class Program
    {

        static bool IsVowel(char vowel)
        {
            vowel = Char.ToLower(vowel);
            return (vowel == 'a' || vowel == 'e' || vowel == 'i' || vowel == 'o' || vowel == 'u');
        }

        static bool IsConsonant(char cons)
        {
            return !IsVowel(cons);
        }

        static bool IsPunctuation(char pun)
        {
            switch (pun)
            {
                case '.':
                case ',':
                case '\'':
                case '\"':
                case '!':
                case '?':
                    return true;
                default:
                    return false;
            }
        }

        static bool IsDigraph(char c1, char c2)
        {

            c1 = Char.ToLower(c1);
            c2 = Char.ToLower(c2);

            if (c1 == 't' && c2 == 's')
            {
                return true;
            }
            else if (c1 == 'd' && c2 == 'z')
            {
                return true;
            }
            else if ((c1 == 'c' || c1 == 's') && c2 == 'h')
            {
                return true;
            }
            else if (c2 == 'y')
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        static bool IsTrigraph(char c1, char c2, char c3)
        {
            c1 = Char.ToLower(c1);
            c2 = Char.ToLower(c2);
            c3 = Char.ToLower(c3);

            return (IsDigraph(c1, c2) && c3 == 'y');
        }

        static void Main(string[] args)
        {
            System.IO.StreamReader readStream;
            System.IO.StreamWriter writeStream;

            bool DragAndDrop = false;

            String ifilename = "";
            String ofilename = "";

            string tempReadString = "";
            string tempWriteString = "";

            char[] tempReadChars;

            if (args.Length == 0)
            {
                System.Console.WriteLine("Enter an input file name: ");
                ifilename = System.Console.ReadLine();
            }
            else
            {
                if (args.Length == 1)
                {
                    ifilename = args[0];
                    DragAndDrop = true;
                }
                else
                {
                    Console.WriteLine("Too many arguments provided, press Enter to close.");
                    Console.ReadLine();
                    Environment.Exit(1);
                }
            }

            ifilename = ifilename.Replace("\"", "");

            ofilename = ifilename;
            ofilename = ofilename.Replace(".txt", "_Out.txt");

            readStream = new System.IO.StreamReader(ifilename);
            writeStream = new System.IO.StreamWriter(ofilename);


            while (!readStream.EndOfStream)
            {
                tempReadString = readStream.ReadLine();
                tempReadChars = tempReadString.ToCharArray();
                tempWriteString = "";

                for (int i = 0; i < tempReadChars.Length; i++)
                {
                    tempWriteString += tempReadChars[i];

                    if ( (i + 1) != tempReadChars.Length )
                    {

                        if (IsPunctuation(tempReadChars[i + 1]))
                        {
                            i++;
                            tempWriteString += tempReadChars[i];
                            continue;
                        }

                        if ( tempReadChars[i+1] == ' ' )
                        {
                            i++;
                            tempWriteString += " ";
                            continue;
                        }
                    }

                    if (IsVowel(tempReadChars[i]))
                    {
                        if ( (i+1) != tempReadChars.Length )
                        {
                            tempWriteString += "-";
                        }
                        tempWriteString += " ";
                    }
                    else
                    {
                        if ( (i+1) != tempReadChars.Length )
                        {
                            if ( IsVowel(tempReadChars[i+1]) )
                            {
                                continue;
                            }
                            else
                            {
                                if ( (i+2) != tempReadChars.Length )
                                {
                                    if ( IsTrigraph(tempReadChars[i],tempReadChars[i+1],tempReadChars[i+2]) )
                                    {
                                        tempWriteString += tempReadChars[i + 1] + tempReadChars[i + 2];
                                        i += 2;
                                        continue;
                                    }
                                }

                                if ( IsDigraph(tempReadChars[i], tempReadChars[i+1]) )
                                {
                                    tempWriteString += tempReadChars[i + 1];
                                    i++;
                                    continue;
                                }
                                else
                                {
                                    tempWriteString += "- ";
                                }
                            }
                        }
                    }
                }

                Console.WriteLine();
                Console.WriteLine(tempReadString);
                Console.WriteLine(tempWriteString);

                writeStream.WriteLine(tempWriteString);

            }

            readStream.Close();
            writeStream.Close();

            if ( !DragAndDrop )
            {
                Console.WriteLine();
                Console.WriteLine("Press Enter to close.");
                Console.ReadLine();
            }
            
            Environment.Exit(0);

        }


    }
}
