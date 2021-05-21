namespace Filament.SampleData
{
    public class SampleDataLoader : AbstractDataLoader
    {
        #region Constants

        private const string Package = "generated/resources/resources.bin";

#if DEBUG
        private const int AiDefaultMatOffset = 0;
        private const int AiDefaultMatSize = 569882;
        private const int BakedColorOffset = 569882;
        private const int BakedColorSize = 78972;
        private const int BakedTextureOffset = 648854;
        private const int BakedTextureSize = 79715;
        private const int PointSpritesOffset = 728569;
        private const int PointSpritesSize = 79805;
        private const int AoPreviewOffset = 808374;
        private const int AoPreviewSize = 80717;
        private const int GroundShadowOffset = 889091;
        private const int GroundShadowSize = 242587;
        private const int HeightFieldOffset = 1131678;
        private const int HeightFieldSize = 70125;
        private const int MirrorOffset = 1201803;
        private const int MirrorSize = 80932;
        private const int SandboxClothOffset = 1282735;
        private const int SandboxClothSize = 555499;
        private const int SandboxLitOffset = 1838234;
        private const int SandboxLitSize = 658730;
        private const int SandboxLitFadeOffset = 2496964;
        private const int SandboxLitFadeSize = 639113;
        private const int SandboxLitTransparentOffset = 3136077;
        private const int SandboxLitTransparentSize = 636604;
        private const int SandboxLitThinRefractionOffset = 3772681;
        private const int SandboxLitThinRefractionSize = 680924;
        private const int SandboxLitThinRefractionSsrOffset = 4453605;
        private const int SandboxLitThinRefractionSsrSize = 686013;
        private const int SandboxLitSolidRefractionOffset = 5139618;
        private const int SandboxLitSolidRefractionSize = 679288;
        private const int SandboxLitSolidRefractionSsrOffset = 5818906;
        private const int SandboxLitSolidRefractionSsrSize = 684357;
        private const int SandboxSpecGlossOffset = 6503263;
        private const int SandboxSpecGlossSize = 635754;
        private const int SandboxSubSurfaceOffset = 7139017;
        private const int SandboxSubSurfaceSize = 582037;
        private const int SandboxUnlitOffset = 7721054;
        private const int SandboxUnlitSize = 79560;
        private const int TexturedLitOffset = 7800614;
        private const int TexturedLitSize = 611504;
#elif RELEASE
        private const int AiDefaultMatOffset = 0;
        private const int AiDefaultMatSize = 379293;
        private const int BakedColorOffset = 379293;
        private const int BakedColorSize = 21975;
        private const int BakedTextureOffset = 401268;
        private const int BakedTextureSize = 22543;
        private const int PointSpritesOffset = 423811;
        private const int PointSpritesSize = 21343;
        private const int AoPreviewOffset = 445154;
        private const int AoPreviewSize = 23412;
        private const int GroundShadowOffset = 468566;
        private const int GroundShadowSize = 123248;
        private const int HeightFieldOffset = 591814;
        private const int HeightFieldSize = 13113;
        private const int MirrorOffset = 604927;
        private const int MirrorSize = 23953;
        private const int SandboxClothOffset = 628880;
        private const int SandboxClothSize = 365435;
        private const int SandboxLitOffset = 994315;
        private const int SandboxLitSize = 507526;
        private const int SandboxLitFadeOffset = 1501841;
        private const int SandboxLitFadeSize = 465538;
        private const int SandboxLitTransparentOffset = 1967379;
        private const int SandboxLitTransparentSize = 462531;
        private const int SandboxLitThinRefractionOffset = 2429910;
        private const int SandboxLitThinRefractionSize = 533389;
        private const int SandboxLitThinRefractionSsrOffset = 2963299;
        private const int SandboxLitThinRefractionSsrSize = 539906;
        private const int SandboxLitSolidRefractionOffset = 3503205;
        private const int SandboxLitSolidRefractionSize = 529066;
        private const int SandboxLitSolidRefractionSsrOffset = 4032271;
        private const int SandboxLitSolidRefractionSsrSize = 533020;
        private const int SandboxSpecGlossOffset = 4565291;
        private const int SandboxSpecGlossSize = 475673;
        private const int SandboxSubSurfaceOffset = 5040964;
        private const int SandboxSubSurfaceSize = 399240;
        private const int SandboxUnlitOffset = 5440204;
        private const int SandboxUnlitSize = 22763;
        private const int TexturedLitOffset = 5462967;
        private const int TexturedLitSize = 447912;
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
