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
        //path be connection for database. START
        private readonly MyContext _context;
        private readonly OgrenciListVM _ogrenciListVM;

        public OgrenciBilgiController(MyContext context, OgrenciListVM ogrenciListVM)
        {
            _context = context;
            _ogrenciListVM = ogrenciListVM;
        }
        //END

        //the page in admin panel to show data. START
        public IActionResult Index()
        {
            OgrenciBilgi[] ogrenciListesi = null;

            if (TempData["Orgs"] != null)
            {
                 ogrenciListesi = JsonConvert.DeserializeObject<OgrenciBilgi[]>(TempData["Orgs"].ToString());
          
            }
            _ogrenciListVM.OgrenciBilgis = ogrenciListesi;

            return View(_ogrenciListVM);
        }
        //END

        //The code, for pull data from SQL. POST section.  START
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
        //END

        //The code. For create new student, action. GET section. START
        [HttpGet]
        public  IActionResult CreateOgrenciBilgi()
        {
            return View();
        }
        //END
        //POST section. START
        [HttpPost]
        public IActionResult CreateOgrenciBilgi(int? ISLEM,  string AD, string SOYAD, string TC_KIMLIK,

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
        //END

        //The code. For edııt default student, action. GET section. START
        [HttpGet]
        public IActionResult EditOgrenciBilgi(int? ID, int? ISLEM = 5)
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
                    Value = ID
                }
            };

            string sql = $"EXEC sp_OgrenciBilgi @ISLEM, @ID";

            var ogr = _context.OgrenciBilgis.FromSqlRaw(sql, param.ToArray()).ToList();

            _ogrenciListVM.OgrenciBilgis = ogr.ToArray();

            TempData["Orgs"] = JsonConvert.SerializeObject(_ogrenciListVM.OgrenciBilgis); ;

            OgrenciBilgi[] ogrenciListesi = null;

            if (TempData["Orgs"] != null)
            {
                ogrenciListesi = JsonConvert.DeserializeObject<OgrenciBilgi[]>(TempData["Orgs"].ToString());

            }

            _ogrenciListVM.OgrenciBilgi = ogrenciListesi[0];

            return View(_ogrenciListVM);
        }
        //END
        //POST section. START
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditOgrenciBilgi(int? ISLEM, int? ID, OgrenciListVM vm)
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
                     Value = ID
                 },
                  new SqlParameter()
                 {
                     ParameterName = "AD",
                     SqlDbType = SqlDbType.NVarChar,
                     Direction = ParameterDirection.Input,
                     IsNullable = true,
                     Value = vm.OgrenciBilgi.AD
                 },
                   new SqlParameter()
                 {
                     ParameterName = "SOYAD",
                     SqlDbType = SqlDbType.NVarChar,
                     Direction = ParameterDirection.Input,
                     IsNullable = true,
                     Value = vm.OgrenciBilgi.SOYAD
                 },
                   new SqlParameter()
                 {
                     ParameterName = "TC_KIMLIK",
                     SqlDbType = SqlDbType.NVarChar,
                     Direction = ParameterDirection.Input,
                     IsNullable = true,
                     Value = vm.OgrenciBilgi.TC_KIMLIK
                 },
                   new SqlParameter()
                 {
                     ParameterName = "DOGUM_TARIHI",
                     SqlDbType = SqlDbType.NVarChar,
                     Direction = ParameterDirection.Input,
                     IsNullable = true,
                     Value = vm.OgrenciBilgi.DOGUM_TARIHI
                 },
                   new SqlParameter()
                 {
                     ParameterName = "DOGUM_YERI",
                     SqlDbType = SqlDbType.NVarChar,
                     Direction = ParameterDirection.Input,
                     IsNullable = true,
                     Value = vm.OgrenciBilgi.DOGUM_YERI
                 },
                   new SqlParameter()
                 {
                     ParameterName = "CEP_TELEFONU",
                     SqlDbType = SqlDbType.NVarChar,
                     Direction = ParameterDirection.Input,
                     IsNullable = true,
                     Value = vm.OgrenciBilgi.CEP_TELEFONU
                 },
                   new SqlParameter()
                 {
                     ParameterName = "EV_TELEFONU",
                     SqlDbType = SqlDbType.NVarChar,
                     Direction = ParameterDirection.Input,
                     IsNullable = true,
                     Value = vm.OgrenciBilgi.EV_TELEFONU
                 },
                   new SqlParameter()
                 {
                     ParameterName = "EMAIL",
                     SqlDbType = SqlDbType.NVarChar,
                     Direction = ParameterDirection.Input,
                     IsNullable = true,
                     Value = vm.OgrenciBilgi.EMAIL
                 },
                   new SqlParameter()
                 {
                     ParameterName = "UYRUK",
                     SqlDbType = SqlDbType.NVarChar,
                     Direction = ParameterDirection.Input,
                     IsNullable = true,
                     Value = vm.OgrenciBilgi.UYRUK
                 },
                   new SqlParameter()
                 {
                     ParameterName = "GUNCEL_TARIH",
                     SqlDbType = SqlDbType.NVarChar,
                     Direction = ParameterDirection.Input,
                     IsNullable = true,
                     Value = vm.OgrenciBilgi.GUNCEL_TARIH
                 }
            };
    


            string sql = $"EXEC sp_OgrenciBilgi @ISLEM, @ID, @AD," +
                $" @SOYAD, @TC_KIMLIK ,@DOGUM_TARIHI, @DOGUM_YERI," +
                $" @CEP_TELEFONU, @EV_TELEFONU, @EMAIL, @UYRUK, @GUNCEL_TARIH";

            _context.Database.ExecuteSqlRaw(sql, paramInsert);

            return RedirectToAction("Index");
        }
        //END

        // the code.For delete default parent, action.GET section. START
        [HttpGet]
        public IActionResult DeleteOgrenciBilgi(int? ID, int? ISLEM = 4)
        {
            SqlParameter[] paramDelete = new SqlParameter[]
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
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input,
                    IsNullable = true,
                    Value = ID
                }
            };

            string sql = $"EXEC sp_OgrenciBilgi @ISLEM, @ID";

            _context.Database.ExecuteSqlRaw(sql, paramDelete);
         
            return View("Index");
        }
        //END
    }
}
