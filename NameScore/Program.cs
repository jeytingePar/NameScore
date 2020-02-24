using System;
using System.IO;

namespace NameScore
{
    class Program
    {
        static void Main(string[] args)
        {
            //get file
            StreamReader sr = new StreamReader("C:\\Users\\jonathane\\Downloads\\p022_names.txt");
            string file = sr.ReadLine();
            file = file.Replace("\"", "");
            string[] names = file.Split(",");

            //score & order names
            Buckets[] mainArray = new Buckets[26];            
            foreach (string name in names)
            {
                HandleName(name, mainArray, 0);
            }

            //calculate total score
            int total = 0;
            int rank = 1;
            Console.WriteLine(ScoreNames(mainArray, ref total, ref rank));            
        }

        public static void HandleName(string name, Buckets[] bucket, int total)
        {
            int currentScore = ScoreLetter(name[0]);

            if (bucket[currentScore - 1] == null)
            {
                bucket[currentScore - 1] = new Buckets();
            }

            if (name.Length == 1)
            {
                bucket[currentScore - 1].score = currentScore + total;
            }
            else
            {
                if (bucket[currentScore - 1].subLetters == null)
                {
                    bucket[currentScore - 1].subLetters = new Buckets[26];
                }
                HandleName(name.Substring(1, name.Length - 1), bucket[currentScore - 1].subLetters, total + currentScore);
            }
        }

        public static int ScoreNames(Buckets[] buckets, ref int total, ref int rank)
        {
            foreach (Buckets bucket in buckets)
            {
                if (bucket != null)
                {                    
                    if (bucket.score > 0)
                    {
                        total += rank++ * bucket.score;
                    }
                    if (bucket.subLetters != null)
                    {
                        ScoreNames(bucket.subLetters, ref total, ref rank);
                    }                    
                }
            }
            return total;
        }

        public static int ScoreLetter(char c)
        {
            return c - 64;
        }

        public class Buckets
        {
            public int score;
            public Buckets[] subLetters;

            public Buckets()
            {
                score = 0;
            }
        }
    }
}
