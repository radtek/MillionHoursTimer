﻿using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
//using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace WpfApp2
{
    public class AppDataObject
    {
        //アイコン画像の保存先ディレクトリ
        private readonly string iconFileDir = "data/icons/";

        private MainWindow mainWindow;

        //プロセスの名前（不変）
        public string ProcessName { get; } = "";

        //ウィンドウに表示される名前
        public string DisplayedName { get; set; } = "";

        //起動した時間
        public DateTime LaunchedTime { get; set; }

        //最後に起動した日
        //public string LastDate { get; private set; } = "";

        //最後に起動を確認した時刻
        public DateTime LastTime { get; private set; }

        public string FileExtension { get; private set; } = "";
        public List<string> FileExtensions { get; private set; } = new List<string>();
        public List<FileData> Files { get; private set; } = new List<FileData>();

        public ImageSource ImageSource { get; private set; }
        public Image IconImage { get; private set; }

        public int TodaysMinutes { get; set; }
        public int TotalMinutes { get; set; }

        //Toggｌ記録用の終了確認フラグ
        public bool IsRunning { get; set; } = false;

        //Toggl設定
        public bool IsLinkedToToggle { get; set; } = false;
        public string LinkedProjectName { get; set; } = "";
        public string LinkedTag { get; set; } = "";


        public List<string> ProjectNames { get; set; } = new List<string>() { "" };
        public List<string> TagNames { get; set; } = new List<string>() { "" };

        public AppDataObject(MainWindow mainWindow, string processName)
        {
            this.mainWindow = mainWindow;
            this.ProcessName = processName;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || this.GetType() != obj.GetType())
            {
                return false;
            }
            AppDataObject data = (AppDataObject)obj;
            return (data.ProcessName == this.ProcessName);
        }

        public override int GetHashCode()
        {
            return ProcessName.GetHashCode();
        }

        public string GetLastTime
        {
            get
            {
                return LastTime.ToString("HH:mm"); ;
            }
        }

        public void SetLastLaunchedTime(string data)
        {
            if (!string.IsNullOrEmpty(data) && data != "-")
            {
                //string[] dateAndTime = data.Split(' ');
                //LastDate = dateAndTime[0];
                //LastTime = DateTime.Parse(dateAndTime[]);
                LastTime = DateTime.Parse(data);
            }
        }

        public int GetIndexOfTag
        {
            get
            {
                int index = mainWindow.TogglManager.Tags.IndexOf(LinkedTag);
                return index;
            }
            set
            {
                LinkedTag = mainWindow.TogglManager.Tags[value];
            }
        }

        public string GetTotalTime
        {
            get
            {
                TimeSpan ts = new TimeSpan(0, TotalMinutes, 0);
                if (ts.Hours == 0 && ts.Minutes == 0)
                {
                    return ("-");
                }
                else
                {
                    return ($"{ts.Hours.ToString().PadLeft(2)}時間　{ts.Minutes.ToString().PadLeft(2)}分");
                }
            }
        }

        public string GetTodaysTime
        {
            get
            {
                TimeSpan ts = new TimeSpan(0, TodaysMinutes, 0);
                if (ts.Hours == 0 && ts.Minutes == 0)
                {
                    return ("-");
                }
                else
                {
                    return ($"{ts.Hours.ToString().PadLeft(2)}時間　{ts.Minutes.ToString().PadLeft(2)}分");
                }
            }
        }

        public string GetLastLaunchedTime
        {
            get
            {
                string lastDate = LastTime.ToString("yyyy");
                if (lastDate != "0001")
                {
                    return LastTime.ToString("yyyy/MM/dd hh:mm");
                }
                else
                {
                    return "-";
                }
            }
        }

        public void SetFileExtensions(string input)
        {
            string[] parsed = input.Split('/');
            if (parsed.Length > 0)
            {
                FileExtensions = new List<string>();
                foreach (string s in parsed)
                {
                    FileExtensions.Add(s);
                }
            }
        }

        /// <summary>
        /// ファイル別の作業時間を読み込み
        /// </summary>
        public void LoadFileData()
        {
            String path = ProcessName + "_files.csv";
            try
            {
                using (StreamReader reader = new StreamReader(path, Encoding.UTF8))
                {
                    reader.ReadLine();
                    while (!reader.EndOfStream)
                    {
                        String line = reader.ReadLine();
                        String[] parsedLine = line.Split(',');
                        FileData file = new FileData()
                        {
                            Name = parsedLine[0],
                            Minutes = int.Parse(parsedLine[1])
                        };
                        Files.Add(file);
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        /// <summary>
        /// ファイルパスからアイコンを読み込み、保存
        /// </summary>
        /// <param name="path"></param>
        public void SetIcon(string path)
        {
            System.Drawing.Icon icon;
            try
            {
                icon = System.Drawing.Icon.ExtractAssociatedIcon(@path);

                //SaveIconImage(ImageSource);
                IconImage = new Image();
                using (MemoryStream s = new MemoryStream())
                {
                    icon.Save(s);
                    ImageSource = BitmapFrame.Create(s);
                }
                SaveIconImage(ImageSource);

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
 
            //MemoryStream data = new MemoryStream(File.ReadAllBytes(path));
            //WriteableBitmap wbmp = new WriteableBitmap(BitmapFrame.Create(data));
            //data.Close();
            //ImageSource = wbmp;

        }

        public void SaveIconImage(ImageSource source)
        {
            string uriPath = iconFileDir + $"{ProcessName}.png";
            using (var fileStream = new FileStream(@uriPath, FileMode.Create))
            {
                BitmapEncoder encoder = new PngBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create((BitmapSource)source));
                encoder.Save(fileStream);
            }
            Console.WriteLine();
        }

        public void LoadIconImage()
        {
            var bmpImage = new BitmapImage();
            string uriPath = iconFileDir + ProcessName + ".png";
            //string uriPath = AppDomain.CurrentDomain.BaseDirectory + "/" + iconFileDir + ProcessName + $".png";

            //bmpImage.BeginInit();
            //bmpImage.UriSource = new Uri(uriPath, UriKind.Relative);
            //bmpImage.EndInit();
            //ImageSource = bmpImage;

            try
            {
                bmpImage.BeginInit();
                bmpImage.UriSource = new Uri(@uriPath, UriKind.Relative);
                bmpImage.EndInit();
                ImageSource = bmpImage;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        //public void LoadIconImage()
        //{
        //    var bmpImage = new BitmapImage();
        //    string uriPath = iconFileDir + $"tinko.png";
        //    try
        //    {
        //        bmpImage.BeginInit();
        //        bmpImage.UriSource = new Uri(@uriPath, UriKind.Relative);
        //        bmpImage.EndInit();
        //        ImageSource = bmpImage;
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.Message);
        //    }
        //}

        public void SaveFileData()
        {
            String path = ProcessName + "_files.csv";
            if (Files.Count > 0)
            {
                try
                {
                    using (var sw = new System.IO.StreamWriter(@path, false, Encoding.UTF8))
                    {
                        sw.WriteLine($"ファイル名,累積作業時間");
                        foreach (FileData file in Files)
                        {
                            sw.WriteLine($"{file.Name},{file.Minutes}");
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.Message);
                }
            }
        }

        public void AddMinute()
        {
            if (!IsRunning)
            {
                LaunchedTime = DateTime.Now;
                IsRunning = true;
            }

            TotalMinutes += mainWindow.CountMinutes;
            string currentDate = DateTime.Now.ToString("yyyy/MM/dd");

            if (currentDate != LastTime.ToString("yyyy/MM/dd"))
            {
                TodaysMinutes = 0;
            }
            else
            {
                TodaysMinutes += mainWindow.CountMinutes;
            }

            LastTime = DateTime.Now;
            //LastDate = currentDate;
        }

        public void AddMinuteToFiles(Process p)
        {
            string fileName = "";
            string windowTitle = p.MainWindowTitle;

            string[] parsed = windowTitle.Split(' ');

            foreach (string s in parsed)
            {
                foreach (string f in FileExtensions)
                {
                    //登録した拡張子に合致するものがあった
                    if (s.Contains(f))
                    {
                        fileName = s;
                        break;
                    }
                }
            }

            //拡張機能
            if (string.IsNullOrEmpty(fileName) && Properties.Settings.Default.isAdditionalFileName)
            {
                string[] parsed0 = windowTitle.Split('-');
                fileName = parsed0[0];
            }

            if (!string.IsNullOrEmpty(fileName))
            {
                FileData file = Files.Find(f => f.Name == fileName);
                //Console.WriteLine(file.Name);
                if (file != null)
                {
                    file.Minutes += mainWindow.CountMinutes;
                }
                else
                {
                    Files.Add(new FileData{ Name = fileName });
                }
            }
        }

        public class FileData
        {
            public string Name { get; set; }
            public int Minutes { get; set; }

            public string GetTime
            {
                get
                {
                    TimeSpan ts = new TimeSpan(0, Minutes, 0);
                    return ($"{ts.Hours.ToString()}時間　{ts.Minutes.ToString()}分");
                }
            }

            public void Set(int num)
            {
                Minutes += num;
            }

            public override bool Equals(object obj)
            {
                if (obj == null || this.GetType() != obj.GetType())
                {
                    return false;
                }
                FileData data = (FileData)obj;
                return (data.Name == this.Name);
            }

            public override int GetHashCode()
            {
                return Name.GetHashCode();
            }
        }
    }
}
