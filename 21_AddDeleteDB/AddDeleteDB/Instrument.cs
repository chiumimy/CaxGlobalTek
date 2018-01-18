using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using CaxGlobaltek;

namespace AddDeleteDB
{
    public class Instrument
    {
        private static ISession session = MyHibernateHelper.SessionFactory.OpenSession();
        public static bool GetInstrumentData(out List<AddDeleteDB.Form1.InstrumentData> listInstrument)
        {
            listInstrument = new List<AddDeleteDB.Form1.InstrumentData>();
            try
            {
                IList<Sys_Instrument> sysInstrument = session.QueryOver<Sys_Instrument>().List<Sys_Instrument>();
                foreach (Sys_Instrument i in sysInstrument)
                {
                    AddDeleteDB.Form1.InstrumentData sInstrumentData = new AddDeleteDB.Form1.InstrumentData();
                    sInstrumentData.instrumentColor = i.instrumentColor;
                    sInstrumentData.instrumentName = i.instrumentName;
                    sInstrumentData.instrumentEngName = i.instrumentEngName;
                    listInstrument.Add(sInstrumentData);
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
