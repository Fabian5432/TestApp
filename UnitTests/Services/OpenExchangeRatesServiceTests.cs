using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Idioms;
using NUnit.Framework;
using TestApp.Services;

namespace TestApp.UnitTests.Services
{
    [TestFixture]
    [TestOf(nameof(OpenExchangeRatesService))]
    public class OpenExchangeRatesServiceTests
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
            guardClauseAssertion.Verify(typeof(OpenExchangeRatesService).GetConstructors());
        }

        #endregion
    }
}