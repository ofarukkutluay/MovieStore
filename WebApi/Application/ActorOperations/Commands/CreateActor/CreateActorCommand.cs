using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.ActorOperations.Commands.CreateActor
{
    public class CreateActorCommand
    {
        public CreateActorViewModel Model { get; set; }
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateActorCommand(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public bool Handle(){
            var actor = _dbContext.Actors.SingleOrDefault(a => a.Name == Model.Name && a.Surname == Model.Surname);
            if(actor is not null)
                throw new InvalidOperationException("Aktör zaten mevcut!");
            actor = _mapper.Map<Actor>(Model);
            _dbContext.Actors.Add(actor);
            int result = _dbContext.SaveChanges();
            return Convert.ToBoolean(result);
        }
    }

    public class CreateActorViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        // public List<CreateMovieActorVM> MovieActor { get; set; }

        // public struct CreateMovieActorVM
        // {
        //     public int MovieId { get; set; }
        // }
    }
}