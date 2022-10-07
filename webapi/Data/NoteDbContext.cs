using System;
using Microsoft.EntityFrameworkCore;
using webapi.Models.Entities;

namespace webapi.Data
{
	public class NoteDbContext:DbContext
	{
		public NoteDbContext(DbContextOptions options):base(options)
		{
		}

        public DbSet<Note> Notes   { get; set; }
    }
}

