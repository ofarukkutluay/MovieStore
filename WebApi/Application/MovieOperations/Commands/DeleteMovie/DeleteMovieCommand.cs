using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.MovieOperations.Commands.DeleteMovie
{
    public class DeleteMovieCommand
    {
        public int MovieId { get; set; }
        private readonly IMovieStoreDbContext _dbContext;

        public DeleteMovieCommand(IMovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Handle(){
            var Movie = _dbContext.Movies.SingleOrDefault(a => a.Id == MovieId);
            if(Movie is null)
                throw new InvalidOperationException("Film bulunamadı!");
            var movieActor = _dbContext.MovieActors.Any(x=>x.MovieId == MovieId);
            if(!movieActor)
                throw new InvalidOperationException("Önce aktörler silinmelidir!");
            _dbContext.Movies.Remove(Movie);            
            int result = _dbContext.SaveChanges();
            return Convert.ToBoolean(result);
        }
    }
}