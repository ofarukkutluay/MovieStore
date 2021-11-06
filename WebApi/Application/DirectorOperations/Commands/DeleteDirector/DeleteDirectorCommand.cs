using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.DirectorOperations.Commands.DeleteDirector
{
    public class DeleteDirectorCommand
    {
        public int DirectorId { get; set; }
        private readonly IMovieStoreDbContext _dbContext;

        public DeleteDirectorCommand(IMovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Handle(){
            var Director = _dbContext.Directors.SingleOrDefault(a => a.Id == DirectorId);
            if(Director is null)
                throw new InvalidOperationException("Yönetmen bulunamadı!");
            var movies = _dbContext.Movies.Any(x=> x.DirectorId == DirectorId);
            if(!movies)
                throw new InvalidOperationException("Önce filmden yönetmen silinmelidir!");
            _dbContext.Directors.Remove(Director);            
            int result = _dbContext.SaveChanges();
            return Convert.ToBoolean(result);
        }
    }
}