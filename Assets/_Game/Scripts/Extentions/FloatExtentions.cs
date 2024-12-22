using UnityEngine;

namespace MH.Extentions
{
    public static class FloatExtentions
    {
        public static int ToMiniSeconds(float floatValueSeconds)
        {
            return Mathf.RoundToInt(floatValueSeconds * 1000);     
        }
    }
}