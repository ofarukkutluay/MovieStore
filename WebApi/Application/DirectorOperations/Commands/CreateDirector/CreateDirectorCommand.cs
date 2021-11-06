using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.DirectorOperations.Commands.CreateDirector
{
    public class CreateDirectorCommand
    {
        public CreateDirectorViewModel Model { get; set; }
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateDirectorCommand(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public bool Handle(){
            var Director = _dbContext.Directors.SingleOrDefault(a => a.Name == Model.Name && a.Surname == Model.Surname);
            if(Director is not null)
                throw new InvalidOperationException("Yönetmen zaten mevcut!");
            Director = _mapper.Map<Director>(Model);
            _dbContext.Directors.Add(Director);
            int result = _dbContext.SaveChanges();
            return Convert.ToBoolean(result);
        }
    }

    public class CreateDirectorViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
    }
}