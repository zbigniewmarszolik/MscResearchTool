using System.Collections.Generic;
using System.Linq;

namespace MScResearchTool.Windows.BusinessLogic
{
    public class SecretGenerator
    {
        private IEnumerable<char> Characters { get; set; }
        private char[] Result { get; set; }
        private int[] Indexes { get; set; }
        private int MaxCount { get; set; }
        private int ActualCount { get; set; }
        private int LastIndex { get; set; }
        private bool IsCycleCompleted { get; set; }
        private string CurrentValue { get; set; }

        public SecretGenerator(IEnumerable<char> dictionary, int maxCount)
        {
            Characters = dictionary;
            MaxCount = maxCount;

            Result = new char[MaxCount];
            Indexes = new int[MaxCount];

            ActualCount = Characters.Count();
        }

        public string Next()
        {
            CurrentValue = string.Empty;

            if (IsCycleCompleted)
                LastIndex++;

            for (int i = 0; i < Indexes.Length; i++)
            {
                Result[i] = Characters.ElementAt(Indexes[i]);
            }

            if (!IsCycleCompleted)
                SetIndexes();

            foreach (var item in Result)
            {
                CurrentValue += item;
            }

            if (LastIndex == 2)
            {
                IsCycleCompleted = false;
                return null;
            }

            return CurrentValue;
        }

        private bool SetIndexes()
        {
            for (int i = Indexes.Length - 1; i >= 0; i--)
            {
                if (Indexes[i] == ActualCount - 1)
                {
                    Indexes[i] = 0;
                    continue;
                }
                else
                {
                    Indexes[i]++;
                    break;
                }
            }

            IsCycleCompleted = Indexes.All(x => x == ActualCount - 1);

            return IsCycleCompleted;
        }
    }
}
