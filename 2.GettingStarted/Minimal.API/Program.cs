using Microsoft.AspNetCore.Mvc;
using Minimal.API;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<GuidGenerator>();
builder.Services.AddScoped<PeopleService>();

var app = builder.Build();

app.MapGet("get-example", () => "Hola froma getto");
app.MapPost("post-example", () => "Hola froma getto");

app.MapGet("ok-object", () => Results.Ok(new { Message = "API is working..." }));

app.Map("slow-request", async () =>
{
    await Task.Delay(2000);

    return Results.Ok(new
    {
        Message = "Slow API is working..."
    });
});

app.MapGet("get", () => "this is a GET");
app.MapPost("post", () => "this is a POST");
app.MapPut("Put", () => "this is a PUT");
app.MapDelete("delete", () => "this is a DELETE");

app.MapMethods("option-or-head", new[] { "HEAD", "OPTIONS" }, () => "Hello form options or head");

var handler = () => "This is coming from a var";
app.MapGet("handler", handler);

app.MapGet("fromclass", Example.SomeMethod);


app.MapGet("get-params/{age:int}", (int age) =>
{
    return $"Age provided war {age}";
});

app.MapGet("cars/{carId:regex(^[a-z0-9]+$)}", (string carId) =>
{
    return $"Car id privided was : {carId}";
});

app.MapGet("books/{isbn:length(13)}", (string isbn) =>
{
    return $"ISBN provided was : {isbn}";
});




app.MapGet("people/search", (string? searchTerm, PeopleService peopleService) =>
{
    if (searchTerm is null) return Results.NotFound();

    var result = peopleService.Search(searchTerm);

    return Results.Ok(result);

});

app.MapGet("mix/{routeParams}", (
    
    [FromRoute]string routeParams, 
    [FromQuery(Name ="q")]int queryParams, 
    [FromServices]GuidGenerator quidGenerator) =>
{
    return $"Route param: {routeParams}, Query param: {queryParams}, Guid: {quidGenerator.NewGuid}";
});


app.MapPost("people", (Person person) =>
{
    return Results.Ok(person);
});





app.Run();
