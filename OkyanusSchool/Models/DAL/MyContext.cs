using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using OkyanusSchool.Models.BLL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OkyanusSchool.Models.DAL
{
    public class MyContext : IdentityDbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options) { }

        public DbSet<OgrenciBilgi> OgrenciBilgis { get; set; }
        public DbSet<OgrenciVeliBilgi>  OgrenciVeliBilgis { get; set; }
    }
    }
