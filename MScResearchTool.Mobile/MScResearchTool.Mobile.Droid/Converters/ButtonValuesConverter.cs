using MScResearchTool.Mobile.Droid.Enums;
using System.Collections.Generic;
using System.Linq;

namespace MScResearchTool.Mobile.Droid.Converters
{
    public class ButtonValuesConverter
    {
        private IDictionary<EButtonValues, string> _buttonValuesDictionary { get; set; }

        public ButtonValuesConverter()
        {
            _buttonValuesDictionary = new Dictionary<EButtonValues, string>();

            FillDictionary();
        }

        public virtual string EnumeratorToString(EButtonValues buttonValue)
        {
            return _buttonValuesDictionary.First(x => x.Key == buttonValue).Value;
        }

        public virtual EButtonValues StringToEnumerator(string buttonValue)
        {
            return _buttonValuesDictionary.First(x => x.Value == buttonValue).Key;
        }

        private void FillDictionary()
        {
            _buttonValuesDictionary.Add(EButtonValues.START, "Enable background work");
            _buttonValuesDictionary.Add(EButtonValues.STOP, "Disable background work");
        }
    }
}