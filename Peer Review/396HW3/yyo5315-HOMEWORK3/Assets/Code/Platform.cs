using UnityEngine;

namespace Assets.Code
{
    public class Platform : MonoBehaviour {
        public bool LandedOn { get; private set; }

        public void Land () {
            if (LandedOn) { return; }
            Game.Ctx.Score.IncreaseScore(2);
            Game.Ctx.Platforms.ShiftPlatform();
            LandedOn = true;
        }
    }
}
