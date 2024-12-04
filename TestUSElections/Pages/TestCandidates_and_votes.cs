using Bunit;
using Microsoft.Extensions.DependencyInjection;
using USElections.Pages;
using USElections.State;
using USElections.USElectionsData;

namespace TestUSElections
{
	[Collection("USElections")]
	public class TestCandidates_and_votes
	{
		[Fact]
		public void ViewIsCreated()
		{
			using var ctx = new TestContext();
			ctx.JSInterop.Mode = JSRuntimeMode.Loose;
			ctx.Services.AddIgniteUIBlazor(
				typeof(IgbCardModule),
				typeof(IgbPieChartModule),
				typeof(IgbGridModule),
				typeof(IgbCategoryChartModule));
			ctx.Services.AddScoped<IStateService>(sp => new MockStateService());
			ctx.Services.AddScoped<IUSElectionsDataService>(sp => new MockUSElectionsDataService());
			var componentUnderTest = ctx.RenderComponent<Candidates_and_votes>();
			Assert.NotNull(componentUnderTest);
		}
	}
}
