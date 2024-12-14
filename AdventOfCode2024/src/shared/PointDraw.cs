using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace AdventOfCode2024
{
    public class PointDrawer
    {
        /// <summary>
        /// Draws a collection of points to an image file
        /// </summary>
        /// <param name="points">Collection of points to draw</param>
        /// <param name="imageWidth">Width of the image</param>
        /// <param name="imageHeight">Height of the image</param>
        /// <param name="outputPath">Path to save the image file</param>
        /// <param name="backgroundColor">Background color of the image (optional)</param>
        /// <param name="pointColor">Color of the points (optional)</param>
        /// <param name="pointSize">Size of the points (optional)</param>
        public static void DrawPointsToImage(
            IEnumerable<Point> points,
            int imageWidth,
            int imageHeight,
            string outputPath,
            Color? backgroundColor = null,
            Color? pointColor = null,
            int pointSize = 4)
        {
            // Use default colors if not provided
            backgroundColor ??= Color.White;
            pointColor ??= Color.Black;

            // Create a new bitmap
            using (Bitmap bitmap = new Bitmap(imageWidth * pointSize, imageHeight* pointSize))
            {
                // Create graphics from the bitmap
                using (Graphics graphics = Graphics.FromImage(bitmap))
                {
                    // Clear the background
                    graphics.Clear(backgroundColor.Value);

                    // Create a solid brush for drawing points
                    using (SolidBrush brush = new SolidBrush(pointColor.Value))
                    {
                        // Draw each point
                        foreach (PointF point in points)
                        {
                            // Ensure point is within image bounds
                            if (point.X >= 0 && point.X < imageWidth &&
                                point.Y >= 0 && point.Y < imageHeight)
                            {
                                // Draw a filled ellipse for each point
                                graphics.FillEllipse(brush,
                                                     point.X * pointSize,
                                                     point.Y * pointSize,
                                                     pointSize,
                                                     pointSize);
                            }
                        }
                    }
                }

                // Save the bitmap
                bitmap.Save(outputPath, ImageFormat.Png);
            }
        }
    }
}