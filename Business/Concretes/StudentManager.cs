using Business.Abstracts;
using Business.Dtos.Requests;
using DataAccess.Abstracts;
using Entities.Concretes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Concretes;

public class StudentManager : IStudentService
{
    private IStudentDal studentDal;

    public StudentManager(IStudentDal studentDal)
    {
        this.studentDal = studentDal;
    }

    public async Task Add(CreateStudentRequest createStudentRequest)
    {
       Student student = new Student();
        // todo: regex yapılacak
        student.StudentNumber = "2023123456";
        student.LastName=createStudentRequest.LastName;
        student.FirstName=createStudentRequest.FirstName;
        student.NationalityId=createStudentRequest.NationalityId;
        student.BirthDate=createStudentRequest.BirthDate;
        await studentDal.AddAsync(student);






    }
}
