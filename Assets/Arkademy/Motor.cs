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
        private void LateUpdate()
        {
            rb.MovePosition(rb.position + speed * Time.deltaTime * moveDir);
        }
    }
}


