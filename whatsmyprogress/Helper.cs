using System;
using System.Collections.Generic;
using System.Text;

namespace whatsmyprogress
{
    public static class Helper
    {
        public static int GetDefaultProjectId()
        {
            if (Program.GetConfig()[Constants.DEFAULT_PROJECTID] != null)
                return Convert.ToInt32(Program.GetConfig()[Constants.DEFAULT_PROJECTID]);
            else
                return 0;
        }
    }
}
