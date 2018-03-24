using MScResearchTool.Server.Core.Enums;
using System.Collections.Generic;
using System.Linq;

namespace MScResearchTool.Server.Web.Converters
{
    public class TaskTypeConverter
    {
        private IDictionary<ETaskType, string> _taskTypeDictionary { get; set; }

        public TaskTypeConverter()
        {
            _taskTypeDictionary = new Dictionary<ETaskType, string>();

            FillDictionary();
        }

        public virtual string EnumeratorToString(ETaskType taskType)
        {
            return _taskTypeDictionary.First(x => x.Key == taskType).Value;
        }

        public virtual ETaskType StringToEnumerator(string taskType)
        {
            return _taskTypeDictionary.First(x => x.Value == taskType).Key;
        }

        private void FillDictionary()
        {
            _taskTypeDictionary.Add(ETaskType.SquareIntegration, "Square integration");
            _taskTypeDictionary.Add(ETaskType.TrapezoidIntegration, "Trapezoid integration");
            _taskTypeDictionary.Add(ETaskType.Cracking, "Cracking");
        }
    }
}
