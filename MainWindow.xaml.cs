﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace ArduinoIDE
{
    /// <summary>
    /// MainWindow.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void BtnHello_Click(object sender, RoutedEventArgs e)
        {
            int randomNum = test();
            tbHi.Text = btnHello.Content.ToString() + " World " + randomNum;
        }

        private int test()
        {
            Random random = new Random();
            return random.Next(1, 100);
        }

        private void Btn_Show_Click(object sender, RoutedEventArgs e)
        {
            Window1 window1 = new Window1("hklee");
            window1.Show();

        }

        private void MenuItem_Exit_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void Btn_Show_Paint_Click(object sender, RoutedEventArgs e)
        {
            InkCanvas window1 = new InkCanvas();
            window1.Show();
        }

        private void Btn_Show_Webview2_Click(object sender, RoutedEventArgs e)
        {
            WindowWebview2 window = new WindowWebview2();
            window.Show();
        }
    }
}
