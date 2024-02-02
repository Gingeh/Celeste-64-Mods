using System;
using System.IO;
using System.Numerics;
using Celeste64;
using Foster.Framework;
using Vec3 = System.Numerics.Vector3;
using Matrix = System.Numerics.Matrix4x4;

namespace WingedBerry
{
    public class WingedBerryMod : GameMod
    {
        public override void OnModLoaded()
        {
            base.OnModLoaded();
            Map.ActorFactory factory = new((map, entity) =>
            {
                var id = $"{map.LoadWorld!.Entry.Map}/{map.LoadStrawberryCounter}";
                var lockedCondition = entity.GetStringProperty("targetname", string.Empty);
                var isLocked = entity.GetIntProperty("locked", 0) > 0;
                var playUnlockSound = entity.GetIntProperty("noUnlockSound", 0) == 0;
                Vec3? bubbleTo = null;
                if (map.FindTargetNode(entity.GetStringProperty("bubbleto", string.Empty), out var point))
                    bubbleTo = point;
                map.LoadStrawberryCounter++;
                return new WingedBerry(id, isLocked, lockedCondition, playUnlockSound, bubbleTo);
            });
            AddActorFactory("WingedBerry", factory);
        }
    }

    public class WingedBerry : Strawberry
    {
        private bool escaping = false;
        private float shadow_alpha = 1.0f;
        private Vec3 Velocity = new(0.0f, 0.0f, -1.0f);
        private Vec3 Acceleration = new(0.0f, 0.0f, 0.1f);

        public WingedBerry(string id, bool isLocked, string? unlockCondition, bool unlockSound, Vec3? bubbleTo)
            : base(id, isLocked, unlockCondition, unlockSound, bubbleTo)
        {
            Model = new(Assets.Models["winged_berry"]);
        }

        public override void Update()
        {
            base.Update();
            if (World.Get<Player>() is { } player && player.Dashes == 0 && !IsCollected && !IsLocked)
            {
                escaping = true;
            }
            if (escaping)
            {
                Position += Velocity;
                Velocity += Acceleration;
                shadow_alpha *= 0.9f;
                PointShadowAlpha = shadow_alpha;
            }
        }
    }
}
