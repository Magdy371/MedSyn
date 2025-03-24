using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using OutpatientClinic.Business.Services.Implementations;
using OutpatientClinic.Business.Services.Interfaces;
using OutpatientClinic.Core.UnitOfWorks;
using OutpatientClinic.DataAccess.Context;
using OutpatientClinic.DataAccess.Entities;
using System.Text;

namespace OutpatientClinic.Presentation
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Configure services
            ConfigureServices(builder);

            var app = builder.Build();

            // Configure the HTTP request pipeline
            ConfigurePipeline(app);

            app.Run();
        }

        public static void ConfigureServices(WebApplicationBuilder builder)
        {
            // Add services to the container
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<OutpatientClinicDbContext>(options =>
                options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
                //.EnableSensitiveDataLogging()
                //.LogTo(Console.WriteLine, LogLevel.Information)
                );

            // Configure Unit of Work
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

            // Register Generic Service if needed
            builder.Services.AddScoped(typeof(IGenericService<>), typeof(GenericService<>));

            // Register Specialized Services
            builder.Services.AddScoped<IDepartmentService, DepartmentService>();
            builder.Services.AddScoped<IAuthService, AuthService>();
            builder.Services.AddScoped<IAdminService, AdminService>();
            builder.Services.AddScoped<IClinicService, ClinicService>();
            builder.Services.AddScoped<IContactInfoService, ContactInfoService>();
            builder.Services.AddScoped<IStaffService, StaffService>();
            builder.Services.AddScoped<IDoctorService, DoctorService>();
            builder.Services.AddScoped<IPatientService, PatientService>();
            builder.Services.AddScoped<IAppointmentService, AppointmentService>();
            builder.Services.AddScoped<IMedicalRecordService, MedicalRecordService>();
            builder.Services.AddScoped<IPrescriptionService, PrescriptionService>();
            builder.Services.AddScoped<IFacilityService, FacilityService>();
            builder.Services.AddScoped<IInventoryService, InventoryService>();
            builder.Services.AddScoped<ISupplierService, SupplierService>();
            builder.Services.AddScoped<ISupplierOrderService, SupplierOrderService>();
            builder.Services.AddScoped<ISupplierOrderDetailsService, SupplierOrderDetailsService>();
            builder.Services.AddScoped<IDeliveryNoteService, DeliveryNoteService>();
            builder.Services.AddScoped<IDeliveryNoteDetailsService, DeliveryNoteDetailsService>();
            builder.Services.AddScoped<ILabTestService, LabTestService>();
            builder.Services.AddScoped<IInsuranceService, InsuranceService>();
            builder.Services.AddScoped<IBillingService, BillingService>();

            // Configure Identity
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<OutpatientClinicDbContext>()
                .AddDefaultTokenProviders();

            // Configure Authentication
            builder.Services.AddControllersWithViews();

            // Enable Authentication & Authorization
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = builder.Configuration["Jwt:Issuer"],
                        ValidAudience = builder.Configuration["Jwt:Audience"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Secret"]))
                    };
                });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminPolicy", policy => policy.RequireRole("Admin"));
                options.AddPolicy("DoctorPolicy", policy => policy.RequireRole("Doctor"));
                options.AddPolicy("PatientPolicy", policy => policy.RequireRole("Patient"));
                options.AddPolicy("StaffPolicy", policy => policy.RequireRole("Staff"));
            });
        }

        public static void ConfigurePipeline(WebApplication app)
        {
            // Configure the HTTP request pipeline
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
        }
    }
}
