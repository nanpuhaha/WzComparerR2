using System;

namespace WzComparerR2.Encoders
{
    public class MovProResEncoder : FFmpegEncoder
    {
        public static new readonly string DefaultArgumentFormat =
            @$"-y -f rawvideo -pixel_format bgra -s %w*%h -r 1000/%t -i ""%i"" -c:v prores_ks -profile:v 4 -pix_fmt yuva444p10le ""%o""";

        public static new readonly string DefaultOutputFileExtension = ".mov";

        public MovProResEncoder()
        {
            this.FFmpegArgumentFormat = DefaultArgumentFormat;
            this.OutputFileExtension = DefaultOutputFileExtension;
        }

        public override string Name => nameof(MovProResEncoder);

        public override GifEncoderCompatibility Compatibility => new GifEncoderCompatibility()
        {
            IsFixedFrameRate = true,
            MinFrameDelay = 1,
            MaxFrameDelay = int.MaxValue,
            FrameDelayStep = 1,
            AlphaSupportMode = AlphaSupportMode.FullAlpha,
            DefaultExtension = string.IsNullOrEmpty(OutputFileExtension) ? DefaultOutputFileExtension : OutputFileExtension,
            SupportedExtensions = new[] { ".mov" },
        };
    }
}
