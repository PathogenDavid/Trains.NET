using SkiaSharp;

namespace Trains.NET.Rendering.Skia
{
    internal class SKBitmapFactory : IBitmapFactory
    {
        public void CloseCanvas(IBitmap bitmap)
        {
            ((SKBitmapWrapper)bitmap).CloseCanvas();
        }

        public IBitmap CreateBitmap(int width, int height) => new SKBitmapWrapper(width, height);

        public ICanvas CreateCanvas(IBitmap bitmap) => new SKCanvasWrapper(((SKBitmapWrapper)bitmap).Canvas);
    }
}
