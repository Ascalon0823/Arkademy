using UnityEngine;

namespace Arkademy
{
    public class PlayerCamera : MonoBehaviour
    {
        
        private void LateUpdate()
        {
            var player = Player.LocalPlayer;
            if (!player) return;
            if (!player.currActor) return;
            transform.position = player.currActor.transform.position + new Vector3(0,0,-10);
        }
    }
}
