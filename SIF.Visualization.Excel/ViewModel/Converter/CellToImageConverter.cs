using System;
using System.Globalization;
using System.IO;
using System.Windows;
using System.Windows.Data;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using SIF.Visualization.Excel.Core;

namespace SIF.Visualization.Excel.ViewModel
{
    internal class CellToImageConverter : IValueConverter
    {
        private RenderTargetBitmap bmp;
        private DrawingVisual drawingVisual;

        private BitmapImage dynImg;
        private PngBitmapEncoder encoder;
        private bool hasMultiple;
        private BitmapImage pluImg;
        private BitmapImage sanImg;
        private BitmapImage staImg;
        private string tempDir;


        /// <summary>
        ///     Checks a cell for the different kinds of violations contained in it, to display a combined picture of them by
        ///     Converting the single ones to one
        ///     new / big one
        /// </summary>
        /// <param name="values">An Array of the different Violationstypes contained in a cell</param>
        /// <param name="targetType"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            DecideIcons(value);
            CreateFusedImage();

            using (var memory = new MemoryStream())
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
        ///     Convert Back Not Implemented yet
        /// </summary>
        /// <param name="value"></param>
        /// <param name="targetTypes"></param>
        /// <param name="parameter"></param>
        /// <param name="culture"></param>
        /// <returns></returns>
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Decides which Icons should be used. If there exists a violation of a certain type that icon gets added
        /// </summary>
        /// <param name="typeOccurrences"></param>
        private void DecideIcons(object o)
        {
            tempDir = AppDomain.CurrentDomain.BaseDirectory + "..\\..\\Resources\\Icons\\violations\\";
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

            if (o == null) return;
            if (o.GetType() == typeof(Cell))
            {
                var cell = (Cell) o;
                hasMultiple = false;
                var typeOccurrences = new int[3];
                foreach (var vio in cell.VisibleViolations)
                {
                    if (vio.Policy.Type == Policy.PolicyType.DYNAMIC)
                        typeOccurrences[0]++;
                    if (vio.Policy.Type == Policy.PolicyType.STATIC)
                        typeOccurrences[1]++;
                    if (vio.Policy.Type == Policy.PolicyType.SANITY)
                        typeOccurrences[2]++;
                }

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

                if (hasMultiple) pluImg.UriSource = new Uri(tempDir + "plus.png", UriKind.Absolute);
            }

            dynImg.EndInit();
            staImg.EndInit();
            sanImg.EndInit();
            pluImg.EndInit();
        }

        /// <summary>
        ///     Creates the fused image
        /// </summary>
        private void CreateFusedImage()
        {
            // Gets the total size of the image
            var imageWidth = System.Convert.ToInt32(
                dynImg.Width + sanImg.Width + staImg.Width
            );
            var imageHeight = Math.Max(System.Convert.ToInt32(sanImg.Height),
                Math.Max(System.Convert.ToInt32(dynImg.Height), System.Convert.ToInt32(staImg.Height)));

            // Draws the images into a DrawingVisual component
            drawingVisual = new DrawingVisual();
            using (var drawingContext = drawingVisual.RenderOpen())
            {
                drawingContext.DrawImage(dynImg, new Rect(0, 0, dynImg.Width, imageHeight));
                drawingContext.DrawImage(staImg, new Rect(dynImg.Width, 0, staImg.Width, imageHeight));
                drawingContext.DrawImage(sanImg, new Rect(dynImg.Width + staImg.Width, 0, sanImg.Width, imageHeight));
                if (hasMultiple)
                    drawingContext.DrawImage(pluImg,
                        new Rect(imageWidth - pluImg.Width, imageHeight - pluImg.Height, pluImg.Width, pluImg.Height));
            }

            // Converts the Visual (DrawingVisual) into a BitmapSource
            bmp = new RenderTargetBitmap(imageWidth, imageHeight, 96, 96,
                PixelFormats.Pbgra32);
            bmp.Render(drawingVisual);

            // Creates a PngBitmapEncoder and adds the BitmapSource to the frames of the encoder
            encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(bmp));
        }
    }
}