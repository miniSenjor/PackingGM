using PackingGM.Model;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;

namespace PackingGM.ViewModel
{
    public class Testing : BaseModel
    {
        public Testing()
        {
            string s = NormalizeText("А");
            if (s == "A")
                Debug.Print("ОК" + s);
            else
                Debug.Print("Не ок" + s);
            
        }
    }
}
