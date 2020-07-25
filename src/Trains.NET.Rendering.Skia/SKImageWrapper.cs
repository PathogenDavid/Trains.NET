using SkiaSharp;

namespace Trains.NET.Rendering.Skia
{
    internal class SKImageWrapper : IImage
    {
        private readonly SKImage _image;

        public SKImageWrapper(SKImage image)
        {
            _image = image;
        }

        public SKImage Image => _image;

        public void Dispose()
        {
            _image.Dispose();
        }
    }
}
