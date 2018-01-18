using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CaxGlobaltek;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace MainProgram
{
    public class ButtonShape
    {
        public static void RightArrowBtn(DevComponents.DotNetBar.ButtonX Btn)
        {
            try
            {
                Point[] pts = {
                                new Point(0,  20),
                                new Point(20, 20),
                                new Point(20, 10),
                                new Point(40, 30),
                                new Point(20, 50),
                                new Point(20, 40),
                                new Point( 0, 40)
                              };
                GraphicsPath polygon_path = new GraphicsPath(FillMode.Winding);
                polygon_path.AddPolygon(pts);
                Region polygon_region = new Region(polygon_path);
                Btn.Region = polygon_region;
                Btn.SetBounds(Btn.Location.X, Btn.Location.Y - 20, pts[3].X + 5, pts[4].Y + 5);
            }
            catch (System.Exception ex)
            {
                CaxLog.ShowListingWindow("箭頭按鈕建立失敗");
            }
        }

        public static void LeftArrowBtn(DevComponents.DotNetBar.ButtonX Btn)
        {
            try
            {
                Point[] pts = {
                                new Point(0,  30),
                                new Point(20, 10),
                                new Point(20, 20),
                                new Point(40, 20),
                                new Point(40, 40),
                                new Point(20, 40),
                                new Point(20, 50)
                              };
                GraphicsPath polygon_path = new GraphicsPath(FillMode.Winding);
                polygon_path.AddPolygon(pts);
                Region polygon_region = new Region(polygon_path);
                Btn.Region = polygon_region;
                Btn.SetBounds(Btn.Location.X, Btn.Location.Y, pts[3].X + 5, pts[6].Y + 5);
            }
            catch (System.Exception ex)
            {
                CaxLog.ShowListingWindow("箭頭按鈕建立失敗");
            }
        }

        public static void UpArrowBtn(DevComponents.DotNetBar.ButtonX Btn)
        {
            try
            {
                Point[] pts = {
                                new Point(0,  20),
                                new Point(20, 0),
                                new Point(40, 20),
                                new Point(30, 20),
                                new Point(30, 40),
                                new Point(10, 40),
                                new Point(10, 20)
                              };
                GraphicsPath polygon_path = new GraphicsPath(FillMode.Winding);
                polygon_path.AddPolygon(pts);
                Region polygon_region = new Region(polygon_path);
                Btn.Region = polygon_region;
                Btn.SetBounds(Btn.Location.X, Btn.Location.Y - 10, pts[2].X + 5, pts[4].Y + 5);
            }
            catch (System.Exception ex)
            {
                CaxLog.ShowListingWindow("箭頭按鈕建立失敗");
            }
        }

        public static void DownArrowBtn(DevComponents.DotNetBar.ButtonX Btn)
        {
            try
            {
                Point[] pts = {
                                new Point(0,  20),
                                new Point(10, 20),
                                new Point(10, 0),
                                new Point(30, 0),
                                new Point(30, 20),
                                new Point(40, 20),
                                new Point(20, 40)
                              };
                GraphicsPath polygon_path = new GraphicsPath(FillMode.Winding);
                polygon_path.AddPolygon(pts);
                Region polygon_region = new Region(polygon_path);
                Btn.Region = polygon_region;
                Btn.SetBounds(Btn.Location.X, Btn.Location.Y + 10, pts[5].X + 5, pts[6].Y + 5);
            }
            catch (System.Exception ex)
            {
                CaxLog.ShowListingWindow("箭頭按鈕建立失敗");
            }
        }
    }
}
