using System;
using Rhino;
using Rhino.FileIO;
using Rhino.Collections;
using Rhino.DocObjects.Custom;
using System.Runtime.InteropServices;
using System.Collections.Generic;

namespace FrameLineEto.Common.Methods
{
    [Guid("29e3a0b1-b503-49f7-88b8-4cc5e2f79cf8")]
    public class FrameLineData : UserData
    {
        public RhinoList<Spacing> Spacings = new RhinoList<Spacing>();
        public Int32[] Main_Params { get; set; }
        public Int32[] Mod1_Params { get; set; }
        public Int32[] Mod2_Params { get; set; }
        public Int32[] Mod3_Params { get; set; }

        public FrameLineData() {}
        public FrameLineData(RhinoList<Spacing> spacings)
        {
            Main_Params = new Int32[] { spacings[0].Start, spacings[0].End, spacings[0].Space };

            if (spacings.Count > 1)
            {
                Mod1_Params = new Int32[] { spacings[1].Start, spacings[1].End, spacings[1].Space };

                if (spacings.Count > 2)
                {
                    Mod2_Params = new Int32[] { spacings[2].Start, spacings[2].End, spacings[2].Space };

                    if (spacings.Count > 3)
                    {
                        Mod3_Params = new Int32[] { spacings[3].Start, spacings[3].End, spacings[3].Space };
                    }
                }
            }
        }

        public override string Description
        {
            get { return "Frame line parameters:"; }
        }

        protected override void OnDuplicate(UserData source)
        {
            if (source is FrameLineData src)
            {
                Main_Params = src.Main_Params;
                Mod1_Params = src.Mod1_Params;
                Mod2_Params = src.Mod2_Params;
                Mod3_Params = src.Mod3_Params;
            }
        }

        public override bool ShouldWrite
        {
            get
            {
                if (Main_Params[2] > 0)
                {
                    return true;
                }
                return false;
            }
        }

        protected override bool Read(BinaryArchiveReader archive)
        {
            ArchivableDictionary dict = archive.ReadDictionary();
            if (dict.ContainsKey("Main_Params"))
            {
                RhinoApp.WriteLine("Reading in progress...");
                Main_Params = (Int32[])dict["Main_Params"];
                Mod1_Params = (Int32[])dict["Mod1_Params"];
                Mod2_Params = (Int32[])dict["Mod2_Params"];
                Mod3_Params = (Int32[])dict["Mod3_Params"];
            }
            return true;
        }
        protected override bool Write(BinaryArchiveWriter archive)
        {
            var dict = new ArchivableDictionary(1, "Parameters");
            dict.Set("Main_Params", Main_Params);
            dict.Set("Mod1_Params", Mod1_Params);
            dict.Set("Mod2_Params", Mod2_Params);
            dict.Set("Mod3_Params", Mod3_Params);

            archive.WriteDictionary(dict);
            return true;
        }
    }
}
