using System.Runtime.InteropServices;

namespace Filament
{
    public readonly struct Color
    {
        public readonly float R;
        public readonly float G;
        public readonly float B;
        public readonly float A;

        #region Methods

        public Color(float r, float g, float b, float a = 0f)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }

        public static LinearColor ToLinearAccurate(sRGBColor color)
        {
            return NativeToLinearAccurate(color);
        }

        /// <summary>
        /// Converts a correlated color temperature to a linear RGB color in sRGB space the temperature must be
        /// expressed in kelvin and must be in the range 1,000K to 15,000K.
        /// </summary>
        public static LinearColor FromCorrelatedColorTemperature(float k)
        {
            return NativeCorrelatedColorTemperature(k);
        }

        /// <summary>
        /// Converts a CIE standard illuminant series D to a linear RGB color in sRGB space the temperature must be
        /// expressed in kelvin and must be in the range 4,000K to 25,000K.
        /// </summary>
        public static LinearColor FromIlluminantD(float k)
        {
            return NativeIlluminantD(k);
        }

        #endregion

        #region P/Invoke

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Color_ToLinearAccurate")]
        private static extern LinearColor NativeToLinearAccurate(sRGBColor color);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Color_cct")]
        private static extern LinearColor NativeCorrelatedColorTemperature(float k);

        [DllImport("libfilament-dotnet", EntryPoint = "filament_Color_illuminantD")]
        private static extern LinearColor NativeIlluminantD(float k);

        #endregion
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct LinearColor
    {
        public float R;
        public float G;
        public float B;

        public LinearColor(float r, float g, float b)
        {
            R = r;
            G = g;
            B = b;
        }
    }

    public struct LinearColorA
    {
        public float R;
        public float G;
        public float B;
        public float A;

        public LinearColorA(float r, float g, float b, float a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct sRGBColor
    {
        public float R;
        public float G;
        public float B;

        public sRGBColor(float r, float g, float b)
        {
            R = r;
            G = g;
            B = b;
        }
    }

    public struct sRGBAColor
    {
        public float R;
        public float G;
        public float B;
        public float A;

        public sRGBAColor(float r, float g, float b, float a)
        {
            R = r;
            G = g;
            B = b;
            A = a;
        }
    }


    public enum RgbType : byte
    {
        /// <summary>sRGB space</summary>
        sRgb,

        /// <summary>Linear space</summary>
        Linear,
    }

    public enum RgbaType : byte
    {
        /// <summary>
        /// The color is defined in sRGB space and the RGB values have not been pre-multiplied by the alpha (for instance,
        /// a 50% transparent red is <1,0,0,0.5>)
        /// </summary>
        sRgb,

        /// <summary>
        /// The color is defined in linear space and the RGB values have not been pre-multiplied by the alpha (for instance,
        /// a 50% transparent red is <1,0,0,0.5>)
        /// <summary>
        Linear,

        /// <summary>
        /// The color is defined in sRGB space and the RGB values have been pre-multiplied by the alpha (for instance, a 50%
        /// transparent red is <0.5,0,0,0.5>)
        /// <summary>
        PremultipliedSrgb,

        /// <summary>
        /// The color is defined in linear space and the RGB values have been pre-multiplied by the alpha (for instance, a
        /// 50% transparent red is <0.5,0,0,0.5>)
        /// <summary>
        PremultipliedLinear
    }
}
