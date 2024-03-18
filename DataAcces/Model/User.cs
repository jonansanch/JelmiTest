using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAcces.Model
{
    public class User
    {
        public Guid UserId { get; set; }
        public required string FirstName { get; set; }
        public string? SecondName { get; set; }
        public required string Surname { get; set; }
        public string? SecondSurname { get; set; }
        public required DateTime Birthdate { get; set; }
        public required int Salary { get; set; }
        public required DateTime CreatedOn { get; set; }          
        public required DateTime ModifiedOn { get; set; }
    }
}
