using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using GestionDeFlotas.Modelos;

    public class AppDbContext : DbContext
    {
        public AppDbContext (DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public DbSet<GestionDeFlotas.Modelos.Conductor> Conductores { get; set; } = default!;

public DbSet<GestionDeFlotas.Modelos.Camion> Camiones { get; set; } = default!;

public DbSet<GestionDeFlotas.Modelos.Taller> Talleres { get; set; } = default!;

public DbSet<GestionDeFlotas.Modelos.Mantenimiento> Mantenimiento { get; set; } = default!;

public DbSet<GestionDeFlotas.Modelos.Usuario> Usuario { get; set; } = default!;

public DbSet<GestionDeFlotas.Modelos.LoginDto> LoginDto { get; set; } = default!;
    }
