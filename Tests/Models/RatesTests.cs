using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Idioms;
using NUnit.Framework;
using TestApp.Models;

namespace Models.Tests
{   
    [TestFixture]
    [TestOf(nameof(RatesModel))]
    public class RatesTests
    {
		#region Fields

		private IFixture _fixture;

		#endregion

		#region Setup Methods

		[SetUp]
		public void SetUp()
		{
			_fixture = new Fixture();
			_fixture.Customize(new AutoMoqCustomization());
		}

		#endregion

		#region Test Methods

		[Test]
		public void ReadonlyPropertiesShouldHaveConstructorParameterAssignedValues()
		{
			// Arrange
			var guardClauseAssertion = new ConstructorInitializedMemberAssertion(_fixture);

			// Act / Assert
			guardClauseAssertion.Verify(typeof(RatesModel));
		}

		#endregion
	}
}
