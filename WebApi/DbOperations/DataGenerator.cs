using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Entities;

namespace WebApi.DbOperations
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var context = new MovieStoreDbContext(serviceProvider.GetRequiredService<DbContextOptions<MovieStoreDbContext>>()))
            {
                List<Genre> genres = new List<Genre>{
                    new Genre{
                        Name = "Action"
                    },
                    new Genre{
                        Name = "Adventure"
                    },
                    new Genre{
                        Name = "Fantacy"
                    }};
                
                List<Actor> actors = new List<Actor>{
                    new Actor
                    {
                        Name = "Daniel",
                        Surname = "Craig"
                    },
                    new Actor
                    {
                        Name = "Timothée",
                        Surname = "Chalamet"
                    },
                    new Actor
                    {
                        Name = "Jason",
                        Surname = "Momoa"
                    },
                    new Actor
                    {
                        Name = "Ana de",
                        Surname = "Armas"
                    }
                };

                List<Director> directors = new List<Director>{
                    new Director
                    {
                        Name = "Denis",
                        Surname = "Villeneuve"
                    },
                    new Director
                    {
                        Name = "Cary Joji",
                        Surname = "Fukunaga"
                    },
                    new Director
                    {
                        Name = "James",
                        Surname = " Wan"
                    }
                };

                List<Movie> movies = new List<Movie>{
                    
                    new Movie
                    {
                        Title = "No Time To Die",
                        GenreId = 1,
                        ReleaseDate = 2021,
                        DirectorId = 2,
                        Price = 100.0m
                    },
                    new Movie
                    {
                        Title = "Dune: Part One",
                        GenreId = 1,
                        ReleaseDate = 2021,
                        DirectorId = 1,
                        Price = 110.0m
                    },
                    new Movie
                    {
                        Title = "Aquaman",
                        GenreId = 3,
                        ReleaseDate = 2018,
                        DirectorId = 3,
                        Price = 50.0m
                    }
                
                };

                List<MovieActor> movieActors = new List<MovieActor>{
                    new MovieActor
                    {
                        ActorId = 1,
                        MovieId = 1
                    },
                    new MovieActor
                    {
                        ActorId = 4,
                        MovieId = 1
                    },
                    new MovieActor
                    {
                        ActorId = 2,
                        MovieId = 2
                    },
                    new MovieActor
                    {
                        ActorId = 3,
                        MovieId = 3
                    }
                };

                List<Customer> customers = new List<Customer>{
                    new Customer{
                        Name = "Omer",
                        Surname = "Kutluay",
                        Email = "omer@kutluay.com",
                        Password ="password"
                    },
                    new Customer{
                        Name = "Ali",
                        Surname = "Avare",
                        Email = "ali@avare.com",
                        Password ="password"
                    }
                };

                List<OrderMovie> orderMovies = new List<OrderMovie>{
                    new OrderMovie{
                        MovieId=1,
                        CustomerId=1,
                        PurchasedDate = new DateTime(2020,12,01),
                        PurchasedPrice = 90
                    },
                    new OrderMovie{
                        MovieId=3,
                        CustomerId=1,
                        PurchasedDate = new DateTime(2020,12,02),
                        PurchasedPrice = 105
                    },
                    new OrderMovie{
                        MovieId=2,
                        CustomerId=2,
                        PurchasedDate = new DateTime(2020,12,02),
                        PurchasedPrice = 120
                    }
                };

                List<CustomerFavoritGenre> customerFavoritGenres = new List<CustomerFavoritGenre>{
                    new CustomerFavoritGenre{
                        CustomerId = 1,
                        GenreId = 1
                    },
                    new CustomerFavoritGenre{
                        CustomerId = 1,
                        GenreId = 2
                    },
                    new CustomerFavoritGenre{
                        CustomerId = 1,
                        GenreId = 3
                    },
                    new CustomerFavoritGenre{
                        CustomerId = 2,
                        GenreId = 1
                    },
                    new CustomerFavoritGenre{
                        CustomerId = 2,
                        GenreId = 3
                    },
                };

                if (context.Genres.Any())
                {
                    return;
                }
                context.Genres.AddRange(genres);


                if (context.Actors.Any())
                {
                    return;
                }
                context.Actors.AddRange(actors);


                if (context.Directors.Any())
                {
                    return;
                }

                context.Directors.AddRange(directors);


                if (context.Movies.Any())
                {
                    return;
                }
                context.Movies.AddRange(movies);


                if (context.MovieActors.Any())
                {
                    return;
                }
                context.MovieActors.AddRange(movieActors);
                if (context.Customers.Any())
                {
                    return;
                }
                context.Customers.AddRange(customers);
                if(context.OrderMovies.Any()){
                    return;
                }
                context.OrderMovies.AddRange(orderMovies);
                if (context.CustomerFavoritGenres.Any())
                {
                    return;
                }
                context.CustomerFavoritGenres.AddRange(customerFavoritGenres);

                context.SaveChanges();
            }

        }

    }

}