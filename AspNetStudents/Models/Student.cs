using System;
namespace AspNetStudents.Models
{
	public class Student
	{
        public int id { get; set; }
        public string? firstName { get; set; } //? indicates property can be nullable
        public string? lastName { get; set; }
    }
}

