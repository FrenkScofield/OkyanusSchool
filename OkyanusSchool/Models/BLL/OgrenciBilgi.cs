using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OkyanusSchool.Models.BLL
{
    public class OgrenciBilgi
    {
        public int ID { get; set; }

        public string AD { get; set; }

        public string SOYAD { get; set; }

        public string TC_KIMLIK { get; set; }

        public DateTime DOGUM_TARIHI { get; set; }

        public string DOGUM_YERI { get; set; }

        public string CEP_TELEFONU { get; set; }

        public string EV_TELEFONU { get; set; }

        public string EMAIL { get; set; }

        public string UYRUK { get; set; }

        public DateTime GUNCEL_TARIH { get; set; }

        public DateTime ILK_KAYIT { get; set; }

    }
}
