using System;
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
using System.Windows.Shapes;

namespace ArduinoIDE
{
    /// <summary>
    /// InkCanvas.xaml에 대한 상호 작용 논리
    /// </summary>
    public partial class InkCanvas : Window
    {
        public bool mClicked = false;
        public SolidColorBrush mycolor = Brushes.Red;
        public Point prePosition;
        public Rectangle temprectangle;
        public Ellipse tempellipse;

        public int erasesize;

        public bool lineClicked = true;
        public bool rectangleClicked = false;
        public bool circleClicked = false;


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
        }

        private void Window_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            mClicked = false;
            temprectangle = null;
            tempellipse = null;
        }

        private void Window_MouseRightButtonDown(object sender, MouseButtonEventArgs e)
        {

        }

        private void Window_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {

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
