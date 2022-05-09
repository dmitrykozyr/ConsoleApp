using GraphQL.Data;
using GraphQL.GraphQL;
using GraphQL.Server.Ui.Voyager;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

// Для многопоточности используем AddPooledDbContextFactory вместо AddDbContext
builder.Services.AddPooledDbContextFactory<ApiDbContext>(options =>
    options.UseSqlite(
        builder.Configuration.GetConnectionString("DefaultConnection")
        ));

// Подключаем возможность выполнять Query
builder.Services.AddGraphQLServer()
                    .AddQueryType<Query>()          // Позволяет выполнять Query в Postman
                    //.AddType<ListType>()
                    .AddProjections()               // Если в Query.cs использует аттрибут UseProjection,
                                                    // то нужно добавить эту строчку
                    .AddMutationType<Mutation>()    // Позволяет выполнять Mutation в Postman
                    .AddFiltering()                 // Эта и нижняя строчка позволяют в QraphQL запросе писать фильтры с where ..
                    .AddSorting();                  // .. и сортировки

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseEndpoints(endpoints =>
{
    endpoints.MapGraphQL();
});

app.UseGraphQLVoyager(new VoyagerOptions()
{
    GraphQLEndPoint = "/graphql"
}, "/graphql-ui");

app.UseAuthorization();
app.MapRazorPages();
app.Run();
