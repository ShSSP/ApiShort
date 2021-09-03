using NUnit.Framework;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using ApiShort.DAL.EFDbModel;
using ApiShort.DAL.Repository;
using System.Data.Entity;
using ApiShort.Models;
using AutoMapper;

namespace ApiShort.Test
{
    [TestFixture]
    public class IntegrationTest
    {
        ShortDB db;
        IRepository<ProjectView> projectViewRepository;

        [SetUp]
        public void Setup()
        {
            db = new ShortDB();
            var projectRepository = new ProjectEFRepository(db);

            var mapperConfig = new MapperConfiguration(cfg => {
                cfg.CreateMap<Project, ProjectView>().ReverseMap();
                cfg.CreateMap<Facility, FacilityView>().ReverseMap();
            });

            projectViewRepository = new ProjectViewRepository(
                projectRepository, 
                mapperConfig.CreateMapper()
            );
        }

        [Test]
        public void CreateProjectTest()
        {
            using (db)
            using (projectViewRepository)
            using (var tr = db.Database.BeginTransaction())
            {
                var oldProjects = projectViewRepository.GetAll()
                    .ToList();
                var oldProjectsCount = oldProjects.Count;
                var oldFacilitiesCount = oldProjects
                    .SelectMany(x => x.Facilities)
                    .Count();

                var newProjectView = GetProjectView();
                projectViewRepository.Create(newProjectView);
                projectViewRepository.Save();

                var newProjects = projectViewRepository.GetAll()
                    .ToList();
                var newProjectsCount = newProjects.Count;
                var newFacilitiesCount = newProjects
                    .SelectMany(x => x.Facilities)
                    .Count();

                oldProjectsCount.Should().Be(newProjectsCount - 1);
                oldFacilitiesCount.Should().Be(newFacilitiesCount - 5);
            }
        }

        [Test]
        public void DeleteTest()
        {
            using (db)
            using (projectViewRepository)
            using (var tr = db.Database.BeginTransaction())
            {
                var oldProjects = projectViewRepository.GetAll()
                    .ToList();
                var oldProjectsCount = oldProjects.Count;
                var oldFacilitiesCount = oldProjects
                    .SelectMany(x => x.Facilities)
                    .Count();

                var projectView = oldProjects.First();
                var deletedFacilitiesCount = projectView.Facilities.Count;

                projectViewRepository.Delete(projectView.Id);
                projectViewRepository.Save();

                var newProjects = projectViewRepository.GetAll()
                    .ToList();
                var newProjectsCount = newProjects.Count;
                var newFacilitiesCount = newProjects
                    .SelectMany(x => x.Facilities)
                    .Count();

                oldProjectsCount.Should().Be(newProjectsCount + 1);
                oldFacilitiesCount.Should().Be(newFacilitiesCount + deletedFacilitiesCount);
            }
        }

        [Test]
        public void UpdateTest()
        {
            using (db)
            using (projectViewRepository)
            using (var tr = db.Database.BeginTransaction())
            {
                var projectView = projectViewRepository.GetAll()
                    .First();

                projectView.Cipher = "New Cipher";
                projectView.Name = "New Name";

                var s1 = db.Projects.ToList().Select(x => db.Entry(x)).ToList();

                projectViewRepository.Update(projectView);
                projectViewRepository.Save();

                var d = db.Projects.ToList().Select(x => db.Entry(x)).ToList();
            }
        }

        private ProjectView GetProjectView()
        {
            var newProjectView = new ProjectView()
            {
                Cipher = "newProjectView",
                Name = "newProjectView"
            };

            var facilities = new[]
            {
                    new FacilityView()
                    {
                        Code = "firstLevelFacility1",
                        Name = "firstLevelFacility1",
                    },
                    new FacilityView()
                    {
                        Code = "firstLevelFacility2",
                        Name = "firstLevelFacility2"
                    },
                    new FacilityView()
                    {
                        Code = "secondLevelFacility3",
                        Name = "secondLevelFacility3"
                    },
                    new FacilityView()
                    {
                        Code = "secondLevelFacility4",
                        Name = "secondLevelFacility4"
                    },
                    new FacilityView()
                    {
                        Code = "secondLevelFacility5",
                        Name = "secondLevelFacility5"
                    },
                };

            newProjectView.Facilities = facilities;

            foreach (var facility in facilities)
                facility.Project = newProjectView;

            facilities[2].AddParentFacilities(facilities[0]);
            facilities[3].AddParentFacilities(facilities[0]);
            facilities[3].AddParentFacilities(facilities[1]);
            facilities[4].AddParentFacilities(facilities[1]);

            facilities[0].AddChildFacilities(facilities[2]);
            facilities[0].AddChildFacilities(facilities[3]);
            facilities[1].AddChildFacilities(facilities[3]);
            facilities[1].AddChildFacilities(facilities[4]);

            return newProjectView;
        }
    }
}