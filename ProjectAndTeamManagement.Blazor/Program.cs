using Blazored.LocalStorage;
using ManagementApp.Blazor.AuthProviders;
using ManagementApp.Blazor.Services;
using ManagementApp.Blazor.Services.Interfaces;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ProjectAndTeamManagement.Blazor;
using ProjectAndTeamManagement.Blazor.Services;
using ProjectAndTeamManagement.Blazor.Services.Interfaces;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddBlazoredLocalStorage();
builder.Services.AddAuthorizationCore();
builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();

builder.Services.AddHttpClient<IAuthenticationService, AuthenticationService>(client => client.BaseAddress = new Uri("https://localhost:7147"));
builder.Services.AddHttpClient<IEmployeeDataService, EmployeeDataService>(client => client.BaseAddress = new Uri("https://localhost:7147"));
builder.Services.AddHttpClient<IRoleService, RoleService>(client => client.BaseAddress = new Uri("https://localhost:7147"));
builder.Services.AddHttpClient<IDepartmentLeadService, DepartmentLeadService>(client => client.BaseAddress = new Uri("https://localhost:7147"));
builder.Services.AddHttpClient<ITeamService, TeamService>(client => client.BaseAddress = new Uri("https://localhost:7147"));
builder.Services.AddHttpClient<IRequestService, RequstService>(client => client.BaseAddress = new Uri("https://localhost:7147"));
builder.Services.AddHttpClient<IRequestStatusService, RequestStatusService>(client => client.BaseAddress = new Uri("https://localhost:7147"));



await builder.Build().RunAsync();
