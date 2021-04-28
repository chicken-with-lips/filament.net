namespace Filament.SampleData
{
    public class SampleDataLoader : AbstractDataLoader
    {
        #region Constants

        private const string Package = "generated/resources/resources.bin";

#if DEBUG
        private const int AiDefaultMatOffset = 0;
        private const int AiDefaultMatSize = 568717;
        private const int BakedColorOffset = 568717;
        private const int BakedColorSize = 78394;
        private const int BakedTextureOffset = 647111;
        private const int BakedTextureSize = 79137;
        private const int PointSpritesOffset = 726248;
        private const int PointSpritesSize = 79227;
        private const int AoPreviewOffset = 805475;
        private const int AoPreviewSize = 80139;
        private const int GroundShadowOffset = 885614;
        private const int GroundShadowSize = 237188;
        private const int HeightFieldOffset = 1122802;
        private const int HeightFieldSize = 69583;
        private const int MirrorOffset = 1192385;
        private const int MirrorSize = 80354;
        private const int SandboxClothOffset = 1272739;
        private const int SandboxClothSize = 554328;
        private const int SandboxLitOffset = 1827067;
        private const int SandboxLitSize = 657467;
        private const int SandboxLitFadeOffset = 2484534;
        private const int SandboxLitFadeSize = 637850;
        private const int SandboxLitTransparentOffset = 3122384;
        private const int SandboxLitTransparentSize = 635341;
        private const int SandboxLitThinRefractionOffset = 3757725;
        private const int SandboxLitThinRefractionSize = 679659;
        private const int SandboxLitThinRefractionSsrOffset = 4437384;
        private const int SandboxLitThinRefractionSsrSize = 684748;
        private const int SandboxLitSolidRefractionOffset = 5122132;
        private const int SandboxLitSolidRefractionSize = 678023;
        private const int SandboxLitSolidRefractionSsrOffset = 5800155;
        private const int SandboxLitSolidRefractionSsrSize = 683092;
        private const int SandboxSpecGlossOffset = 6483247;
        private const int SandboxSpecGlossSize = 634491;
        private const int SandboxSubSurfaceOffset = 7117738;
        private const int SandboxSubSurfaceSize = 580764;
        private const int SandboxUnlitOffset = 7698502;
        private const int SandboxUnlitSize = 78982;
        private const int TexturedLitOffset = 7777484;
        private const int TexturedLitSize = 610241;
#elif RELEASE
        private const int AiDefaultMatOffset = 0;
        private const int AiDefaultMatSize = 386111;
        private const int BakedColorOffset = 386111;
        private const int BakedColorSize = 21975;
        private const int BakedTextureOffset = 408086;
        private const int BakedTextureSize = 22543;
        private const int PointSpritesOffset = 430629;
        private const int PointSpritesSize = 21343;
        private const int AoPreviewOffset = 451972;
        private const int AoPreviewSize = 23412;
        private const int GroundShadowOffset = 475384;
        private const int GroundShadowSize = 114378;
        private const int HeightFieldOffset = 589762;
        private const int HeightFieldSize = 13113;
        private const int MirrorOffset = 602875;
        private const int MirrorSize = 23953;
        private const int SandboxClothOffset = 626828;
        private const int SandboxClothSize = 372911;
        private const int SandboxLitOffset = 999739;
        private const int SandboxLitSize = 512233;
        private const int SandboxLitFadeOffset = 1511972;
        private const int SandboxLitFadeSize = 471861;
        private const int SandboxLitTransparentOffset = 1983833;
        private const int SandboxLitTransparentSize = 468480;
        private const int SandboxLitThinRefractionOffset = 2452313;
        private const int SandboxLitThinRefractionSize = 539739;
        private const int SandboxLitThinRefractionSsrOffset = 2992052;
        private const int SandboxLitThinRefractionSsrSize = 546242;
        private const int SandboxLitSolidRefractionOffset = 3538294;
        private const int SandboxLitSolidRefractionSize = 535181;
        private const int SandboxLitSolidRefractionSsrOffset = 4073475;
        private const int SandboxLitSolidRefractionSsrSize = 539101;
        private const int SandboxSpecGlossOffset = 4612576;
        private const int SandboxSpecGlossSize = 482475;
        private const int SandboxSubSurfaceOffset = 5095051;
        private const int SandboxSubSurfaceSize = 406499;
        private const int SandboxUnlitOffset = 5501550;
        private const int SandboxUnlitSize = 22763;
        private const int TexturedLitOffset = 5524313;
        private const int TexturedLitSize = 455799;
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
