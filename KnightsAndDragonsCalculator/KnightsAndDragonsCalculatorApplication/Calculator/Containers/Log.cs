using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KnightsAndDragonsCalculatorApplication.Calculator.Containers
{

    public enum LogType
    {
        BugFix,
        Data,
        NewFeature
    }

    public class Log
    {
        public DateTime Date { get; set; }
        public LogType Type { get; set; }
        public string Description { get; set; }
        public string TypeDescription { get { return GetTypeDescription(Type); } }
        public string RowClass { get { return GetRowClass(Type); } }

        public Log() { }

        public Log(DateTime date, LogType type, string description)
        {
            Date = date;
            Type = type;
            Description = description;
        }

        private string GetTypeDescription(LogType type)
        {
            switch (type)
            {
                case LogType.BugFix:
                    return Strings.LogTypeBugFix;
                case LogType.Data:
                    return Strings.LogTypeData;
                case LogType.NewFeature:
                    return Strings.LogTypeNewFeature;
                default:
                    return string.Empty;
            }
        }

        private string GetRowClass(LogType type)
        {
            switch (type)
            {
                case LogType.BugFix:
                    return "danger";
                case LogType.Data:
                    return "warning";
                case LogType.NewFeature:
                    return "success";
                default:
                    return string.Empty;
            }
        }
    }
}