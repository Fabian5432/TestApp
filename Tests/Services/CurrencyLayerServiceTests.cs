using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Idioms;
using NUnit.Framework;
using System.Threading.Tasks;
using TestApp.Services;

namespace Tests.Services
{   
    [TestFixture]
    [TestOf(nameof(CurrencyLayerService))]
    public class CurrencyLayerServiceTests
    {
        #region Fields

        private IFixture _fixture;

        #endregion

        #region Setup Methods

        [SetUp]
        public void Setup()
        {
            _fixture = new Fixture();
            _fixture.Customize(new AutoMoqCustomization());
        }

        #endregion

        #region Test Methods

        [Test]
        public void ShouldThrowGuardClauseExceptionForNullConstructorParameters()
        {
            // Arrange
            var guardClauseAssertion = new GuardClauseAssertion(_fixture);

            // Act / Assert
            guardClauseAssertion.Verify(typeof(CurrencyLayerService).GetConstructors());
        }

        #endregion
    }
}
