using JurmasModels;
using Microsoft.EntityFrameworkCore;

namespace JurmasWPF.Services
{
    public interface IDBClient
    {
        DbSet<Recipee> Recipees { get; set; }
        DbSet<Jurmas> Users { get; set; }
    }
}