using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NXOpen;
using NXOpen.Drawings;

namespace FixAssignDimension
{
    public class Variables
    {
        public static string Is_Local = "";
        public static DrawingSheet drawingSheet1;
        public static NXObject[] SelDimensionAry = new NXObject[] { };
        public static NXObject[] CleanDimensionAry = new NXObject[] { };
        public static Dictionary<NXObject, string> DicSelDimension = new Dictionary<NXObject, string>();
        public static Dictionary<NXObject, string> DicCleanDimension = new Dictionary<NXObject, string>();
    }
}
