using BestMovies.Models.ApiModels;
using Newtonsoft.Json;

namespace BestMovies.DataAccess.RestApiDataAccess;

public class MovieRestApiDao : IMovieDao
{
    private readonly IRestApiDataAccess _api;

    public MovieRestApiDao(IRestApiDataAccess api)
    {
        _api = api;
    }

    public async Task<Movie> GetMovieAsync(string idString)
    {
        if (!int.TryParse(idString, out var id)) throw new Exception("Invalid Id");
        
        var url = $"movie/{id}";
        var response = await _api.SendRequestAsync(url);
        
        var movie = JsonConvert.DeserializeObject<Movie>(response.Content!);
        return movie ?? new Movie();
    }

    public async Task<List<Movie>> GetTrendingMoviesAsync()
    {
        var url = "movie/week";
        var response = await _api.SendRequestAsync(url);

        var movies = JsonConvert.DeserializeObject<List<Movie>>(response.Content!);
        return movies ?? new List<Movie>();
    }

    public async Task<Credits> GetCreditsFromMovie(string idString)
    {
        if (!int.TryParse(idString, out var id)) throw new Exception("Invalid Id");

        var url = $"movie/{id}/credits";
        var response = await _api.SendRequestAsync(url);

        var credits = JsonConvert.DeserializeObject<Credits>(response.Content!);
        return credits ?? new Credits();
    }
}