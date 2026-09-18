using Vintagestory.API.Client;
using Vintagestory.API.Server;
using Vintagestory.API.Config;
using Vintagestory.API.Common;

namespace BunnyPet;

public class BunConfig
{
    public static BunConfig Config;
    //radius
    //strenght
    //targetable
};

public class BunnyPetModSystem : ModSystem
{
    // Called on server and client
    // Useful for registering block/entity classes on both sides
    public override void Start(ICoreAPI api)
    {
        base.Start(api);
        Mod.Logger.Notification("Hello from template mod Start: " + api.Side);
        api.RegisterEntityBehaviorClass(
            "pettemporalaura",
            typeof(EntityBehaviorPetTemporalAura)
        );

        api.RegisterEntityBehaviorClass(
            "temporaldebug",
            typeof(EntityBehaviorTemporalDebug)
        );

        try
        {
            var Config = api.LoadModConfig<BunConfig>("bunconfig.json");
            if (Config != null)
            {
                api.Logger.Notification("BunnyPet Config has been loaded.");
                BunConfig.Config = Config;
            }
            else
            {
                api.Logger.Notification("No BunnyPet Config specified. Falling back to default settings.");
                BunConfig.Config = new();
            }
        }
        catch
        {
            BunConfig.Config = new();
            api.Logger.Error("Failed to load custom mod configuration. Falling back to default settings.");
        }
        finally
        {
            api.StoreModConfig(BunConfig.Config, "bunconfig.json");
        }
    }

    public override void StartServerSide(ICoreServerAPI api)
    {
        Mod.Logger.Notification("Hello from template mod server side: " + Lang.Get("bunnypet:hello"));
    }

    public override void StartClientSide(ICoreClientAPI api)
    {
        Mod.Logger.Notification("Hello from template mod client side: " + Lang.Get("bunnypet:hello"));
    }
}
