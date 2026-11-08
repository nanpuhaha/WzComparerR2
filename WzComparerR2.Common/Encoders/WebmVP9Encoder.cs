using System;

namespace WzComparerR2.Encoders
{
    public class WebmVP9Encoder : FFmpegEncoder
    {
        public static new readonly string DefaultArgumentFormat =
            @$"-y -f rawvideo -pixel_format bgra -s %w*%h -r 1000/%t -i ""%i"" -c:v libvpx-vp9 -pix_fmt yuva420p ""%o""";

        public static new readonly string DefaultOutputFileExtension = ".webm";

        public WebmVP9Encoder()
        {
            this.FFmpegArgumentFormat = DefaultArgumentFormat;
            this.OutputFileExtension = DefaultOutputFileExtension;
        }

        public override string Name => nameof(WebmVP9Encoder);

        public override GifEncoderCompatibility Compatibility => new GifEncoderCompatibility()
        {
            IsFixedFrameRate = true,
            MinFrameDelay = 1,
            MaxFrameDelay = int.MaxValue,
            FrameDelayStep = 1,
            AlphaSupportMode = AlphaSupportMode.FullAlpha,
            DefaultExtension = string.IsNullOrEmpty(OutputFileExtension) ? DefaultOutputFileExtension : OutputFileExtension,
            SupportedExtensions = new[] { ".webm" },
        };
    }
}
