using System.Collections.Generic;
using System.Linq;

namespace MScResearchTool.Server.Core.Models
{
    public class CrackingCharacters
    {
        private static CrackingCharacters _instance;

        protected CrackingCharacters()
        {
            var chars = Enumerable.Range('0', '9' - '0' + 1).Select(i => (char)i).ToList();
            chars.AddRange(Enumerable.Range('A', 'Z' - 'A' + 1).Select(i => (char)i));
            chars.AddRange(Enumerable.Range('a', 'z' - 'a' + 1).Select(i => (char)i));
            Characters = chars;
        }

        public IList<char> Characters { get; }

        public static CrackingCharacters Instance()
        {
            if (_instance == null)
            {
                _instance = new CrackingCharacters();
            }

            return _instance;
        }
    }
}
