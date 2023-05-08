using Core.Persistence;
using DataAccess.Abstracts;
using DataAccess.Contexts;
using Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Concretes;

public class EfStudentDal : EfRepositoryBase<Student, Guid, BaseDbContext>, IStudentDal
{
    public EfStudentDal(BaseDbContext context) : base(context)
    {
    }
}
