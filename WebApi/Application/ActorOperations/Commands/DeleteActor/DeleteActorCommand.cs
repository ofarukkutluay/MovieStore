using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.ActorOperations.Commands.DeleteActor
{
    public class DeleteActorCommand
    {
        public int ActorId { get; set; }
        private readonly IMovieStoreDbContext _dbContext;

        public DeleteActorCommand(IMovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Handle(){
            var actor = _dbContext.Actors.SingleOrDefault(a => a.Id == ActorId);
            if(actor is null)
                throw new InvalidOperationException("Aktör bulunamadı!");
            var movieActors = _dbContext.MovieActors.Any(x => x.ActorId == ActorId);
            if(!movieActors)
                throw new InvalidOperationException("Önce filmden aktör silinmelidir!");
            _dbContext.Actors.Remove(actor);            
            int result = _dbContext.SaveChanges();
            return Convert.ToBoolean(result);
        }
    }
}