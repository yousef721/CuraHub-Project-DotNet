using CMS.Data.Access.Layer.Repository.IRepository.ICuraHubRepository;
using CMS.Data.Access.Layer.Repository.IRepository.ICuraHubRepository.IClinicRepository;
using CMS.Data.Access.Layer.Repository.IRepository.ICuraHubRepository.IIdentityRepository;
using CMS.Data.Access.Layer.Repository.IRepository.ICuraHubRepository.IMedicalAnalysisLabRepository;
using CMS.Data.Access.Layer.Repository.IRepository.ICuraHubRepository.IPharmacyRepository;
using CMS.Data.Access.Layer.Repository.IRepository.IQ_ARepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CMS.Data.Access.Layer.Repository.IRepository
{
    public interface IUnitOfWork
    {
        // Identity
        IApplicationUserRepository ApplicationUserRepository { get; }

        // Clinic
        IDoctorRepository DoctorRepository { get; }
        IPatientHistoryRepository PatientHistoryRepository { get; }

        IPatientAppointmentRepository PatientAppointmentRepository { get; }
        IPatientRepository PatientRepository { get; }
        IQualificationRepository QualificationRepository { get; }
        IClinicReceptionistRepository ClinicReceptionistRepository { get; }
        IRequestDoctorRepository RequestDoctorRepository { get; }
        IRequestClinicReceptionistRepository RequestClinicReceptionistRepository { get; }
        IScheduleRepository ScheduleRepository { get; }
        ISpecializationRepository SpecializationRepository { get; }

        IPatientAppointmentCardRepository PatientAppointmentCardRepository { get; }
        IMedicalPrescriptionRepository MedicalPrescriptionRepository { get; }

        IDoctorReviewRepository DoctorReviewRepository { get; }

        // Pharmacy
        IMedicineManufactoryRepository MedicineManufactoryRepository { get; }
        IMedicineOrderRepository MedicineOrderRepository { get; }
        IMedicineRepository MedicineRepository { get; }
        IPharmacistRepository PharmacistRepository { get; }
        IPharmacyCategoryRepository PharmacyCategoryRepository { get; }
        IPharmacyCustomerRepository PharmacyCustomerRepository { get; }
        IPharmacyDeliveryRepresentativeRepository PharmacyDeliveryRepresentativeRepository { get; }
        IPharmacyOrderRepository PharmacyOrderRepository { get; }
        IPharmacyCartRepository PharmacyCartRepository { get; }


        // Medical Analysis Lab 
        IMedicalAnalysisLabAppointmentRepository MedicalAnalysisLabAppointmentRepository { get; }
        IMedicalAnalysisLabBranchRepository MedicalAnalysisLabBranchRepository { get; }
        IMedicalAnalysisLabCustomerRepository MedicalAnalysisLabCustomerRepository { get; }
        IMedicalAnalysisLabReceptionistRepository MedicalAnalysisLabReceptionistRepository { get; }
        IMedicalAnalysisLabRepository MedicalAnalysisLabRepository { get; }
        IMedicalAnalysisSpecialistRepository MedicalAnalysisSpecialistRepository { get; }
        IMedicalAnalysisTestCustomerRepository MedicalAnalysisTestCustomerRepository { get; }
        IMedicalAnalysisTestRepository MedicalAnalysisTestRepository { get; }
        IMedicalAnalysisTestResultRepository MedicalAnalysisTestResultRepository { get; }
        IRequestMedicalAnalysisLabReceptionistRepository RequestMedicalAnalysisLabReceptionistRepository { get; }
        IRequestMedicalAnalysisSpecialistRepository RequestMedicalAnalysisSpecialistRepository { get; }

        // Q && A
        IQuestionAndAnswerRepository QuestionAndAnswerRepository { get; }

        IMessageRepository MessageRepository { get; }

        public int Commit();

    }
}
