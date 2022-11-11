using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JurmasDAL;

public class JurmasContext : DbContext
{
    private readonly string ConnectionString;
    public JurmasContext(string connectionString)
    {
        ConnectionString = connectionString;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer(ConnectionString);
    }

    public DbSet<Jurmas> Jurmases { get; set; }

    public DbSet<Recipee> Recipees { get; set; }
    public DbSet<RecipeeIngredient> RecipeeIngredients { get; set; }
    public DbSet<RecipeeStep> RecipeeSteps { get; set; }
    public DbSet<RecipeeHistory> RecipeeHistory { get; set; }
    public DbSet<RecipeePhoto> RecipeePhotos { get; set; }
    public DbSet<RecipeeStatus> Statuses { get; set; }

    public DbSet<Ingredient> Ingredients { get; set; }
    public DbSet<Category> Categories { get; set; }
}
