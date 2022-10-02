using JurmasModels;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.Pkcs;
using System.Text;
using System.Threading.Tasks;

namespace JurmasWPF.Services;

public class DBClient : DbContext, IDBClient
{
	public DbSet<Jurmas> Users { get; set; }
	public DbSet<Recipee> Recipees { get; set; }

	public DBClient(DbContextOptions<DBClient> options) : base(options)
	{

	}
}
