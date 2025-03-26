using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PackingGM.Model
{
    /// <summary>
    /// Спецификация упаковки
    /// </summary>
    public class SPU : NMK
    {
        public ICollection<GMNumber> GMNumbers { get; set; }
        public ICollection<SPUVersion> SPUVersions { get; set; }
    }
}
