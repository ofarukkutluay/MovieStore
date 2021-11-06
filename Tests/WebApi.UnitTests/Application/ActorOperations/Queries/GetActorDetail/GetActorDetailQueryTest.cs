using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using Tests.WebApi.UnitTests.TestSetup;
using WebApi.Application.ActorOperations.Queries.GetActorDetail;
using WebApi.DbOperations;
using WebApi.Entities;
using Xunit;

namespace Tests.WebApi.UnitTests.Application.ActorOperations.Queries.GetActorDetail
{
    public class GetActorDetailQueryTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;

        public GetActorDetailQueryTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenNotFoundActorIdIsGiven_InvalidOperationException_ShouldBeReturn(){
        
            int id = _context.Actors.Max(x => x.Id)+111;

            GetActorDetailQuery query = new GetActorDetailQuery(_context,_mapper);
            query.ActorId = id;

            FluentActions.Invoking(()=>query.Handle()).Should().Throw<InvalidOperationException>().And.Message.Should().Be("Aktör bulunamadı!");

        }

        [Fact]
        public void WhenValidActorIdIsGiven_Actor_ShouldBeReturn(){
            var newActor = new Actor {Name="GetActorDetail", Surname="GetActorDetailTest"};
            _context.Actors.Add(newActor);
            _context.SaveChanges();
            var actor = _context.Actors.SingleOrDefault(a => a.Name==newActor.Name && a.Surname==newActor.Surname);

            GetActorDetailQuery query = new GetActorDetailQuery(_context,_mapper);
            query.ActorId = actor.Id;

            FluentActions.Invoking(()=>query.Handle()).Invoke();
            var findActor = _context.Actors.SingleOrDefault(a => a.Id == actor.Id);
            findActor.Should().NotBeNull();
            findActor.Name.Should().Be(newActor.Name);
            findActor.Surname.Should().Be(newActor.Surname);
            findActor.IsActive.Should().Be(actor.IsActive);
        }

    }
}
