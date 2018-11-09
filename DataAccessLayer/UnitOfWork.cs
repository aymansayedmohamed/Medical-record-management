using IDataAccessLayer;
using Microsoft.Win32.SafeHandles;
using Models;
using System;
using System.Runtime.InteropServices;

namespace DataAccessLayer
{
    public class UnitOfWork : IUnitOfWork
    {
        private IRepository<PatientMedicalHistory> _patientMedicalHistoryRepository;

        public UnitOfWork(IRepository<PatientMedicalHistory> PatientMedicalHistoryRepository)
        {
            _patientMedicalHistoryRepository = PatientMedicalHistoryRepository;
        }

        public IRepository<PatientMedicalHistory> PatientMedicalHistoryRepository => _patientMedicalHistoryRepository;

        bool disposed = false;

        SafeHandle handle = new SafeFileHandle(IntPtr.Zero, true);

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                handle.Dispose();
            }

            disposed = true;
        }

    }
}
