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

        /// <summary>
        ///     Mapea un objeto Shift en un objeto ShiftEntity
        /// </summary>
        /// <param name="shiftEntity"></param>
        /// <returns></returns>
        public static ShiftEntity Map(Shift shift)
        {

            if (shift is null)
            {
                return null;
            }
            else
            {
                return new ShiftEntity()
                {
                    Id = shift.Id,
                    Description = shift.Description
                };
            }

        }

    }
}
