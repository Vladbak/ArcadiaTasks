using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TextImages
{
    public static class Constants
    {

        public static int VerticalDetalization=10;
        public static int HorizontalDetalization = 10;
        public static int MaxValueOfRGB = 256;

        static string StringOfSymbols = "@#%xo;:,. ";
        public static char[] ArrayOfDrawingSymbols = StringOfSymbols.ToCharArray();

        //number of different symbols which i'm using for drawing with
        public static int NumofSymbolsForDrawing = ArrayOfDrawingSymbols.Length;
        //lenght of range. RGB value can be from 0 to 256. I'll separate all pixels to several ranges
        //and bind symbols to those ranges. The darker range - the darker ascii symbol.
        public static double FrameForEachSymbol = Math.Ceiling((float)MaxValueOfRGB / (float)NumofSymbolsForDrawing);

        
    }
}
