using Contact_Management_System.Models;
using Contact_Management_System.Utility;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointDefinitions(typeof(Contact));

var app = builder.Build();
app.UseEndpointDefinitions();

app.Run();
