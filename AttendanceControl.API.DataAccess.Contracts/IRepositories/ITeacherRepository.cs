using AttendanceControl.API.DataAccess.Contracts.Entities;
using AttendanceControl.API.DataAccess.Contracts.IRepositories;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AttendanceControl.API.DataAccess.Contracts.IRepositories
{
    public interface ITeacherRepository
    {
        public Task<TeacherEntity> Get(int id);
        public Task<TeacherEntity> GetByDni(string dni);

        public Task<List<TeacherEntity>> GetAll();

        public Task<TeacherEntity> Save(TeacherEntity entity);

        public Task<TeacherEntity> Update(TeacherEntity entity);

        public Task<bool> Register(TeacherEntity teacherEntity, TeacherCredentialsEntity teacherCredentialsEntity);

        public Task<TeacherEntity> GetByCredentials(string username, string password);
    }
}
