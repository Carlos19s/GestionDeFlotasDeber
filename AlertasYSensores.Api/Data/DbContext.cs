using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Sensores.Modelos;

    public class DbContext : Microsoft.EntityFrameworkCore.DbContext
    {
        public DbContext (DbContextOptions<DbContext> options)
            : base(options)
        {
        }

        public DbSet<Sensores.Modelos.Alerta> Alerta { get; set; } = default!;


public DbSet<Sensores.Modelos.Sensor> Sensor { get; set; } = default!;
    }
