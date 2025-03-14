using CMS.Models;
using CMS.Models.CuraHub;
using CMS.Models.CuraHub.ClinicSection;
using CMS.Models.CuraHub.ClinicSection.ClinicSectionVM.ClinicReceptionistVM;
using CMS.Models.CuraHub.IdentitySection;
using CMS.Models.CuraHub.MedicalAnalysisLabSection;
using CMS.Models.CuraHub.PersonalDetails;
using CMS.Models.CuraHub.PersonalDetails.CustomerSection;
using CMS.Models.CuraHub.PersonalDetails.EmployeeSection;
using CMS.Models.CuraHub.PersonalDetails.EmployeeSection.DeliveryRepresentativeSection;
using CMS.Models.CuraHub.PersonalDetails.EmployeeSection.MedicalSection;
using CMS.Models.CuraHub.PersonalDetails.EmployeeSection.ReceptionistSection;
using CMS.Models.CuraHub.PharmacySection;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Data.Access.Layer.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        /// <summary>
        /// Clinic Tables
        /// </summary>
        public DbSet<Patient> Patients { get; set; }
        public DbSet<PatientHistory> PatientHistories { get; set; }
        public DbSet<PatientAppointment> PatientAppointments { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Qualification> Qualifications { get; set; }
        public DbSet<ClinicReceptionist> ClinicReceptionists { get; set; }
        public DbSet<RequestClinicReceptionist> RequestClinicReceptionists { get; set; }
        public DbSet<RequestDoctor> RequestDoctors { get; set; }
        public DbSet<Schedule> Schedules { get; set; }

        public DbSet<DoctorReview> DoctorReviews { get; set; }


        /// <summary>
        /// Parmacy Tables
        /// </summary>
        public DbSet<Medicine> Medicines { get; set; }
        public DbSet<MedicineManufactory> MedicineManufactories { get; set; }
        public DbSet<MedicineOrder> MedicineOrders { get; set; }
        public DbSet<Pharmacist> Pharmacists { get; set; }
        public DbSet<PharmacyCategory> PharmacyCategories { get; set; }
        public DbSet<PharmacyCustomer> PharmacyCustomers { get; set; }
        public DbSet<PharmacyDeliveryRepresentative> PharmacyDeliveryRepresentatives { get; set; }
        public DbSet<PharmacyOrder> PharmacyOrders { get; set; }
        public DbSet<PharmacyCart> PharmacyCarts { get; set; }
        /// <summary>
        /// Medical Analysis Lab Tables
        /// </summary>
        public DbSet<MedicalAnalysisLabAppointment> MedicalAnalysisLabAppointments { get; set; }
        public DbSet<MedicalAnalysisLabBranch> MedicalAnalysisLabBranches { get; set; }
        public DbSet<MedicalAnalysisLab> MedicalAnalysisLabs { get; set; }
        public DbSet<MedicalAnalysisLabCustomer> MedicalAnalysisLabCustomers { get; set; }
        public DbSet<MedicalAnalysisLabReceptionist> MedicalAnalysisLabReceptionists { get; set; }
        public DbSet<MedicalAnalysisSpecialist> MedicalAnalysisSpecialists { get; set; }
        public DbSet<MedicalAnalysisTest> MedicalAnalysisTests { get; set; }
        public DbSet<MedicalAnalysisTestCustomer> MedicalAnalysisTestCustomers { get; set; }
        public DbSet<MedicalAnalysisTestResult> MedicalAnalysisTestResults { get; set; }
        public DbSet<RequestMedicalAnalysisLabReceptionist> RequestMedicalAnalysisLabReceptionists { get; set; }
        public DbSet<RequestMedicalAnalysisSpecialist> RequestMedicalAnalysisSpecialists { get; set; }

        public DbSet<PatientAppointmentCard> PatientAppointmentCards { get; set; }

        public DbSet<MedicalPrescription> MedicalPrescriptions { get; set; }

        public DbSet<Message> Messages { get; set; }


        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
           : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {

            modelBuilder.Entity<PersonalDetails>().UseTphMappingStrategy();
            modelBuilder.Entity<Customer>().UseTphMappingStrategy();
            modelBuilder.Entity<Employee>().UseTphMappingStrategy();
            modelBuilder.Entity<Medical>().UseTphMappingStrategy();
            modelBuilder.Entity<Receptionist>().UseTphMappingStrategy();
            modelBuilder.Entity<RequestReceptionist>().UseTphMappingStrategy();
            modelBuilder.Entity<DeliveryRepresentative>().UseTphMappingStrategy();


            modelBuilder.Ignore<PersonalDetails>();
            modelBuilder.Ignore<Customer>();
            modelBuilder.Ignore<Employee>();
            modelBuilder.Ignore<Medical>();
            modelBuilder.Ignore<Receptionist>();
            modelBuilder.Ignore<RequestReceptionist>();
            modelBuilder.Ignore<DeliveryRepresentative>();


            //modelBuilder.Entity<PatientHistory>().ToTable("Histories");
            //modelBuilder.Entity<PatientAppointment>().ToTable("PatientAppointments");
            //modelBuilder.Entity<Doctor>().ToTable("Doctors");
            //modelBuilder.Entity<Qualification>().ToTable("Qualifications");
            //modelBuilder.Entity<RequestReceptionist>().ToTable("RequestReceptionists");
            //modelBuilder.Entity<Patient>().ToTable("Patients");
            //modelBuilder.Entity<Schedule>().ToTable("Schedules");
            //modelBuilder.Entity<RequestDoctor>().ToTable("RequestDoctors");
            //modelBuilder.Entity<Receptionist>().ToTable("Receptionists");




            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);

        }


    }
}
