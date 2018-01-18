using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using CaxGlobaltek;
using NXOpen;
using NXOpen.UF;
using NXOpen.Utilities;
using System.Windows.Forms;

namespace PartInformation
{
    public class Functions
    {
        //public static DraftingConfig cDraftingConfig = new DraftingConfig();
        private static Session theSession = Session.GetSession();
        private static UI theUI = UI.GetUI();
        private static UFSession theUfSession = UFSession.GetUFSession();


        /// <summary>
        /// 單筆資料插入
        /// </summary>
        /// <param name="attrStr"></param>
        /// <param name="text"></param>
        /// <param name="FontSize"></param>
        /// <param name="textLocation"></param>
        /// <param name="defult"></param>
        /// <returns></returns>
        public static bool InsertNote(string attrStr, string text, string FontSize, Point3d textLocation, string RevCount = "", NXOpen.Annotations.OriginBuilder.AlignmentPosition defult = NXOpen.Annotations.OriginBuilder.AlignmentPosition.MidCenter)
        {
            try
            {
                Session theSession = Session.GetSession();
                Part workPart = theSession.Parts.Work;
                Part displayPart = theSession.Parts.Display;
                // ----------------------------------------------
                //   Menu: Insert->Annotation->Note...
                // ----------------------------------------------
                NXOpen.Session.UndoMarkId markId1;
                markId1 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Start");
                NXOpen.Annotations.SimpleDraftingAid nullAnnotations_SimpleDraftingAid = null;
                NXOpen.Annotations.DraftingNoteBuilder draftingNoteBuilder1;
                draftingNoteBuilder1 = workPart.Annotations.CreateDraftingNoteBuilder(nullAnnotations_SimpleDraftingAid);
                draftingNoteBuilder1.Origin.Plane.PlaneMethod = NXOpen.Annotations.PlaneBuilder.PlaneMethodType.XyPlane;
                draftingNoteBuilder1.Origin.SetInferRelativeToGeometry(false);
                draftingNoteBuilder1.Origin.SetInferRelativeToGeometry(true);
                draftingNoteBuilder1.Origin.Anchor = defult;

                
                draftingNoteBuilder1.Style.LetteringStyle.GeneralTextSize = Convert.ToDouble(FontSize);

                //text文字
                int a = workPart.Fonts.AddFont("chineset");
                draftingNoteBuilder1.Style.LetteringStyle.GeneralTextFont = a;

                string[] text1;
                if (attrStr == InspectionDlg.TablePosi.MaterialPos)
                {
                    string[] splitText = text.Split(' ');
                    text1 = new string[splitText.Length];
                    for (int i = 0; i < splitText.Length; i++)
                    {
                        text1[i] = splitText[i];
                        //text1[i] = "<F2>" + text[i] + "<F>";
                    }
                }
                else
                {
                    text1 = new string[1];
                    text1[0] = text;
                }
                //string[] text1 = new string[1];
                //text1[0] = "<C2>" + text + "<C>";
                //text1[0] = "<F2>" + text + "<F>";
                
                draftingNoteBuilder1.Text.TextBlock.SetText(text1);

                draftingNoteBuilder1.Style.LetteringStyle.GeneralTextSize = Convert.ToDouble(FontSize);
                

                theSession.SetUndoMarkName(markId1, "Note Dialog");
                draftingNoteBuilder1.Origin.Plane.PlaneMethod = NXOpen.Annotations.PlaneBuilder.PlaneMethodType.XyPlane;
                draftingNoteBuilder1.Origin.SetInferRelativeToGeometry(true);
                //NXOpen.Annotations.LeaderData leaderData1;
                //leaderData1 = workPart.Annotations.CreateLeaderData();
                //leaderData1.StubSize = 5.0;
                //leaderData1.Arrowhead = NXOpen.Annotations.LeaderData.ArrowheadType.FilledArrow;
                //draftingNoteBuilder1.Leader.Leaders.Append(leaderData1);
                //leaderData1.StubSide = NXOpen.Annotations.LeaderSide.Inferred;
                double symbolscale1;
                symbolscale1 = draftingNoteBuilder1.Text.TextBlock.SymbolScale;
                double symbolaspectratio1;
                symbolaspectratio1 = draftingNoteBuilder1.Text.TextBlock.SymbolAspectRatio;
                draftingNoteBuilder1.Origin.SetInferRelativeToGeometry(true);
                draftingNoteBuilder1.Origin.SetInferRelativeToGeometry(true);
                NXOpen.Annotations.Annotation.AssociativeOriginData assocOrigin1;
                assocOrigin1.OriginType = NXOpen.Annotations.AssociativeOriginType.Drag;
                NXOpen.View nullView = null;
                assocOrigin1.View = nullView;
                assocOrigin1.ViewOfGeometry = nullView;
                NXOpen.Point nullPoint = null;
                assocOrigin1.PointOnGeometry = nullPoint;
                assocOrigin1.VertAnnotation = null;
                assocOrigin1.VertAlignmentPosition = NXOpen.Annotations.AlignmentPosition.TopLeft;
                assocOrigin1.HorizAnnotation = null;
                assocOrigin1.HorizAlignmentPosition = NXOpen.Annotations.AlignmentPosition.TopLeft;
                assocOrigin1.AlignedAnnotation = null;
                assocOrigin1.DimensionLine = 0;
                assocOrigin1.AssociatedView = nullView;
                assocOrigin1.AssociatedPoint = nullPoint;
                assocOrigin1.OffsetAnnotation = null;
                assocOrigin1.OffsetAlignmentPosition = NXOpen.Annotations.AlignmentPosition.TopLeft;
                assocOrigin1.XOffsetFactor = 0.0;
                assocOrigin1.YOffsetFactor = 0.0;
                assocOrigin1.StackAlignmentPosition = NXOpen.Annotations.StackAlignmentPosition.Above;
                draftingNoteBuilder1.Origin.SetAssociativeOrigin(assocOrigin1);

                //text擺放位置
                draftingNoteBuilder1.Origin.Origin.SetValue(null, nullView, textLocation);

                draftingNoteBuilder1.Origin.SetInferRelativeToGeometry(true);
                NXOpen.Session.UndoMarkId markId2;
                markId2 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Note");
                NXObject nXObject1;
                nXObject1 = draftingNoteBuilder1.Commit();
                //塞屬性，以利重複執行時抓取資料，加入RevCount是給製圖版次、製圖日期、說明  使用
                if (RevCount != "")
                {
                    text = text + "," + RevCount;
                }
                nXObject1.SetAttribute(attrStr, text);
                theSession.DeleteUndoMark(markId2, null);
                theSession.SetUndoMarkName(markId1, "Note");
                theSession.SetUndoMarkVisibility(markId1, null, NXOpen.Session.MarkVisibility.Visible);
                draftingNoteBuilder1.Destroy();


                // ----------------------------------------------
                //   Menu: Tools->Journal->Stop Recording
                // ----------------------------------------------
            }
            catch (System.Exception ex)
            {
                UI.GetUI().NXMessageBox.Show("Message", NXMessageBox.DialogType.Error, ex.Message);
                return false;
            }
            return true;
        }

        public static bool InsertERPNote(string attrStr, string text, string FontSize, Point3d textLocation, NXOpen.Annotations.OriginBuilder.AlignmentPosition defult = NXOpen.Annotations.OriginBuilder.AlignmentPosition.MidCenter)
        {
            try
            {
                Session theSession = Session.GetSession();
                Part workPart = theSession.Parts.Work;
                Part displayPart = theSession.Parts.Display;
                // ----------------------------------------------
                //   Menu: Insert->Annotation->Note...
                // ----------------------------------------------
                NXOpen.Session.UndoMarkId markId1;
                markId1 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Start");
                NXOpen.Annotations.SimpleDraftingAid nullAnnotations_SimpleDraftingAid = null;
                NXOpen.Annotations.DraftingNoteBuilder draftingNoteBuilder1;
                draftingNoteBuilder1 = workPart.Annotations.CreateDraftingNoteBuilder(nullAnnotations_SimpleDraftingAid);
                draftingNoteBuilder1.Origin.Plane.PlaneMethod = NXOpen.Annotations.PlaneBuilder.PlaneMethodType.XyPlane;
                draftingNoteBuilder1.Origin.SetInferRelativeToGeometry(false);
                draftingNoteBuilder1.Origin.SetInferRelativeToGeometry(true);
                draftingNoteBuilder1.Origin.Anchor = defult;


                draftingNoteBuilder1.Style.LetteringStyle.GeneralTextSize = Convert.ToDouble(FontSize);

                //text文字
                int a = workPart.Fonts.AddFont("chineset");
                draftingNoteBuilder1.Style.LetteringStyle.GeneralTextFont = a;
                string[] text1 = new string[1];
                //text1[0] = "<C2>" + text + "<C>";
                //text1[0] = "<F2>" + text + "<F>";
                text1[0] = text;
                draftingNoteBuilder1.Text.TextBlock.SetText(text1);

                draftingNoteBuilder1.Style.LetteringStyle.GeneralTextSize = Convert.ToDouble(FontSize);


                theSession.SetUndoMarkName(markId1, "Note Dialog");
                draftingNoteBuilder1.Origin.Plane.PlaneMethod = NXOpen.Annotations.PlaneBuilder.PlaneMethodType.XyPlane;
                draftingNoteBuilder1.Origin.SetInferRelativeToGeometry(true);
                //NXOpen.Annotations.LeaderData leaderData1;
                //leaderData1 = workPart.Annotations.CreateLeaderData();
                //leaderData1.StubSize = 5.0;
                //leaderData1.Arrowhead = NXOpen.Annotations.LeaderData.ArrowheadType.FilledArrow;
                //draftingNoteBuilder1.Leader.Leaders.Append(leaderData1);
                //leaderData1.StubSide = NXOpen.Annotations.LeaderSide.Inferred;
                double symbolscale1;
                symbolscale1 = draftingNoteBuilder1.Text.TextBlock.SymbolScale;
                double symbolaspectratio1;
                symbolaspectratio1 = draftingNoteBuilder1.Text.TextBlock.SymbolAspectRatio;
                draftingNoteBuilder1.Origin.SetInferRelativeToGeometry(true);
                draftingNoteBuilder1.Origin.SetInferRelativeToGeometry(true);
                draftingNoteBuilder1.Style.LetteringStyle.Angle = 90.0;
                NXOpen.Annotations.Annotation.AssociativeOriginData assocOrigin1;
                assocOrigin1.OriginType = NXOpen.Annotations.AssociativeOriginType.Drag;
                NXOpen.View nullView = null;
                assocOrigin1.View = nullView;
                assocOrigin1.ViewOfGeometry = nullView;
                NXOpen.Point nullPoint = null;
                assocOrigin1.PointOnGeometry = nullPoint;
                assocOrigin1.VertAnnotation = null;
                assocOrigin1.VertAlignmentPosition = NXOpen.Annotations.AlignmentPosition.TopLeft;
                assocOrigin1.HorizAnnotation = null;
                assocOrigin1.HorizAlignmentPosition = NXOpen.Annotations.AlignmentPosition.TopLeft;
                assocOrigin1.AlignedAnnotation = null;
                assocOrigin1.DimensionLine = 0;
                assocOrigin1.AssociatedView = nullView;
                assocOrigin1.AssociatedPoint = nullPoint;
                assocOrigin1.OffsetAnnotation = null;
                assocOrigin1.OffsetAlignmentPosition = NXOpen.Annotations.AlignmentPosition.TopLeft;
                assocOrigin1.XOffsetFactor = 0.0;
                assocOrigin1.YOffsetFactor = 0.0;
                assocOrigin1.StackAlignmentPosition = NXOpen.Annotations.StackAlignmentPosition.Above;
                draftingNoteBuilder1.Origin.SetAssociativeOrigin(assocOrigin1);

                //text擺放位置
                draftingNoteBuilder1.Origin.Origin.SetValue(null, nullView, textLocation);

                draftingNoteBuilder1.Origin.SetInferRelativeToGeometry(true);
                NXOpen.Session.UndoMarkId markId2;
                markId2 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Note");
                NXObject nXObject1;
                nXObject1 = draftingNoteBuilder1.Commit();
                //塞屬性，以利重複執行時抓取資料
                nXObject1.SetAttribute(attrStr, text);
                theSession.DeleteUndoMark(markId2, null);
                theSession.SetUndoMarkName(markId1, "Note");
                theSession.SetUndoMarkVisibility(markId1, null, NXOpen.Session.MarkVisibility.Visible);
                draftingNoteBuilder1.Destroy();


                // ----------------------------------------------
                //   Menu: Tools->Journal->Stop Recording
                // ----------------------------------------------
            }
            catch (System.Exception ex)
            {
                UI.GetUI().NXMessageBox.Show("Message", NXMessageBox.DialogType.Error, ex.Message);
                return false;
            }
            return true;
        }

        /// <summary>
        /// 多筆資料插入
        /// </summary>
        /// <param name="attrStr"></param>
        /// <param name="text"></param>
        /// <param name="FontSize"></param>
        /// <param name="textLocation"></param>
        /// <param name="defult"></param>
        /// <returns></returns>
        public static bool InsertNote(string attrStr, string[] text, string FontSize, Point3d textLocation, NXOpen.Annotations.OriginBuilder.AlignmentPosition defult = NXOpen.Annotations.OriginBuilder.AlignmentPosition.MidCenter)
        {
            //nXObject1 = null;
            try
            {
                Session theSession = Session.GetSession();
                Part workPart = theSession.Parts.Work;
                Part displayPart = theSession.Parts.Display;
                // ----------------------------------------------
                //   Menu: Insert->Annotation->Note...
                // ----------------------------------------------
                NXOpen.Session.UndoMarkId markId1;
                markId1 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Visible, "Start");
                NXOpen.Annotations.SimpleDraftingAid nullAnnotations_SimpleDraftingAid = null;
                NXOpen.Annotations.DraftingNoteBuilder draftingNoteBuilder1;
                draftingNoteBuilder1 = workPart.Annotations.CreateDraftingNoteBuilder(nullAnnotations_SimpleDraftingAid);
                draftingNoteBuilder1.Origin.Plane.PlaneMethod = NXOpen.Annotations.PlaneBuilder.PlaneMethodType.XyPlane;
                draftingNoteBuilder1.Origin.SetInferRelativeToGeometry(false);
                draftingNoteBuilder1.Origin.SetInferRelativeToGeometry(true);
                draftingNoteBuilder1.Origin.Anchor = defult;
                
                //draftingNoteBuilder1.Style.LetteringStyle.GeneralTextFont = 10;
                draftingNoteBuilder1.Style.LetteringStyle.GeneralTextSize = Convert.ToDouble(FontSize);

                int fontIndex1;
                fontIndex1 = workPart.Fonts.AddFont("chineset");
                draftingNoteBuilder1.Style.LetteringStyle.GeneralTextFont = fontIndex1;
                //text文字
                string[] text1 = new string[text.Length];
                //text1[0] = "<C2>" + text + "<C>";
                for (int i = 0; i < text.Length; i++)
                {
                    text1[i] = text[i];
                    //text1[i] = "<F2>" + text[i] + "<F>";
                }
                //text1[0] = "<F18>" + text + "<F>";
                draftingNoteBuilder1.Text.TextBlock.SetText(text1);

                //draftingNoteBuilder1.Style.LetteringStyle.GeneralTextSize = Convert.ToDouble(FontSize);


                theSession.SetUndoMarkName(markId1, "Note Dialog");
                draftingNoteBuilder1.Origin.Plane.PlaneMethod = NXOpen.Annotations.PlaneBuilder.PlaneMethodType.XyPlane;
                draftingNoteBuilder1.Origin.SetInferRelativeToGeometry(true);
                //NXOpen.Annotations.LeaderData leaderData1;
                //leaderData1 = workPart.Annotations.CreateLeaderData();
                //leaderData1.StubSize = 5.0;
                //leaderData1.Arrowhead = NXOpen.Annotations.LeaderData.ArrowheadType.FilledArrow;
                //draftingNoteBuilder1.Leader.Leaders.Append(leaderData1);
                //leaderData1.StubSide = NXOpen.Annotations.LeaderSide.Inferred;
                double symbolscale1;
                symbolscale1 = draftingNoteBuilder1.Text.TextBlock.SymbolScale;
                double symbolaspectratio1;
                symbolaspectratio1 = draftingNoteBuilder1.Text.TextBlock.SymbolAspectRatio;
                draftingNoteBuilder1.Origin.SetInferRelativeToGeometry(true);
                draftingNoteBuilder1.Origin.SetInferRelativeToGeometry(true);
                NXOpen.Annotations.Annotation.AssociativeOriginData assocOrigin1;
                assocOrigin1.OriginType = NXOpen.Annotations.AssociativeOriginType.Drag;
                NXOpen.View nullView = null;
                assocOrigin1.View = nullView;
                assocOrigin1.ViewOfGeometry = nullView;
                NXOpen.Point nullPoint = null;
                assocOrigin1.PointOnGeometry = nullPoint;
                assocOrigin1.VertAnnotation = null;
                assocOrigin1.VertAlignmentPosition = NXOpen.Annotations.AlignmentPosition.TopLeft;
                assocOrigin1.HorizAnnotation = null;
                assocOrigin1.HorizAlignmentPosition = NXOpen.Annotations.AlignmentPosition.TopLeft;
                assocOrigin1.AlignedAnnotation = null;
                assocOrigin1.DimensionLine = 0;
                assocOrigin1.AssociatedView = nullView;
                assocOrigin1.AssociatedPoint = nullPoint;
                assocOrigin1.OffsetAnnotation = null;
                assocOrigin1.OffsetAlignmentPosition = NXOpen.Annotations.AlignmentPosition.TopLeft;
                assocOrigin1.XOffsetFactor = 0.0;
                assocOrigin1.YOffsetFactor = 0.0;
                assocOrigin1.StackAlignmentPosition = NXOpen.Annotations.StackAlignmentPosition.Above;
                draftingNoteBuilder1.Origin.SetAssociativeOrigin(assocOrigin1);

                //text擺放位置
                draftingNoteBuilder1.Origin.Origin.SetValue(null, nullView, textLocation);
                draftingNoteBuilder1.Origin.SetInferRelativeToGeometry(true);
                NXOpen.Session.UndoMarkId markId2;
                markId2 = theSession.SetUndoMark(NXOpen.Session.MarkVisibility.Invisible, "Note");
                //NXObject nXObject1;
                //nXObject1 = draftingNoteBuilder1.Commit();
                //塞屬性，以利重複執行時抓取資料，加入RevCount是給製圖版次、製圖日期、說明  使用

                //nXObject1.SetAttribute(attrStr, te);
                theSession.DeleteUndoMark(markId2, null);
                theSession.SetUndoMarkName(markId1, "Note");
                theSession.SetUndoMarkVisibility(markId1, null, NXOpen.Session.MarkVisibility.Visible);
                draftingNoteBuilder1.Destroy();


                // ----------------------------------------------
                //   Menu: Tools->Journal->Stop Recording
                // ----------------------------------------------
            }
            catch (System.Exception ex)
            {
                UI.GetUI().NXMessageBox.Show("Message", NXMessageBox.DialogType.Error, ex.Message);
                return false;
            }
            return true;
        }

        public static bool GetTextPos(DraftingConfig cDraftingConfig, int i, string KeyToCompare, string ValueToCompare, out Point3d TextPos, out string FontSize)
        {
            TextPos = new Point3d();
            FontSize = "";
            try
            {
                string ptStr = "";
                if (cDraftingConfig.Drafting[i].PageNumberPosText == KeyToCompare)
                {
                    ptStr = cDraftingConfig.Drafting[i].PageNumberPos;
                    FontSize = cDraftingConfig.Drafting[i].PageNumberFontSize;
                }
                else if (cDraftingConfig.Drafting[i].ERPcodePosText == KeyToCompare)
                {
                    ptStr = cDraftingConfig.Drafting[i].ERPcodePos;
                    FontSize = "1.5";
                }
                else if (cDraftingConfig.Drafting[i].ERPRevPosText == KeyToCompare)
                {
                    ptStr = cDraftingConfig.Drafting[i].ERPRevPos;
                    FontSize = "3";
                }
                else if (cDraftingConfig.Drafting[i].PartDescriptionPosText == KeyToCompare)
                {
                    //ptStr = cDraftingConfig.Drafting[i].PartDescriptionPos;
                    //FontSize = cDraftingConfig.Drafting[i].PartDescriptionFontSize;
                    ptStr = cDraftingConfig.Drafting[i].PartDescriptionPos;
                    int Subtraction = ValueToCompare.Length - 12;
                    if (Subtraction <= 0)
                    {
                        FontSize = cDraftingConfig.Drafting[i].PartDescriptionFontSize;
                    }
                    else
                    {
                        if (Subtraction <= 3)
                        {
                            FontSize = (Convert.ToDouble(cDraftingConfig.Drafting[i].PartDescriptionFontSize) - (Convert.ToDouble(cDraftingConfig.Drafting[i].MatMinFontSize))).ToString();
                        }
                        else if (Subtraction > 3 && Subtraction <= 5)
                        {
                            FontSize = (Convert.ToDouble(cDraftingConfig.Drafting[i].PartDescriptionFontSize) - (Convert.ToDouble(cDraftingConfig.Drafting[i].MatMinFontSize)) * 2).ToString();
                        }
                        else if (Subtraction > 5 && Subtraction <= 7)
                        {
                            FontSize = (Convert.ToDouble(cDraftingConfig.Drafting[i].PartDescriptionFontSize) - (Convert.ToDouble(cDraftingConfig.Drafting[i].MatMinFontSize)) * 3).ToString();
                        }
                        else if (Subtraction > 7)
                        {
                            FontSize = (Convert.ToDouble(cDraftingConfig.Drafting[i].PartDescriptionFontSize) - (Convert.ToDouble(cDraftingConfig.Drafting[i].MatMinFontSize)) * 3.5).ToString();
                        }

                        if (Convert.ToDouble(FontSize) <= 0)
                        {
                            FontSize = cDraftingConfig.Drafting[i].MatMinFontSize;
                        }
                    }
                }
                else if (cDraftingConfig.Drafting[i].AuthDatePosText == KeyToCompare)
                {
                    ptStr = cDraftingConfig.Drafting[i].AuthDatePos;
                    FontSize = (Convert.ToDouble(cDraftingConfig.Drafting[i].AuthDateFontSize) - (Convert.ToDouble(cDraftingConfig.Drafting[i].MatMinFontSize)) * 0.3).ToString();
                }
                else if (cDraftingConfig.Drafting[i].MaterialPosText == KeyToCompare)
                {
                    ptStr = cDraftingConfig.Drafting[i].MaterialPos;
                    int Subtraction = ValueToCompare.Length - 5;
                    if (Subtraction <= 0)
                    {
                        FontSize = cDraftingConfig.Drafting[i].MaterialFontSize;
                    }
                    else
                    {
                        if (Subtraction <= 3)
                        {
                            FontSize = (Convert.ToDouble(cDraftingConfig.Drafting[i].MaterialFontSize) - (Convert.ToDouble(cDraftingConfig.Drafting[i].MatMinFontSize))).ToString();
                        }
                        else if (Subtraction > 3 && Subtraction <= 5)
                        {
                            FontSize = (Convert.ToDouble(cDraftingConfig.Drafting[i].MaterialFontSize) - (Convert.ToDouble(cDraftingConfig.Drafting[i].MatMinFontSize) * 2)).ToString();
                        }
                        else if (Subtraction > 5 && Subtraction <= 10)
                        {
                            FontSize = (Convert.ToDouble(cDraftingConfig.Drafting[i].PartDescriptionFontSize) - (Convert.ToDouble(cDraftingConfig.Drafting[i].MatMinFontSize)) * 3).ToString();
                        }
                        else if (Subtraction > 10)
                        {
                            FontSize = (Convert.ToDouble(cDraftingConfig.Drafting[i].PartDescriptionFontSize) - (Convert.ToDouble(cDraftingConfig.Drafting[i].MatMinFontSize)) * 3.5).ToString();
                        }
                        
                        if (Convert.ToDouble(FontSize) <= 0)
                        {
                            FontSize = cDraftingConfig.Drafting[i].MatMinFontSize;
                        }
                    }
                }
                else if (cDraftingConfig.Drafting[i].PartNumberPosText == KeyToCompare)
                {
                    ptStr = cDraftingConfig.Drafting[i].PartNumberPos;
                    int Subtraction = ValueToCompare.Length - 13;
                    if (Subtraction <= 0)
                    {
                        FontSize = cDraftingConfig.Drafting[i].PartNumberFontSize;
                    }
                    else
                    {
                        if (Subtraction <= 3)
                        {
                            FontSize = (Convert.ToDouble(cDraftingConfig.Drafting[i].PartNumberFontSize) - (Convert.ToDouble(cDraftingConfig.Drafting[i].MatMinFontSize))).ToString();
                        }
                        else if (Subtraction > 3 && Subtraction <= 6)
                        {
                            FontSize = (Convert.ToDouble(cDraftingConfig.Drafting[i].PartNumberFontSize) - (Convert.ToDouble(cDraftingConfig.Drafting[i].MatMinFontSize)) * 2).ToString();
                        }
                        else if (Subtraction > 6)
                        {
                            FontSize = (Convert.ToDouble(cDraftingConfig.Drafting[i].PartNumberFontSize) - (Convert.ToDouble(cDraftingConfig.Drafting[i].MatMinFontSize)) * 3).ToString();
                        }
                        
                        if (Convert.ToDouble(FontSize) <= 0)
                        {
                            FontSize = cDraftingConfig.Drafting[i].MatMinFontSize;
                        }
                    }
                    //FontSize = cDraftingConfig.Drafting[i].PartNumberFontSize;
                }
                else if (cDraftingConfig.Drafting[i].PartUnitPosText == KeyToCompare)
                {
                    ptStr = cDraftingConfig.Drafting[i].PartUnitPos;
                    FontSize = cDraftingConfig.Drafting[i].PartUnitFontSize;
                }
                else if (cDraftingConfig.Drafting[i].ProcNamePosText == KeyToCompare)
                {
                    ptStr = cDraftingConfig.Drafting[i].ProcNamePos;
                    FontSize = cDraftingConfig.Drafting[i].ProcNameFontSize;
                }
                else if (cDraftingConfig.Drafting[i].RevDateStartPosText == KeyToCompare)
                {
                    ptStr = cDraftingConfig.Drafting[i].RevDateStartPos;
                    FontSize = (Convert.ToDouble(cDraftingConfig.Drafting[i].RevDateFontSize) - (Convert.ToDouble(cDraftingConfig.Drafting[i].MatMinFontSize)) * 0.3).ToString();
                }
                else if (cDraftingConfig.Drafting[i].RevStartPosText == KeyToCompare)
                {
                    ptStr = cDraftingConfig.Drafting[i].RevStartPos;
                    FontSize = cDraftingConfig.Drafting[i].RevFontSize;
                }
                else if (cDraftingConfig.Drafting[i].SecondPageNumberPosText == KeyToCompare)
                {
                    ptStr = cDraftingConfig.Drafting[i].SecondPageNumberPos;
                    FontSize = cDraftingConfig.Drafting[i].PageNumberFontSize;
                }
                else if (cDraftingConfig.Drafting[i].SecondPartNumberPosText == KeyToCompare)
                {
                    ptStr = cDraftingConfig.Drafting[i].SecondPartNumberPos;
                    int Subtraction = ValueToCompare.Length - 13;
                    if (Subtraction <= 0)
                    {
                        FontSize = cDraftingConfig.Drafting[i].PartNumberFontSize;
                    }
                    else
                    {
                        if (Subtraction <= 3)
                        {
                            FontSize = (Convert.ToDouble(cDraftingConfig.Drafting[i].PartNumberFontSize) - (Convert.ToDouble(cDraftingConfig.Drafting[i].MatMinFontSize))).ToString();
                        }
                        else if (Subtraction > 3 && Subtraction <= 6)
                        {
                            FontSize = (Convert.ToDouble(cDraftingConfig.Drafting[i].PartNumberFontSize) - (Convert.ToDouble(cDraftingConfig.Drafting[i].MatMinFontSize)) * 2).ToString();
                        }
                        else if (Subtraction > 6)
                        {
                            FontSize = (Convert.ToDouble(cDraftingConfig.Drafting[i].PartNumberFontSize) - (Convert.ToDouble(cDraftingConfig.Drafting[i].MatMinFontSize)) * 3).ToString();
                        }

                        if (Convert.ToDouble(FontSize) <= 0)
                        {
                            FontSize = cDraftingConfig.Drafting[i].MatMinFontSize;
                        }
                        //FontSize = (Convert.ToDouble(cDraftingConfig.Drafting[i].PartNumberFontSize) - (Convert.ToDouble(cDraftingConfig.Drafting[i].MatMinFontSize) * Subtraction)).ToString();
                        //if (Convert.ToDouble(FontSize) <= 0)
                        //{
                        //    FontSize = cDraftingConfig.Drafting[i].MatMinFontSize;
                        //}
                    }
                    //FontSize = cDraftingConfig.Drafting[i].PartNumberFontSize;
                }
                else if (cDraftingConfig.Drafting[i].TolTitle0PosText == KeyToCompare)
                {
                    ptStr = cDraftingConfig.Drafting[i].TolTitle0Pos;
                    FontSize = cDraftingConfig.Drafting[i].TolFontSize;
                }
                else if (cDraftingConfig.Drafting[i].TolTitle1PosText == KeyToCompare)
                {
                    ptStr = cDraftingConfig.Drafting[i].TolTitle1Pos;
                    FontSize = cDraftingConfig.Drafting[i].TolFontSize;
                }
                else if (cDraftingConfig.Drafting[i].TolTitle2PosText == KeyToCompare)
                {
                    ptStr = cDraftingConfig.Drafting[i].TolTitle2Pos;
                    FontSize = cDraftingConfig.Drafting[i].TolFontSize;
                }
                else if (cDraftingConfig.Drafting[i].TolTitle3PosText == KeyToCompare)
                {
                    ptStr = cDraftingConfig.Drafting[i].TolTitle3Pos;
                    FontSize = cDraftingConfig.Drafting[i].TolFontSize;
                }
                else if (cDraftingConfig.Drafting[i].TolTitle4PosText == KeyToCompare)
                {
                    ptStr = cDraftingConfig.Drafting[i].TolTitle4Pos;
                    FontSize = cDraftingConfig.Drafting[i].TolFontSize;
                }
                else if (cDraftingConfig.Drafting[i].AngleTitlePosText == KeyToCompare)
                {
                    ptStr = cDraftingConfig.Drafting[i].AngleTitlePos;
                    FontSize = cDraftingConfig.Drafting[i].TolFontSize;
                }
                else if (cDraftingConfig.Drafting[i].TolValue0PosText == KeyToCompare)
                {
                    ptStr = cDraftingConfig.Drafting[i].TolValue0Pos;
                    FontSize = cDraftingConfig.Drafting[i].TolFontSize;
                }
                else if (cDraftingConfig.Drafting[i].TolValue1PosText == KeyToCompare)
                {
                    ptStr = cDraftingConfig.Drafting[i].TolValue1Pos;
                    FontSize = cDraftingConfig.Drafting[i].TolFontSize;
                }
                else if (cDraftingConfig.Drafting[i].TolValue2PosText == KeyToCompare)
                {
                    ptStr = cDraftingConfig.Drafting[i].TolValue2Pos;
                    FontSize = cDraftingConfig.Drafting[i].TolFontSize;
                }
                else if (cDraftingConfig.Drafting[i].TolValue3PosText == KeyToCompare)
                {
                    ptStr = cDraftingConfig.Drafting[i].TolValue3Pos;
                    FontSize = cDraftingConfig.Drafting[i].TolFontSize;
                }
                else if (cDraftingConfig.Drafting[i].TolValue4PosText == KeyToCompare)
                {
                    ptStr = cDraftingConfig.Drafting[i].TolValue4Pos;
                    FontSize = cDraftingConfig.Drafting[i].TolFontSize;
                }
                else if (cDraftingConfig.Drafting[i].AngleValuePosText == KeyToCompare)
                {
                    ptStr = cDraftingConfig.Drafting[i].AngleValuePos;
                    FontSize = cDraftingConfig.Drafting[i].TolFontSize;
                }
                else if (cDraftingConfig.Drafting[i].PreparedPosText == KeyToCompare)
                {
                    ptStr = cDraftingConfig.Drafting[i].PreparedPos;
                    FontSize = (Convert.ToDouble(cDraftingConfig.Drafting[i].RevDateFontSize) - (Convert.ToDouble(cDraftingConfig.Drafting[i].MatMinFontSize)) * 0.1).ToString();
                    //FontSize = cDraftingConfig.Drafting[i].TolFontSize;
                }
                else if (cDraftingConfig.Drafting[i].ReviewedPosText == KeyToCompare)
                {
                    ptStr = cDraftingConfig.Drafting[i].ReviewedPos;
                    FontSize = (Convert.ToDouble(cDraftingConfig.Drafting[i].RevDateFontSize) - (Convert.ToDouble(cDraftingConfig.Drafting[i].MatMinFontSize)) * 0.1).ToString();
                    //FontSize = cDraftingConfig.Drafting[i].TolFontSize;
                }
                else if (cDraftingConfig.Drafting[i].ApprovedPosText == KeyToCompare)
                {
                    ptStr = cDraftingConfig.Drafting[i].ApprovedPos;
                    FontSize = (Convert.ToDouble(cDraftingConfig.Drafting[i].RevDateFontSize) - (Convert.ToDouble(cDraftingConfig.Drafting[i].MatMinFontSize)) * 0.1).ToString();
                    //FontSize = cDraftingConfig.Drafting[i].TolFontSize;
                }
                else if (cDraftingConfig.Drafting[i].InstructionPosText == KeyToCompare)
                {
                    ptStr = cDraftingConfig.Drafting[i].InstructionPos;
                    FontSize = cDraftingConfig.Drafting[i].TolFontSize;
                }

                TextPos.X = Convert.ToDouble(ptStr.Split(',')[0]);
                TextPos.Y = Convert.ToDouble(ptStr.Split(',')[1]);
                TextPos.Z = Convert.ToDouble(ptStr.Split(',')[2]);
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        public static bool GetTextPos(Drafting sDrafting, string KeyToCompare, string ValueToCompare, string PartUnits, out Point3d TextPos, out string FontSize)
        {
            TextPos = new Point3d();
            FontSize = "";
            try
            {
                string ptStr = "";
                if (sDrafting.PageNumberPosText == KeyToCompare)
                {
                    ptStr = sDrafting.PageNumberPos;
                    FontSize = sDrafting.PageNumberFontSize;
                }
                else if (sDrafting.ERPcodePosText == KeyToCompare)
                {
                    ptStr = sDrafting.ERPcodePos;
                    FontSize = "1.5";
                }
                else if (sDrafting.ERPRevPosText == KeyToCompare)
                {
                    ptStr = sDrafting.ERPRevPos;
                    FontSize = "3";
                }
                else if (sDrafting.PartDescriptionPosText == KeyToCompare)
                {
                    //ptStr = cDraftingConfig.Drafting[i].PartDescriptionPos;
                    //FontSize = cDraftingConfig.Drafting[i].PartDescriptionFontSize;
                    ptStr = sDrafting.PartDescriptionPos;
                    int Subtraction = ValueToCompare.Length - 12;
                    if (Subtraction <= 0)
                    {
                        FontSize = sDrafting.PartDescriptionFontSize;
                    }
                    else
                    {
                        if (Subtraction <= 3)
                        {
                            FontSize = (Convert.ToDouble(sDrafting.PartDescriptionFontSize) - (Convert.ToDouble(sDrafting.MatMinFontSize))).ToString();
                        }
                        else if (Subtraction > 3 && Subtraction <= 5)
                        {
                            FontSize = (Convert.ToDouble(sDrafting.PartDescriptionFontSize) - (Convert.ToDouble(sDrafting.MatMinFontSize)) * 2).ToString();
                        }
                        else if (Subtraction > 5 && Subtraction <= 7)
                        {
                            FontSize = (Convert.ToDouble(sDrafting.PartDescriptionFontSize) - (Convert.ToDouble(sDrafting.MatMinFontSize)) * 3).ToString();
                        }
                        else if (Subtraction > 7)
                        {
                            FontSize = (Convert.ToDouble(sDrafting.PartDescriptionFontSize) - (Convert.ToDouble(sDrafting.MatMinFontSize)) * 3.5).ToString();
                        }

                        if (Convert.ToDouble(FontSize) <= 0)
                        {
                            FontSize = sDrafting.MatMinFontSize;
                        }
                    }
                }
                else if (sDrafting.AuthDatePosText == KeyToCompare)
                {
                    ptStr = sDrafting.AuthDatePos;
                    FontSize = (Convert.ToDouble(sDrafting.AuthDateFontSize) - (Convert.ToDouble(sDrafting.MatMinFontSize)) * 0.3).ToString();
                }
                else if (sDrafting.MaterialPosText == KeyToCompare)
                {
                    ptStr = sDrafting.MaterialPos;
                    int Subtraction = ValueToCompare.Length - 5;
                    if (Subtraction <= 0)
                    {
                        FontSize = sDrafting.MaterialFontSize;
                    }
                    else
                    {
                        if (Subtraction <= 3)
                        {
                            FontSize = (Convert.ToDouble(sDrafting.MaterialFontSize) - (Convert.ToDouble(sDrafting.MatMinFontSize))).ToString();
                        }
                        else if (Subtraction > 3 && Subtraction <= 5)
                        {
                            FontSize = (Convert.ToDouble(sDrafting.MaterialFontSize) - (Convert.ToDouble(sDrafting.MatMinFontSize) * 2)).ToString();
                        }
                        else if (Subtraction > 5 && Subtraction <= 10)
                        {
                            FontSize = (Convert.ToDouble(sDrafting.PartDescriptionFontSize) - (Convert.ToDouble(sDrafting.MatMinFontSize)) * 3).ToString();
                        }
                        else if (Subtraction > 10)
                        {
                            FontSize = (Convert.ToDouble(sDrafting.PartDescriptionFontSize) - (Convert.ToDouble(sDrafting.MatMinFontSize)) * 3.5).ToString();
                        }

                        if (Convert.ToDouble(FontSize) <= 0)
                        {
                            FontSize = sDrafting.MatMinFontSize;
                        }
                    }
                }
                else if (sDrafting.PartNumberPosText == KeyToCompare)
                {
                    ptStr = sDrafting.PartNumberPos;
                    int Subtraction = ValueToCompare.Length - 13;
                    if (Subtraction <= 0)
                    {
                        FontSize = sDrafting.PartNumberFontSize;
                    }
                    else
                    {
                        if (Subtraction <= 3)
                        {
                            FontSize = (Convert.ToDouble(sDrafting.PartNumberFontSize) - (Convert.ToDouble(sDrafting.MatMinFontSize))).ToString();
                        }
                        else if (Subtraction > 3 && Subtraction <= 6)
                        {
                            FontSize = (Convert.ToDouble(sDrafting.PartNumberFontSize) - (Convert.ToDouble(sDrafting.MatMinFontSize)) * 2).ToString();
                        }
                        else if (Subtraction > 6)
                        {
                            FontSize = (Convert.ToDouble(sDrafting.PartNumberFontSize) - (Convert.ToDouble(sDrafting.MatMinFontSize)) * 3).ToString();
                        }

                        if (Convert.ToDouble(FontSize) <= 0)
                        {
                            FontSize = sDrafting.MatMinFontSize;
                        }
                    }
                    //FontSize = cDraftingConfig.Drafting[i].PartNumberFontSize;
                }
                else if (sDrafting.PartUnitPosText == KeyToCompare)
                {
                    ptStr = sDrafting.PartUnitPos;
                    FontSize = sDrafting.PartUnitFontSize;
                }
                else if (sDrafting.ProcNamePosText == KeyToCompare)
                {
                    ptStr = sDrafting.ProcNamePos;
                    FontSize = sDrafting.ProcNameFontSize;
                }
                else if (sDrafting.RevDateStartPosText == KeyToCompare)
                {
                    ptStr = sDrafting.RevDateStartPos;
                    FontSize = (Convert.ToDouble(sDrafting.RevDateFontSize) - (Convert.ToDouble(sDrafting.MatMinFontSize)) * 0.3).ToString();
                }
                else if (sDrafting.RevStartPosText == KeyToCompare)
                {
                    ptStr = sDrafting.RevStartPos;
                    FontSize = sDrafting.RevFontSize;
                }
                else if (sDrafting.SecondPageNumberPosText == KeyToCompare)
                {
                    ptStr = sDrafting.SecondPageNumberPos;
                    FontSize = sDrafting.PageNumberFontSize;
                }
                else if (sDrafting.SecondPartNumberPosText == KeyToCompare)
                {
                    ptStr = sDrafting.SecondPartNumberPos;
                    int Subtraction = ValueToCompare.Length - 13;
                    if (Subtraction <= 0)
                    {
                        FontSize = sDrafting.PartNumberFontSize;
                    }
                    else
                    {
                        if (Subtraction <= 3)
                        {
                            FontSize = (Convert.ToDouble(sDrafting.PartNumberFontSize) - (Convert.ToDouble(sDrafting.MatMinFontSize))).ToString();
                        }
                        else if (Subtraction > 3 && Subtraction <= 6)
                        {
                            FontSize = (Convert.ToDouble(sDrafting.PartNumberFontSize) - (Convert.ToDouble(sDrafting.MatMinFontSize)) * 2).ToString();
                        }
                        else if (Subtraction > 6)
                        {
                            FontSize = (Convert.ToDouble(sDrafting.PartNumberFontSize) - (Convert.ToDouble(sDrafting.MatMinFontSize)) * 3).ToString();
                        }

                        if (Convert.ToDouble(FontSize) <= 0)
                        {
                            FontSize = sDrafting.MatMinFontSize;
                        }
                        //FontSize = (Convert.ToDouble(cDraftingConfig.Drafting[i].PartNumberFontSize) - (Convert.ToDouble(cDraftingConfig.Drafting[i].MatMinFontSize) * Subtraction)).ToString();
                        //if (Convert.ToDouble(FontSize) <= 0)
                        //{
                        //    FontSize = cDraftingConfig.Drafting[i].MatMinFontSize;
                        //}
                    }
                    //FontSize = cDraftingConfig.Drafting[i].PartNumberFontSize;
                }
                else if (sDrafting.TolTitle0PosText == KeyToCompare)
                {
                    ptStr = sDrafting.TolTitle0Pos;
                    FontSize = sDrafting.TolFontSize;
                }
                else if (sDrafting.TolTitle1PosText == KeyToCompare)
                {
                    ptStr = sDrafting.TolTitle1Pos;
                    FontSize = sDrafting.TolFontSize;
                }
                else if (sDrafting.TolTitle2PosText == KeyToCompare)
                {
                    ptStr = sDrafting.TolTitle2Pos;
                    FontSize = sDrafting.TolFontSize;
                }
                else if (sDrafting.TolTitle3PosText == KeyToCompare)
                {
                    ptStr = sDrafting.TolTitle3Pos;
                    FontSize = sDrafting.TolFontSize;
                }
                else if (sDrafting.TolTitle4PosText == KeyToCompare)
                {
                    ptStr = sDrafting.TolTitle4Pos;
                    FontSize = sDrafting.TolFontSize;
                }
                else if (sDrafting.AngleTitlePosText == KeyToCompare)
                {
                    ptStr = sDrafting.AngleTitlePos;
                    FontSize = sDrafting.TolFontSize;
                }
                else if (sDrafting.TolValue0PosText == KeyToCompare)
                {
                    ptStr = sDrafting.TolValue0Pos;
                    FontSize = sDrafting.TolFontSize;
                }
                else if (sDrafting.TolValue1PosText == KeyToCompare)
                {
                    ptStr = sDrafting.TolValue1Pos;
                    FontSize = sDrafting.TolFontSize;
                }
                else if (sDrafting.TolValue2PosText == KeyToCompare)
                {
                    ptStr = sDrafting.TolValue2Pos;
                    FontSize = sDrafting.TolFontSize;
                }
                else if (sDrafting.TolValue3PosText == KeyToCompare)
                {
                    ptStr = sDrafting.TolValue3Pos;
                    FontSize = sDrafting.TolFontSize;
                }
                else if (sDrafting.TolValue4PosText == KeyToCompare)
                {
                    ptStr = sDrafting.TolValue4Pos;
                    FontSize = sDrafting.TolFontSize;
                }
                else if (sDrafting.AngleValuePosText == KeyToCompare)
                {
                    ptStr = sDrafting.AngleValuePos;
                    FontSize = sDrafting.TolFontSize;
                }
                else if (sDrafting.PreparedPosText == KeyToCompare)
                {
                    ptStr = sDrafting.PreparedPos;
                    FontSize = (Convert.ToDouble(sDrafting.RevDateFontSize) - (Convert.ToDouble(sDrafting.MatMinFontSize)) * 0.1).ToString();
                    //FontSize = cDraftingConfig.Drafting[i].TolFontSize;
                }
                else if (sDrafting.ReviewedPosText == KeyToCompare)
                {
                    ptStr = sDrafting.ReviewedPos;
                    FontSize = (Convert.ToDouble(sDrafting.RevDateFontSize) - (Convert.ToDouble(sDrafting.MatMinFontSize)) * 0.1).ToString();
                    //FontSize = cDraftingConfig.Drafting[i].TolFontSize;
                }
                else if (sDrafting.ApprovedPosText == KeyToCompare)
                {
                    ptStr = sDrafting.ApprovedPos;
                    FontSize = (Convert.ToDouble(sDrafting.RevDateFontSize) - (Convert.ToDouble(sDrafting.MatMinFontSize)) * 0.1).ToString();
                    //FontSize = cDraftingConfig.Drafting[i].TolFontSize;
                }
                else if (sDrafting.InstructionPosText == KeyToCompare)
                {
                    ptStr = sDrafting.InstructionPos;
                    FontSize = sDrafting.TolFontSize;
                }
                else if (sDrafting.InstApprovedPosText == KeyToCompare)
                {
                    ptStr = sDrafting.InstApprovedPos;
                    FontSize = sDrafting.TolFontSize;
                }

                TextPos.X = Convert.ToDouble(ptStr.Split(',')[0]);
                TextPos.Y = Convert.ToDouble(ptStr.Split(',')[1]);
                TextPos.Z = Convert.ToDouble(ptStr.Split(',')[2]);

                if (PartUnits == "inch")
                {
                    TextPos.X = TextPos.X / 25.4;
                    TextPos.Y = TextPos.Y / 25.4;
                    FontSize = (Convert.ToDouble(FontSize) / 25.4).ToString();
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        public static bool RenameSheet(int SheetCount, Tag[] SheetTagAry)
        {
            try
            {
                int count = 2;//續頁從S2開始
                for (int i = 0; i < SheetCount; i++)
                {
                    //輪巡每個Sheet
                    NXOpen.Drawings.DrawingSheet CurrentSheet = (NXOpen.Drawings.DrawingSheet)NXObjectManager.Get(SheetTagAry[i]);
                    CurrentSheet.Open();
                    NXObject[] SheetObj = CaxME.FindObjectsInView(CurrentSheet.View.Tag).ToArray();
                    string SheetType = "";
                    foreach (NXObject obj in SheetObj)
                    {
                        try
                        {
                            SheetType = obj.GetStringAttribute("SheetType");
                            break;
                        }
                        catch (System.Exception ex)
                        {
                            continue;
                        }
                    }
                    if (SheetType == "1")
                    {
                        CaxME.SheetRename(CurrentSheet, "S1");
                    }
                    else
                    {
                        CaxME.SheetRename(CurrentSheet, "S" + count.ToString());
                        count++;
                    }
                }
            }
            catch (System.Exception ex)
            {
                MessageBox.Show(ex.ToString());
                return false;
            }
            return true;
        }

        public static bool WriteSheetData(NXOpen.Annotations.Note[] NotesAry, Drafting sDrafting, string AttTitle, string AttValue, string PartUnits, string CusRevText = "")
        {
            try
            {
                string PartNumCusRev = "";
                if (CusRevText != "")
                {
                    PartNumCusRev = AttValue + "(" + CusRevText + ")";
                }
                else
                {
                    PartNumCusRev = AttValue;
                }

                string NoteValue = "";
                foreach (NXOpen.Annotations.Note singleNote in NotesAry)
                {
                    try
                    {
                        NoteValue = singleNote.GetStringAttribute(AttTitle);
                    }
                    catch (System.Exception ex)
                    {
                        continue;
                    }
                    if (NoteValue != "")
                    {
                        CaxPublic.DelectObject(singleNote);
                    }
                }

                Point3d TextPt = new Point3d();
                string FontSize = "";
                GetTextPos(sDrafting, AttTitle, PartNumCusRev, PartUnits, out TextPt, out FontSize);
                
                if (AttTitle == InspectionDlg.TablePosi.ERPRevPos || AttTitle == InspectionDlg.TablePosi.ERPcodePos)
                {
                    InsertERPNote(AttTitle, PartNumCusRev, FontSize, TextPt);
                }
                else
                {
                    InsertNote(AttTitle, PartNumCusRev, FontSize, TextPt);
                }
                
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        public static bool WriteHistoryOfRevsion(Drafting sDrafting, string AttTitle, string AttValue, string PartUnits, int RevCount, string RevRowHeight)
        {
            try
            {
                Point3d TextPt = new Point3d();
                string FontSize = "";
                GetTextPos(sDrafting, AttTitle, AttValue, PartUnits, out TextPt, out FontSize);

                if (RevCount != 0)
                {
                    if (PartUnits == "inch")
                    {
                        TextPt.Y = TextPt.Y + (Convert.ToDouble(RevRowHeight) / 25.4 * RevCount);
                    }
                    else
                    {
                        TextPt.Y = TextPt.Y + (Convert.ToDouble(RevRowHeight) * RevCount);
                    }
                }
                Functions.InsertNote(AttTitle, AttValue, FontSize, TextPt, RevCount.ToString());
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        public static bool GetDraftingConfig(DraftingConfig cDraftingConfig, Tag SheetTagAry, string PartUnits, out Drafting sDrafting)
        {
            sDrafting = new Drafting();
            try
            {
                for (int i = 0; i < cDraftingConfig.Drafting.Count; i++)
                {
                    NXOpen.Drawings.DrawingSheet CurrentSheet = (NXOpen.Drawings.DrawingSheet)NXObjectManager.Get(SheetTagAry);
                    string SheetSize = cDraftingConfig.Drafting[i].SheetSize;
                    if (PartUnits == "mm")
                    {
                        if (Math.Ceiling(CurrentSheet.Height) != Convert.ToDouble(SheetSize.Split(',')[0]) || Math.Ceiling(CurrentSheet.Length) != Convert.ToDouble(SheetSize.Split(',')[1]))
                        {
                            continue;
                        }
                    }
                    else
                    {
                        if (Math.Ceiling(CurrentSheet.Height * 25.4) != Convert.ToDouble(SheetSize.Split(',')[0]) || Math.Ceiling(CurrentSheet.Length * 25.4) != Convert.ToDouble(SheetSize.Split(',')[1]))
                        {
                            continue;
                        }
                    }
                    sDrafting = cDraftingConfig.Drafting[i];
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        public static bool DeleteNote(Part workPart, NXOpen.Annotations.Note[] NotesAry)
        {
            try
            {
                foreach (NXOpen.Annotations.Note singleNote in NotesAry)
                {
                    string NoteValue1 = "";
                    #region 處理0
                    try
                    {
                        NoteValue1 = singleNote.GetStringAttribute(InspectionDlg.TablePosi.TolTitle0Pos);
                    }
                    catch (System.Exception ex)
                    {

                    }
                    if (NoteValue1 != "")
                    {
                        CaxPublic.DelectObject(singleNote);
                        workPart.DeleteAttributeByTypeAndTitle(NXObject.AttributeType.String, InspectionDlg.TablePosi.TolValue0Pos);
                        continue;
                    }
                    try
                    {
                        NoteValue1 = singleNote.GetStringAttribute(InspectionDlg.TablePosi.TolValue0Pos);
                    }
                    catch (System.Exception ex)
                    {

                    }
                    if (NoteValue1 != "")
                    {
                        CaxPublic.DelectObject(singleNote);
                        continue;
                    }
                    #endregion
                    #region 處理1
                    try
                    {
                        NoteValue1 = singleNote.GetStringAttribute(InspectionDlg.TablePosi.TolTitle1Pos);
                    }
                    catch (System.Exception ex)
                    {

                    }
                    if (NoteValue1 != "")
                    {
                        CaxPublic.DelectObject(singleNote);
                        workPart.DeleteAttributeByTypeAndTitle(NXObject.AttributeType.String, InspectionDlg.TablePosi.TolValue1Pos);
                        continue;
                    }
                    try
                    {
                        NoteValue1 = singleNote.GetStringAttribute(InspectionDlg.TablePosi.TolValue1Pos);
                    }
                    catch (System.Exception ex)
                    {

                    }
                    if (NoteValue1 != "")
                    {
                        CaxPublic.DelectObject(singleNote);
                        continue;
                    }
                    #endregion
                    #region 處理2
                    try
                    {
                        NoteValue1 = singleNote.GetStringAttribute(InspectionDlg.TablePosi.TolTitle2Pos);
                    }
                    catch (System.Exception ex)
                    {

                    }
                    if (NoteValue1 != "")
                    {
                        CaxPublic.DelectObject(singleNote);
                        workPart.DeleteAttributeByTypeAndTitle(NXObject.AttributeType.String, InspectionDlg.TablePosi.TolValue2Pos);
                        continue;
                    }
                    try
                    {
                        NoteValue1 = singleNote.GetStringAttribute(InspectionDlg.TablePosi.TolValue2Pos);
                    }
                    catch (System.Exception ex)
                    {

                    }
                    if (NoteValue1 != "")
                    {
                        CaxPublic.DelectObject(singleNote);
                        continue;
                    }
                    #endregion
                    #region 處理3
                    try
                    {
                        NoteValue1 = singleNote.GetStringAttribute(InspectionDlg.TablePosi.TolTitle3Pos);
                    }
                    catch (System.Exception ex)
                    {

                    }
                    if (NoteValue1 != "")
                    {
                        CaxPublic.DelectObject(singleNote);
                        workPart.DeleteAttributeByTypeAndTitle(NXObject.AttributeType.String, InspectionDlg.TablePosi.TolValue3Pos);
                        continue;
                    }
                    try
                    {
                        NoteValue1 = singleNote.GetStringAttribute(InspectionDlg.TablePosi.TolValue3Pos);
                    }
                    catch (System.Exception ex)
                    {

                    }
                    if (NoteValue1 != "")
                    {
                        CaxPublic.DelectObject(singleNote);
                        continue;
                    }
                    #endregion
                    #region 處理4
                    try
                    {
                        NoteValue1 = singleNote.GetStringAttribute(InspectionDlg.TablePosi.TolTitle4Pos);
                    }
                    catch (System.Exception ex)
                    {

                    }
                    if (NoteValue1 != "")
                    {
                        CaxPublic.DelectObject(singleNote);
                        workPart.DeleteAttributeByTypeAndTitle(NXObject.AttributeType.String, InspectionDlg.TablePosi.TolValue4Pos);
                        continue;
                    }
                    try
                    {
                        NoteValue1 = singleNote.GetStringAttribute(InspectionDlg.TablePosi.TolValue4Pos);
                    }
                    catch (System.Exception ex)
                    {

                    }
                    if (NoteValue1 != "")
                    {
                        CaxPublic.DelectObject(singleNote);
                        continue;
                    }
                    #endregion
                    #region 處理角度
                    try
                    {
                        NoteValue1 = singleNote.GetStringAttribute(InspectionDlg.TablePosi.AngleTitlePos);
                    }
                    catch (System.Exception ex)
                    {

                    }
                    if (NoteValue1 != "")
                    {
                        CaxPublic.DelectObject(singleNote);
                        workPart.DeleteAttributeByTypeAndTitle(NXObject.AttributeType.String, InspectionDlg.TablePosi.AngleTitlePos);
                        continue;
                    }
                    try
                    {
                        NoteValue1 = singleNote.GetStringAttribute(InspectionDlg.TablePosi.AngleValuePos);
                    }
                    catch (System.Exception ex)
                    {

                    }
                    if (NoteValue1 != "")
                    {
                        CaxPublic.DelectObject(singleNote);
                        continue;
                    }
                    #endregion
                    #region 處理製圖
                    try
                    {
                        NoteValue1 = singleNote.GetStringAttribute(InspectionDlg.TablePosi.PreparedPos);
                    }
                    catch (System.Exception ex)
                    {

                    }
                    if (NoteValue1 != "")
                    {
                        CaxPublic.DelectObject(singleNote);
                        workPart.DeleteAttributeByTypeAndTitle(NXObject.AttributeType.String, InspectionDlg.TablePosi.PreparedPos);
                        continue;
                    }
                    #endregion
                    #region 處理校對
                    try
                    {
                        NoteValue1 = singleNote.GetStringAttribute(InspectionDlg.TablePosi.ReviewedPos);
                    }
                    catch (System.Exception ex)
                    {

                    }
                    if (NoteValue1 != "")
                    {
                        CaxPublic.DelectObject(singleNote);
                        workPart.DeleteAttributeByTypeAndTitle(NXObject.AttributeType.String, InspectionDlg.TablePosi.ReviewedPos);
                        continue;
                    }
                    #endregion
                    #region 處理審核
                    try
                    {
                        NoteValue1 = singleNote.GetStringAttribute(InspectionDlg.TablePosi.ApprovedPos);
                    }
                    catch (System.Exception ex)
                    {

                    }
                    if (NoteValue1 != "")
                    {
                        CaxPublic.DelectObject(singleNote);
                        workPart.DeleteAttributeByTypeAndTitle(NXObject.AttributeType.String, InspectionDlg.TablePosi.ApprovedPos);
                        continue;
                    }
                    #endregion
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }

        public static bool GetListRevIns(NXOpen.Annotations.Note[] NotesAry, int RevCount, out List<string> ListRev, out List<NXOpen.Annotations.Note> ListInstruction)
        {
            ListRev = new List<string>(); 
            ListInstruction = new List<NXOpen.Annotations.Note>();
            try
            {
                foreach (NXOpen.Annotations.Note singleNote in NotesAry)
                {
                    try
                    {
                        ListRev.Add(singleNote.GetStringAttribute(InspectionDlg.TablePosi.RevStartPos));
                        RevCount++;
                    }
                    catch (System.Exception ex)
                    { }

                    try
                    {
                        singleNote.GetStringAttribute(InspectionDlg.TablePosi.InstructionPos);
                        ListInstruction.Add(singleNote);
                    }
                    catch (System.Exception ex)
                    { }
                }
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
