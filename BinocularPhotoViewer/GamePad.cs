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
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Windows.Threading;
using SharpDX.XInput;

namespace BinocularPhotoViewer
{
    class GamePad
    {
        DispatcherTimer _timer = new DispatcherTimer();
        private string _leftAxis;
        private string _rightAxis;
        private string _buttons;
        private Controller _controller;

        public GamePad()
        {
            // DataContext = this;
             GamePad_Load();
             //Closing += MainWindow_Closing;
             
        }
        

        public void DisplayControllerInformation()
        {
             var state = _controller.GetState();
             LeftAxis = string.Format("X: {0} Y: {1}", state.Gamepad.LeftThumbX, state.Gamepad.LeftThumbY);
             RightAxis = string.Format("X: {0} Y: {1}", state.Gamepad.RightThumbX, state.Gamepad.RightThumbX);
             //Buttons = string.Format("A: {0} B: {1} X: {2} Y: {3}", state.Gamepad.Buttons.ToString(), state.Gamepad.LeftThumbY);
             Buttons = string.Format("{0}", state.Gamepad.Buttons);
        }

        void GamePad_Exit(object sender, CancelEventArgs e)
        {
             _controller = null;
        }

        void GamePad_Load()
        {
             _controller = new Controller(UserIndex.One);
             if (_controller.IsConnected) return;
             MessageBox.Show("Gameroller is not connected ... you know ;)");
             App.Current.Shutdown();
        }

        #region Properties

        public string LeftAxis
        {
             get
             {
                 return _leftAxis;
             }
             set
             {
                if (value == _leftAxis) return;
                _leftAxis = value;
                OnPropertyChanged();
             }
        }

        public string RightAxis
        {
            get
            {
               return _rightAxis;
            }
            set
            {
              if (value == _rightAxis) return;
              _rightAxis = value;
              OnPropertyChanged();
           }
       }

       public string Buttons
       {
            get
            {
                return _buttons;
            }
            set
            {
               if (value == _buttons) return;
               _buttons = value;
                OnPropertyChanged();
            }
       }

       public event PropertyChangedEventHandler PropertyChanged;

       protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
       {
           var handler = PropertyChanged;
          if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
       }

       #endregion          
    }
}
