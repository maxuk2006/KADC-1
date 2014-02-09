using KnightsAndDragonsCalculatorApplication.Calculator.Containers;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Xml.Serialization;

namespace KnightsAndDragonsCalculatorApplication.Calculator.Tables
{
    public sealed class LogTable
    {
        private static volatile LogTable instance;
        private static object syncRoot = new Object();

        private List<Log> _logs;

        private LogTable() 
        {
            Initialize();
        }

        public static LogTable Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new LogTable();
                    }
                }

                return instance;
            }
        }

        public List<Log> GetLogs()
        {
            return _logs;
        }

        private void Initialize()
        {
            XmlSerializer xs = new XmlSerializer(typeof(List<Log>));
            using (var fs = new FileStream(HttpContext.Current.Server.MapPath(Strings.FileNameLogs), FileMode.Open))
            {
                _logs = xs.Deserialize(fs) as List<Log>;
            }
            _logs.Sort((x, y) => -1 * x.Date.CompareTo(y.Date));
        }

        #region Old Initialization

        private void InitializeOld()
        {
            _logs = new List<Log>();
            _logs.Add(new Log(DateTime.Now, LogType.NewFeature, "Added Log page."));
            _logs.Sort((x, y) => -1 * x.Date.CompareTo(y.Date));

            XmlSerializer xs = new XmlSerializer(typeof(List<Log>));
            using (var fs = new FileStream(@"d:\logs.xml", FileMode.Create))
            {
                xs.Serialize(fs, _logs);
            }
        }

        #endregion
    }
}