using System;
using System.Linq;

namespace CodingTask
{
    class Program
    {
        static void Main(string[] args)
        {
            bool running = true;

            while (running)
            {
                int invalid, valid;
                string[] inputList;
                invalid = 0;
                valid = 0;

                Console.Write("Patient ID(s):");
                string input = Console.ReadLine();


                if (input == "url")
                {
                    input = new System.Net.WebClient().DownloadString("https://s3.amazonaws.com/cognisant-interview-resources/identifiers.json");
                    input = input.Trim('[', ']');
                    input = input.Replace("\"", string.Empty);
                    inputList = input.Split(",");

                }
                else if (input == "exit")
                {
                    running = false;
                    break;
                }
                else
                {   
                    inputList = new string[] { input };
                }

                foreach (string id in inputList)
                {
                    if (VerifyID(id))
                    {
                        valid++;
                    }
                    else
                    {
                        invalid++;
                    }
                }

                Console.WriteLine("Valid Results ["+valid+"]");
                Console.WriteLine("Inavlid Results [" + invalid + "]");
                Console.WriteLine("Total IDs: [" + (valid + invalid) + "]\n");
                Console.WriteLine("Press any key to input another ID(s)");
                Console.ReadKey();
                Console.Clear();
            }         
        }

        static bool VerifyID(string id)
        {
            if (id.Length != 10 || !IsDigits(id) || IdenticalChars(id))
            {
                return false;
            }
            if (!WeightCheck(id))
            {
                return false;
            }
            return true;
        }

        static bool IsDigits(string id)
        {
            return id.All(char.IsDigit);
        }

        static bool IdenticalChars(string id)
        {
            return id.All(ch => ch == id[0]);
        }

        static bool WeightCheck(string id)
        {
            
            int[] weights = { 11, 10, 9, 8, 7, 6, 5, 4, 3 };
            int weightSum = 0;
            for (int i = 0; i < weights.Length; i++)
            {
                weightSum += weights[i] * int.Parse(id[i].ToString()); 
            }                                                             
 
            int remainder = weightSum % 12;
            int finalDigit = 12 - remainder;

            if (finalDigit == 11)
            {
                finalDigit = 0;
            }

            return (finalDigit == int.Parse(id[9].ToString()));
        }
    }
}
