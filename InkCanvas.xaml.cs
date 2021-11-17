using Microsoft.Win32;
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
using System.Windows.Shapes;

namespace ArduinoIDE
{
    /// <summary>
    /// InkCanvas.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class InkCanvas : Window
    {
        public bool mClicked = false;
        public bool mRightClicked = false;

        public SolidColorBrush mycolor = Brushes.Red;
        public Point prePosition;
        public Rectangle temprectangle;
        public Ellipse tempellipse;

        public int erasesize;

        public bool lineClicked = true;
        public bool rectangleClicked = false;
        public bool circleClicked = false;


        public Image myImage;
        public bool imageMoved = true;
        public Image findImage;

        public InkCanvas()
        {
            InitializeComponent();
        }

        private void Window_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            mClicked = true;
            prePosition = e.GetPosition(canvas);
        }

        private void Window_MouseMove(object sender, MouseEventArgs e)
        {
            var nowPosition = e.GetPosition(canvas);
            if (mClicked)
            {
                if (lineClicked)
                {
                    Line line = new Line();
                    line.X1 = prePosition.X;
                    line.X2 = nowPosition.X;
                    line.Y1 = prePosition.Y;
                    line.Y2 = nowPosition.Y;
                    line.Stroke = mycolor;
                    line.StrokeThickness = 2;
                    canvas.Children.Add(line);

                    prePosition = nowPosition;

                }
                else if (rectangleClicked)
                {
                    if (e.MouseDevice.LeftButton == MouseButtonState.Pressed)
                    {
                        Rectangle myrectangle = new Rectangle();

                        myrectangle.Stroke = Brushes.Plum;
                        myrectangle.Fill = mycolor;
                        myrectangle.Opacity = 0.8;

                        double left = prePosition.X;
                        double top = prePosition.Y;

                        double width = nowPosition.X - prePosition.X;
                        double height = nowPosition.Y - prePosition.Y;

                        if (nowPosition.X < prePosition.X)
                        {
                            left = nowPosition.X;
                            width *= -1;
                        }
                        if (nowPosition.Y < prePosition.Y)
                        {
                            top = nowPosition.Y;
                            height *= -1;
                        }

                        myrectangle.Margin = new Thickness(left, top, 0, 0);
                        myrectangle.Width = width;
                        myrectangle.Height = height;

                        canvas.Children.Remove(temprectangle);
                        temprectangle = myrectangle;
                        canvas.Children.Add(myrectangle);
                    }
                    else
                    {
                        temprectangle = null;
                    }
                }
                else if (circleClicked)
                {
                    if (e.MouseDevice.LeftButton == MouseButtonState.Pressed)
                    {
                        Ellipse myellipse = new Ellipse();

                        myellipse.Fill = mycolor;
                        myellipse.StrokeThickness = 2;
                        myellipse.Stroke = Brushes.LightSkyBlue;
                        myellipse.Opacity = 0.8;

                        double left = prePosition.X;
                        double top = prePosition.Y;

                        double width = nowPosition.X - prePosition.X;
                        double height = nowPosition.Y - prePosition.Y;

                        if (nowPosition.X < prePosition.X)
                        {
                            left = nowPosition.X;
                            width *= -1;
                        }
                        if (nowPosition.Y < prePosition.Y)
                        {
                            top = nowPosition.Y;
                            height *= -1;
                        }

                        myellipse.Margin = new Thickness(left, top, 0, 0);
                        myellipse.Width = width;
                        myellipse.Height = height;

                        canvas.Children.Remove(tempellipse);
                        tempellipse = myellipse;
                        canvas.Children.Add(myellipse);
                    }
                    else
                    {
                        tempellipse = null;
                    }
                }
            }
            else if (imageMoved)
             {
                if (findImage != null)
                {
                    double imageleft = nowPosition.X - findImage.ActualWidth / 2;
                    double imagetop = nowPosition.Y - findImage.ActualHeight / 2;
                    findImage.Margin = new Thickness(imageleft, imagetop, 0, 0);
                }
            }
        }

        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            mClicked = false;
            temprectangle = null;
            tempellipse = null;
        }

        private void Window_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {
            mRightClicked = true;

            var nowPosition = e.GetPosition(canvas);

            int count = canvas.Children.Count;
            for (int i = 0; i < count; i++)
            {
                Image im = canvas.Children[i] as Image;
                if (im != null)
                {
                    if (im.Margin.Left < nowPosition.X && im.Margin.Left + im.ActualWidth > nowPosition.X)
                    {
                        if (im.Margin.Top < nowPosition.Y && im.Margin.Top + im.ActualHeight > nowPosition.Y)
                        {
                            imageMoved = true;
                            findImage = im;
                        }
                    }
                }
            }


        }

        private void Window_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            mRightClicked = false;

            imageMoved = false;
            findImage = null;

        }

        private void Button_line_Click(object sender, RoutedEventArgs e)
        {
            lineClicked = true;
            circleClicked = false;
            rectangleClicked = false;

            button_line.BorderBrush = Brushes.Red;
            button_circle.BorderBrush = Brushes.Black;
            button_rectangle.BorderBrush = Brushes.Black;
        }

        private void Button_circle_Click(object sender, RoutedEventArgs e)
        {
            lineClicked = false;
            circleClicked = true;
            rectangleClicked = false;

            button_line.BorderBrush = Brushes.Black;
            button_circle.BorderBrush = Brushes.Red;
            button_rectangle.BorderBrush = Brushes.Black;
        }

        private void Button_rectangle_Click(object sender, RoutedEventArgs e)
        {
            lineClicked = false;
            circleClicked = false;
            rectangleClicked = true;

            button_line.BorderBrush = Brushes.Black;
            button_circle.BorderBrush = Brushes.Black;
            button_rectangle.BorderBrush = Brushes.Red;
        }

        private void Button_erase_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_fileopen_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();

            if (openDialog.ShowDialog() == true)
            {
                if (File.Exists(openDialog.FileName))
                {
                    Stream imageStreamSource = new FileStream(openDialog.FileName, FileMode.Open, FileAccess.Read, FileShare.Read);
                    PngBitmapDecoder decoder = new PngBitmapDecoder(imageStreamSource, BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
                    BitmapSource bitmapSource = decoder.Frames[0];

                    myImage = new Image();
                    myImage.Source = bitmapSource;
                    myImage.Width = 200;

                    myImage.Tag = System.IO.Path.GetFullPath(openDialog.FileName);
                    canvas.Children.Add(myImage);

                }
            }
        }

        private void Button_jsonsave_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Button_paint_Click(object sender, RoutedEventArgs e)
        {
            mycolor = Brushes.Black;
        }

        private void Button_allerase_Click(object sender, RoutedEventArgs e)
        {
            canvas.Children.Clear();
            canvas.Background = Brushes.Transparent;
        }

        private void Color_black_Click(object sender, RoutedEventArgs e)
        {
            mycolor = Brushes.Black;
        }

        private void Color_red_Click(object sender, RoutedEventArgs e)
        {
            mycolor = Brushes.Red;
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            erasesize = (comboBox.SelectedIndex + 1) * 10;
        }

        private void Button_jsonload_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Color_green_Click(object sender, RoutedEventArgs e)
        {
            mycolor = Brushes.Green;
        }

        private void Color_blue_Click(object sender, RoutedEventArgs e)
        {
            mycolor = Brushes.Blue;
        }

        private void Color_yellow_Click(object sender, RoutedEventArgs e)
        {
            mycolor = Brushes.Yellow;
        }
    }
}
