using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;

namespace LoopTest
{
    public abstract class PerformanceResult : INotifyPropertyChanged
    {
        private double forInMS;
        private double foreachInMS;
        private double whileInMS;

        public event PropertyChangedEventHandler PropertyChanged;

        public string Name
        {
            get; set;
        }

        public double ForInMS
        {
            get { return forInMS; }
            set
            {
                if (forInMS == value) return;
                forInMS = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ForInMS)));
            }
        }

        public double ForeachInMS
        {
            get { return foreachInMS; }
            set
            {
                if (foreachInMS == value) return;
                foreachInMS = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(ForeachInMS)));
            }
        }

        public double WhileInMS
        {
            get { return whileInMS; }
            set
            {
                if (whileInMS == value) return;
                whileInMS = value;
                PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(nameof(WhileInMS)));
            }
        }

        public Func<object> CreateData { get; set; }

        public abstract void RunTest();
    }

    public class PerformanceResult<T> : PerformanceResult
    {
        static int maximumDataCount = 10000000;

        public override void RunTest()
        {
            List<T> data = new List<T>();
            //ArrayList data = new ArrayList();

            //Collection<T> data = new Collection<T>();
            for (int i = 0; i < maximumDataCount; i++)
            {
                data.Add((T)CreateData());
            }

            //T[] data = new T[maximumDataCount];
            //for (int i = 0; i < maximumDataCount; i++)
            //{
            //    data[i] = (T)CreateData();
            //}

            int count;
            Stopwatch sw = new Stopwatch();

            long total = 0;
            int testTimes = 10;

            // foreach
            for (int t = 0; t < testTimes; t++)
            {
                sw.Reset();
                sw.Start();
                count = 0;
                foreach (var i in data)
                {
                    var temp = i;
                    count++;
                }
                sw.Stop();
                total += sw.ElapsedMilliseconds;
            }
            ForeachInMS = total / testTimes;

            // while
            total = 0;
            for (int t = 0; t < testTimes; t++)
            {
                sw.Reset();
                sw.Start();
                count = 0;
                IEnumerator enumerator = data.GetEnumerator();
                while (enumerator.MoveNext())
                {
                    var temp = enumerator.Current;
                    count++;
                }
                sw.Stop();
                total += sw.ElapsedMilliseconds;
            }
            WhileInMS = total / testTimes;

            // for
            total = 0;
            for (int t = 0; t < testTimes; t++)
            {
                sw.Reset();
                sw.Start();
                count = 0;
                for (int i = 0; i < data.Count; i++)
                {
                    var temp = data[i];
                    count++;
                }
                sw.Stop();
                total += sw.ElapsedMilliseconds;
            }
            ForInMS = total / testTimes;
        }
    }
}
