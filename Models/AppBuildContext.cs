using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;

namespace AppBuild.Models
{
    public class AppBuildContext : DbContext
    {
        public AppBuildContext([NotNullAttribute] DbContextOptions options) : base(options)
        {
        }

        public DbSet<AppModel> AppModels { get; set; }
        public DbSet<Envs> Envs { get; set; }
        public DbSet<BuildModel> BuildModel { get; set; }

    }
}