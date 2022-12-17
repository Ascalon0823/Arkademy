using UnityEngine;

namespace Arkademy.Abilities
{
    public class QuickShotAbility : AbilityBase
    {
        public GameObject currActor;
        public ProjectileBehaviour arrow;

        public override bool CanUse()
        {
            return base.CanUse() && currActor && arrow ;
        }

        public override void PayLoad()
        {
            remainingReuseTime = reuseTime;
            var p = Instantiate(arrow, currActor.transform.position, Quaternion.identity);
            p.targetDir = currActor.GetComponent<Facing>().facingDir;
            p.GetComponent<DamageDealer>().faction = currActor.GetComponent<Damageable>().faction;
            var interact = currActor.GetComponent<Interaction>();
            if (interact&&interact.currCandidate&&interact.currCandidate.transform.root.GetComponentInChildren<Damageable>())
            {
                p.target = interact.currCandidate.transform.root.GetComponentInChildren<Damageable>();
            }
            
            p.ignores = new[] {currActor};
        }
    }
}
