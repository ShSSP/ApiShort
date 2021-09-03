using ApiShort.DAL.EFDbModel;
using ApiShort.Models;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ApiShort.DAL.Repository
{
    public class ProjectViewRepository : IRepository<ProjectView>, IDisposable
    {
        private readonly IMapper mapper;
        private readonly IRepository<Project> projectRepository;

        public ProjectViewRepository(IRepository<Project> projectRepository, IMapper mapper)
        {
            this.mapper = mapper;
            this.projectRepository = projectRepository;
        }

        public IEnumerable<ProjectView> GetAll()
        {
            return projectRepository.GetAll()
                .Select(x => mapper.Map<ProjectView>(x))
                .ToList();
        }

        public ProjectView Get(int id)
        {
            return mapper.Map<ProjectView>(projectRepository.Get(id));
        }
        public void Create(ProjectView item)
        {
            projectRepository.Create(mapper.Map<Project>(item));
        }

        public void Update(ProjectView item)
        {
            projectRepository.Update(mapper.Map<Project>(item));
        }

        public void Delete(int id)
        {
            projectRepository.Delete(id);
        }

        public void Save()
        {
            projectRepository.Save();
        }

        public void Dispose()
        {
            projectRepository.Dispose();
        }
    }
}
