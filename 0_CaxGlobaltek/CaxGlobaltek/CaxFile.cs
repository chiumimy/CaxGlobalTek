using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Newtonsoft.Json;
using NXOpen;
using NXOpen.UF;
using System.Collections;
using NXOpen.Utilities;
using System.Windows.Forms;

namespace CaxGlobaltek
{
    public class CaxFile
    {
        public static bool WriteJsonFileData(string jsonPath, object jsonObject)
        {
            try
            {
                File.WriteAllText(jsonPath, JsonConvert.SerializeObject(jsonObject, Formatting.Indented));
            }
            catch (System.Exception ex)
            {
                return false;
            }
            return true;
        }
    }
}
