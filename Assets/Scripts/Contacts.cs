using System.Collections;
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

    public int maxHits { get => m_MaxHits; }
    public int hits { get => m_Hits; }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.layer == PLAYER_LAYER)
        {
            m_Hits += m_Power.player;
        } 
        else if(collision.gameObject.layer == GROUND_LAYER)
        {
            m_Hits += m_Power.ground;
        }

        onContact?.Invoke();
    }
}
