using Autofac;

namespace System
{
    /// <summary>
    /// Holder for container
    /// </summary>
    public static class IoC
    {
        /// <summary>
        /// <see cref="IContainer"/>
        /// </summary>
        public static IContainer Container { get; private set; }

        /// <summary>
        /// Build <paramref name="containerBuilder"/> and asign it to <see cref="Container"/>
        /// </summary>
        /// <param name="containerBuilder">Target <see cref="ContainerBuilder"/></param>
        public static void BuildContainer(ContainerBuilder containerBuilder)
        {
            Container = containerBuilder.Build();
        }
    }
}
