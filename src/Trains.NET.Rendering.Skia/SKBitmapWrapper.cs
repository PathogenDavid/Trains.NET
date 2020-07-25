using System;
using SkiaSharp;

namespace Trains.NET.Rendering.Skia
{
    internal class SKBitmapWrapper : IBitmap, IDisposable
    {
        //private readonly SKBitmap _bitmap;
        private SKPictureRecorder? _recorder;

        internal void CloseCanvas()
        {
            _picture = _recorder?.EndRecording();
            _canvas?.Dispose();
            _canvas = null;
            _recorder?.Dispose();
            _recorder = null;
        }

        private SKCanvas? _canvas;
        private SKPicture? _picture;
        private readonly int _width;
        private readonly int _height;

        public SKBitmapWrapper(int width, int height)
        {
            //_bitmap = new SKBitmap(width, height, SKImageInfo.PlatformColorType, SKAlphaType.Premul);

            _width = width;
            _height = height;
        }

        public SKPicture? Picutre => _picture;

        //public SKBitmap Bitmap => _bitmap;

        public SKCanvas Canvas
        {
            get
            {
                if (_recorder == null)
                {
                    _recorder = new SKPictureRecorder();
                    _canvas = _recorder.BeginRecording(new SKRect(0, 0, _width, _height));
                }
                return _canvas!;
            }
        }

        public void Dispose()
        {
         //   _bitmap?.Dispose();
            _canvas?.Dispose();
            _recorder?.Dispose();
            _picture?.Dispose();
        }
    }
}
