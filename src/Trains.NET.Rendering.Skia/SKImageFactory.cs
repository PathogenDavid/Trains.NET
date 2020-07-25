using SkiaSharp;

namespace Trains.NET.Rendering.Skia
{
    internal class SKImageFactory : IImageFactory
    {
        public IImageCanvas CreateImageCanvas(int width, int height)
        {
            return new ImageCanvas(width, height);
        }

        private class ImageCanvas : IImageCanvas
        {
            private readonly SKSurface _surface;

            public ImageCanvas(int width, int height)
            {
                var imageInfo = new SKImageInfo(width, height, SKImageInfo.PlatformColorType, SKAlphaType.Premul);
                _surface = SKSurface.Create(imageInfo);
            }

            public ICanvas Canvas
            {
                get
                {
                    return new SKCanvasWrapper(_surface.Canvas);
                }
            }

            public void Dispose()
            {
                _surface.Dispose();
            }

            public IImage Render()
            {
                return new SKImageWrapper(_surface.Snapshot());
            }
        }
    }
}
