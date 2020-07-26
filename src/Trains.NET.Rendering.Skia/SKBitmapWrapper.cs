using System;
using SkiaSharp;

namespace Trains.NET.Rendering.Skia
{
    internal class SKBitmapWrapper : IBitmap, IDisposable
    {
        //private readonly SKBitmap _bitmap;

        internal void CloseCanvas()
        {
            _image = _surface.Snapshot();
            //_picture = _recorder?.EndRecording();
            _canvas?.Dispose();
            _canvas = null;
            _surface?.Dispose();
            _surface = null;
            //_recorder?.Dispose();
            //_recorder = null;
        }

        private SKSurface _surface;
        private SKCanvas? _canvas;
        private SKImage _image;
        private readonly SKImageInfo _info;

        public SKBitmapWrapper(int width, int height)
        {
            //_bitmap = new SKBitmap(width, height, SKImageInfo.PlatformColorType, SKAlphaType.Premul);

            _info = new SKImageInfo(width, height, SKImageInfo.PlatformColorType, SKAlphaType.Premul);

        }

        public SKImage? Image => _image;

        //public SKBitmap Bitmap => _bitmap;

        public SKCanvas Canvas
        {
            get
            {
                if (_surface == null)
                {
                    _surface = SKSurface.Create(_info);
                    _canvas = _surface.Canvas;
                }
                return _canvas!;
            }
        }

        public void Dispose()
        {
         //   _bitmap?.Dispose();
            _canvas?.Dispose();
            _surface?.Dispose();
            _image?.Dispose();
        }
    }
}
