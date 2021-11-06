using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.MovieOperations.Commands.CreateMovie
{
    public class CreateMovieCommand
    {
        public CreateMovieViewModel Model { get; set; }
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateMovieCommand(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public bool Handle(){
            var Movie = _dbContext.Movies.SingleOrDefault(a => a.Title == Model.Title && a.ReleaseDate == Model.ReleaseDate);
            if(Movie is not null)
                throw new InvalidOperationException("Film zaten mevcut!");
            Movie = _mapper.Map<Movie>(Model);
            _dbContext.Movies.Add(Movie);
            int result = _dbContext.SaveChanges();
            return Convert.ToBoolean(result);
        }
    }

    public class CreateMovieViewModel
    {
        public string Title { get; set; }
        
        public int ReleaseDate { get; set; }
        public int GenreId { get; set; }
        public int DirectorId { get; set; }
        public decimal Price { get; set; }  

    }
}