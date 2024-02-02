using System;
using Celeste64;
namespace BlahajBerry
{
    public class BlahajBerry : GameMod
    {
        public override void OnActorAdded(Actor actor)
        {
            base.OnActorAdded(actor);
            if (actor is Strawberry strawberry)
            {
                strawberry.Model = new(Assets.Models["blahaj"]);
            }
        }
    }
}
