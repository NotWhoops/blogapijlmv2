using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using blogapijlmv2.Models;
using Microsoft.EntityFrameworkCore;

namespace blogapijlmv2.Properties.Services.Context
{

public class Context : DbContext
{
        
public Context(DbContextOptions options) : base(options)
{
    
}

public DbSet<UserModel> UserInfo {get; set;}
public DbSet<BlogItemModel> BlogInfo {get; set;}


}
}
