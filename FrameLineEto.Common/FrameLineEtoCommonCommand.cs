using System;
using Rhino;
using Rhino.Commands;
using Rhino.Geometry;
using Rhino.Input;
using Rhino.Input.Custom;
using Rhino.UI;
using Eto.Drawing;
using Eto.Forms;

namespace FrameLineEto.Common
{
    public class FrameLineEtoCommonCommand : Rhino.Commands.Command
  {
    public override string EnglishName => "FrameLineEto.CommonCommand";

    protected override Result RunCommand(Rhino.RhinoDoc doc, RunMode mode)
    {
        return Result.Success;
    }
}
}