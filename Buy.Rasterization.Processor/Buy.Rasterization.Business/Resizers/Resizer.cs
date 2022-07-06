using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Buy.Rasterization.Business.Resizers
{
    public sealed class Resizer : IResizer {
        public Task<string> Resize(Stream stream, int size) {
            using (var image = new Bitmap(Image.FromStream(stream))) {
                var imageSize = GetSize(image, size);
                var resizedImage = new Bitmap(imageSize.Width, imageSize.Height);
                using (var graphics = Graphics.FromImage(resizedImage)) {
                    graphics.CompositingQuality = CompositingQuality.HighSpeed;
                    graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
                    graphics.CompositingMode = CompositingMode.SourceCopy;
                    graphics.DrawImage(image, 0, 0, imageSize.Width, imageSize.Height);

                    var tmp = Path.GetTempFileName();
                    using (var fStream = File.Open(tmp, FileMode.Open)) {
                        var quality = Encoder.Quality;
                        var encoderParameters = new EncoderParameters(1);
                        encoderParameters.Param[0] = new EncoderParameter(quality, 75);
                        var codec = ImageCodecInfo.GetImageDecoders()
                            .FirstOrDefault(entry => entry.FormatID == ImageFormat.Png.Guid);
                        resizedImage.Save(fStream, codec, encoderParameters);
                    }

                    return Task.FromResult(tmp);
                }
            }
        }

        private static ImageSize GetSize(Image map, int size) {
            int width, height;
            if (map.Width > map.Height) {
                width = size;
                height = Convert.ToInt32(map.Height * size / (double)map.Width);
            }
            else {
                width = Convert.ToInt32(map.Width * size / (double)map.Height);
                height = size;
            }

            return new ImageSize {
                Height = height,
                Width = width
            };
        }
    }
}