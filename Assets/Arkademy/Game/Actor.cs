using UnityEngine;

namespace Arkademy.Game
{
    public class Actor : MonoBehaviour
    {
        [SerializeField] private int health;
        [SerializeField] private int maxHealth;
        
        public void TakeDamage(float damage)
        {
            var d = Mathf.FloorToInt(damage); 
            health -= Mathf.FloorToInt(damage);
            OnTookDamage(d);
            health = Mathf.Max(health, 0);
            if (health == 0)
            {
                OnKill();
            }
        }

        protected void OnTookDamage(int damage)
        {
            Debug.Log($"Actor {gameObject.name} took {damage} damage");
        }

        protected void OnKill()
        {
            Debug.Log($"Actor {gameObject.name} killed");
            Destroy(gameObject);
        }
    }
}