using UnityEngine;

namespace Arkademy
{
    public class FacingAnchorControl : MonoBehaviour
    {
        [SerializeField] private Facing facing;

        // Update is called once per frame
        void Update()
        {
            if (!facing) return;
            transform.up = (Vector3)facing.facingDir;
        }
    }
}
