﻿using Microsoft.EntityFrameworkCore;

namespace Test.DBContext;

public class MyContext : DbContext
{
    public MyContext(DbContextOptions<MyContext> options) : base(options)
    {
        
    }
}
