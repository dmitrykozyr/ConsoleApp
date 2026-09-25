using ApiAuthentication.Models;
using ApiAuthentication.Repositories;
using ApiAuthentication.Services.Interfaces;

namespace ApiAuthentication.Services;

public class MovieService : IMovieService
{
    public Movie Get(int id) => MovieRepository.Movies.FirstOrDefault(x => x.Id == id);

    public List<Movie> GetAll() => MovieRepository.Movies;

    public Movie Create(Movie movie)
    {
        movie.Id = MovieRepository.Movies.Count + 1;

        MovieRepository.Movies.Add(movie);

        return movie;
    }

    public Movie Update(Movie updatedMovie)
    {
        var oldMovie = MovieRepository.Movies.FirstOrDefault(x => x.Id == updatedMovie.Id);

        if (oldMovie is null)
        {
            return null;
        }

        oldMovie.Title          = updatedMovie.Title;
        oldMovie.Description    = updatedMovie.Description;
        oldMovie.Rating         = updatedMovie.Rating;

        return updatedMovie;
    }

    public bool Delete(int id)
    {
        var movie = MovieRepository.Movies.FirstOrDefault(x => x.Id == id);

        if (movie is null)
        {
            return false;
        }

        MovieRepository.Movies.Remove(movie);

        return true;
    }
}
