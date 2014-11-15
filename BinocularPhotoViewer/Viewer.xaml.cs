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
using System.Windows.Threading;
using Microsoft.Xna.Framework;

namespace BinocularPhotoViewer
{
    /// <summary>
    /// Interaction logic for Viewer.xaml
    /// </summary>
    public partial class Viewer : Window
    {
        DispatcherTimer _timer = new DispatcherTimer();     //to poll for Xbox controller events
        TransformGroup xformGroupLeft, xformGroupRight;
        ScaleTransform xformLeft, xformRight;
        private bool slider_active=false;



        public Viewer()
        {
            InitializeComponent();
            LeftCanvas.Visibility = Visibility.Visible;

            //initialise GamePad class object
            //myGamePad = new GamePad();

            //set timer object
            _timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(100) };
            _timer.Tick += _timer_Tick;
            _timer.Start();

            //set transforms for left canvas
            xformGroupLeft = new TransformGroup();
            xformLeft = new ScaleTransform();
            xformGroupLeft.Children.Add(xformLeft);
            leftImage.RenderTransform = xformGroupLeft;

            //set transforms for right canvas
            xformGroupRight = new TransformGroup();
            xformRight = new ScaleTransform();
            xformGroupRight.Children.Add(xformRight);
            rightImage.RenderTransform = xformGroupRight;


            slider_active = true;
        }

        void _timer_Tick(object sender, EventArgs e)
        {
            //check for Xbox Controller inputs  and apply transforms accordingly
            UpdateInput();
        }

        #region Left Canvas
        private void LeftCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            LeftCanvas.Focusable = true;
            LeftCanvas.Focus();
        }

        private void LeftCanvas_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            RightCanvas.Focusable = false;
            leftSliderZoom.Focusable = false;
            rightSliderZoom.Focusable = false;
            LeftCanvas.Focus();
        }

        private void LeftCanvas_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            RightCanvas.Focusable = true;
            leftSliderZoom.Focusable = true;
            rightSliderZoom.Focusable = true;
        }

        //to move the image on the left canvas
        private void leftCanvas_KeyDown(object sender, KeyEventArgs e)
        {
            Double LeftPos = Convert.ToDouble(leftImage.GetValue(Canvas.LeftProperty));
            Double topPos = Convert.ToDouble(leftImage.GetValue(Canvas.TopProperty));
            
            //make sure that the slider doesn't move when moving the image using key press events
            LeftCanvas.Focus();

            if (e.Key  == Key.Left)
            {
                //if (LeftPos < (LeftCanvas.ActualWidth - leftImage.Width))
                    leftImage.SetValue(Canvas.LeftProperty, LeftPos - 10);
            }

            else if (e.Key == Key.Right)
            {
                leftImage.SetValue(Canvas.LeftProperty, LeftPos + 10);
            }

            else if (e.Key == Key.Up)
            {
                //if (topPos < (LeftCanvas.ActualHeight - leftImage.Height))
                    leftImage.SetValue(Canvas.TopProperty, topPos - 10);
            }
            else if (e.Key == Key.Down)
            {
                leftImage.SetValue(Canvas.TopProperty, topPos + 10);
            }

        }

        //to zoom the image on the left canvas
        private void leftSliderZoom_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (slider_active)
            {
                //set the other elements to focusable
                RightCanvas.Focusable = true;
                rightSliderZoom.Focusable = true;

                TransformGroup transformGroup = (TransformGroup)leftImage.RenderTransform;
                ScaleTransform transform = (ScaleTransform)transformGroup.Children[0];

                double zoom = leftSliderZoom.Value / 10;
                transform.ScaleX = zoom;
                transform.ScaleY = zoom;
            }
        }

        #endregion  

        #region Right Canvas
        
        private void RightCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        {
            RightCanvas.Focusable = true;
            RightCanvas.Focus();
        }
        
        private void RightCanvas_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            LeftCanvas.Focusable = true;
            leftSliderZoom.Focusable = true;
            rightSliderZoom.Focusable = true;
        }

        private void RightCanvas_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            LeftCanvas.Focusable = false;
            leftSliderZoom.Focusable = false;
            rightSliderZoom.Focusable = false;
            RightCanvas.Focus();
        }

        //to move the image on the right canvas
        private void rightCanvas_KeyDown(object sender, KeyEventArgs e)
        {
            Double LeftPos = Convert.ToDouble(rightImage.GetValue(Canvas.LeftProperty));
            Double topPos = Convert.ToDouble(rightImage.GetValue(Canvas.TopProperty));

            //make sure that the slider doesn't move when moving the image using key press events
            RightCanvas.Focus();

            if (e.Key == Key.Left)
            {
                //if (LeftPos < (LeftCanvas.ActualWidth - leftImage.Width))
                rightImage.SetValue(Canvas.LeftProperty, LeftPos - 10);
            }

            else if (e.Key == Key.Right)
            {
                rightImage.SetValue(Canvas.LeftProperty, LeftPos + 10);
            }

            else if (e.Key == Key.Up)
            {
                //if (topPos < (LeftCanvas.ActualHeight - leftImage.Height))
                rightImage.SetValue(Canvas.TopProperty, topPos - 10);
            }
            else if (e.Key == Key.Down)
            {
                rightImage.SetValue(Canvas.TopProperty, topPos + 10);
            }

        }

        //to zoom the image on the right canvas
        private void rightSliderZoom_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            
            if (slider_active)
            {
                TransformGroup transformGroup = (TransformGroup)rightImage.RenderTransform;
                ScaleTransform transform = (ScaleTransform)transformGroup.Children[0];
                
                //set the other elements to focusable
                LeftCanvas.Focusable = true;
                leftSliderZoom.Focusable = true;

                //set the zoom levels
                double zoom = rightSliderZoom.Value / 10;
                transform.ScaleX = zoom;
                transform.ScaleY = zoom;
            }
        }
        #endregion

                
    }
}
