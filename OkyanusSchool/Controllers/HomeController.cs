using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using OkyanusSchool.Models;
using OkyanusSchool.Models.BLL;
using OkyanusSchool.Models.DAL;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace OkyanusSchool.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly MyContext _context;

        public HomeController(MyContext context,ILogger<HomeController> logger)
        {
            _context = context;
            _logger = logger;
        }

        public IActionResult Index()
        {
            return View();
        }
        //public IActionResult GetAll()
        //{
        //    List<OgrenciBilgi> list;
        //    string sql = "EXEC dbo.sp_OgrenciBilgi";
        //    list = _context.OgrenciBilgis.FromSqlRaw<OgrenciBilgi>(sql).ToList();
        //    Debugger.Break();
        //    return View("Index");
        //}

        public IActionResult OgrBilgi()
        {
            List<OgrenciBilgi> list;
            string sql = "EXEC dbo.sp_OgrenciBilgi @ISLEM";

            List<SqlParameter> parms = new List<SqlParameter>
    {
        // Create parameter(s)    
        new SqlParameter { ParameterName = "@ISLEM", Value = 1 }
    };

            list = _context.OgrenciBilgis.FromSqlRaw<OgrenciBilgi>(sql, parms.ToArray()).ToList();

           // Debugger.Break();

            return View("Index");
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
