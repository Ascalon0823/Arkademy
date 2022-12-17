using UnityEngine;

namespace Arkademy
{
    public class Facing : MonoBehaviour
    {
        public Vector2 facingDir;

        [SerializeField] private Motor motor;

        [SerializeField] private Interaction interaction;
        // Start is called before the first frame update
        void Start()
        {
            motor = GetComponent<Motor>();
            interaction = GetComponent<Interaction>();
            facingDir = Vector2.up;
        }

        // Update is called once per frame
        void Update()
        {
            if (interaction&&(interaction.currCandidate || interaction.currTarget))
            {
                var target = interaction.currTarget ? interaction.currTarget : interaction.currCandidate;
                var vec = target.transform.position - transform.position;
                facingDir = vec;
                return;
            }
            if (motor)
            {
                facingDir = motor.moveDir == Vector2.zero ? facingDir : motor.moveDir;
            }
        }
    }
}
