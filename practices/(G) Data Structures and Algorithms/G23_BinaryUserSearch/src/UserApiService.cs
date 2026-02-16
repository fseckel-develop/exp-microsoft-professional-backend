using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

public static class UserApiService
{
    private static string url = "https://randomuser.me/api/?results=10";

    public static async Task<List<User>> FetchUsersFromAPI()
    {
        List<User> users = new List<User>();

        using HttpClient client = new HttpClient();
        var response = await client.GetFromJsonAsync<ApiResponse>(url);
        if (response is null) return users;

        foreach (var result in response.Results!)
        {
            users.Add(new User
            {
                Username = result.Login.Username,
                Name = $"{result.Name.First} {result.Name.Last}"
            });
        }

        return users;
    }
}