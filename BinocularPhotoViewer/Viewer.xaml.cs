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
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

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
        GamePadState lastGamePadState;

        //To indicate which image has been selected--left or right
        enum SelectedImage : int { left = 1, right = 2 };
        int imageSelected = 0;
        
        //enum to indicate direction
        enum Direction: int 
        {
            up = 1,
            down,
            left,
            right
        };

        //varaible to hold which direction moved 
        int move = 0;
        int moveValue = 10; //how much to move by

        //For zooming
        enum Zoom : int { zoomIn = 1, zoomOut = -1 };
        int zoom = 0;
        float zoomValue = 0.1f;

        //create variables that will store where the user set the first image and 
        //then will be used to display the other images at the same spatial window coordinates and state
        bool setImagePosition = true;
        //other variables

        public Viewer()
        {
            InitializeComponent();
            LeftCanvas.Visibility = Visibility.Visible;

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

            //set timer object
            _timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(100) };
            _timer.Tick += _timer_Tick;
            _timer.Start();

            //slider_active = true;
        }

        void _timer_Tick(object sender, EventArgs e)
        {
            //check for Xbox Controller inputs  and set flags
            UpdateInput();
            UpdatePosition();
        }

        void UpdateInput()
        {
            GamePadState currentState = GamePad.GetState(PlayerIndex.One);

            //check for GamePadState and various buttons that have been pressed
            //based on button presses take specific actions
            if (currentState.IsConnected && lastGamePadState != currentState)
            {
                #region   Shoulder Button
                //if Rightshoulder button has been pressed select right image and set flags
                //if leftshoulder button has been pressed select left image and set flags
                if (currentState.Buttons.LeftShoulder == ButtonState.Pressed)
                {
                    imageSelected = (int)SelectedImage.left;
                }

                if (currentState.Buttons.RightShoulder == ButtonState.Pressed)
                {
                    imageSelected = (int)SelectedImage.right;
                }
                #endregion

                #region Dpad Direction
                //if DPad has been pressed...check which direction and move the image accordingly
                if (currentState.DPad.Up == ButtonState.Pressed)
                {
                    move = (int)Direction.up;
                }
                else if (currentState.DPad.Down == ButtonState.Pressed)
                {
                    move = (int)Direction.down;
                }
                else if (currentState.DPad.Left == ButtonState.Pressed)
                {
                    move = (int)Direction.left;
                }
                else if (currentState.DPad.Right == ButtonState.Pressed)
                {
                    move = (int)Direction.right;
                }
                else
                    move = 0;
                #endregion

                #region LeftThumbstick Direction
                //ifLeftThumbstick has been moved up or down zoom in and out respectively
                if (lastGamePadState.IsButtonUp(Buttons.LeftThumbstickUp) && currentState.IsButtonDown(Buttons.LeftThumbstickUp))
                {
                    zoom = (int)Zoom.zoomOut;
                }

                else if (lastGamePadState.IsButtonUp(Buttons.LeftThumbstickDown) && currentState.IsButtonDown(Buttons.LeftThumbstickDown))
                {
                    zoom = (int)Zoom.zoomIn;
                }
                #endregion

                //if Button A has been pressed move to the next image and save state accordingly
                if (currentState.Buttons.A == ButtonState.Pressed)
                {
                   //save state and move to the next image
                }
            }
            
            lastGamePadState = currentState;
        }

        void UpdatePosition()
        {
            //update position of left image
            if (imageSelected == (int)SelectedImage.left)
            {
                //get focus on left image
                LeftCanvas.Focus();
                Double LeftPos = Convert.ToDouble(leftImage.GetValue(Canvas.LeftProperty));
                Double topPos = Convert.ToDouble(leftImage.GetValue(Canvas.TopProperty));
                if(move != 0)
                    moveImage(LeftPos,topPos,leftImage);
                if (zoom != 0)
                {
                    zoomImage(LeftPos, topPos, leftImage);
                }
            }
            //update position of right image
            else if (imageSelected == (int)SelectedImage.right)
            {
                //get focus on right image
                RightCanvas.Focus();
                Double LeftPos = Convert.ToDouble(rightImage.GetValue(Canvas.LeftProperty));
                Double topPos = Convert.ToDouble(rightImage.GetValue(Canvas.TopProperty));
                if (move != 0)
                    moveImage(LeftPos, topPos, rightImage);
                if (zoom != 0)
                {
                    zoomImage(LeftPos, topPos, rightImage);
                }
            }
        }

        void moveImage(double LeftPos, double topPos,Image image)
        {
            if (move == (int)Direction.left)
            {
                //if (LeftPos < (LeftCanvas.ActualWidth - leftImage.Width))
                image.SetValue(Canvas.LeftProperty, LeftPos - moveValue);
            }

            else if (move == (int)Direction.right)
            {
                image.SetValue(Canvas.LeftProperty, LeftPos + moveValue);
            }

            else if (move == (int)Direction.up)
            {
                //if (topPos < (LeftCanvas.ActualHeight - leftImage.Height))
                image.SetValue(Canvas.TopProperty, topPos - moveValue);
            }
            else if (move == (int)Direction.down)
            {
                image.SetValue(Canvas.TopProperty, topPos + moveValue);
            }
            else if (move == -1)
            { 
                //image needs to be redrawn at topPos, leftPos because it was scaled 
                image.SetValue(Canvas.TopProperty, topPos);
                image.SetValue(Canvas.LeftProperty, LeftPos);
            }

            //reset move to 0
            move = 0;
        }

        void zoomImage(double LeftPos, double topPos, Image image)
        { 
            TransformGroup transformGroup = (TransformGroup)image.RenderTransform;
            ScaleTransform transform = (ScaleTransform)transformGroup.Children[0];

            //scale image and then redraw it at the previous position
            //only scale if scaling factor is greater than zero
            if (transform.ScaleX + zoomValue * zoom > 0)
            {
                transform.ScaleX += zoomValue * zoom;
                transform.ScaleY += zoomValue * zoom;
            }
            
            move = -1;
            moveImage(LeftPos, topPos, image);

            //reset zoom to 0
            zoom = 0;
        }

        //#region Left Canvas
        //private void LeftCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    LeftCanvas.Focusable = true;
        //    LeftCanvas.Focus();
        //}

        //private void LeftCanvas_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        //{
        //    RightCanvas.Focusable = false;
        //    leftSliderZoom.Focusable = false;
        //    rightSliderZoom.Focusable = false;
        //    LeftCanvas.Focus();
        //}

        //private void LeftCanvas_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        //{
        //    RightCanvas.Focusable = true;
        //    leftSliderZoom.Focusable = true;
        //    rightSliderZoom.Focusable = true;
        //}

        ////to move the image on the left canvas
        //private void leftCanvas_KeyDown(object sender, KeyEventArgs e)
        //{
        //    Double LeftPos = Convert.ToDouble(leftImage.GetValue(Canvas.LeftProperty));
        //    Double topPos = Convert.ToDouble(leftImage.GetValue(Canvas.TopProperty));
            
        //    //make sure that the slider doesn't move when moving the image using key press events
        //    LeftCanvas.Focus();

        //    if (e.Key  == Key.Left)
        //    {
        //        //if (LeftPos < (LeftCanvas.ActualWidth - leftImage.Width))
        //            leftImage.SetValue(Canvas.LeftProperty, LeftPos - 10);
        //    }

        //    else if (e.Key == Key.Right)
        //    {
        //        leftImage.SetValue(Canvas.LeftProperty, LeftPos + 10);
        //    }

        //    else if (e.Key == Key.Up)
        //    {
        //        //if (topPos < (LeftCanvas.ActualHeight - leftImage.Height))
        //            leftImage.SetValue(Canvas.TopProperty, topPos - 10);
        //    }
        //    else if (e.Key == Key.Down)
        //    {
        //        leftImage.SetValue(Canvas.TopProperty, topPos + 10);
        //    }


        //}

        ////to zoom the image on the left canvas
        //private void leftSliderZoom_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        //{
        //    if (slider_active)
        //    {
        //        //set the other elements to focusable
        //        RightCanvas.Focusable = true;
        //        rightSliderZoom.Focusable = true;

        //        TransformGroup transformGroup = (TransformGroup)leftImage.RenderTransform;
        //        ScaleTransform transform = (ScaleTransform)transformGroup.Children[0];

        //        double zoom = leftSliderZoom.Value / 10;
        //        transform.ScaleX = zoom;
        //        transform.ScaleY = zoom;
        //    }
        //}

        //#endregion  

        //#region Right Canvas
        
        //private void RightCanvas_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    RightCanvas.Focusable = true;
        //    RightCanvas.Focus();
        //}
        
        //private void RightCanvas_LostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        //{
        //    LeftCanvas.Focusable = true;
        //    leftSliderZoom.Focusable = true;
        //    rightSliderZoom.Focusable = true;
        //}

        //private void RightCanvas_GotKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        //{
        //    LeftCanvas.Focusable = false;
        //    leftSliderZoom.Focusable = false;
        //    rightSliderZoom.Focusable = false;
        //    RightCanvas.Focus();
        //}

        ////to move the image on the right canvas
        //private void rightCanvas_KeyDown(object sender, KeyEventArgs e)
        //{
        //    Double LeftPos = Convert.ToDouble(rightImage.GetValue(Canvas.LeftProperty));
        //    Double topPos = Convert.ToDouble(rightImage.GetValue(Canvas.TopProperty));

        //    //make sure that the slider doesn't move when moving the image using key press events
        //    RightCanvas.Focus();

        //    if (e.Key == Key.Left)
        //    {
        //        //if (LeftPos < (LeftCanvas.ActualWidth - leftImage.Width))
        //        rightImage.SetValue(Canvas.LeftProperty, LeftPos - 10);
        //    }

        //    else if (e.Key == Key.Right)
        //    {
        //        rightImage.SetValue(Canvas.LeftProperty, LeftPos + 10);
        //    }

        //    else if (e.Key == Key.Up)
        //    {
        //        //if (topPos < (LeftCanvas.ActualHeight - leftImage.Height))
        //        rightImage.SetValue(Canvas.TopProperty, topPos - 10);
        //    }
        //    else if (e.Key == Key.Down)
        //    {
        //        rightImage.SetValue(Canvas.TopProperty, topPos + 10);
        //    }

        //}

        ////to zoom the image on the right canvas
        //private void rightSliderZoom_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        //{
            
        //    if (slider_active)
        //    {
        //        TransformGroup transformGroup = (TransformGroup)rightImage.RenderTransform;
        //        ScaleTransform transform = (ScaleTransform)transformGroup.Children[0];
                
        //        //set the other elements to focusable
        //        LeftCanvas.Focusable = true;
        //        leftSliderZoom.Focusable = true;

        //        //set the zoom levels
        //        double zoom = rightSliderZoom.Value / 10;
        //        transform.ScaleX = zoom;
        //        transform.ScaleY = zoom;
        //    }
        //}
        //#endregion

                
    }
}
