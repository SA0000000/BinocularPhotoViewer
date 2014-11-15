﻿#pragma checksum "..\..\Viewer.xaml" "{406ea660-64cf-4c82-b6f0-42d48172a799}" "E656DC56117D3E664D384D3C3CF9ABF7"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.34014
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace BinocularPhotoViewer {
    
    
    /// <summary>
    /// Viewer
    /// </summary>
    public partial class Viewer : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 1 "..\..\Viewer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal BinocularPhotoViewer.Viewer Viewer1;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\Viewer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas LeftCanvas;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\Viewer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image leftImage;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\Viewer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Canvas RightCanvas;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\Viewer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image rightImage;
        
        #line default
        #line hidden
        
        
        #line 29 "..\..\Viewer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider leftSliderZoom;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\Viewer.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Slider rightSliderZoom;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/BinocularPhotoViewer;component/viewer.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\Viewer.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            this.Viewer1 = ((BinocularPhotoViewer.Viewer)(target));
            return;
            case 2:
            this.LeftCanvas = ((System.Windows.Controls.Canvas)(target));
            
            #line 17 "..\..\Viewer.xaml"
            this.LeftCanvas.KeyDown += new System.Windows.Input.KeyEventHandler(this.leftCanvas_KeyDown);
            
            #line default
            #line hidden
            
            #line 17 "..\..\Viewer.xaml"
            this.LeftCanvas.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.LeftCanvas_MouseDown);
            
            #line default
            #line hidden
            
            #line 17 "..\..\Viewer.xaml"
            this.LeftCanvas.GotKeyboardFocus += new System.Windows.Input.KeyboardFocusChangedEventHandler(this.LeftCanvas_GotKeyboardFocus);
            
            #line default
            #line hidden
            
            #line 18 "..\..\Viewer.xaml"
            this.LeftCanvas.LostKeyboardFocus += new System.Windows.Input.KeyboardFocusChangedEventHandler(this.LeftCanvas_LostKeyboardFocus);
            
            #line default
            #line hidden
            return;
            case 3:
            this.leftImage = ((System.Windows.Controls.Image)(target));
            return;
            case 4:
            this.RightCanvas = ((System.Windows.Controls.Canvas)(target));
            
            #line 24 "..\..\Viewer.xaml"
            this.RightCanvas.KeyDown += new System.Windows.Input.KeyEventHandler(this.rightCanvas_KeyDown);
            
            #line default
            #line hidden
            
            #line 24 "..\..\Viewer.xaml"
            this.RightCanvas.MouseDown += new System.Windows.Input.MouseButtonEventHandler(this.RightCanvas_MouseDown);
            
            #line default
            #line hidden
            
            #line 24 "..\..\Viewer.xaml"
            this.RightCanvas.GotKeyboardFocus += new System.Windows.Input.KeyboardFocusChangedEventHandler(this.RightCanvas_GotKeyboardFocus);
            
            #line default
            #line hidden
            
            #line 25 "..\..\Viewer.xaml"
            this.RightCanvas.LostKeyboardFocus += new System.Windows.Input.KeyboardFocusChangedEventHandler(this.RightCanvas_LostKeyboardFocus);
            
            #line default
            #line hidden
            return;
            case 5:
            this.rightImage = ((System.Windows.Controls.Image)(target));
            return;
            case 6:
            this.leftSliderZoom = ((System.Windows.Controls.Slider)(target));
            
            #line 29 "..\..\Viewer.xaml"
            this.leftSliderZoom.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.leftSliderZoom_ValueChanged);
            
            #line default
            #line hidden
            return;
            case 7:
            this.rightSliderZoom = ((System.Windows.Controls.Slider)(target));
            
            #line 30 "..\..\Viewer.xaml"
            this.rightSliderZoom.ValueChanged += new System.Windows.RoutedPropertyChangedEventHandler<double>(this.rightSliderZoom_ValueChanged);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
