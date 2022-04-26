using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using OkyanusSchool.Models.BLL;
using OkyanusSchool.Models.DAL;
using OkyanusSchool.Models.VM;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace OkyanusSchool.Areas.WebCms.Controllers
{
    [Area("WebCms")]
    [Route("WebCms/[controller]/[action]")]
    public class OgretmenBilgiController : Controller
    {
        //path be connection for database. START
        private readonly MyContext _context;
        private readonly OgretmenBilgiVM _ogretmenBilgiVM;

        public OgretmenBilgiController(MyContext context, OgretmenBilgiVM ogretmenBilgiVM)
        {
            _context = context;
            _ogretmenBilgiVM = ogretmenBilgiVM;
        }

        //END

        //the page in admin panel to show data. START
        public IActionResult Index()
        {
            PullOgretmenBilgi();

            OgretmenBilgi[] ogretmenListesi = null;

            if (TempData["Orgs"] != null)
            {
                ogretmenListesi = JsonConvert.DeserializeObject<OgretmenBilgi[]>(TempData["Orgs"].ToString());

            }
            _ogretmenBilgiVM.OgretmenBilgis = ogretmenListesi;

            return View(_ogretmenBilgiVM);
        }
        //END

        //The code, for pull data from SQL. POST section.  START
        [HttpPost]
        public async Task<IActionResult> PullOgretmenBilgi(int? ISLEM = 1)
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

            string sql = $"EXEC sp_OgretmenBilgi @ISLEM, @ID";

            var ogr = _context.OgretmenBilgis.FromSqlRaw(sql, param.ToArray()).ToList();

            _ogretmenBilgiVM.OgretmenBilgis = ogr.ToArray();

            TempData["Orgs"] = JsonConvert.SerializeObject(_ogretmenBilgiVM.OgretmenBilgis); ;

            return RedirectToAction("Index");
        }
        //END

        //The code. For create new teacher, action. GET section. START
        [HttpGet]
        public IActionResult CreateOgretmenBilgi()
        {
            return View();
        }
        //END
        //POST section. START
        [HttpPost]
        public IActionResult CreateOgretmenBilgi(int? ISLEM, string AD, string SOYAD, string TC_KIMLIK,

                                                           string DOGUM_TARIHI, string DOGUM_YERI, string CEP_TELEFONU_1,

                                                           string CEP_TELEFONU_2, string EMAIL, string KONUM)
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
                    ParameterName = "EMAIL",
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input,
                    IsNullable = true,
                    Value = EMAIL
                },
                  new SqlParameter()
                {
                    ParameterName = "KONUM",
                    SqlDbType = SqlDbType.NVarChar,
                    Direction = ParameterDirection.Input,
                    IsNullable = true,
                    Value = KONUM
                }
           };

            string sql = $"EXEC sp_OgretmenBilgi @ISLEM, @ID, @AD," +
                $" @SOYAD, @TC_KIMLIK, @DOGUM_TARIHI, @DOGUM_YERI," +
                $" @CEP_TELEFONU_1,@CEP_TELEFONU_2, @EMAIL, @KONUM";

            _context.Database.ExecuteSqlRaw(sql, paramInsert);

            return RedirectToAction("Index");
        }
        //END



    }
}
