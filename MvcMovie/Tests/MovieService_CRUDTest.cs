using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using MvcMovie.Data;
using MvcMovie.Features.Movies.Services;
using MvcMovie.Features.Movies.Models;
using Xunit;

namespace Tests;

public class MovieService_CRUDTest
{
    private static MovieService CreateService()
    {
        var options = new DbContextOptionsBuilder<MvcMovieContext>().UseInMemoryDatabase(Guid.NewGuid().ToString()).Options;
        var context = new MvcMovieContext(options);
        return new MovieService(context);
    }

    [Fact]
    public async Task MovieService_CanPerformCRUDOp()
    {
        var service = CreateService();

        //Create
        var movie = new Movie
        {
            Title = "Shark Tale",
            ReleaseDate = DateTime.Parse("2004-10-01"),
            Genre = "Animated Adventure",
            Price = 14.99M,
            Rating = "PG"
        };

        await service.AddAsync(movie);

        //Read
        var read = await service.GetByIdAsync(movie.Id);
        Assert.NotNull(read);
        Assert.Equal(movie.Title, read.Title);

        //Update
        read.Title = "Shark Tale (HD)";
        await service.UpdateAsync(read);
        var updated = await service.GetByIdAsync(movie.Id);
        Assert.NotNull(updated);
        Assert.Equal(read.Title, updated.Title);

        //Delete
        await service.DeleteAsync(movie.Id);
        var deleted = await service.GetByIdAsync(movie.Id);
        Assert.Null(deleted);
    }
}
