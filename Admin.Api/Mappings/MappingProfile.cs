using Admin.Api.Resources;
using Admin.Core.Models;
using AutoMapper;

namespace Admin.Api.Mappings
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Domain to resource
            CreateMap<Service, ServiceResource>();
            CreateMap<Service, SaveServiceResource>();

            CreateMap<Tutor, TutorResource>();
            CreateMap<Tutor, SaveTutorResource>();


            CreateMap<Branch, BranchResource>();
            CreateMap<Branch, SaveBranchResource>();

            CreateMap<LessonType, LessonTypeResource>();
            CreateMap<LessonType, SaveLessonTypeResource>();


            CreateMap<Group, SaveGroupResource>();
            CreateMap<Group, GroupResource>();

            CreateMap<Department, SaveDepartmentResouorce>();
            CreateMap<Department, DepartmentResource>();








            //Resource to Domain
            CreateMap<ServiceResource, Service>();
            CreateMap<SaveServiceResource, Service>();

            CreateMap<TutorResource, Tutor>();
            CreateMap<SaveTutorResource, Tutor>();

            CreateMap<BranchResource, Branch>();
            CreateMap<SaveBranchResource, Branch>();

            CreateMap<LessonTypeResource, LessonType>();
            CreateMap<SaveLessonTypeResource, LessonType>();

            CreateMap<GroupResource, Group>();
            CreateMap<SaveGroupResource, Group>();


            CreateMap<DepartmentResource, Department>();
            CreateMap<SaveDepartmentResouorce, Department>();






        }
    }
}
