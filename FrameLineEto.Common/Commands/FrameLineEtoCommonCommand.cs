using System;
using Rhino;
using Rhino.Commands;
using Rhino.UI;

namespace FrameLineEto.Common.Commands
{
    public class FrameLineEtoCommonCommand : Rhino.Commands.Command
    {
        private Views.FrameLineEtoModelessForm Form
        {
            get;
            set;
        }

        public override string EnglishName => "FrameLineEto";

        protected override Result RunCommand(Rhino.RhinoDoc doc, RunMode mode)
            {
                return Result.Success;
            }
    }
}