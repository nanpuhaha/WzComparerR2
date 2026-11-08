using System;

namespace WzComparerR2.Encoders
{
    public class WebpEncoder : FFmpegEncoder
    {
        public static new readonly string DefaultArgumentFormat =
            @$"-y -f rawvideo -pixel_format bgra -s %w*%h -r 1000/%t -i ""%i"" -c:v libwebp_anim -lossless 1 -loop 0 ""%o""";

        public static new readonly string DefaultOutputFileExtension = ".webp";

        public WebpEncoder()
        {
            this.FFmpegArgumentFormat = DefaultArgumentFormat;
            this.OutputFileExtension = DefaultOutputFileExtension;
        }

        public override string Name => nameof(WebpEncoder);

        public override GifEncoderCompatibility Compatibility => new GifEncoderCompatibility()
        {
            IsFixedFrameRate = true,
            MinFrameDelay = 1,
            MaxFrameDelay = int.MaxValue,
            FrameDelayStep = 1,
            AlphaSupportMode = AlphaSupportMode.FullAlpha,
            DefaultExtension = string.IsNullOrEmpty(OutputFileExtension) ? DefaultOutputFileExtension : OutputFileExtension,
            SupportedExtensions = new[] { ".webp" },
        };
    }
}
