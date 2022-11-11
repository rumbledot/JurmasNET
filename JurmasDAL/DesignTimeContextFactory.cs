using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JurmasDAL;

public class DesignTimeContextFactory : IDesignTimeDbContextFactory<JurmasContext>
{
    public JurmasContext CreateDbContext(string[] args)
    {
        return new JurmasContext(@"server=ABRAMDESK\SQL2019;user id=sa;password=Strongpassword123;initial catalog=JurmasDB");
    }
}
