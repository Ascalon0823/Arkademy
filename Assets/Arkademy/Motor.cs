using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Arkademy
{
    public class Motor : MonoBehaviour
    {
        public Rigidbody2D rb;
        public Vector2 moveDir;
        public float speed;
        private void FixedUpdate()
        {
            rb.MovePosition(rb.position + speed * Time.fixedDeltaTime * moveDir);
        }
    }
}


