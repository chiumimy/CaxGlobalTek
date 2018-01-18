using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NXOpen;
using NXOpen.Drawings;
using CaxGlobaltek;

namespace FixCreateBalloon
{
    public class Variables
    {
        public static string Is_Local = "";
        public static Dictionary<string, List<CaxME.DimenData>> DicDimenData = new Dictionary<string, List<CaxME.DimenData>>();
        public static DrawingSheet FirstDrawingSheet;
    }

    //public struct DimenData
    //{
    //    public NXObject Obj { get; set; }
    //    public DrawingSheet CurrentSheet { get; set; }
    //    public double LocationX { get; set; }
    //    public double LocationY { get; set; }
    //    public double LocationZ { get; set; }
    //}
}
