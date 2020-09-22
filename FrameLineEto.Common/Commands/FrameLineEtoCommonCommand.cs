using System;
using Rhino.Commands;
using Rhino.Collections;
using Rhino.UI;
using FrameLineEto.Common.Methods;

namespace FrameLineEto.Common.Commands
{
    public class FrameLineEtoCommonCommand : Command
    {
        private Views.FrameLineEtoModelessForm Form { get; set; }

        public override string EnglishName => "FrameLineEto";

        protected override Result RunCommand(Rhino.RhinoDoc doc, RunMode mode)
        {
            if (null == Form)
            {
                Form = new Views.FrameLineEtoModelessForm { Owner = RhinoEtoApp.MainWindow };
                Form.RestorePosition();
                Form.Closed += OnFormClosed;
                Form.Show();
            }
            return Result.Success;
        }

        private void OnFormClosed(object sender, EventArgs e)
        {
            Form.SavePosition();
            Form.Dispose();
            Form = null;
        }
    }
}