using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IOSelector
{
    #region IOSelector params
    public class Params
    {
        public String ExcelFilePath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop) + "\\IO.xlsx";
        public int OSAIInputStartCode = 1010;
        public int OSAIOutputStartCode = 1200;

        public int SettingsPerSection = 16;
    }
    #endregion

}
