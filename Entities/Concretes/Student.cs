using Core.Persestens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities.Concretes;

public class Student : Entity<Guid>
{
    public string StudentNumber { get; set; }
    public string? NationalityId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime BirthDate { get; set; }
    public bool States { get; set; }

}
