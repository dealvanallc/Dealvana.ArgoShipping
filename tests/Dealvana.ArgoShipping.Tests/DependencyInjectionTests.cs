using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace Dealvana.ArgoShipping.Tests;

[TestFixture]
public class DependencyInjectionTests
{
    private ServiceCollection InitializeServiceCollection()
    {
        var serviceCollection = new ServiceCollection();
        var configurationBuilder = new ConfigurationBuilder()
            .AddInMemoryCollection(new Dictionary<string, string?>()
            {
                ["ArgoShippingSettings:Username"] = "someusername",
                ["ArgoShippingSettings:Password"] = "somepassword",
                ["CustomArgoShippingSettings:Username"] = "someusername",
                ["CustomArgoShippingSettings:Password"] = "somepassword",
                ["ArgoShippingSettingsSandbox:Username"] = "someusername",
                ["ArgoShippingSettingsSandbox:Password"] = "somepassword",
                ["ArgoShippingSettingsSandBox:Sandbox"] = "true",
                ["ArgoShippingSettingsInvalid:Username"] = "someusername"
            });

        serviceCollection.AddSingleton<IConfiguration>(configurationBuilder.Build());

        return serviceCollection;
    }

    [Test]
    public void CanCreateFromConfiguration()
    {
        var services = InitializeServiceCollection()
            .AddArgoShippingService();

        var serviceProvider = services.BuildServiceProvider();
        Assert.DoesNotThrow(() => serviceProvider.GetRequiredService<IArgoShippingService>());
    }

    [Test]
    public void CanCreateFromCustomConfiguration()
    {
        var services = InitializeServiceCollection()
            .AddArgoShippingService("CustomArgoShippingSettings");

        var serviceProvider = services.BuildServiceProvider();
        Assert.DoesNotThrow(() => serviceProvider.GetRequiredService<IArgoShippingService>());
    }

    [Test]
    public void CanCreateFromManualSettings()
    {
        var services = InitializeServiceCollection()
            .AddArgoShippingService(new ArgoShippingSettings
            {
                Username = "someusername",
                Password = "somepassword"
            });

        var serviceProvider = services.BuildServiceProvider();
        Assert.DoesNotThrow(() => serviceProvider.GetRequiredService<IArgoShippingService>());
    }

    [Test]
    public void CanCreateFromSettingParameters()
    {
        var services = InitializeServiceCollection()
            .AddArgoShippingService("someusername", "somepassword", false);

        var serviceProvider = services.BuildServiceProvider();
        Assert.DoesNotThrow(() => serviceProvider.GetRequiredService<IArgoShippingService>());
    }

    [Test]
    public void CanCreateSandbox()
    {
        var services = InitializeServiceCollection()
            .AddArgoShippingService("ArgoShippingSettingsSandbox");

        var serviceProvider = services.BuildServiceProvider();
        var argoService = serviceProvider.GetRequiredService<IArgoShippingService>();

        Assert.That(argoService.IsSandbox, Is.True);
    }

    [Test]
    public void DoesFailValidation()
    {
        var services = InitializeServiceCollection()
            .AddArgoShippingService("ArgoShippingSettingsInvalid");

        var serviceProvider = services.BuildServiceProvider();
        Assert.Throws(typeof(InvalidOperationException), () => serviceProvider.GetRequiredService<IArgoShippingService>());
    }
}
