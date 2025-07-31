using Autofac;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace ESApplication.Processing
{
    
    public static class CompositionRoot
    {
        private static Autofac.IContainer container;

        public static void SetContainer(Autofac.IContainer autoFacContainer)
        {
            container = autoFacContainer;
        }

        internal static ILifetimeScope BeginLifetimeScope()
        {
            return container.BeginLifetimeScope();
        }
    }
}
