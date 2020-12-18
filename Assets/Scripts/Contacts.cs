using System.Linq;
using System.Collections.Generic;
using UnityEngine;

public class Contacts : MonoBehaviour
{
    const int PLAYER_LAYER = 8;
    const int GROUND_LAYER = 10;

    public event System.Action onContact;

    [System.Serializable]
    public class Power
    {
        public int player = 1;
        public int ground = 3;
    }

    [SerializeField]
    Power m_Power = new Power();

    [SerializeField]
    int m_MaxHits = 3;
    
    [SerializeField]
    int m_Hits = 0;

    [SerializeField]
    float m_ResponseTime = 0.5f;
    [SerializeField]
    float m_ReflectThreshold = -0.15f;

    public int maxHits { get => m_MaxHits; }
    public int hits { get => m_Hits; }

    Rigidbody2D rb2d = null;

    void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (rb2d.isKinematic == true) return;

        if(collision.gameObject.layer == PLAYER_LAYER)
        {
            m_Hits += m_Power.player;

            List<ContactPoint2D> items = collision.contacts.Where(x => x.separation < m_ReflectThreshold).ToList();

            if(items.Count() > 0)
            {
                Debug.Log($"{items.Count} {items[0].separation} separation");
            }

            //rb2d.velocity = Vector3.Reflect(lastVelocity, collision.contacts[0].normal) * m_ReflectMultiplier;
            rb2d.isKinematic = true;
            Invoke("ResetCollision", m_ResponseTime);
        } 
        else if(collision.gameObject.layer == GROUND_LAYER)
        {
            m_Hits += m_Power.ground;
        }

        onContact?.Invoke();
    }

    void ResetCollision()
    {
        rb2d.isKinematic = false;
    }

    Vector2 lastVelocity;
    void FixedUpdate()
    {
        lastVelocity = rb2d.velocity;
    }
}
