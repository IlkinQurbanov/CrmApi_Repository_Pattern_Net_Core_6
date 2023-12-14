using Admin.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Admin.Core.Services
{
    public interface ITutorService
    {
        Task<IEnumerable<Tutor>> GetAllTutors();
        Task<Tutor> GetTutorById(int id);
        Task<Tutor> CreateTutor(Tutor newTutor);
        Task UpdateTutor(Tutor tutorToBeUpdated, Tutor newTutor);
        Task DeleteTutor(Tutor tutor);
        Task SoftDeleteTutor(Tutor tutor); // Soft Delete method
    }
}
