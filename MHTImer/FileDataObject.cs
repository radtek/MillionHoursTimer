﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MHTimer
{

    public class FileDataObject
    {
        public string Name { get; set; } = "";
        public TimeSpan TotalTime { get; set; }
        public TimeSpan TimeFromLaunched { get; set; }
        public bool IsCountStarted { get; set; } = false;
        public bool IsCounted { get; set; } = false;

        public string GetTime
        {
            get
            {
                return AppDataObject.GetFormattedStringFromTimeSpan(TotalTime);
            }
        }

        public void AccumulateTime()
        {
            //同名ファイルが複数回計測されるのを防ぐ
            if (IsCounted) return;

            TimeFromLaunched = TimeFromLaunched.Add(TimeSpan.FromSeconds(Settings.CountingSecondsInterval));


            //指定した時間が経過していたら、データの記録を開始
            if (TimeFromLaunched.Minutes >= Settings.MinCountStartTime)
            {
                if (!IsCountStarted)
                {
                    TotalTime = TotalTime.Add(TimeSpan.FromSeconds(Settings.MinCountStartTime));
                    IsCountStarted = true;
                }
                else
                {
                    TotalTime = TotalTime.Add(TimeSpan.FromSeconds(Settings.CountingSecondsInterval));
                }
            }
        }

        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
            {
                return false;
            }
            FileDataObject data = (FileDataObject)obj;
            return (data.Name == this.Name);
        }

        public override int GetHashCode()
        {
            return Name.GetHashCode();
        }
    }

}
