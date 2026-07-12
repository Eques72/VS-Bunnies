using Vintagestory.API.Common;
using Vintagestory.API.Common.Entities;
using Vintagestory.API.MathTools;
using Vintagestory.GameContent;

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


}