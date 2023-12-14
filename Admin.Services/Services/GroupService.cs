using Admin.Core.Services;
using Admin.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Admin.Core.Models;

namespace Admin.Services.Services
{
    public class GroupService : IGroupService
    {
        private readonly IUnitOfWork _unitOfWork;

        public GroupService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Group>> GetAllGroups()
        {
            return await _unitOfWork.Groups.GetAllAsync();
        }

        public async Task<Group> GetGroupById(int id)
        {
            return await _unitOfWork.Groups.GetByIdAsync(id);
        }

        public async Task<IEnumerable<Group>> GetAllWithTutor()
        {
            return await _unitOfWork.Groups
            .GetAllWithTutorAsync();
        }

        public async Task<IEnumerable<Group>> GetAllWithService()
        {
            return await _unitOfWork.Groups
             .GetAllWithServiceAsync();
        }

        public async Task<IEnumerable<Group>> GetAllWithLessonType()
        {
            return await _unitOfWork.Groups
             .GetAllWithLessonTypeAsync();
        }

        public async Task<IEnumerable<Group>> GetGroupsByServiceId(int serviceId)
        {

            return await _unitOfWork.Groups
                .GetAllWithServiceByServiceIdAsync(serviceId);
        }

        public async Task<IEnumerable<Group>> GetGroupsByTutorId(int tutorId)
        {
            {
                return await _unitOfWork.Groups
                    .GetAllWithTutorByTutorIdAsync(tutorId);
            }
        }

        public async Task<IEnumerable<Group>> GetGroupsByLessonTypeId(int lessontypeId)
        {
            return await _unitOfWork.Groups
                .GetAllWithLessonTypeByLessonTypeIdAsync(lessontypeId);
        }

        public async Task<Group> CreateGroup(Group newGroup)
        {
            await _unitOfWork.Groups.AddAsync(newGroup);
            await _unitOfWork.CommitAsync();
            return newGroup;
        }

        public async Task UpdateGroup(Group groupToBeUpdated, Group group)
        {
            if (groupToBeUpdated == null || group == null)
            {
                throw new ArgumentNullException("groupToBeUpdated and group parameters cannot be null.");
            }

            groupToBeUpdated.GroupNumber = group.GroupNumber;
            groupToBeUpdated.StartDate = group.StartDate;
            groupToBeUpdated.EndDate = group.EndDate;
            groupToBeUpdated.ExpiredOrCancelled = group.ExpiredOrCancelled;
            groupToBeUpdated.ActualEndDate = group.ActualEndDate;
            groupToBeUpdated.ServiceId = group.ServiceId;
            groupToBeUpdated.NumberOfLessonsPerWeek = group.NumberOfLessonsPerWeek;
            groupToBeUpdated.DaysOfTheWeekWithlessons = group.DaysOfTheWeekWithlessons;
            groupToBeUpdated.LessonTypeId = group.LessonTypeId;
            groupToBeUpdated.OnlinePassDate = group.OnlinePassDate;
            groupToBeUpdated.TutorId = group.TutorId;
            groupToBeUpdated.BranchId = group.BranchId;
            groupToBeUpdated.StartTime = group.StartTime;
            groupToBeUpdated.EndTime = group.EndTime;
            groupToBeUpdated.CountOfStuudents = group.CountOfStuudents;
            groupToBeUpdated.NewStudentsCount = group.NewStudentsCount;
            groupToBeUpdated.FreezingStudentNumber = group.FreezingStudentNumber;
            groupToBeUpdated.NumberOfStudentsDroppingOut = group.NumberOfStudentsDroppingOut;
 

            await _unitOfWork.CommitAsync();
        }

        public async Task DeleteGroup(Group group)
        {
            group.IsDeleted = true;
            group.DeletedAt = DateTime.UtcNow;

            await _unitOfWork.CommitAsync();
        }

        public async Task SoftDeleteGroup(Group group)
        {
            group.IsDeleted = true;
            group.DeletedAt = DateTime.UtcNow;

            await _unitOfWork.CommitAsync();
        }
    }
}
