using Vintagestory.API.Common;
using Vintagestory.GameContent;
using Vintagestory.API.Common.Entities;
using Vintagestory.API.Datastructures;
using Vintagestory.API.MathTools;

namespace BunnyPet;

public class EntityBehaviorTemporalDebug : EntityBehavior
{
    private const int LOG_INTERVAL_MS = 250;
    private float logAccumulator;

    public EntityBehaviorTemporalDebug(Entity entity) : base(entity)
    {
    }

    public override string PropertyName() => "temporaldebug";

    public override void Initialize(EntityProperties properties, JsonObject attributes)
    {
        base.Initialize(properties, attributes);

        entity.Api.Logger.Notification(
            $"[BunnyPet DEBUG] TemporalDebug initialized " +
            $"entityId={entity.EntityId} " +
            $"side={entity.Api.Side}"
        );

        var temporalBehavior =
            entity.GetBehavior<EntityBehaviorTemporalStabilityAffected>();

        entity.Api.Logger.Notification(
            $"[BunnyPet DEBUG] TemporalStabilityAffected " +
            $"{(temporalBehavior != null ? "FOUND" : "NOT FOUND")} " +
            $"entityId={entity.EntityId}"
        );
        temporalBehavior.OwnStability = 0.50;

    }

    // public override void OnGameTick(float deltaTime)
    // {

    //     var temporalBehavior =
    //         entity.GetBehavior<EntityBehaviorTemporalStabilityAffected>();

    //     if (temporalBehavior == null)
    //         return;

    //     const double TEST_BONUS = 0.05;

    //     double ownStabilityBefore =
    //         temporalBehavior.OwnStability;

    //     // double vanillaVelocity =
    //     //     temporalBehavior.TempStabChangeVelocity;
    //     double vanillaVelocity =
    //         entity.Attributes.GetDouble("tempStabChangeVelocity");


    //     double attributeBefore =
    //         entity.Attributes.GetDouble("tempStabChangeVelocity");

    //     temporalBehavior.TempStabChangeVelocity =
    //         vanillaVelocity - TEST_BONUS;

    //     double modifiedVelocity =
    //         temporalBehavior.TempStabChangeVelocity;

    //     entity.Api.Logger.Notification(
    //         $"[BunnyPet DEBUG] " +
    //         $"Own={ownStabilityBefore:F6} " +
    //         $"Vanilla={vanillaVelocity:F6} " +
    //         $"Modified={modifiedVelocity:F6} " +
    //         $"Attribute={attributeBefore:F6} " +
    //         $"dt={deltaTime:F4}"
    //     );

public override void OnGameTick(float deltaTime)
{
    var temporalBehavior =
        entity.GetBehavior<EntityBehaviorTemporalStabilityAffected>();

    if (temporalBehavior == null)
        return;

    double before = temporalBehavior.OwnStability;

    const double TEST_GAIN_PER_SECOND = 0.01;

    temporalBehavior.OwnStability =
        GameMath.Clamp(
            temporalBehavior.OwnStability +
            TEST_GAIN_PER_SECOND * deltaTime,
            0,
            1
        );

    double after = temporalBehavior.OwnStability;

    entity.Api.Logger.Notification(
        $"[BunnyPet DEBUG] " +
        $"OwnBefore={before:F6} " +
        $"OwnAfter={after:F6} " +
        $"Delta={after - before:F6} " +
        $"dt={deltaTime:F4}"
    );
}

// public override void OnGameTick(float deltaTime)
// {
//     var temporalBehavior =
//         entity.GetBehavior<EntityBehaviorTemporalStabilityAffected>();

//     if (temporalBehavior == null)
//         return;

//     double before = temporalBehavior.OwnStability;

//     temporalBehavior.OwnStability = 0.50;

//     double after = temporalBehavior.OwnStability;

//     entity.Api.Logger.Notification(
//         $"[BunnyPet DEBUG] " +
//         $"OwnBefore={before:F6} " +
//         $"OwnAfter={after:F6}"
//     );
// }

        // // logAccumulator += deltaTime;

        // // if (logAccumulator < LOG_INTERVAL_MS / 1000f)
        // //     return;

        // // logAccumulator = 0;

        // var temporalBehavior =
        //     entity.GetBehavior<EntityBehaviorTemporalStabilityAffected>();

        // // if (temporalBehavior == null)
        // //     return;

        // double attributeVelocity =
        //     entity.Attributes.GetDouble("tempStabChangeVelocity");

        // // entity.Api.Logger.Notification(
        // //     $"[BunnyPet DEBUG] " +
        // //     $"OwnStability={temporalBehavior.OwnStability:F6} " +
        // //     $"TempStabChangeVelocity={temporalBehavior.TempStabChangeVelocity:F6} " +
        // //     $"AttributeVelocity={attributeVelocity:F6}"
        // // );

        // var before = temporalBehavior.TempStabChangeVelocity;

        // temporalBehavior.TempStabChangeVelocity = before + 0.05;

        // var after = temporalBehavior.TempStabChangeVelocity;

        // entity.Api.Logger.Notification(
        //     $"[BunnyPet DEBUG] " +
        //     $"BEFORE={before:F6} " +
        //     $"AFTER={after:F6} " +
        //     $"AttributeVelocity={attributeVelocity:F6}"
        // );

    // }
}
