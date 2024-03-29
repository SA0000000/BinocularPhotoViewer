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
using System.Windows.Shapes;
using System.Windows.Threading;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Input;

namespace BinocularPhotoViewer
{
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
        //int zoom = 0;
        float zoomValue = 0.025f;
        int x_zoom = 0, y_zoom = 0;

        //create variables that will store where the user set the first image and 
        //then will be used to display the other images at the same spatial window coordinates and state
        bool setImagePosition = false;
        double leftImg_leftPos, leftImg_topPos, rightImg_leftPos, rightImg_topPos;
        double []img_zoomVal = new double[2];

        //create an instance of Class Images to store and deal with all images
        Images images;

        public Viewer(String studynum, String[] filelist,int training, int task1,int task2,int task3)
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

            //set initial zoom values for images
            //image
            img_zoomVal[0] = xformLeft.ScaleX + 0.15f * (-1);
            img_zoomVal[1] = xformLeft.ScaleY + 0.15f * (-1);
           
            //init Images class object
            images = new Images(studynum,filelist,training, task1,task2,task3);

            //set first image on the canvas
            leftImage.Source = rightImage.Source = new BitmapImage(new Uri(@"FirstImage.png", UriKind.RelativeOrAbsolute));

            //set timer object
            _timer = new DispatcherTimer { Interval = TimeSpan.FromMilliseconds(100) };
            _timer.Tick += _timer_Tick;
            _timer.Start();
        }

        //poll for Xbox Controller events
        void _timer_Tick(object sender, EventArgs e)
        {
            //check for Xbox Controller inputs  and set flags
            UpdateInput();
            UpdatePosition();
        }

        //check for Xbox Controller State and take actions accordingly
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

                #region LeftThumbstick Zooming
                //ifLeftThumbstick has been moved up or down zoom in and out respectively
                if (lastGamePadState.IsButtonUp(Buttons.LeftThumbstickUp) && currentState.IsButtonDown(Buttons.LeftThumbstickUp))
                    y_zoom = (int)Zoom.zoomOut;
                else if (lastGamePadState.IsButtonUp(Buttons.LeftThumbstickDown) && currentState.IsButtonDown(Buttons.LeftThumbstickDown))
                    y_zoom = (int)Zoom.zoomIn;
                else if (lastGamePadState.IsButtonUp(Buttons.LeftThumbstickRight) && currentState.IsButtonDown(Buttons.LeftThumbstickRight))
                    x_zoom = (int)Zoom.zoomIn;
                else if (lastGamePadState.IsButtonUp(Buttons.LeftThumbstickLeft) && currentState.IsButtonDown(Buttons.LeftThumbstickLeft))
                    x_zoom = (int)Zoom.zoomOut;
                else
                {
                    x_zoom = 0;
                    y_zoom = 0;
                }
                #endregion

                //if Button A has been pressed move to the next image and save state accordingly
                if (currentState.Buttons.A == ButtonState.Pressed)
                {
                   //save state and move to the next image
                    NextImage();
                }
            }
            
            lastGamePadState = currentState;
        }

        //update position of the image on the canvas based on Xbox controller state
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
                if (x_zoom != 0 || y_zoom != 0)
                {
                    zoomImage(LeftPos, topPos, leftImage);
                }

                //store the values where the left image should be drawn and what their zoom value should be
                //so that all other subsequent images with the same values
                leftImg_leftPos = LeftPos;
                leftImg_topPos = topPos;
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
                if (x_zoom != 0 || y_zoom != 0)
                {
                    zoomImage(LeftPos, topPos, rightImage);
                }

                //store the values where the left image should be drawn and what their zoom value should be
                //so that all other subsequent images with the same values
                rightImg_leftPos = LeftPos;
                rightImg_topPos = topPos;
            }

        }

        //move the image in 2D
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

        //make image larger or smaller
        void zoomImage(double LeftPos, double topPos, Image image)
        { 
            TransformGroup transformGroup = (TransformGroup)image.RenderTransform;
            ScaleTransform transform = (ScaleTransform)transformGroup.Children[0];

            //scale image and then redraw it at the previous position
            //only scale if scaling factor is greater than zero
            if (transform.ScaleX + zoomValue * x_zoom > 0 || transform.ScaleY + zoomValue * y_zoom > 0)
            {
                transform.ScaleX += zoomValue * x_zoom;
                transform.ScaleY += zoomValue * y_zoom;
                img_zoomVal[0] = transform.ScaleX;
                img_zoomVal[1] = transform.ScaleY;
            }
            
            //after zooming make sure the top left corner of the image doesn't change
            move = -1; 
            moveImage(LeftPos, topPos, image);

            //reset zoom to 0
            x_zoom = 0;
            y_zoom = 0;
        }

        //move to the next image
        void NextImage()
        {
            String uri = images.nextImage();
            leftImage.Source = rightImage.Source = new BitmapImage(new Uri(uri, UriKind.RelativeOrAbsolute));
            
            //draw the image at the place where the user last set the location to
            moveImage(leftImg_leftPos, leftImg_topPos, leftImage);
            moveImage(rightImg_leftPos, rightImg_topPos, rightImage);

            //set default zoom values for images
            //left image
            xformLeft.ScaleX = xformRight.ScaleX = img_zoomVal[0];
            xformLeft.ScaleY = xformRight.ScaleY = img_zoomVal[1];          

            //check if the user has reached the last image 
            //if yes then inform them they are done
            if(uri.Equals(@"LastImage.png"))
            {
                if (MessageBox.Show("Congratulations!! You have successfully finished the study!! :) :)","Viewer",MessageBoxButton.OK) == MessageBoxResult.OK)
                    this.Close();
            }
        }
    }
}
