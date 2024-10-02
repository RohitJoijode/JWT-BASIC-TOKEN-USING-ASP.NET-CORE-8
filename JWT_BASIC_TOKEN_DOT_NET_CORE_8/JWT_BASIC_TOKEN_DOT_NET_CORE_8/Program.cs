using JWT_BASIC_TOKEN_DOT_NET_CORE_8;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

//Configure JWT Settings
var jwtSetting = new Jwtsettings();
builder.Configuration.GetSection("JwtSetting").Bind(jwtSetting);
builder.Services.AddSingleton(jwtSetting);

//Configure JWT authentication
//builder.Services.AddAuthentication(options =>
//{
//    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
//    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
//}).AddJwtBearer(options => options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
//{
//    ValidateIssuer = true,
//    ValidateAudience = true,
//    ValidateLifetime = true,
//    ValidateIssuerSigningKey = true,
//    ValidIssuer = "yourIssuer",
//    ValidAudience = "yourIssuer",
//    ClockSkew = TimeSpan.Zero,
//    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("082F41538C1178DE768A9AC86291678D"))
//});

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = "yourIssuer",
        ValidAudience = "yourAudience",
        ClockSkew = TimeSpan.Zero, // Set the clock skew to zero to ensure token expires at the exact time
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("082F41538C1178DE768A9AC86291678D"))
    };
});


builder.Services.AddAuthorization(); //Here Added by Rohit Joijode 

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication(); //Here Added by Rohit Joijode 
app.UseAuthorization();  


app.MapControllers();

app.Run();
