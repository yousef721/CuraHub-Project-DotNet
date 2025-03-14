//using CMS.Models.ClinicCustomer;
using CMS.Models.CuraHub.ClinicSection;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Data.Access.Layer.Data.Configrations.CuraHubConfigration.ClinicConfigration
{
    public class ScheduleConfig : IEntityTypeConfiguration<Schedule>
    {
        public void Configure(EntityTypeBuilder<Schedule> builder)
        {


            builder.HasKey(e => e.Id);

            

            builder.HasOne(e => e.Doctor)
                .WithMany(e => e.Schedules)
                .HasForeignKey(e => e.DoctorId);
            builder.HasMany(e => e.PatientAppointments)
                .WithOne(e => e.Schedule)
                .HasForeignKey(e => e.ScheduleId);

            builder.ToTable(name: "Schedules", schema: "Clinic");

        }
    }
}
