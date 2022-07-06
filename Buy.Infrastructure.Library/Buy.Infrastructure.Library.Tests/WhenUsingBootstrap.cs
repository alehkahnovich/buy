using Buy.Infrastructure.Library.Dependencies;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using Xunit;

namespace Buy.Infrastructure.Library.Tests
{
    public class WhenUsingBootstrap {
        private readonly Mock<IServiceCollection> _container;

        public WhenUsingBootstrap() {
            _container = new Mock<IServiceCollection>();    
        }

        [Fact]
        public void ShouldBootDependenciesByType() {
            //Arrange.
            //Act.
            _container.Object.BootDependencies(GetType());
            //Assert.
            _container.Verify(entry => entry.Add(It.IsAny<ServiceDescriptor>()), Times.Exactly(1));
        }

        [Fact]
        public void ShouldBootDependenciesByAssembly() {
            //Arrange.
            //Act.
            _container.Object.BootDependencies();
            //Assert.
            _container.Verify(entry => entry.Add(It.IsAny<ServiceDescriptor>()), Times.Exactly(1));
        }
    }
}
