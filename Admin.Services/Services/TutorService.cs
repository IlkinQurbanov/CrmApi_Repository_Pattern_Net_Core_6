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
    public class TutorService : ITutorService
    {

        private readonly IUnitOfWork _unitOfWork;
        public TutorService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<Tutor> CreateTutor(Tutor newTutor)
        {
            await _unitOfWork.Tutors
                .AddAsync(newTutor);
            await _unitOfWork.CommitAsync();

            return newTutor;
        }

        //Simple delete

        //public async Task DeleteTutor(Tutor tutor)
        //{
        //    _unitOfWork.Tutors.Remove(tutor);

        //    await _unitOfWork.CommitAsync();
        //}

        public async Task DeleteTutor(Tutor tutor)
        {
            tutor.IsDeleted = true;
            tutor.DeletedAt = DateTime.UtcNow;

            await _unitOfWork.CommitAsync();
        }


        public async Task SoftDeleteTutor(Tutor tutor)
        {
            tutor.IsDeleted = true;
            tutor.DeletedAt = DateTime.UtcNow;

            await _unitOfWork.CommitAsync();
        }

        public async Task<IEnumerable<Tutor>> GetAllTutors()
        {
            return await _unitOfWork.Tutors.GetAllAsync();
        }

        public async Task<Tutor> GetTutorById(int id)
        {
            return await _unitOfWork.Tutors.GetByIdAsync(id);
        }

        public async Task UpdateTutor(Tutor tutorToBeUpdated, Tutor tutor)
        {
            tutorToBeUpdated.Name = tutor.Name;

            await _unitOfWork.CommitAsync();
        }
    }
}
