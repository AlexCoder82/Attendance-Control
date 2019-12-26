using AttendanceControl.API.Application.Contracts.IServices;
using AttendanceControl.API.Application.Mappers;
using AttendanceControl.API.Business.Models;
using AttendanceControl.API.DataAccess.Contracts.Entities;
using AttendanceControl.API.DataAccess.Contracts.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceControl.API.Application.Services
{
    public class SchoolClassService : ISchoolClassService
    {
        private readonly ISchoolClassRepository _schoolClassRepository;

        public SchoolClassService(ISchoolClassRepository schoolClassRepository)
        {
            _schoolClassRepository = schoolClassRepository;
        }

        public async Task<bool> SetNotCurrent(int id)
        {           
            return await _schoolClassRepository.SetNotCurrent(id);
        }

        public async Task<SchoolClass> Save(SchoolClass schoolClass)
        {
            SchoolClassEntity schoolClassEntity = SchoolClassMapper.Map(schoolClass);
  
            schoolClassEntity = await _schoolClassRepository.Save(schoolClassEntity);

            schoolClass = SchoolClassMapper.Map(schoolClassEntity);
            return schoolClass;
        }

        public  async Task<SchoolClass> Update(int courseId, SchoolClass schoolClass)
        {
            throw new NotImplementedException();
        }
    }
}
