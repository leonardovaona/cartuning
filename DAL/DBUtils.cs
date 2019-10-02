using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class DBUtils
{
    public static object GetPropertyValue(object obj, string PropName)
    {
        Type objType = obj.GetType();
        System.Reflection.PropertyInfo pInfo = objType.GetProperty(PropName);
        object PropValue = pInfo.GetValue(obj, System.Reflection.BindingFlags.GetProperty, null, null, null);
        return PropValue;
    }

    public static int CalcularDigitoVerificador(string sCadena, int startIndex = 0)
    {
        int i = 1;
        int DV = startIndex + 1;
        foreach (char sChar in sCadena)
        {
            DV += System.Convert.ToInt32(sChar) * i;
            //DV += (AscW(sChar)) * i;
            i += 1;
        }
        return DV;
    }
}
