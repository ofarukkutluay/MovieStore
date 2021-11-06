using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.ActorOperations.Commands.UpdateActor
{
    public class UpdateActorCommand
    {
        public int ActorId { get; set; }
        public UpdateActorViewModel Model { get; set; }
        private readonly IMovieStoreDbContext _dbContext;

        public UpdateActorCommand(IMovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Handle(){
            var actor = _dbContext.Actors.SingleOrDefault(a => a.Id == ActorId);
            if(actor is null)
                throw new InvalidOperationException("Aktör bulunamadı!");
            actor.Name = Model.Name == default ? actor.Name : Model.Name;
            actor.Surname = Model.Surname == default ? actor.Surname : Model.Surname;
            actor.IsActive = Model.IsActive == default ? actor.IsActive : Model.IsActive;

            int result = _dbContext.SaveChanges();
            return Convert.ToBoolean(result);
        }
    }

    public class UpdateActorViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsActive { get; set; }

    }
}