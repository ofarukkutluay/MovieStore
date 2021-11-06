using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.DirectorOperations.Commands.UpdateDirector
{
    public class UpdateDirectorCommand
    {
        public int DirectorId { get; set; }
        public UpdateDirectorViewModel Model { get; set; }
        private readonly IMovieStoreDbContext _dbContext;

        public UpdateDirectorCommand(IMovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Handle(){
            var Director = _dbContext.Directors.SingleOrDefault(a => a.Id == DirectorId);
            if(Director is null)
                throw new InvalidOperationException("Yönetmen bulunamadı!");
            Director.Name = Model.Name == default ? Director.Name : Model.Name;
            Director.Surname = Model.Surname == default ? Director.Surname : Model.Surname;
            Director.IsActive = Model.IsActive == default ? Director.IsActive : Model.IsActive;

            int result = _dbContext.SaveChanges();
            return Convert.ToBoolean(result);
        }
    }

    public class UpdateDirectorViewModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsActive { get; set; }

    }
}