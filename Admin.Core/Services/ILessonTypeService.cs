using Admin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Core.Services
{
    public interface ILessonTypeService
    {
        Task<IEnumerable<LessonType>> GetAllLessonTypes();
        Task<LessonType> GetLessonTypeById(int id);
        Task<LessonType> CreateLessonType(LessonType newLessonType);
        Task UpdateLessonType(LessonType lessontypeToBeUpdated, LessonType newLessonType);
        Task DeleteLessonType(LessonType lessontype);
        Task SoftDeleteLessonType(LessonType lessontype); // Soft Delete method
    }
}
