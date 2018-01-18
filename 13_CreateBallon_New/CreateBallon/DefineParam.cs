using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NXOpen;

namespace CreateBallon
{
    public class TextData
    {
        public string mainText { get; set; }
        public string BeforeText { get; set; }
        public string AfterText { get; set; }
        public string AboveText { get; set; }
        public string BelowText { get; set; }
        public string BallonNum { get; set; }
        public string UpperTol { get; set; }
        public string LowerTol { get; set; }
    }

    public struct DimenData
    {
        public NXObject Obj { get; set; }
        public NXOpen.Drawings.DrawingSheet CurrentSheet { get; set; }
        public double LocationX { get; set; }
        public double LocationY { get; set; }
        public double LocationZ { get; set; }
    }

    public class DefineParam
    {
        public static Dictionary<NXOpen.Drawings.DrawingSheet, List<DisplayableObject>> DicSheetData_IPQC = new Dictionary<NXOpen.Drawings.DrawingSheet, List<DisplayableObject>>();
        public static Dictionary<NXOpen.Drawings.DrawingSheet, List<DisplayableObject>> DicSheetData_Self = new Dictionary<NXOpen.Drawings.DrawingSheet, List<DisplayableObject>>();
        public static Dictionary<DisplayableObject, TextData> DicDimenData_IPQC = new Dictionary<DisplayableObject, TextData>();
        public static Dictionary<DisplayableObject, TextData> DicDimenData_Self = new Dictionary<DisplayableObject, TextData>();
        public static Dictionary<string, List<DimenData>> DicDimenData = new Dictionary<string, List<DimenData>>();
        public static NXOpen.Drawings.DrawingSheet FirstDrawingSheet;
    }

    public struct Sheet_DefineNum
    {
        public string sheet { get; set; }
        public string defineNum { get; set; }
    }
}
