using Admin.Core.Models;
using Admin.Core.Services;
using Admin.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Services.Services
{
    public class LessonTypeService : ILessonTypeService
    {

        private readonly IUnitOfWork _unitOfWork;
        public LessonTypeService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<LessonType> CreateLessonType(LessonType newLessonType)
        {
            await _unitOfWork.LessonTypes
                .AddAsync(newLessonType);
            await _unitOfWork.CommitAsync();

            return newLessonType;
        }

        // Simple Delete method

        //public async Task DeleteLessonType(LessonType lessontype)
        //{
        //    _unitOfWork.LessonTypes.Remove(lessontype);

        //    await _unitOfWork.CommitAsync();
        //}


        public async Task DeleteLessonType(LessonType lessontype)
        {
            lessontype.IsDeleted = true;
            lessontype.DeletedAt = DateTime.UtcNow;

            await _unitOfWork.CommitAsync();
        }

        public async Task SoftDeleteLessonType(LessonType lessontype)
        {
            lessontype.IsDeleted = true;
            lessontype.DeletedAt = DateTime.UtcNow;

            await _unitOfWork.CommitAsync();
        }


        public async Task<IEnumerable<LessonType>> GetAllLessonTypes()
        {
            return await _unitOfWork.LessonTypes.GetAllAsync();
        }

        public async Task<LessonType> GetLessonTypeById(int id)
        {
            return await _unitOfWork.LessonTypes.GetByIdAsync(id);
        }

        public async Task UpdateLessonType(LessonType lessontypeToBeUpdated, LessonType lessontype)
        {
            lessontypeToBeUpdated.Name = lessontype.Name;

            await _unitOfWork.CommitAsync();
        }

    }
}
