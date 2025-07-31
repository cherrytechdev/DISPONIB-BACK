using Autofac;
using Autofac.Core;
using Autofac.Features.Variance;
using FluentValidation;
using MediatR;
using MediatR.Pipeline;
using System.Reflection; 

namespace ESApplication.Processing
{
    public class MediatorModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterSource(new HandlerControvariantRegistrationSource(
                typeof(IRequestHandler<,>),
                typeof(INotificationHandler<>),
                typeof(IValidator<>)
            ));

            builder.RegisterAssemblyTypes(typeof(IMediator).GetTypeInfo().Assembly).AsImplementedInterfaces();

            var mediatrOpenTypes = new[]
            {
                typeof(IRequestHandler<,>),
                typeof(INotificationHandler<>),
                typeof(IValidator<>),
                //typeof(ICommandHandler<,>),
            };

            foreach (var mediatrOpenType in mediatrOpenTypes)
            {
                builder
                    .RegisterAssemblyTypes(AppDomain.CurrentDomain.GetAssemblies())
                    .AsClosedTypesOf(mediatrOpenType)
                    .AsImplementedInterfaces();
            }

            builder.RegisterGeneric(typeof(RequestPostProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));
            builder.RegisterGeneric(typeof(RequestPreProcessorBehavior<,>)).As(typeof(IPipelineBehavior<,>));

            builder.Register<ServiceFactory>(ctx =>
            {
                var c = ctx.Resolve<IComponentContext>();
                return t => c.Resolve(t);
            });
        }

        public class HandlerControvariantRegistrationSource : IRegistrationSource
        {
            private readonly IRegistrationSource source = new ContravariantRegistrationSource();
            private readonly List<Type> types = new List<Type>();

            public HandlerControvariantRegistrationSource(params Type[] types)
            {
                if (types == null)
                    throw new ArgumentNullException(nameof(types));
                if (!types.All(x => x.IsGenericTypeDefinition))
                    throw new ArgumentException("types must be generic");
                this.types.AddRange(types);
            }

            public IEnumerable<IComponentRegistration> RegistrationsFor(
                Service service,
                Func<Service, IEnumerable<ServiceRegistration>> registrationAccessor)
            {
                var components = source.RegistrationsFor(service, registrationAccessor);
                foreach (var c in components)
                {
                    var defs = c.Target.Services
                        .OfType<TypedService>()
                        .Select(x => x.ServiceType.GetGenericTypeDefinition());

                    if (defs.Any(types.Contains))
                        yield return c;
                }
            }

            public bool IsAdapterForIndividualComponents => source.IsAdapterForIndividualComponents;
        }
    }
}