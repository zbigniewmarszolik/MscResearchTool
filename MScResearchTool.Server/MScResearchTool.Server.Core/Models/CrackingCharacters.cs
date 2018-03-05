namespace MScResearchTool.Server.Core.Models
{
    public class CrackingCharacters
    {
        private static CrackingCharacters _instance;

        protected CrackingCharacters()
        {
            Characters = new char[]
            {
                'a', 'b', 'c', 'd', 'e'
            };
        }

        public char[] Characters { get; }

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
