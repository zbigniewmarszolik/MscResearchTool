using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MScResearchTool.Windows.WPF
{
    public class Siema
    {
        private int MaxCount { get; set; }
        private int ActualCount { get; set; }
        IEnumerable<char> Suki { get; set; }
        int[] Indeces { get; set; }
        char[] Result { get; set; }
        private string Value { get; set; }

        public Siema(int maxCount)
        {
            MaxCount = maxCount;

            Result = new char[MaxCount];
            Indeces = new int[MaxCount];



            Suki = new Kurwy().Suczyska;

            ActualCount = Suki.Count();
        }


        public string Next()
        {
            Value = string.Empty;

            if (IsGood)
                Nextttt++;

            for (int i = 0; i < Indeces.Length; i++)
            {
                Result[i] = Suki.ElementAt(Indeces[i]);
            }

            if (!IsGood)
                BardzoNext();

            foreach (var sukus in Result)
            {
                Value += sukus;
            }

            if (Nextttt == 2)
            {
                IsGood = false;
                  return null;
              }

            return Value;
        }

        private bool IsGood { get; set; }
        private int Nextttt { get; set; }


        private bool BardzoNext()
        {
            for (int i = Indeces.Length - 1; i >= 0; i--)
            {
                if (Indeces[i] == ActualCount - 1)
                {
                    Indeces[i] = 0;
                    continue;
                }
                else
                {
                    Indeces[i]++;
                    break;
                }
            }
            
            IsGood = Indeces.All(x => x == ActualCount - 1);

            return IsGood;
        }

        private class Kurwy
        {
            public IList<char> Suczyska { get; set; }

            public Kurwy()
            {
                var chars = Enumerable.Range('0', '9' - '0' + 1).Select(i => (char)i).ToList();
                chars.AddRange(Enumerable.Range('A', 'Z' - 'A' + 1).Select(i => (char)i));
                chars.AddRange(Enumerable.Range('a', 'z' - 'a' + 1).Select(i => (char)i));

                Suczyska = chars.ToList();
            }
        }

    }
}
