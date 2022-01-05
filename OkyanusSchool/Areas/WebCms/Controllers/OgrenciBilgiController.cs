using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OkyanusSchool.Models.BLL;
using OkyanusSchool.Models.DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace OkyanusSchool.Areas.WebCms.Controllers
{
    [Area("WebCms")]
    [Route("WebCms/[controller]/[action]")]
    public class OgrenciBilgiController : Controller
    {
        private readonly MyContext _context;

        public OgrenciBilgiController(MyContext context)
        {
            _context = context;
        }
        public async Task<IActionResult> Index()
        {
          
            return View( await _context.OgrenciBilgis.ToListAsync());
        }


        //public IActionResult OgrBilgi(int ISLEM)
        //{
        //    List<OgrenciBilgi> list;
        //    string sql = "EXEC dbo.sp_OgrenciBilgi @ISLEM";

        //    List<SqlParameter> parms = new List<SqlParameter>
        //    {
        //new SqlParameter { ParameterName = "@ISLEM", Value = ISLEM }
        //    };
        //    list = _context.OgrenciBilgis.FromSqlRaw<OgrenciBilgi>(sql, parms.ToArray()).ToList();

        //    // Debugger.Break();

        //    return View("Index");
        //}
        [HttpPost]
        public IActionResult OgrBilgi2(int? ISLEM)
        {
            var param = new SqlParameter("@ISLEM", ISLEM);

            var ogr = _context.Database.ExecuteSqlCommand($"exec sp_OgrenciBilgi {ISLEM}");

            return RedirectToAction(nameof(Index));
          
        }
            //[HttpPost]
            //public async Task<IActionResult> SimpleProcedure(int ISLEM)
            //{
            //    var q = _context.OgrenciBilgis.FromSql($"exec sp_OgrenciBilgi {ISLEM}").ToList();

            //   await _context.OgrenciBilgis.AddAsync();

            //   await _context.SaveChangesAsync();

            //    return View (ISLEM);
            //}
        }
}
