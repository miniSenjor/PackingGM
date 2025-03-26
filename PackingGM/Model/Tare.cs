using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PackingGM.Model
{
    /// <summary>
    /// Тара
    /// </summary>
    public class Tare : NMK
    {
        public ICollection<SPUTare> SPUTares { get; set; }
    }
}
