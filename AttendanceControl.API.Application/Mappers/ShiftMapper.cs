using AttendanceControl.API.Business.Models;
using AttendanceControl.API.DataAccess.Contracts.Entities;
using System.Linq;

namespace AttendanceControl.API.Application.Mappers
{
    /// <summary>
    ///     Mapeos de objetos ShiftEntity y Shift
    /// </summary>
    public static class ShiftMapper
    {

        /// <summary>
        ///     Mapea un objeto ShiftEntity en un objeto Shift
        /// </summary>
        /// <param name="shiftEntity"></param>
        /// <returns></returns>
        public static Shift Map(ShiftEntity shiftEntity)
        {

            if (shiftEntity is null)
            {
                return null;
            }
            else
            {
                return new Shift()
                {
                    Id = shiftEntity.Id,
                    Description = shiftEntity.Description
                };
            }

        }

    }
}
