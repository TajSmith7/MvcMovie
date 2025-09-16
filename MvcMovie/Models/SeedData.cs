using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using MvcMovie.Data;
using MvcMovie.Features.Movies.Models;
using System;
using System.Linq;

namespace MvcMovie.Models;

public static class SeedData
{
    public static void Initialize(IServiceProvider serviceProvider)
    {
        using (var context = new MvcMovieContext(
            serviceProvider.GetRequiredService<
                DbContextOptions<MvcMovieContext>>()))
        {
            // Look for any movies.
            if (context.Movie.Any())
            {
                return;   // DB has been seeded
            }
            context.Movie.AddRange(
                new Movie
                {
                    Title = "The Lion King",
                    ReleaseDate = DateTime.Parse("1994-6-15"),
                    Genre = "Family Musical",
                    Price = 10.00M,
                    Rating = "G"
                },
                new Movie
                {
                    Title = "Harry Potter and the Sorcerer's Stone",
                    ReleaseDate = DateTime.Parse("2001-11-16"),
                    Genre = "Family Fantasy",
                    Price = 8.08M,
                    Rating = "PG"
                },
                new Movie
                {
                    Title = "Raya and the Last Dragon",
                    ReleaseDate = DateTime.Parse("2021-3-5"),
                    Genre = "Family Fantasy",
                    Price = 13.89M,
                    Rating = "PG"
                },
                 new Movie
                 {
                     Title = "Charlie and the Chocolate Factory",
                     ReleaseDate = DateTime.Parse("2005-7-10"),
                     Genre = "Family Fantasy",
                     Price = 5.99M,
                     Rating = "PG"
                 },
                new Movie
                {
                    Title = "Rio",
                    ReleaseDate = DateTime.Parse("2011-4-15"),
                    Genre = "Family Adventure",
                    Price = 8.99M,
                    Rating = "G"
                }
            );
            context.SaveChanges();
        }
    }
}