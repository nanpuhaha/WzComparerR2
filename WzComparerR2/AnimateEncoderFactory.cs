using System;
using System.Collections.Generic;
using WzComparerR2.Config;
using WzComparerR2.Encoders;

namespace WzComparerR2
{
    public static class AnimateEncoderFactory
    {
        static AnimateEncoderFactory()
        {
            registeredEncoders = new Dictionary<int, IAnimateEncoderProvider>();
            RegisterEncoders();
        }

        private static Dictionary<int, IAnimateEncoderProvider> registeredEncoders;

        private static void RegisterEncoders()
        {
            registeredEncoders.Add(0, new AnimateEncoderProvider<BuildInApngEncoder>
            {
                ID = 0,
                Name = nameof(BuildInApngEncoder),
                CreateEncoderCallback = () => new BuildInApngEncoder(),
                ConfigureEncoderCallback = (encoder, config) =>
                {
                    encoder.OptimizeEnabled = config.PaletteOptimized;
                }
            });

            registeredEncoders.Add(1, new AnimateEncoderProvider<WebpEncoder>
            {
                ID = 1,
                Name = nameof(WebpEncoder),
                CreateEncoderCallback = () => new WebpEncoder(),
                ConfigureEncoderCallback = (encoder, config) =>
                {
                    encoder.FFmpegBinPath = config.FFmpegBinPath;
                }
            });

            registeredEncoders.Add(2, new AnimateEncoderProvider<WebmVP9Encoder>
            {
                ID = 2,
                Name = nameof(WebmVP9Encoder),
                CreateEncoderCallback = () => new WebmVP9Encoder(),
                ConfigureEncoderCallback = (encoder, config) =>
                {
                    encoder.FFmpegBinPath = config.FFmpegBinPath;
                }
            });

            registeredEncoders.Add(3, new AnimateEncoderProvider<MovProResEncoder>
            {
                ID = 3,
                Name = nameof(MovProResEncoder),
                CreateEncoderCallback = () => new MovProResEncoder(),
                ConfigureEncoderCallback = (encoder, config) =>
                {
                    encoder.FFmpegBinPath = config.FFmpegBinPath;
                }
            });

            registeredEncoders.Add(4, new AnimateEncoderProvider<MovQTRLEEncoder>
            {
                ID = 4,
                Name = nameof(MovQTRLEEncoder),
                CreateEncoderCallback = () => new MovQTRLEEncoder(),
                ConfigureEncoderCallback = (encoder, config) =>
                {
                    encoder.FFmpegBinPath = config.FFmpegBinPath;
                }
            });

            registeredEncoders.Add(5, new AnimateEncoderProvider<FFmpegEncoder>
            {
                ID = 5,
                Name = nameof(FFmpegEncoder),
                CreateEncoderCallback = () => new FFmpegEncoder(),
                ConfigureEncoderCallback = (encoder, config) =>
                {
                    encoder.FFmpegBinPath = config.FFmpegBinPath;
                    encoder.FFmpegArgumentFormat = config.FFmpegArgument;
                    encoder.OutputFileExtension = config.FFmpegOutputFileExtension;
                }
            });
        }

        public static GifEncoder CreateEncoder(ImageHandlerConfig config)
        {
            return CreateEncoder(config.GifEncoder, config);
        }

        public static GifEncoder CreateEncoder(int id, ImageHandlerConfig config)
        {
            if (!registeredEncoders.TryGetValue(id, out var provider))
            {
                if (!registeredEncoders.TryGetValue(0, out provider))
                {
                    throw new Exception($"Encoder ID {id} has not registered");
                }
            }

            var encoder = provider.CreateEncoder();
            provider.ConfigureEncoder(encoder, config);
            return encoder;
        }

        public interface IAnimateEncoderProvider
        {
            GifEncoder CreateEncoder();
            void ConfigureEncoder(GifEncoder encoder, ImageHandlerConfig config);
        }

        public class AnimateEncoderProvider<T> : IAnimateEncoderProvider where T : GifEncoder
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public Func<T> CreateEncoderCallback { get; set; }
            public Action<T, ImageHandlerConfig> ConfigureEncoderCallback { get; set; }

            public GifEncoder CreateEncoder()
            {
                if (this.CreateEncoderCallback == null)
                {
                    throw new ArgumentNullException(nameof(CreateEncoderCallback));
                }

                return this.CreateEncoderCallback();
            }

            public void ConfigureEncoder(GifEncoder encoder, ImageHandlerConfig config)
            {
                if (this.ConfigureEncoderCallback != null)
                {
                    this.ConfigureEncoderCallback((T)encoder, config);
                }
            }
        }
    }
}
