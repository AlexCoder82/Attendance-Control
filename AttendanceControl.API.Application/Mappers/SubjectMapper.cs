using AttendanceControl.API.Business.Models;
using AttendanceControl.API.DataAccess.Contracts.Entities;
using System.Linq;

namespace AttendanceControl.API.Application.Mappers
{
    public static class SubjectMapper
    {
        public static Subject Map(SubjectEntity subjectEntity)
        {
            return new Subject()
            {
                Id = subjectEntity.Id,
                Name = subjectEntity.Name
                      
            };
        }

        public static SubjectEntity Map(Subject subject)
        {
            return new SubjectEntity()
            {
                Id = subject.Id,
                Name = subject.Name           
            };
        }
    }
}
