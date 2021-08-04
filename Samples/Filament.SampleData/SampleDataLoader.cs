namespace Filament.SampleData
{
    public class SampleDataLoader : AbstractDataLoader
    {
        #region Constants

        private const string Package = "generated/resources/resources.bin";

#if DEBUG
        private const int AiDefaultMatOffset = 0;
        private const int AiDefaultMatSize = 907936;
        private const int BakedColorOffset = 907936;
        private const int BakedColorSize = 111848;
        private const int BakedTextureOffset = 1019784;
        private const int BakedTextureSize = 112805;
        private const int PointSpritesOffset = 1132589;
        private const int PointSpritesSize = 113211;
        private const int AoPreviewOffset = 1245800;
        private const int AoPreviewSize = 114475;
        private const int GroundShadowOffset = 1360275;
        private const int GroundShadowSize = 305484;
        private const int HeightFieldOffset = 1665759;
        private const int HeightFieldSize = 91090;
        private const int ImageOffset = 1829868;
        private const int ImageSize = 73019;
        private const int MirrorOffset = 1829868;
        private const int MirrorSize = 114401;
        private const int SandboxClothOffset = 1944269;
        private const int SandboxClothSize = 880174;
        private const int SandboxLitOffset = 2824443;
        private const int SandboxLitSize = 1092484;
        private const int SandboxLitFadeOffset = 3916927;
        private const int SandboxLitFadeSize = 1067422;
        private const int SandboxLitTransparentOffset = 4984349;
        private const int SandboxLitTransparentSize = 1063397;
        private const int SandboxLitThinRefractionOffset = 6047746;
        private const int SandboxLitThinRefractionSize = 1160010;
        private const int SandboxLitThinRefractionSsrOffset = 7207756;
        private const int SandboxLitThinRefractionSsrSize = 1166699;
        private const int SandboxLitSolidRefractionOffset = 8374455;
        private const int SandboxLitSolidRefractionSize = 1158198;
        private const int SandboxLitSolidRefractionSsrOffset = 9532653;
        private const int SandboxLitSolidRefractionSsrSize = 1164947;
        private const int SandboxSpecGlossOffset = 10697600;
        private const int SandboxSpecGlossSize = 1041334;
        private const int SandboxSubSurfaceOffset = 11738934;
        private const int SandboxSubSurfaceSize = 933411;
        private const int SandboxUnlitOffset = 12672345;
        private const int SandboxUnlitSize = 112922;
        private const int TexturedLitOffset = 12785267;
        private const int TexturedLitSize = 982895;
#elif RELEASE
        private const int AiDefaultMatOffset = 0;
        private const int AiDefaultMatSize = 365090;
        private const int BakedColorOffset = 365090;
        private const int BakedColorSize = 22079;
        private const int BakedTextureOffset = 387169;
        private const int BakedTextureSize = 22648;
        private const int PointSpritesOffset = 409817;
        private const int PointSpritesSize = 21447;
        private const int AoPreviewOffset = 431264;
        private const int AoPreviewSize = 23516;
        private const int GroundShadowOffset = 454780;
        private const int GroundShadowSize = 70763;
        private const int HeightFieldOffset = 525543;
        private const int HeightFieldSize = 13145;
        private const int ImageOffset = 538688;
        private const int ImageSize = 8581;
        private const int MirrorOffset = 547269;
        private const int MirrorSize = 24075;
        private const int SandboxClothOffset = 571344;
        private const int SandboxClothSize = 347387;
        private const int SandboxLitOffset = 918731;
        private const int SandboxLitSize = 484781;
        private const int SandboxLitFadeOffset = 1403512;
        private const int SandboxLitFadeSize = 445430;
        private const int SandboxLitTransparentOffset = 1848942;
        private const int SandboxLitTransparentSize = 442231;
        private const int SandboxLitThinRefractionOffset = 2291173;
        private const int SandboxLitThinRefractionSize = 513926;
        private const int SandboxLitThinRefractionSsrOffset = 2805099;
        private const int SandboxLitThinRefractionSsrSize = 520442;
        private const int SandboxLitSolidRefractionOffset = 3325541;
        private const int SandboxLitSolidRefractionSize = 512383;
        private const int SandboxLitSolidRefractionSsrOffset = 3837924;
        private const int SandboxLitSolidRefractionSsrSize = 516198;
        private const int SandboxSpecGlossOffset = 4354122;
        private const int SandboxSpecGlossSize = 455560;
        private const int SandboxSubSurfaceOffset = 4809682;
        private const int SandboxSubSurfaceSize = 380789;
        private const int SandboxUnlitOffset = 5190471;
        private const int SandboxUnlitSize = 22885;
        private const int TexturedLitOffset = 5213356;
        private const int TexturedLitSize = 428228;
#endif

        #endregion

        #region Methods

        public byte[] LoadAiDefaultMat()
        {
            return Load(Package, AiDefaultMatOffset, AiDefaultMatSize);
        }

        public byte[] LoadBakedColor()
        {
            return Load(Package, BakedColorOffset, BakedColorSize);
        }

        public byte[] LoadBakedTexture()
        {
            return Load(Package, BakedTextureOffset, BakedTextureSize);
        }

        public byte[] LoadPointSprites()
        {
            return Load(Package, PointSpritesOffset, PointSpritesSize);
        }

        public byte[] LoadAoPreview()
        {
            return Load(Package, AoPreviewOffset, AoPreviewSize);
        }

        public byte[] LoadGroundShadow()
        {
            return Load(Package, GroundShadowOffset, GroundShadowSize);
        }

        public byte[] LoadHeightField()
        {
            return Load(Package, HeightFieldOffset, HeightFieldSize);
        }

        public byte[] LoadMirror()
        {
            return Load(Package, MirrorOffset, MirrorSize);
        }

        public byte[] LoadSandboxCloth()
        {
            return Load(Package, SandboxClothOffset, SandboxClothSize);
        }

        public byte[] LoadSandboxLit()
        {
            return Load(Package, SandboxLitOffset, SandboxLitSize);
        }

        public byte[] LoadSandboxLitFade()
        {
            return Load(Package, SandboxLitFadeOffset, SandboxLitFadeSize);
        }

        public byte[] LoadSandboxLitTransparent()
        {
            return Load(Package, SandboxLitTransparentOffset, SandboxLitTransparentSize);
        }

        public byte[] LoadSandboxLitThinRefraction()
        {
            return Load(Package, SandboxLitThinRefractionOffset, SandboxLitThinRefractionSize);
        }

        public byte[] LoadSandboxLitThinRefractionSsr()
        {
            return Load(Package, SandboxLitThinRefractionSsrOffset, SandboxLitThinRefractionSsrSize);
        }

        public byte[] LoadSandboxLitSolidRefraction()
        {
            return Load(Package, SandboxLitSolidRefractionOffset, SandboxLitSolidRefractionSize);
        }

        public byte[] LoadSandboxLitSolidRefractionSsr()
        {
            return Load(Package, SandboxLitSolidRefractionSsrOffset, SandboxLitSolidRefractionSsrSize);
        }

        public byte[] LoadASandboxSpecGloss()
        {
            return Load(Package, SandboxSpecGlossOffset, SandboxSpecGlossSize);
        }

        public byte[] LoadASandboxSubSurface()
        {
            return Load(Package, SandboxSubSurfaceOffset, SandboxSubSurfaceSize);
        }

        public byte[] LoadSandboxUnlit()
        {
            return Load(Package, SandboxUnlitOffset, SandboxUnlitSize);
        }

        public byte[] LoadTexturedLit()
        {
            return Load(Package, TexturedLitOffset, TexturedLitSize);
        }

        #endregion
    }
}
