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
        public IActionResult OgrBilgi2(int? ISLEM)
        {
            //  var param = new SqlParameter("@ISLEM", ISLEM);

            // List<SqlParameter> param = new List<SqlParameter>
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

            return RedirectToAction("Index"); ;

        }
    }
}
