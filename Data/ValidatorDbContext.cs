using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using IDNoValidator.Models;

namespace IDNoValidator.Data
{
  public class ValidatorDbContext : DbContext
  {
    public ValidatorDbContext(DbContextOptions<ValidatorDbContext> options) : base(options) { }
    public DbSet<IDNumberModel> IDNumbers { get; set; }
  }
}
