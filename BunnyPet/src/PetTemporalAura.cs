using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.API.MathTools;
using Vintagestory.GameContent;
using Vintagestory.API.Datastructures;
using PetAI;

class EntityBehaviorPetTemporalAura : EntityBehavior
{
    SystemTemporalStability tempStabSys;
    ICoreAPI api;

    const float BASE_RADIUS = 6f;
    const float BASE_STABILITY_BONUS = 0.15f;
    const float MIN_STABILITY = 0f;
    const float MAX_STABILITY = 1.5f;

    float radius = 6f;
    float stabilityBonus = 0.15f;

    public EntityBehaviorPetTemporalAura(Entity entity) : base(entity) { }

    public override string PropertyName() => "pettemporalaura";

    public override void Initialize(EntityProperties properties, JsonObject attributes)
    {
        base.Initialize(properties, attributes);

        api = entity.Api;
        radius = attributes?["radius"].AsFloat(BASE_RADIUS) ?? BASE_RADIUS;
        stabilityBonus = attributes?["stabilityBonus"].AsFloat(BASE_STABILITY_BONUS) ?? BASE_STABILITY_BONUS;

        tempStabSys = api.ModLoader.GetModSystem<SystemTemporalStability>();
        if (tempStabSys != null)
        {
            tempStabSys.OnGetTemporalStability += OnGetTemporalStability;
        }
    }

    float OnGetTemporalStability(float stability, double x, double y, double z)
    {
        if (entity?.Alive != true) 
        return stability;

        double distSq = entity.Pos.SquareDistanceTo(x, y, z);
        if (distSq > radius * radius) 
        return stability;

        IPlayer nearest = api.World.NearestPlayer(x, y, z);
        if (nearest == null) 
        return stability;

        var tameable = entity.GetBehavior<PetAI.EntityBehaviorTameable>();

        if (tameable == null)
            return stability;

        if (nearest.PlayerUID != tameable.OwnerId)
            return stability;

        return GameMath.Clamp(stability + stabilityBonus, MIN_STABILITY, MAX_STABILITY);
    }

    public override void OnEntityDespawn(EntityDespawnData despawn)
    {
        tempStabSys.OnGetTemporalStability -= OnGetTemporalStability;
        base.OnEntityDespawn(despawn);
    }
}