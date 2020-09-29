using System;
using Rhino;
using Rhino.FileIO;
using Rhino.Collections;
using Rhino.DocObjects.Custom;
using System.Runtime.InteropServices;

namespace FrameLineEto.Common.Methods
{
    [Guid("29e3a0b1-b503-49f7-88b8-4cc5e2f79cf8")]
    public class FrameLineData : UserData
    {
        public RhinoList<Spacing> Spacings = new RhinoList<Spacing>();

        public int Main_From { get; set; }
        public int Main_To { get; set; }
        public int Main_Spacing { get; set; }

        public int Mod1_From { get; set; }
        public int Mod1_To { get; set; }
        public int Mod1_Spacing { get; set; }

        public int Mod2_From { get; set; }
        public int Mod2_To { get; set; }
        public int Mod2_Spacing { get; set; }

        public int Mod3_From { get; set; }
        public int Mod3_To { get; set; }
        public int Mod3_Spacing { get; set; }

        public FrameLineData() {}
        public FrameLineData(RhinoList<Spacing> spacings)
        {
            Main_From = spacings[0].Start;
            Main_To = spacings[0].End;
            Main_Spacing = spacings[0].Space;

            if (spacings.Count > 1)
            {
                Mod1_From = spacings[1].Start;
                Mod1_To = spacings[1].End;
                Mod1_Spacing = spacings[1].Space;

                if (spacings.Count > 2)
                {
                    Mod2_From = spacings[2].Start;
                    Mod2_To = spacings[2].End;
                    Mod2_Spacing = spacings[2].Space;

                    if (spacings.Count > 3)
                    {
                        Mod3_From = spacings[3].Start;
                        Mod3_To = spacings[3].End;
                        Mod3_Spacing = spacings[3].Space;
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
                Main_From = src.Main_From;
                Main_To = src.Main_To;
                Main_Spacing = src.Main_Spacing;

                Mod1_From = src.Mod1_From;
                Mod1_To = src.Mod1_To;
                Mod1_Spacing = src.Mod1_Spacing;

                Mod2_From = src.Mod2_From;
                Mod2_To = src.Mod2_To;
                Mod2_Spacing = src.Mod2_Spacing;

                Mod3_From = src.Mod3_From;
                Mod3_To = src.Mod3_To;
                Mod3_Spacing = src.Mod3_Spacing;
            }
        }

        public override bool ShouldWrite
        {
            get
            {
                if (Main_Spacing > 0)
                {
                    return true;
                }
                return false;
            }
        }

        protected override bool Read(BinaryArchiveReader archive)
        {
            ArchivableDictionary dict = archive.ReadDictionary();
            if (dict.ContainsKey("Main_From"))
            {
                RhinoApp.WriteLine("Reading in progress...");

                Main_From = (int)dict["Main_From"];
                Main_To = (int)dict["Main_To"];
                Main_Spacing = (int)dict["Main_Spacing"];

                Mod1_From = (int)dict["Mod1_From"];
                Mod1_To = (int)dict["Mod1_To"];
                Mod1_Spacing = (int)dict["Mod1_Spacing"];

                Mod2_From = (int)dict["Mod2_From"];
                Mod2_To = (int)dict["Mod2_To"];
                Mod2_Spacing = (int)dict["Mod2_Spacing"];

                Mod3_From = (int)dict["Mod3_From"];
                Mod3_To = (int)dict["Mod3_To"];
                Mod3_Spacing = (int)dict["Mod3_Spacing"];
            }
            return true;
        }
        protected override bool Write(BinaryArchiveWriter archive)
        {
            var dict = new ArchivableDictionary(1, "Parameters");

            dict.Set("Main_From", Main_From);
            dict.Set("Main_To", Main_To);
            dict.Set("Main_Spacing", Main_Spacing);

            dict.Set("Mod1_From", Mod1_From);
            dict.Set("Mod1_To", Mod1_To);
            dict.Set("Mod1_Spacing", Mod1_Spacing);

            dict.Set("Mod2_From", Mod2_From);
            dict.Set("Mod2_To", Mod2_To);
            dict.Set("Mod2_Spacing", Mod2_Spacing);

            dict.Set("Mod3_From", Mod3_From);
            dict.Set("Mod3_To", Mod3_To);
            dict.Set("Mod3_Spacing", Mod3_Spacing);

            archive.WriteDictionary(dict);
            return true;
        }
    }
}
