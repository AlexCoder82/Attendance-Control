﻿
using AttendanceControl.API.Business.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace AttendanceControl.API.Application.Contracts.IServices
{
    /// <summary>
    ///  Contratos de la lógica relacionada con los cursos
    /// </summary>
    public interface ICourseService
    {
        public Task<bool> AssignSubject(int courseId,int subjectId);
        public Task<bool> RemoveAssignedSubject(int courseId,int subjectId);
        public Task<List<Course>> GetAll();
    }
}
