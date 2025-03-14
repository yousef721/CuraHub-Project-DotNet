using AspNetCoreGeneratedDocument;
using CMS.Data.Access.Layer.Data;
using CMS.Data.Access.Layer.Repository.IRepository;
using CMS.Models.CuraHub.ClinicSection;
using CMS.Models.CuraHub.IdentitySection;
using CMS.Utitlities.StaticData;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CMS.Perestation.Layer.DbInitilization
{
    public class DbInitilizer : IDbInitilizer
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IUnitOfWork _unitOfWork;
        private readonly ApplicationDbContext _dbContext;

        public DbInitilizer(IUnitOfWork unitOfWork, UserManager<ApplicationUser> userManager , RoleManager<IdentityRole> roleManager,ApplicationDbContext dbContext) 
        {
            this._userManager = userManager;
            this._roleManager = roleManager;
            this._dbContext = dbContext;
            this._unitOfWork = unitOfWork;
        }
        public  void Initilizer()
        {
            try
            {
                if (this._dbContext.Database.GetPendingMigrations().Any())
                {
                    this._dbContext.Database.Migrate();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
           
            if (!this._roleManager.Roles.Any())
            {
                this._roleManager.CreateAsync(new(Role.AdminRole)).GetAwaiter().GetResult();
                this._roleManager.CreateAsync(new(Role.CustomerRole)).GetAwaiter().GetResult();
                this._roleManager.CreateAsync(new(Role.DoctorRole)).GetAwaiter().GetResult();
                this._roleManager.CreateAsync(new(Role.CompanyRole)).GetAwaiter().GetResult();
                this._roleManager.CreateAsync(new(Role.ClinicReceptionistRole)).GetAwaiter().GetResult();

            }
            if (this._userManager.FindByNameAsync("Admin").GetAwaiter().GetResult() == null)
            {
                this._userManager.CreateAsync(new()
                {
                    UserName = "Admin",
                    Email = "Admin@gmail.com",
                    FirstName = "Admin",
                    LastName = "Admin",
                    ProfilePicture = "Admin.jpg",
                    PhoneNumber = "+201023456789"

                }, "@Admin123").GetAwaiter().GetResult();
                var AdminUser = _userManager.FindByEmailAsync("Admin@gmail.com").GetAwaiter().GetResult();
                if (AdminUser != null)
                {
                    this._userManager.AddToRoleAsync(AdminUser, Role.AdminRole).GetAwaiter().GetResult();
                }
            }

            if (this._unitOfWork.SpecializationRepository.Retrive().Count() == 0)
            {
                this._unitOfWork.SpecializationRepository.Create(new Specialization { Name = "Neurology", Icon = "Neurology.jpg" });
                this._unitOfWork.SpecializationRepository.Create(new Specialization { Name = "Cardiology", Icon = "Cardiology.jpg" });
                this._unitOfWork.SpecializationRepository.Create(new Specialization { Name = "Dermatology", Icon = "Dermatology.jpg" });
                this._unitOfWork.SpecializationRepository.Create(new Specialization { Name = "Pediatrics", Icon = "Pediatrics.jpg" });
                this._unitOfWork.SpecializationRepository.Create(new Specialization { Name = "Dentistry", Icon = "Dentistry.jpg" });
                this._unitOfWork.SpecializationRepository.Create(new Specialization { Name = "Otolaryngology", Icon = "Otolaryngology .jpg" });
                this._unitOfWork.SpecializationRepository.Create(new Specialization { Name = "Ophthalmology", Icon = "Ophthalmology.jpg" });
                this._unitOfWork.SpecializationRepository.Create(new Specialization { Name = "Endocrinology", Icon = "Endocrinology.webp" });
                this._unitOfWork.SpecializationRepository.Create(new Specialization { Name = "General Surgery", Icon = "GeneralSurgery.png" });
                this._unitOfWork.SpecializationRepository.Create(new Specialization { Name = "Orthopedic Surgery", Icon = "OrthopedicSurgery.webp" });
                this._unitOfWork.SpecializationRepository.Create(new Specialization { Name = "Plastic Surgery", Icon = "PlasticSurgery.webp" });
                this._unitOfWork.SpecializationRepository.Create(new Specialization { Name = "Hematology", Icon = "Hematology.webp" });
                this._unitOfWork.SpecializationRepository.Create(new Specialization { Name = "Gastroenterology", Icon = "Gastroenterology.jpg" });
                this._unitOfWork.SpecializationRepository.Create(new Specialization { Name = "Nephrology", Icon = "Nephrology.png" });
                this._unitOfWork.SpecializationRepository.Create(new Specialization { Name = "Psychiatry", Icon = "Psychiatry.webp" });
                this._unitOfWork.SpecializationRepository.Create(new Specialization { Name = "Intensive Care Medicine", Icon = "IntensiveCareMedicine.jpg" });
                this._unitOfWork.SpecializationRepository.Create(new Specialization { Name = "Emergency Medicine ", Icon = "EmergencyMedicine.jpg" });
                this._unitOfWork.SpecializationRepository.Create(new Specialization { Name = "Rheumatology", Icon = "Rheumatology.jpg" });
                this._unitOfWork.SpecializationRepository.Create(new Specialization { Name = "Pulmonology", Icon = "Pulmonology.jpg" });
                this._unitOfWork.SpecializationRepository.Create(new Specialization { Name = "Oncology", Icon = "Oncology.webp" });
                this._unitOfWork.SpecializationRepository.Create(new Specialization { Name = "Infectious Diseases", Icon = "InfectiousDiseases.png" });
                this._unitOfWork.SpecializationRepository.Create(new Specialization { Name = "Psychology", Icon = "Psychology.png" });
                this._unitOfWork.SpecializationRepository.Create(new Specialization { Name = "Pediatric Neurology", Icon = "PediatricNeurology.jpg" });
                this._unitOfWork.SpecializationRepository.Create(new Specialization { Name = "Adult Neurology", Icon = "AdultNeurology.jpg" });
                this._unitOfWork.SpecializationRepository.Create(new Specialization { Name = "Dermatological Neurology", Icon = "DermatologicalNeurology.jpg" });
                this._unitOfWork.SpecializationRepository.Create(new Specialization { Name = "Neuropsychiatry", Icon = "Neuropsychiatry.png" });
                this._unitOfWork.SpecializationRepository.Create(new Specialization { Name = "Neuroorthopedics", Icon = "Neuroorthopedics.png" });
                this._unitOfWork.SpecializationRepository.Create(new Specialization { Name = "Neurosurgery", Icon = "Neurosurgery.jpg" });
                this._unitOfWork.SpecializationRepository.Create(new Specialization { Name = "Neurology", Icon = "Neurology.jpg" });
                this._unitOfWork.SpecializationRepository.Create(new Specialization { Name = "Dermatological Neurology", Icon = "DermatologicalNeurology.jpg" });
                this._unitOfWork.Commit();


            }

        }
    
        
    }
}
