using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimateColliders : MonoBehaviour
{

    [SerializeField]
    PolygonCollider2D[] m_PolygonCollider2Ds = null;

    int m_CurrentIndex = 0;

    void Awake()
    {
        for (int i = 0; i < m_PolygonCollider2Ds.Length; i++)
        {
            m_PolygonCollider2Ds[i].enabled = false;
        }
        m_PolygonCollider2Ds[m_CurrentIndex].enabled = true;
    }

    public void SetColliderByIndex(int index)
    {
        m_PolygonCollider2Ds[m_CurrentIndex].enabled = false;
        m_PolygonCollider2Ds[index].enabled = true;
        m_CurrentIndex = index;
    }
}
