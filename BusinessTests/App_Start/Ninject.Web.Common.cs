[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(BusinessTests.App_Start.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(BusinessTests.App_Start.NinjectWebCommon), "Stop")]

namespace BusinessTests.App_Start
{
    using System;
    using System.Web;
    using Business;
    using Business.Vaildators;
    using DataAccessLayer;
    using IBusiness;
    using IBusiness.IVaildators;
    using IDataAccessLayer;
    using Microsoft.Web.Infrastructure.DynamicModuleHelper;
    using Models;
    using Ninject;
    using Ninject.Web.Common;
    using Ninject.Web.Common.WebHost;

    public static class NinjectWebCommon 
    {
        private static readonly Bootstrapper bootstrapper = new Bootstrapper();

        /// <summary>
        /// Starts the application
        /// </summary>
        public static void Start() 
        {
            DynamicModuleUtility.RegisterModule(typeof(OnePerRequestHttpModule));
            DynamicModuleUtility.RegisterModule(typeof(NinjectHttpModule));
            bootstrapper.Initialize(CreateKernel);
        }
        
        /// <summary>
        /// Stops the application.
        /// </summary>
        public static void Stop()
        {
            bootstrapper.ShutDown();
        }
        
        /// <summary>
        /// Creates the kernel that will manage your application.
        /// </summary>
        /// <returns>The created kernel.</returns>
        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();
            try
            {
                kernel.Bind<Func<IKernel>>().ToMethod(ctx => () => new Bootstrapper().Kernel);
                kernel.Bind<IHttpModule>().To<HttpApplicationInitializationHttpModule>();
                RegisterServices(kernel);
                return kernel;
            }
            catch
            {
                kernel.Dispose();
                throw;
            }
        }

        /// <summary>
        /// Load your modules or register your services here!
        /// </summary>
        /// <param name="kernel">The kernel.</param>
        private static void RegisterServices(IKernel kernel)
        {
            kernel.Bind<IPatientMedicalHistoryServiceValidator>().To<PatientMedicalHistoryServiceValidator>().InRequestScope();
        
            kernel.Bind<IRepository<PatientMedicalHistory>>().To<Repository<PatientMedicalHistory>>().InRequestScope();

            kernel.Bind<IDataAccessLayer.IUnitOfWork>().To<DataAccessLayer.UnitOfWork>().InRequestScope();

            kernel.Bind<IDeliveryWay>().To<EmailDeliveryWay>().InRequestScope();

            kernel.Bind<IDeliveryWay>().To<FTPDeliveryWay>().InRequestScope();

            kernel.Bind<IMedicalHistoryReport>().To<MedicalHistoryReport>().InRequestScope();

            kernel.Bind<IEmail>().To<Email>().InRequestScope();

            kernel.Bind<ISmtpClient>().To<SmtpClientWrapperTest>().InRequestScope();
        }        
    }
}