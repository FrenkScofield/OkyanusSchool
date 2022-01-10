using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OkyanusSchool.Models.BLL;
using OkyanusSchool.Models.DAL;
using OkyanusSchool.Models.VM;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace OkyanusSchool.Areas.WebCms.Controllers
{
    [Area("WebCms")]
    [Route("WebCms/[controller]/[action]")]
    public class OgrenciBilgiController : Controller
    {
        private readonly MyContext _context;
        private readonly OgrenciListVM _ogrenciListVM;

        public OgrenciBilgiController(MyContext context, OgrenciListVM ogrenciListVM)
        {
            _context = context;
            _ogrenciListVM = ogrenciListVM;
        }
        public async Task<IActionResult> Index()
        {
            OgrenciBilgi[] ogrenciListesi = null;

            if (TempData["Orgs"] != null)
            {
                 ogrenciListesi = JsonConvert.DeserializeObject<OgrenciBilgi[]>(TempData["Orgs"].ToString());
          
            }
            _ogrenciListVM.OgrenciBilgis = ogrenciListesi;

            return View(_ogrenciListVM);
        }

        [HttpPost]
        public IActionResult PullOgrenciBilgi(int? ISLEM)
        {
            SqlParameter[] param = new SqlParameter[] 
            {
                new SqlParameter()
                {
                    ParameterName = "ISLEM",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input,
                    IsNullable = true,
                    Value = ISLEM
                },

                new SqlParameter()
                {
                    ParameterName = "ID",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input,
                    IsNullable = true,
                    Value = DBNull.Value
                }
            };

            string sql = $"EXEC sp_OgrenciBilgi @ISLEM, @ID";

            var ogr = _context.OgrenciBilgis.FromSqlRaw(sql, param.ToArray()).ToList();

            _ogrenciListVM.OgrenciBilgis = ogr.ToArray();

            TempData["Orgs"] = JsonConvert.SerializeObject(_ogrenciListVM.OgrenciBilgis); ;

            return RedirectToAction("Index"); 
        }

        [HttpGet]
        public  IActionResult CreateOgrenciBilgi()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> CreateOgrenciBilgi(int? ISLEM,  string AD, string SOYAD, string TC_KIMLIK,

                                                            string DOGUM_TARIHI , string DOGUM_YERI, string CEP_TELEFONU,

                                                            string EV_TELEFONU, string EMAIL, string UYRUK, string GUNCEL_TARIH) 
        {
            List<SqlParameter> paramInsert = new List<SqlParameter>
           {
                new SqlParameter()
                {
                    ParameterName = "ISLEM",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input,
                    IsNullable = true,
                    Value = ISLEM
                },

                new SqlParameter()
                {
                    ParameterName = "ID",
                    SqlDbType = SqlDbType.Int,
                    Direction = ParameterDirection.Input,
                    IsNullable = true,
                    Value = DBNull.Value
                },
                 new SqlParameter()
                {
                    ParameterName = "AD",
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input,
                    IsNullable = true,
                    Value = AD
                },
                  new SqlParameter()
                {
                    ParameterName = "SOYAD",
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input,
                    IsNullable = true,
                    Value = SOYAD
                },
                  new SqlParameter()
                {
                    ParameterName = "TC_KIMLIK",
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input,
                    IsNullable = true,
                    Value = TC_KIMLIK
                },
                  new SqlParameter()
                {
                    ParameterName = "DOGUM_TARIHI",
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input,
                    IsNullable = true,
                    Value = DOGUM_TARIHI
                },
                  new SqlParameter()
                {
                    ParameterName = "DOGUM_YERI",
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input,
                    IsNullable = true,
                    Value = DOGUM_YERI
                },
                  new SqlParameter()
                {
                    ParameterName = "CEP_TELEFONU",
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input,
                    IsNullable = true,
                    Value = CEP_TELEFONU
                },
                  new SqlParameter()
                {
                    ParameterName = "EV_TELEFONU",
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input,
                    IsNullable = true,
                    Value = EV_TELEFONU
                },
                  new SqlParameter()
                {
                    ParameterName = "EMAIL",
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input,
                    IsNullable = true,
                    Value = EMAIL
                },
                  new SqlParameter()
                {
                    ParameterName = "UYRUK",
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input,
                    IsNullable = true,
                    Value = UYRUK
                },
                  new SqlParameter()
                {
                    ParameterName = "GUNCEL_TARIH",
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input,
                    IsNullable = true,
                    Value = GUNCEL_TARIH
                }
           };

            string sql = $"EXEC sp_OgrenciBilgi @ISLEM, @ID, @AD," +
                $" @SOYAD, @TC_KIMLIK ,@DOGUM_TARIHI, @DOGUM_YERI," +
                $" @CEP_TELEFONU, @EV_TELEFONU, @EMAIL, @UYRUK, @GUNCEL_TARIH";

            _context.Database.ExecuteSqlRaw(sql, paramInsert);

            return RedirectToAction("Index");
        }
    }
}
