//using Data.Entities;
//using Domain.Models;

namespace Business.Factories;

//public class ProjectFactory
//{
//    public static ProjectEntity? Create(AddProjectForm form) => form == null ? null : new ProjectEntity
//    {
//        ProjectName = form.ProjectName,
//        Description = form.Description,
//        StartDate = form.StartDate,
//        EndDate = form.EndDate,
//        Budget = form.Budget,

//        //LÄGG in client och members och status
//    };

//    public static Project Create(ProjectEntity entity)
//    {
//        if (entity == null)
//            return null;

//        var project = new Project
//        {
//            Id = entity.Id,
//            ProjectName = entity.ProjectName,

//            Description = entity.Description,
//            StartDate = entity.StartDate,
//            EndDate = entity.EndDate,
//            Budget = entity.Budget,


//        };
//        if (entity.Client != null)
//        {
//            project.Client = new Client
//            {
//                Id = entity.Id,
//                ClientName = entity.Client.ClientName,
//            };
//        }
//        if (entity.User != null)
//        {
//            project.Member = new Member
//            {
//                Id = entity.User.Id,
//                FirstName = entity.User.FirstName,
//                LastName = entity.User.LastName,
//                //Mappa resten
//            };
//        }
//        return project;
//    }

//}