using Microsoft.Web.WebView2.Core;
using System;
using System.Collections.Generic;
using System.IO;
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

namespace ArduinoIDE
{
    /// <summary>
    /// WindowWebview2.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class WindowWebview2 : Window
    {
        public WindowWebview2()
        {
            InitializeComponent();
            InitializeAsync();
        }

        async void InitializeAsync()
        {
            //var env = await CoreWebView2Environment.CreateAsync(Path.Combine(Path.GetTempPath(), "WebView2"));
            var env = await CoreWebView2Environment.CreateAsync(Path.Combine(Directory.GetCurrentDirectory(), "../../WebView2"));
            await webView.EnsureCoreWebView2Async(env);

            webView.Source = new Uri("http://www.microsoft.com");
        }
    }
}
