﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.ComponentModel;
using System.Windows;

namespace WpfApp2.Components
{
    public partial class NotifyIcon : Component
    {
        public NotifyIcon()
        {
            InitializeComponent();
            toolStripMenuItemOpenApp.Click += toolStripMenuItem_OpenApp;
            toolStripMenuItemExitApp.Click += toolStripMenuItem_CloseApp;
        }

        private void toolStripMenuItem_OpenApp(object sender, EventArgs e)
        {
            // MainWindow を生成、表示
            var wnd = new MainWindow();
            wnd.Show();
        }

        private void toolStripMenuItem_CloseApp(object sender, EventArgs e)
        {
            if (MessageBoxResult.Yes != MessageBox.Show("終了してよろしいですか？", "終了確認", MessageBoxButton.YesNo, MessageBoxImage.Information))
            {
                //e.Cancel = true;
                Application.Current.Shutdown();
                return;
            }
            // MainWindow を生成、表示
            //var wnd = new MainWindow();
            //wnd.Show();
        }

        public NotifyIcon(IContainer container)
        {
            container.Add(this);

            InitializeComponent();
        }
    }
}
