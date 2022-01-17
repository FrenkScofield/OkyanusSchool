using OkyanusSchool.Models.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OkyanusSchool.Models.VM
{
    public class OgrenciListVM
    {
        public OgrenciBilgi[] OgrenciBilgis { get; set; }

        public OgrenciBilgi OgrenciBilgi { get; set; }
        public IEnumerable<OgrenciBilgi> OgrenciBilgisl { get; set; }
    }
}
