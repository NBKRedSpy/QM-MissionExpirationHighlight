using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace QM_MissionExpirationHighlight
{
    public static class Utility
    {
        public static Color ParseHtmlColor(string htmlColor, string fallbackColor)
        {
            bool isSuccess;
            Color color;

            isSuccess = ColorUtility.TryParseHtmlString(htmlColor, out color);

            if (isSuccess) return color;

            Debug.Log($"Unable to parse HTML color '{htmlColor}'.  Using the default of {fallbackColor}");

            isSuccess = ColorUtility.TryParseHtmlString(fallbackColor, out color);
            if (isSuccess) return color;

            //Default should always parse.
            throw new ApplicationException($"Unable to parse HTML color '{htmlColor}'");
        }
    }
}
