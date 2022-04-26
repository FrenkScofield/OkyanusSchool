using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OkyanusSchool.Models.BLL;
using OkyanusSchool.Models.DAL;
using OkyanusSchool.Models.VM;
using System;
using System.Collections.Generic;
using System.Data;
using Microsoft.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace OkyanusSchool.Areas.WebCms.Controllers
{
    [Area("WebCms")]
    [Route("WebCms/[controller]/[action]")]
    public class VeliBilgiController : Controller
    {
        //path be connection for database. START
        private readonly MyContext _context;
        private readonly OgrenciVeliBilgiListVM _ogrenciVeliBilgiListVM;

        public VeliBilgiController(MyContext context, OgrenciVeliBilgiListVM ogrenciVeliBilgiListVM)
        {
            _context = context;
            _ogrenciVeliBilgiListVM = ogrenciVeliBilgiListVM;
        }
        //END

        //the page in admin panel to show data. START
        public IActionResult Index()
        {
            PullOgrenciVeliBilgi();

            OgrenciVeliBilgi[]  ogrenciVeliBilgis = null;

            if (TempData["Orgs"] != null)
            {
                ogrenciVeliBilgis = JsonConvert.DeserializeObject<OgrenciVeliBilgi[]>(TempData["Orgs"].ToString());

            }
            _ogrenciVeliBilgiListVM.OgrenciVeliBilgis = ogrenciVeliBilgis;

            return View(_ogrenciVeliBilgiListVM);
        }
        //END

        //The code, for pull data from SQL. POST section.  START
        [HttpPost]
        public IActionResult PullOgrenciVeliBilgi(int? ISLEM = 1)
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

            string sql = $"EXEC sp_OgrVeliBilgi @ISLEM, @ID";

            var ogr = _context.OgrenciVeliBilgis.FromSqlRaw(sql, param.ToArray()).ToList();

            _ogrenciVeliBilgiListVM.OgrenciVeliBilgis = ogr.ToArray();

            TempData["Orgs"] = JsonConvert.SerializeObject(_ogrenciVeliBilgiListVM.OgrenciVeliBilgis); ;

            return RedirectToAction("Index");
        }
        //END

        //The code. For create new parent, action. GET section. START
        [HttpGet]
        public IActionResult CreateOgrVeliBilgi()
        {
            return View();  
        }
        //END
        //POST section. START
        [HttpPost]
        public IActionResult CreateOgrenciVeliBilgi(int? ISLEM,  bool YASAM_DURUMU, string TIPI, string AD, string SOYAD, string TC_KIMLIK,

                                                           string ILISKI_DURUMU, string DOGUM_TARIHI, string DOGUM_YERI, string CEP_TELEFONU_1,

                                                           string CEP_TELEFONU_2, string IS_TELEFONU, string EMAIL, string UYRUK, string MESLEK)
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
                    ParameterName = "YASAM_DURUMU",
                    SqlDbType = SqlDbType.Bit,
                    Direction = ParameterDirection.Input,
                    IsNullable = true,
                    Value = YASAM_DURUMU
                },
                 new SqlParameter()
                {
                    ParameterName = "TIPI",
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input,
                    IsNullable = true,
                    Value = TIPI
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
                    ParameterName = "ILISKI_DURUMU",
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input,
                    IsNullable = true,
                    Value = ILISKI_DURUMU
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
                    ParameterName = "CEP_TELEFONU_1",
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input,
                    IsNullable = true,
                    Value = CEP_TELEFONU_1
                },
                  new SqlParameter()
                {
                    ParameterName = "CEP_TELEFONU_2",
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input,
                    IsNullable = true,
                    Value = CEP_TELEFONU_2
                },
                  new SqlParameter()
                {
                    ParameterName = "IS_TELEFONU",
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input,
                    IsNullable = true,
                    Value = IS_TELEFONU
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
                    ParameterName = "MESLEK",
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input,
                    IsNullable = true,
                    Value = MESLEK
                }
           };

            string sql = $"EXEC sp_OgrVeliBilgi @ISLEM, @ID, @YASAM_DURUMU, @TIPI, @AD," +
                $" @SOYAD, @TC_KIMLIK, @ILISKI_DURUMU, @DOGUM_TARIHI, @DOGUM_YERI," +
                $" @CEP_TELEFONU_1,@CEP_TELEFONU_2, @IS_TELEFONU, @EMAIL, @UYRUK, @MESLEK";

            _context.Database.ExecuteSqlRaw(sql, paramInsert);

            return RedirectToAction("Index");
        }
        //END

        //The code. For edııt default parent, action. GET section. START
        [HttpGet]
        public IActionResult EditOgrenciVeliBilgi(int? ID, int? ISLEM = 5)
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

            string sql = $"EXEC sp_OgrVeliBilgi @ISLEM, @ID";

            var ogr = _context.OgrenciVeliBilgis.FromSqlRaw(sql, param.ToArray()).ToList();

            _ogrenciVeliBilgiListVM.OgrenciVeliBilgis = ogr.ToArray();

            TempData["Orgs"] = JsonConvert.SerializeObject(_ogrenciVeliBilgiListVM.OgrenciVeliBilgis); ;

            OgrenciVeliBilgi[] ogrenciListesi = null;

            if (TempData["Orgs"] != null)
            {
                ogrenciListesi = JsonConvert.DeserializeObject<OgrenciVeliBilgi[]>(TempData["Orgs"].ToString());
            }

            _ogrenciVeliBilgiListVM.OgrenciVeliBilgi = ogrenciListesi[0];

            return View(_ogrenciVeliBilgiListVM);
        }
        //END
        //POST section. START
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult EditOgrenciVeliBilgi(int? ISLEM, int? ID, OgrenciVeliBilgiListVM vm)
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
                    ParameterName = "YASAM_DURUMU",
                    SqlDbType = SqlDbType.Bit,
                    Direction = ParameterDirection.Input,
                    IsNullable = true,
                    Value = vm.OgrenciVeliBilgi.YASAM_DURUMU
                },
                 new SqlParameter()
                {
                    ParameterName = "TIPI",
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input,
                    IsNullable = true,
                    Value = vm.OgrenciVeliBilgi.TIPI
                },
                 new SqlParameter()
                {
                    ParameterName = "AD",
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input,
                    IsNullable = true,
                    Value = vm.OgrenciVeliBilgi.AD
                },
                  new SqlParameter()
                {
                    ParameterName = "SOYAD",
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input,
                    IsNullable = true,
                    Value = vm.OgrenciVeliBilgi.SOYAD
                },
                  new SqlParameter()
                {
                    ParameterName = "TC_KIMLIK",
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input,
                    IsNullable = true,
                    Value = vm.OgrenciVeliBilgi.TC_KIMLIK
                },
                   new SqlParameter()
                {
                    ParameterName = "ILISKI_DURUMU",
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input,
                    IsNullable = true,
                    Value = vm.OgrenciVeliBilgi.ILISKI_DURUMU
                },
                  new SqlParameter()
                {
                    ParameterName = "DOGUM_TARIHI",
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input,
                    IsNullable = true,
                    Value = vm.OgrenciVeliBilgi.DOGUM_TARIHI
                },
                  new SqlParameter()
                {
                    ParameterName = "DOGUM_YERI",
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input,
                    IsNullable = true,
                    Value = vm.OgrenciVeliBilgi.DOGUM_YERI
                },
                  new SqlParameter()
                {
                    ParameterName = "CEP_TELEFONU_1",
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input,
                    IsNullable = true,
                    Value = vm.OgrenciVeliBilgi.CEP_TELEFONU_1
                },
                  new SqlParameter()
                {
                    ParameterName = "CEP_TELEFONU_2",
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input,
                    IsNullable = true,
                    Value = vm.OgrenciVeliBilgi.CEP_TELEFONU_2
                },
                  new SqlParameter()
                {
                    ParameterName = "IS_TELEFONU",
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input,
                    IsNullable = true,
                    Value = vm.OgrenciVeliBilgi.IS_TELEFONU
                },
                  new SqlParameter()
                {
                    ParameterName = "EMAIL",
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input,
                    IsNullable = true,
                    Value = vm.OgrenciVeliBilgi.EMAIL
                },
                  new SqlParameter()
                {
                    ParameterName = "UYRUK",
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input,
                    IsNullable = true,
                    Value = vm.OgrenciVeliBilgi.UYRUK
                },
                  new SqlParameter()
                {
                    ParameterName = "MESLEK",
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input,
                    IsNullable = true,
                    Value = vm.OgrenciVeliBilgi.MESLEK
                }
           };

            string sql = $"EXEC sp_OgrVeliBilgi @ISLEM, @ID, @YASAM_DURUMU, @TIPI, @AD," +
               $" @SOYAD, @TC_KIMLIK, @ILISKI_DURUMU, @DOGUM_TARIHI, @DOGUM_YERI," +
               $" @CEP_TELEFONU_1,@CEP_TELEFONU_2, @IS_TELEFONU, @EMAIL, @UYRUK, @MESLEK";

            _context.Database.ExecuteSqlRaw(sql, paramInsert);

            return RedirectToAction("Index");
        }
        //END

        // the code.For delete default parent, action.GET section. START
        [HttpGet]
        public IActionResult DeleteOgrenciVeliBilgi(int? ID, int? ISLEM = 4)
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

            string sql = $"EXEC sp_OgrVeliBilgi @ISLEM, @ID";

            _context.Database.ExecuteSqlRaw(sql, paramDelete);

            return RedirectToAction(nameof(Index));
        }
    }
}
