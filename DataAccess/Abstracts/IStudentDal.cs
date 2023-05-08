using Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Core.Persestens;
using Core.Persistence;

namespace DataAccess.Abstracts;

public interface IStudentDal : IAsyncRepository<Student,Guid>,IRepository<Student,Guid>
{

}
