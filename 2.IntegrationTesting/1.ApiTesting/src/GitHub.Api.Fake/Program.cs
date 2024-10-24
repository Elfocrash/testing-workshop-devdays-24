
using System.Net;
using WireMock.RequestBuilders;
using WireMock.ResponseBuilders;
using WireMock.Server;

var server = WireMockServer.Start();

server.Given(Request.Create().UsingGet().WithPath("/users/nickchapsas"))
    .RespondWith(Response.Create()
        .WithStatusCode(200)
        .WithHeader("Content-Type", "application/json; charset=utf-8")
        .WithBody(@"{
    ""login"": ""Elfocrash"",
    ""id"": 8199968,
    ""node_id"": ""MDQ6VXNlcjgxOTk5Njg="",
    ""avatar_url"": ""https://avatars.githubusercontent.com/u/8199968?v=4"",
    ""gravatar_id"": """",
    ""url"": ""https://api.github.com/users/Elfocrash"",
    ""html_url"": ""https://github.com/Elfocrash"",
    ""followers_url"": ""https://api.github.com/users/Elfocrash/followers"",
    ""following_url"": ""https://api.github.com/users/Elfocrash/following{/other_user}"",
    ""gists_url"": ""https://api.github.com/users/Elfocrash/gists{/gist_id}"",
    ""starred_url"": ""https://api.github.com/users/Elfocrash/starred{/owner}{/repo}"",
    ""subscriptions_url"": ""https://api.github.com/users/Elfocrash/subscriptions"",
    ""organizations_url"": ""https://api.github.com/users/Elfocrash/orgs"",
    ""repos_url"": ""https://api.github.com/users/Elfocrash/repos"",
    ""events_url"": ""https://api.github.com/users/Elfocrash/events{/privacy}"",
    ""received_events_url"": ""https://api.github.com/users/Elfocrash/received_events"",
    ""type"": ""User"",
    ""site_admin"": false,
    ""name"": ""Nick Chapsas"",
    ""company"": null,
    ""blog"": ""https://dometrain.com"",
    ""location"": ""London, UK"",
    ""email"": null,
    ""hireable"": null,
    ""bio"": ""I just like making stuff | Microsoft MVP"",
    ""twitter_username"": ""nickchapsas"",
    ""public_repos"": 48,
    ""public_gists"": 2,
    ""followers"": 5752,
    ""following"": 0,
    ""created_at"": ""2014-07-18T09:32:23Z"",
    ""updated_at"": ""2022-08-10T14:00:41Z""
}"));

Console.WriteLine(server.Url);

Console.ReadKey();
server.Dispose();
