using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.DirectorOperations.Queries.GetDirectorDetail;
using WebApi.DbOperations;
using WebApi.Entities;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.DirectorOperations.Queries.GetDirectorDetail
{
    public class GetDirectorDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetDirectorDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenNotFoundDirectorIdIsGiven_InvalidOperationException_ShouldBeReturn(){
        
            int id = _context.Directors.Max(x => x.Id)+111;

            GetDirectorDetailQuery query = new GetDirectorDetailQuery(_context,_mapper);
            query.DirectorId = id;

            FluentActions.Invoking(()=>query.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Yönetmen bulunamadı!");

        }

        [Fact]
        public void WhenValidDirectorIdIsGiven_Director_ShouldBeReturn(){
            var newDirector = new Director {Name="GetDirectorDetail", Surname="GetDirectorDetailTest"};
            _context.Directors.Add(newDirector);
            _context.SaveChanges();
            var Director = _context.Directors.SingleOrDefault(a => a.Name==newDirector.Name && a.Surname==newDirector.Surname);

            GetDirectorDetailQuery query = new GetDirectorDetailQuery(_context,_mapper);
            query.DirectorId = Director.Id;

            FluentActions.Invoking(()=>query.Handle()).Invoke();
            var findDirector = _context.Directors.SingleOrDefault(a => a.Id == Director.Id);
            findDirector.Should().NotBeNull();
            findDirector.Name.Should().Be(newDirector.Name);
            findDirector.Surname.Should().Be(newDirector.Surname);
            findDirector.IsActive.Should().Be(Director.IsActive);
        }

    }
}
