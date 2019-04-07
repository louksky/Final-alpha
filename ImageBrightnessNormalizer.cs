using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace comparlize
{
    class ImageBrightnessNormalizer
    {
        private const float MIN_BRIGHTNESS = 0;
        private const float MAX_BRIGHTNESS = 1;

        public static Bitmap NormalizeImageBrightness(Bitmap image)
        {
            float minBrightness = MAX_BRIGHTNESS;
            float maxBrightness = MIN_BRIGHTNESS;

            /* Get the brightness range of the image. */
            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    float pixelBrightness = image.GetPixel(x, y).GetBrightness();
                    minBrightness = Math.Min(minBrightness, pixelBrightness);
                    maxBrightness = Math.Max(maxBrightness, pixelBrightness);
                }
            }

            /* Normalize the image brightness. */
            Color pixelColor;
            float normalizedPixelBrightness;
            Color normalizedPixelColor;

            for (int x = 0; x < image.Width; x++)
            {
                for (int y = 0; y < image.Height; y++)
                {
                    pixelColor = image.GetPixel(x, y);
                    normalizedPixelBrightness = (pixelColor.GetBrightness() - minBrightness) / (maxBrightness - minBrightness);
                    normalizedPixelColor = ColorConverter.ColorFromAhsb(pixelColor.A, pixelColor.GetHue(), pixelColor.GetSaturation(), normalizedPixelBrightness);
                    image.SetPixel(x, y, normalizedPixelColor);
                }
            }
            return image;
        }
    }
}
