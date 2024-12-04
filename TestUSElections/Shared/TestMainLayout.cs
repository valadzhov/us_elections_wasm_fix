using Bunit;
using Microsoft.Extensions.DependencyInjection;
using USElections.Shared;
using USElections.USElectionsData;
using USElections.State;

namespace TestUSElections
{
	[Collection("USElections")]
	public class TestMainLayout
	{
		[Fact]
		public void ViewIsCreated()
		{
			using var ctx = new TestContext();
			ctx.JSInterop.Mode = JSRuntimeMode.Loose;
			ctx.Services.AddIgniteUIBlazor(
				typeof(IgbNavbarModule),
				typeof(IgbIconButtonModule),
				typeof(IgbRippleModule),
				typeof(IgbNavDrawerModule),
				typeof(IgbNavDrawerItemModule));
			ctx.Services.AddScoped<IUSElectionsDataService>(sp => new MockUSElectionsDataService());
			ctx.Services.AddScoped<IStateService>(sp => new MockStateService());
			var componentUnderTest = ctx.RenderComponent<MainLayout>();
			Assert.NotNull(componentUnderTest);
		}
	}
}
