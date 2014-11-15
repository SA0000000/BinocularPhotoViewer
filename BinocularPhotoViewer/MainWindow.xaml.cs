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
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Win32;

namespace BinocularPhotoViewer
{
    
    public partial class MainWindow : Window
    {
        private static int ImageNum = 0;
        private const int MAX = 18;
        private static String[] filenames;   //= new String[12];
        Image[] myPictureBoxes;
        OpenFileDialog openFileDialog = new OpenFileDialog();
        WrapPanel myWrapPanel;
        


        public MainWindow()
        {
            InitializeComponent();
            filenames = new String[MAX];
            myPictureBoxes = new Image[MAX];
            myWrapPanel = new WrapPanel();
            myWrapPanel.Orientation = Orientation.Horizontal;
            myWrapPanel.HorizontalAlignment = HorizontalAlignment.Left;
            myWrapPanel.VerticalAlignment = VerticalAlignment.Top;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            #region Add images

            if (ImageNum < MAX)       //if maximum number of images have not been added
            {
                //Open the dialog box and if user selected image add it to my list of images
                if (openFileDialog.ShowDialog() == true)
                {
                    ////Read the files
                    // flowLayoutPanel.SuspendLayout();
                    foreach (String file in openFileDialog.FileNames)
                    {
                        //Create an image and add it to the table layout
                        try
                        {
                           
                            ImageNum++;

                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show("Oops! " + ex.Message);
                        }
                    }   //end of foreach loop
                    //flowLayoutPanel.ResumeLayout();
                }
            }
            else       //when maximum number of images have been added
            {
                MessageBox.Show("No more room for images!!!");
            }
            #endregion

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            // Clear the picture.
            for (int k = 0; k < ImageNum - 1; k++)
                myPictureBoxes[k].Source = null;

            ImageNum = 0;
            txtNumImages.Text = "";
            //myWrapPanel.Controls.Clear();

        }

        //to make image fit into the thumbnail
        //static Image FixedSize(Image imgPhoto, int Width, int Height)
        //{
        //    int sourceWidth = imgPhoto.Width;
        //    int sourceHeight = imgPhoto.Height;
        //    int sourceX = 0;
        //    int sourceY = 0;
        //    int destX = 0;
        //    int destY = 0;

        //    float nPercent = 0;
        //    float nPercentW = 0;
        //    float nPercentH = 0;

        //    nPercentW = ((float)Width / (float)sourceWidth);
        //    nPercentH = ((float)Height / (float)sourceHeight);
        //    if (nPercentH < nPercentW)
        //    {
        //        nPercent = nPercentH;
        //        destX = System.Convert.ToInt16((Width -
        //                      (sourceWidth * nPercent)) / 2);
        //    }
        //    else
        //    {
        //        nPercent = nPercentW;
        //        destY = System.Convert.ToInt16((Height -
        //                      (sourceHeight * nPercent)) / 2);
        //    }

        //    int destWidth = (int)(sourceWidth * nPercent);
        //    int destHeight = (int)(sourceHeight * nPercent);

        //    Bitmap bmPhoto = new Bitmap(Width, Height, PixelFormat.Format24bppRgb);
        //    bmPhoto.SetResolution(imgPhoto.HorizontalResolution,
        //                     imgPhoto.VerticalResolution);

        //    Graphics grPhoto = Graphics.FromImage(bmPhoto);
        //    grPhoto.Clear(Color.Black);
        //    grPhoto.InterpolationMode = InterpolationMode.HighQualityBicubic;

        //    grPhoto.DrawImage(imgPhoto,
        //        new Rectangle(destX, destY, destWidth, destHeight),
        //        new Rectangle(sourceX, sourceY, sourceWidth, sourceHeight),
        //        GraphicsUnit.Pixel);

        //    grPhoto.Dispose();
        //    return bmPhoto;
        //}

        private void btnStart_Click(object sender, RoutedEventArgs e)
        {
            Viewer myViewer = new Viewer();
            myViewer.Show();
        }
    }
}
