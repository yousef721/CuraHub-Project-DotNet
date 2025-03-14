using CMS.Data.Access.Layer.Data;
using CMS.Data.Access.Layer.Repository.CuraHubRepository;
using CMS.Data.Access.Layer.Repository.CuraHubRepository.ClinicRepository;
using CMS.Data.Access.Layer.Repository.CuraHubRepository.IdentityRepository;
using CMS.Data.Access.Layer.Repository.CuraHubRepository.MedicalAnalysisLabRepository;
using CMS.Data.Access.Layer.Repository.CuraHubRepository.PharmacyRepository;
using CMS.Data.Access.Layer.Repository.CuraHubRepository.Q_ARepository;
using CMS.Data.Access.Layer.Repository.IRepository;
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

namespace CMS.Data.Access.Layer.Repository
{
    public class UnitOfWork : IUnitOfWork, IDisposable
    {
        private readonly ApplicationDbContext _dbContext;

        

        public IApplicationUserRepository ApplicationUserRepository {  get; private set; }

        public IDoctorRepository DoctorRepository {  get; private set; }

        public IPatientHistoryRepository PatientHistoryRepository {  get; private set; }

        public IPatientAppointmentRepository PatientAppointmentRepository {  get; private set; }

        public IPatientRepository PatientRepository {  get; private set; }

        public IQualificationRepository QualificationRepository {  get; private set; }

        public IClinicReceptionistRepository ClinicReceptionistRepository {  get; private set; }

        public IRequestDoctorRepository RequestDoctorRepository {  get; private set; }

        public IRequestClinicReceptionistRepository RequestClinicReceptionistRepository {  get; private set; }

        public IScheduleRepository ScheduleRepository {  get; private set; }

        public ISpecializationRepository SpecializationRepository {  get; private set; }

        public IPatientAppointmentCardRepository PatientAppointmentCardRepository { get; private set; }

        public IMedicalPrescriptionRepository MedicalPrescriptionRepository { get; private set; }

        public IDoctorReviewRepository DoctorReviewRepository { get; private set; }


        public IMedicineManufactoryRepository MedicineManufactoryRepository {  get; private set; }

        public IMedicineOrderRepository MedicineOrderRepository {  get; private set; }

        public IMedicineRepository MedicineRepository {  get; private set; }

        public IPharmacistRepository PharmacistRepository {  get; private set; }

        public IPharmacyCategoryRepository PharmacyCategoryRepository {  get; private set; }

        public IPharmacyCustomerRepository PharmacyCustomerRepository {  get; private set; }

        public IPharmacyDeliveryRepresentativeRepository PharmacyDeliveryRepresentativeRepository {  get; private set; }

        public IPharmacyOrderRepository PharmacyOrderRepository {  get; private set; }

        public IMedicalAnalysisLabAppointmentRepository MedicalAnalysisLabAppointmentRepository {  get; private set; }

        public IMedicalAnalysisLabBranchRepository MedicalAnalysisLabBranchRepository {  get; private set; }

        public IMedicalAnalysisLabCustomerRepository MedicalAnalysisLabCustomerRepository {  get; private set; }

        public IMedicalAnalysisLabReceptionistRepository MedicalAnalysisLabReceptionistRepository {  get; private set; }

        public IMedicalAnalysisLabRepository MedicalAnalysisLabRepository {  get; private set; }

        public IMedicalAnalysisSpecialistRepository MedicalAnalysisSpecialistRepository {  get; private set; }

        public IMedicalAnalysisTestCustomerRepository MedicalAnalysisTestCustomerRepository {  get; private set; }

        public IMedicalAnalysisTestRepository MedicalAnalysisTestRepository {  get; private set; }

        public IMedicalAnalysisTestResultRepository MedicalAnalysisTestResultRepository {  get; private set; }

        public IRequestMedicalAnalysisLabReceptionistRepository RequestMedicalAnalysisLabReceptionistRepository {  get; private set; }

        public IRequestMedicalAnalysisSpecialistRepository RequestMedicalAnalysisSpecialistRepository {  get; private set; }

        public IPharmacyCartRepository PharmacyCartRepository { get; private set; }

        public IQuestionAndAnswerRepository QuestionAndAnswerRepository {  get; private set; }
        public IMessageRepository MessageRepository { get; private set; }

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            this._dbContext = dbContext;


            ApplicationUserRepository = new ApplicationUserRepository(_dbContext);
            DoctorRepository = new DoctorRepository(_dbContext);
            PatientHistoryRepository = new PatientHistoryRepository(_dbContext);
            PatientAppointmentRepository = new PatientAppointmentRepository(_dbContext);
            PatientRepository = new PatientRepository(_dbContext);
            QualificationRepository = new QualificationRepository(_dbContext);
            ClinicReceptionistRepository = new ClinicReceptionistRepository(_dbContext);
            RequestDoctorRepository = new RequestDoctorRepository(_dbContext);
            RequestClinicReceptionistRepository = new RequestClinicReceptionistRepository(_dbContext);
            ScheduleRepository = new ScheduleRepository(_dbContext);
            SpecializationRepository = new SpecializationRepository(_dbContext);
            MedicineManufactoryRepository = new MedicineManufactoryRepository(_dbContext);
            MedicineOrderRepository = new MedicineOrderRepository(_dbContext);
            MedicineRepository = new MedicineRepository(_dbContext);
            PharmacistRepository = new PharmacistRepository(_dbContext);
            PharmacyCategoryRepository = new PharmacyCategoryRepository(_dbContext);
            PharmacyCustomerRepository = new PharmacyCustomerRepository(_dbContext);
            PharmacyDeliveryRepresentativeRepository = new PharmacyDeliveryRepresentativeRepository(_dbContext);
            PharmacyOrderRepository = new PharmacyOrderRepository(_dbContext);
            PharmacyCartRepository = new PharmacyCartRepository(_dbContext);

            MedicalAnalysisLabAppointmentRepository = new MedicalAnalysisLabAppointmentRepository(_dbContext);
            MedicalAnalysisLabBranchRepository = new MedicalAnalysisLabBranchRepository(_dbContext);
            MedicalAnalysisLabCustomerRepository = new MedicalAnalysisLabCustomerRepository(_dbContext);
            MedicalAnalysisLabReceptionistRepository = new MedicalAnalysisLabReceptionistRepository(_dbContext);
            MedicalAnalysisLabRepository = new MedicalAnalysisLabRepository(_dbContext);
            MedicalAnalysisSpecialistRepository = new MedicalAnalysisSpecialistRepository(_dbContext);
            MedicalAnalysisTestCustomerRepository = new MedicalAnalysisTestCustomerRepository(_dbContext);
            MedicalAnalysisTestRepository = new MedicalAnalysisTestRepository(_dbContext);
            MedicalAnalysisTestResultRepository = new MedicalAnalysisTestResultRepository(_dbContext);
            RequestMedicalAnalysisLabReceptionistRepository = new RequestMedicalAnalysisLabReceptionistRepository(_dbContext);
            RequestMedicalAnalysisSpecialistRepository = new RequestMedicalAnalysisSpecialistRepository(_dbContext);
            QuestionAndAnswerRepository = new QuestionAndAnswerRepository(_dbContext);
            PatientAppointmentCardRepository = new PatientAppointmentCardRepository(_dbContext);
            MedicalPrescriptionRepository = new MedicalPrescriptionRepository(_dbContext);
            DoctorReviewRepository = new DoctorReviewRepository(_dbContext);
            MessageRepository = new MessageRepository (_dbContext);
        }
        public void Dispose()
        {
            _dbContext.Dispose();
        }

        public int Commit()
        {
            return _dbContext.SaveChanges();
        }
    }
}
