[assembly: WebActivatorEx.PreApplicationStartMethod(typeof(LeaRun.Application.Web.NinjectWebCommon), "Start")]
[assembly: WebActivatorEx.ApplicationShutdownMethodAttribute(typeof(LeaRun.Application.Web.NinjectWebCommon), "Stop")]

namespace LeaRun.Application.Web
{
    using System;
    using System.Web;

    using Microsoft.Web.Infrastructure.DynamicModuleHelper;

    using Ninject;
    using Ninject.Web.Common;
    using Repository.SteelMember.IBLL;
    using Repository.SteelMember.IDAL;
    using Repository.SteelMember.BLL;
    using Repository.SteelMember.DAL;


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
            kernel.Bind<TreeIBLL>().To<TreeBLL>();
            kernel.Bind<ProjectInfoIBLL>().To<ProjectInfoBLL>();
            kernel.Bind<FileIBLL>().To<FileBLL>();
            kernel.Bind<MemberUnitIBLL>().To<MemberUnitBLL>();
            //kernel.Bind<FactoryWarehouseIBLL>().To<FactoryWarehouseBLL>();
            //kernel.Bind<CompanyIBLL>().To<CompanyBLL>();
            //kernel.Bind<OrderManagementIBLL>().To<OrderManagementBLL>();
            kernel.Bind<ProcessManagementIBLL>().To<ProcessManagementBLL>();
            //kernel.Bind<ProjectManagementIBLL>().To<ProjectManagementBLL>();
            //kernel.Bind<ProjectWarehouseIBLL>().To<ProjectWarehouseBLL>();
            kernel.Bind<RawMaterialIBLL>().To<RawMaterialBLL>();
            //kernel.Bind<ShipManagementIBLL>().To<ShipManagementBLL>();
            //kernel.Bind<AnalysisRawMaterialIBLL>().To<AnalysisRawMaterialBLL>();
            //kernel.Bind<OrderMemberIBLL>().To<OrderMemberBLL>();
            kernel.Bind<MemberMaterialIBLL>().To<MemberMaterialBLL>();
            kernel.Bind<MemberProcessIBLL>().To<MemberProcessBLL>();
            //kernel.Bind<CollarIBLL>().To<CollarBLL>();
            //kernel.Bind<CollarMemberIBLL>().To<CollarMemberBLL>();
            //kernel.Bind<RawMaterialPurchaseIBLL>().To<RawMaterialPurchaseBLL>();
            //kernel.Bind<PurchaseIBLL>().To<PurchaseBLL>();


            kernel.Bind<TreeIDAL>().To<TreeDAL>();
            kernel.Bind<ProjectInfoIDAL>().To<ProjectInfoDAL>();
            kernel.Bind<FileIDAL>().To<FileDAL>();
            kernel.Bind<MemberUnitIDAL>().To<MemberUnitDAL>();
            //kernel.Bind<FactoryWarehouseIDAL>().To<FactoryWarehouseDAL>();
            //kernel.Bind<CompanyIDAL>().To<CompanyDAL>();
            //kernel.Bind<OrderManagementIDAL>().To<OrderManagementDAL>();
            kernel.Bind<ProcessManagementIDAL>().To<ProcessManagementDAL>();
            //kernel.Bind<ProjectManagementIDAL>().To<ProjectManagementDAL>();
            //kernel.Bind<ProjectWarehouseIDAL>().To<ProjectWarehouseDAL>();
            kernel.Bind<RawMaterialIDAL>().To<RawMaterialDAL>();
            //kernel.Bind<ShipManagementIDAL>().To<ShipManagementDAL>();
            //kernel.Bind<AnalysisRawMaterialIDAL>().To<AnalysisRawMaterialDAL>();
            //kernel.Bind<OrderMemberIDAL>().To<OrderMemberDAL>();
            kernel.Bind<MemberMaterialIDAL>().To<MemberMaterialDAL>();
            kernel.Bind<MemberProcessIDAL>().To<MemberProcessDAL>();
            //kernel.Bind<CollarIDAL>().To<CollarDAL>();
            //kernel.Bind<CollarMemberIDAL>().To<CollarMemberDAL>();
            //kernel.Bind<RawMaterialPurchaseIDAL>().To<RawMaterialPurchaseDAL>();
            //kernel.Bind<PurchaseIDAL>().To<PurchaseDAL>();
        }        
    }
}
