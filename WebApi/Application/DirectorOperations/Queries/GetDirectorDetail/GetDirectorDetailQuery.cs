
using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.DbOperations;
using WebApi.Entities;

namespace WebApi.Application.DirectorOperations.Queries.GetDirectorDetail
{
    public class GetDirectorDetailQuery
    {
        public int DirectorId { get; set; }
        private readonly IMovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetDirectorDetailQuery(IMovieStoreDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public DirectorDetailViewModel Handle()
        {
            var director = _dbContext.Directors.Include(a=>a.Movies).SingleOrDefault(x => x.Id==DirectorId);
            if(director is null)
                throw new InvalidOperationException("Yönetmen bulunamadı!");
            DirectorDetailViewModel returnObj = _mapper.Map<DirectorDetailViewModel>(director);
            return returnObj;
        }
    }

    public class DirectorDetailViewModel
    {
        public int Id { get; set; }
        public string FullName { get; set; }
        public List<DirectorMoviesVM> Movies { get; set; }

        public struct DirectorMoviesVM
        {
            public int Id { get; set; }
            public string Title { get; set; }   
        }

    }

}