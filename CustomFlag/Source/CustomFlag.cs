using System;
using Celeste64;
namespace CustomFlag
{
    public class CustomFlag : GameMod
    {
        public override void OnActorAdded(Actor actor)
        {
            base.OnActorAdded(actor);
            if (actor is Checkpoint checkpoint)
            {
                SkinnedTemplate flag_model = Assets.Models["custom_flag_on"];
                flag_model.Materials[0].Texture = Assets.Textures["custom_flag"];

                checkpoint.ModelOn = new(flag_model);
                checkpoint.ModelOn.Play("Idle");
                checkpoint.ModelOn.Transform = System.Numerics.Matrix4x4.CreateScale(0.2f);
            }
        }
    }
}
