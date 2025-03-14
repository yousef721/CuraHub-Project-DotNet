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
    public class PatientAppointmentCardConfig : IEntityTypeConfiguration<PatientAppointmentCard>
    {
        public void Configure(EntityTypeBuilder<PatientAppointmentCard> builder)
        {
            builder.HasKey(e => new { e.ApplicationUserId, e.PatientId, e.ScheduleId });



            builder.ToTable(name: "PatientAppointmentCards", schema: "Clinic");




        }
    }
}
