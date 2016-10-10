using SIF.Visualization.Excel.Core;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using System.Resources;
using System.Windows;
using System.Windows.Data;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SIF.Visualization.Excel.Properties;

namespace SIF.Visualization.Excel.ViewModel
{
    class MultipleTOImageConverter : IMultiValueConverter
    {
        private BitmapImage dynImg;
        private BitmapImage staImg;
        private BitmapImage sanImg;
        private BitmapImage pluImg;
        private String tempDir;
        private RenderTargetBitmap bmp;
        private DrawingVisual drawingVisual;
        private PngBitmapEncoder encoder;


        /// <summary>
        /// Checks a cell for the different kinds of violations contained in it, to display a combined picture of them by Converting the single ones to one 
        /// new / big one
        /// </summary>
        /// <param name="values">An Array of the different Violationstypes contained in a cell</param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object[] values, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
                InitializeImages();
                DecideIcons((int[])values[0]);
                
                dynImg.EndInit();
                staImg.EndInit();
                sanImg.EndInit();
                pluImg.EndInit();

                CreateFusedImage();
                
                using (MemoryStream memory = new MemoryStream())
                {
                    encoder.Save(memory);
                    memory.Position = 0;

                    var bitmapImage = new BitmapImage();
                    bitmapImage.BeginInit();
                    bitmapImage.StreamSource = memory;
                    bitmapImage.CacheOption = BitmapCacheOption.OnLoad;
                    bitmapImage.EndInit();
                    return bitmapImage;
                }

            
        }

        /// <summary>
        /// Decides which Icons should be used. If there exists a violation of a certain type that icon gets added
        /// </summary>
        /// <param name="typeOccurrences"></param>
        private void DecideIcons(int[] typeOccurrences)
        {
            bool hasMultiple = false;
            if (typeOccurrences[0] > 1)
            {
                dynImg.UriSource = new Uri(tempDir + "dynamic.png", UriKind.Absolute);
                hasMultiple = true;
            }
            else if (typeOccurrences[0] == 1)
            {
                dynImg.UriSource = new Uri(tempDir + "dynamic.png", UriKind.Absolute);
            }
            if (typeOccurrences[1] > 1)
            {
                staImg.UriSource = new Uri(tempDir + "static.png", UriKind.Absolute);
                hasMultiple = true;
            }
            else if (typeOccurrences[1] == 1)
            {
                staImg.UriSource = new Uri(tempDir + "static.png", UriKind.Absolute);
            }
            if (typeOccurrences[2] > 1)
            {
                sanImg.UriSource = new Uri(tempDir + "sanity.png", UriKind.Absolute);
                hasMultiple = true;
            }
            else if (typeOccurrences[2] == 1)
            {
                sanImg.UriSource = new Uri(tempDir + "sanity.png", UriKind.Absolute);
            }

            if (hasMultiple)
            {
                pluImg.UriSource = new Uri(tempDir + "plus.png", UriKind.Absolute);
            }

        }

        /// <summary>
        /// Creates the fused image
        /// </summary>
        private void CreateFusedImage()
        {
            // Gets the total size of the image
            int imageWidth = System.Convert.ToInt32(
                dynImg.Width + sanImg.Width + staImg.Width
                );
            int imageHeight = Math.Max(System.Convert.ToInt32(sanImg.Height), Math.Max(System.Convert.ToInt32(dynImg.Height), System.Convert.ToInt32(staImg.Height)));

            // Draws the images into a DrawingVisual component
            drawingVisual = new DrawingVisual();
            using (DrawingContext drawingContext = drawingVisual.RenderOpen())
            {
                drawingContext.DrawImage(dynImg, new Rect(0, 0, dynImg.Width, imageHeight));
                drawingContext.DrawImage(staImg, new Rect(dynImg.Width, 0, staImg.Width, imageHeight));
                drawingContext.DrawImage(sanImg, new Rect(dynImg.Width + staImg.Width, 0, sanImg.Width, imageHeight));
                //drawingContext.DrawImage(pluImg, new Rect((imageWidth / 2)- (pluImg.Width / 2), imageHeight, pluImg.Width, pluImg.Height));
            }

            // Converts the Visual (DrawingVisual) into a BitmapSource
            bmp = new RenderTargetBitmap(imageWidth, imageHeight, 96, 96,
               PixelFormats.Pbgra32);
            bmp.Render(drawingVisual);

            // Creates a PngBitmapEncoder and adds the BitmapSource to the frames of the encoder
            encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bmp));
        }

        /// <summary>
        /// Initializes the required images
        /// </summary>
        private void InitializeImages()
        {
            tempDir = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\icons\\violations\\";
             dynImg = new BitmapImage();
             staImg = new BitmapImage();
             sanImg = new BitmapImage();
             pluImg = new BitmapImage();

            dynImg.BeginInit();
            staImg.BeginInit();
            sanImg.BeginInit();
            pluImg.BeginInit();


            dynImg.UriSource = new Uri(tempDir + "empty.png", UriKind.Absolute);
            staImg.UriSource = new Uri(tempDir + "empty.png", UriKind.Absolute);
            sanImg.UriSource = new Uri(tempDir + "empty.png", UriKind.Absolute);
            pluImg.UriSource = new Uri(tempDir + "empty.png", UriKind.Absolute);
        
        }

        /// <summary>
        /// Convert Back Not Implemented yet
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetTypes"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object[] ConvertBack(object value, Type[] targetTypes, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
        }


    }
}
