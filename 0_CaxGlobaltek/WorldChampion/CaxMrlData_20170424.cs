using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace WorldChampion.MRL
{
    public class DataObject
    {
        protected bool useProperty = true; //指示用屬性(Property)還是欄位(Field)當做該類別與外界交換資料的介面

        //傳回參數名稱的串列
        public List<string> GetAllParameterName()
        {
            Type classType = this.GetType();
            BindingFlags bindingFlags = BindingFlags.Public | BindingFlags.Instance;
            MemberInfo[] memberInfoAry;
            if (useProperty)
            {
                memberInfoAry = (MemberInfo[])classType.GetProperties(bindingFlags);
            }
            else
            {
                memberInfoAry = (MemberInfo[])classType.GetFields(bindingFlags);
            }
            List<string> memberNameList = new List<string>();
            for (int i = 0; i < memberInfoAry.Length; i++)
            {
                memberNameList.Add(memberInfoAry[i].Name);
            }
            return memberNameList;
        }
        //傳回指定參數名稱的值
        public object GetPropertyValue(string propertyName)
        {
            object propertyValue = null;
            PropertyInfo propertyInfo = this.GetType().GetProperty(propertyName);
            if (propertyInfo != null)
            {
                propertyValue = propertyInfo.GetValue(this, null);
            }
            return propertyValue;
        }
        //設定指定參數名稱的值
        public bool SetPropertyValue(string propertyName, string propertyValue)
        {
            PropertyInfo propertyInfo = this.GetType().GetProperty(propertyName);
            if (propertyInfo != null)
            {
                propertyInfo.SetValue(this, propertyValue, null);
                return true;
            }

            return false;
        }

    }

    #region 標準零件整體資訊
    public class StdpartSupplier
    {
        public StdpartSupplier()
        {
            STDPART_CLASS_ARY = new List<StdpartClass>();
        }
        public string SUPPLIER_NAME { get; set; }
        public List<StdpartClass> STDPART_CLASS_ARY { get; set; }
    }
    public class StdpartClass
    {
        public StdpartClass()
        {
            STDPART_ARY = new List<Stdpart>();
        }
        public string STDPART_CLASS_NAME { get; set; }
        public string STDPART_CLASS_CODE { get; set; }
        public List<Stdpart> STDPART_ARY { get; set; }
    }
    public class Stdpart
    {
        public string STDPART_NAME { get; set; }
        public string STDPART_NAME_CODE { get; set; }
        public string STDPART_PREVIEW_PIC { get; set; }
    }
    #endregion

    #region 標準零件詳細資訊
    //標準零件詳細資訊
    public class StdpartDetailData
    {
        public StdpartDetailData()
        {
            DESCRIPTION_NAME_ARY = new List<string>();
            DESCRIPTION_VALUE_ARY = new List<string>();
            TECH_PICTURE_ARY = new List<string>();
            SPEC_NAME_ARY = new List<string>();
            SPEC_GROUP_ARY = new List<SpecGroup>();
        }
        public string SUPPLIER { get; set; }
        public string STDPART_CLASS { get; set; }
        public string STDPART_CLASS_CODE { get; set; }
        public string STDPART_NAME { get; set; }
        public string STDPART_NAME_CODE { get; set; }
        public List<string> DESCRIPTION_NAME_ARY { get; set; }
        public List<string> DESCRIPTION_VALUE_ARY { get; set; }
        public string STDPART_PICTURE { get; set; }
        public string USING_EXAMPLE_PICETURE { get; set; }
        public List<string> TECH_PICTURE_ARY { get; set; }
        public string SPEC_PICTURE { get; set; }
        public List<string> SPEC_NAME_ARY { get; set; }
        public List<SpecGroup> SPEC_GROUP_ARY { get; set; }
    }
    //規格群組
    public class SpecGroup
    {
        public SpecGroup()
        {
            PART_SPEC_ARY = new List<OneStdpartSpec>();
        }
        public string SPEC_GROUP_NAME { get; set; }
        public List<OneStdpartSpec> PART_SPEC_ARY { get; set; }
    }
    //單一規格
    public class OneStdpartSpec
    {
        public OneStdpartSpec()
        {
            SPEC_VALUE_ARY = new List<string>();
        }
        public string STDPART_SPECNAME { get; set; }
        public List<string> SPEC_VALUE_ARY { get; set; }
    }
    #endregion

    #region 車削刀具數據
    //外徑車削刀桿
    public class ExternalHolderData : DataObject
    {
        public string SUPPLIER { get; set; }//顯示
        public string ISO_CODE { get; set; }
        public string ISO_SPEC { get; set; }
        public string PRODUCT_NO { get; set; }//顯示
        public string HAND_SIDE { get; set; }//顯示
        public string SHANK_H { get; set; }
        public string SHANK_W { get; set; }//顯示
        public string SHANK_L { get; set; }
        public string SHANK_L_NX { get; set; }//顯示
        public string FUNC_H { get; set; }
        public string FUNC_W { get; set; }
        public string FUNC_W_NX { get; set; }//顯示
        public string FUNC_L { get; set; }
        public string FUNC_L_NX { get; set; }//顯示
        public string HEAD_W { get; set; }
        public string OK_INSERT { get; set; }//顯示
        public string ERP_NO { get; set; }//顯示

    }
    //內徑車削刀桿
    public class InternalHolderData : DataObject
    {
        public string SUPPLIER { get; set; }//顯示
        public string ISO_CODE { get; set; }
        public string ISO_SPEC { get; set; }//顯示
        public string PRODUCT_NO { get; set; }//顯示
        public string HAND_SIDE { get; set; }
        public string SHANK_H { get; set; }
        public string SHANK_B { get; set; }
        public string SHANK_W { get; set; }
        //public string SHANK_L { get; set; }
        public string SHANK_L_NX { get; set; }
        //public string FUNC_H { get; set; }
        //public string FUNC_W { get; set; }
        public string FUNC_W_NX { get; set; }
        //public string FUNC_L { get; set; }
        public string FUNC_L_NX { get; set; }
        public string FUNC_L_A { get; set; }
        public string FUNC_L_B { get; set; }
        //public string HEAD_W { get; set; }
        public string HEAD_L { get; set; }
        public string CLEARANCE_W { get; set; }
        public string OK_INSERT { get; set; }
        public string MIN_DIAMETER { get; set; }
        public string ERP_NO { get; set; }

    }
    //廠商尺寸規格配置(用在登錄刀桿)
    public class SupplierDimConfig
    {
        public string SUPPLIER { get; set; }
        public string PRODUCT_CLASS { get; set; }
        public string DIM_NAME { get; set; }
        public string DISPLAY_ORDER { get; set; }
    }
    //車削刀片
    public class TurningInsertData : DataObject
    {
        public string SUPPLIER { get; set; }//顯示
        public string ISO_CODE { get; set; }
        public string ISO_SPEC { get; set; }//顯示
        public string PRODUCT_NO { get; set; }//顯示
        public string ERP_NO { get; set; }//顯示
        public string CHIP_BREAKER { get; set; }//顯示
        public string METHOD { get; set; }//顯示
        public string FN_MIN { get; set; }
        public string FN_MAX { get; set; }
        public string AP_MIN { get; set; }
        public string AP_MAX { get; set; }
        public string GRADE { get; set; }//顯示
        public string OK_HOLDER { get; set; }//顯示
    }
    //廠商刀具材質與切削材料配置
    public class SupplierGradeMaterialMapp
    {
        public string SUPPLIER { get; set; }
        public string GRADE { get; set; }
        public string P_CLASS { get; set; }
        public string M_CLASS { get; set; }
        public string K_CLASS { get; set; }
        public string N_CLASS { get; set; }
        public string S_CLASS { get; set; }
        public string H_CLASS { get; set; }
    }

    #endregion

    #region 銑削刀具數據
    public class MillingToolData : DataObject
    {
        public string CUTTER_ID;
        public string SUPPLIER;//
        public string CUTTER_NAME;//
        public string CUTTER_TYPE;//
        public string CUTTER_ERP_NO;//
        public string FLUTE_NUM;//
        public string GRADE;//
        public string DIAMETER;//
        public string FLUTE_LENGTH;//
        public string FLUTE_A_ANGLE;//
        public string FLUTE_B_ANGLE;//
        public string RADIUS;//
        public string NECK_DIAMETER;
        public string NECK_LENGTH;
        public string BETA_ANGLE;
        public string SHANK_DIAMETER;//
        public string LENGTH;//
        public string ALPHA_ANGLE;
        public string THREAD_SPEC;
        public string INSERT_SPEC;
        public string INSERT_ERP_NO;
        public string INSERT_GRADE;
        public string DESIGNATION; //
    }
    #endregion


}
