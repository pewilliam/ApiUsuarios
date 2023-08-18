using ApiUsuarios;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSingleton<UserRepository>();
var services = builder.Services;

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1",
        new() { Title = "ApiUsers", Version = "v1" });
});

builder.Services.AddAWSLambdaHosting(LambdaEventSource.RestApi);

var app = builder.Build();
/*
app.MapGet("/users", ([FromServices] UserRepository repo) =>
{
    return repo.GetAll();
});*/

app.MapGet("/users/{id}", ([FromServices] UserRepository repo, int cpf) =>
{
    var user = repo.GetByCpf(cpf);
    if (user is null)
        return Results.NotFound("Usuário não encontrado.");

    return Results.Ok(user);

});

app.MapPost("/users", ([FromServices] UserRepository repo, User user) =>
{
    repo.Create(user);
    return Results.Created($"/customers/{user.Cpf}", user);
});

/*
app.MapPut("/users/{id}", ([FromServices] UserRepository repo, int cpf, User updatedUser) =>
{
    var user = repo.GetByCpf(cpf);
    if (user is null) return Results.NotFound();

    repo.Update(cpf, updatedUser);
    return Results.Ok(updatedUser);
});

app.MapDelete("/users/{id}", ([FromServices] UserRepository repo, int cpf) =>
{
    var user = repo.GetByCpf(cpf);
    if (user is null) return Results.NotFound();

    repo.Delete(cpf);
    return Results.Ok();
});*/

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint(
    "/swagger/v1/swagger.json",
    "v1"));
app.Run("http://localhost:7022");
