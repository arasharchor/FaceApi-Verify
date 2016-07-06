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
using Microsoft.ProjectOxford.Face;
using Microsoft.ProjectOxford.Face.Contract;
using System.IO;
using System.Configuration;

namespace FaceApi_Verify
{
    public partial class MainWindow : Window
    {
        private readonly IFaceServiceClient _faceserviceclient = new FaceServiceClient(ConfigurationManager.AppSettings["APIKey"]);
        BitmapImage _faceimage1;
        BitmapImage _faceimage2;

        public MainWindow()
        {
            InitializeComponent();

            cmbImages1.SelectedIndex = 0;
            cmbImages2.SelectedIndex = 1;
        }

        private async void btnFacialVerification_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Guid faceid1;
                Guid faceid2;

                ComboBoxItem cbi1 = (ComboBoxItem)cmbImages1.SelectedItem;
                ComboBoxItem cbi2 = (ComboBoxItem)cmbImages2.SelectedItem;

                // Detect the face in each image - need the FaceId for each
                using (Stream faceimagestream = GetStreamFromUrl(cbi1.Tag.ToString()))
                {
                    var faces = await _faceserviceclient.DetectAsync(faceimagestream, returnFaceId: true);
                    if (faces.Length > 0)
                        faceid1 = faces[0].FaceId;
                    else
                        throw new Exception("No face found in image 1.");
                }
                using (Stream faceimagestream = GetStreamFromUrl(cbi2.Tag.ToString()))
                {
                    var faces = await _faceserviceclient.DetectAsync(faceimagestream, returnFaceId: true);
                    if (faces.Length > 0)
                        faceid2 = faces[0].FaceId;
                    else
                        throw new Exception("No face found in image 2.");
                }

                // Verify the faces
                var result = await _faceserviceclient.VerifyAsync(faceid1, faceid2);

                txtInfo.Text = "Match Confidence: " + result.Confidence.ToString() + Environment.NewLine;
                txtInfo.Text += "Identical Images: " + result.IsIdentical.ToString() + Environment.NewLine;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private static Stream GetStreamFromUrl(string url)
        {
            byte[] imagedata = null;

            using (var wc = new System.Net.WebClient())
            {
                imagedata = wc.DownloadData(url);
            }

            return new MemoryStream(imagedata);
        }

        private void cmbImages1_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                // Load the image
                _faceimage1 = new BitmapImage();
                _faceimage1.BeginInit();
                ComboBoxItem cbi = (ComboBoxItem)cmbImages1.SelectedItem;
                _faceimage1.UriSource = new Uri(cbi.Tag.ToString(), UriKind.RelativeOrAbsolute);
                _faceimage1.EndInit();
                imgFace1.Source = _faceimage1;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void cmbImages2_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                // Load the image
                _faceimage2 = new BitmapImage();
                _faceimage2.BeginInit();
                ComboBoxItem cbi = (ComboBoxItem)cmbImages2.SelectedItem;
                _faceimage2.UriSource = new Uri(cbi.Tag.ToString(), UriKind.RelativeOrAbsolute);
                _faceimage2.EndInit();
                imgFace2.Source = _faceimage2;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

    }
}

