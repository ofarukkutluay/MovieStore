using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.MovieOperations.Commands.UpdateMovie
{
    public class UpdateMovieCommand
    {
        public int MovieId { get; set; }
        public UpdateMovieViewModel Model { get; set; }
        private readonly IMovieStoreDbContext _dbContext;

        public UpdateMovieCommand(IMovieStoreDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public bool Handle(){
            var Movie = _dbContext.Movies.SingleOrDefault(a => a.Id == MovieId);
            if(Movie is null)
                throw new InvalidOperationException("Film bulunamadı!");
            Movie.Title = Model.Title == default ? Movie.Title : Model.Title;
            Movie.ReleaseDate = Model.ReleaseDate == default ? Movie.ReleaseDate : Model.ReleaseDate;
            Movie.GenreId = Model.GenreId == default ? Movie.GenreId : Model.GenreId;
            Movie.DirectorId = Model.DirectorId == default ? Movie.DirectorId : Model.DirectorId;
            Movie.Price = Model.Price == default ? Movie.Price : Model.Price;

            int result = _dbContext.SaveChanges();
            return Convert.ToBoolean(result);
        }
    }

    public class UpdateMovieViewModel
    {
        public string Title { get; set; }
        
        public int ReleaseDate { get; set; }
        public int GenreId { get; set; }
        public int DirectorId { get; set; }
        public decimal Price { get; set; }  

    }
}