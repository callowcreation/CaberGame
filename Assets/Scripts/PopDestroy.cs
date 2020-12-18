using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopDestroy : MonoBehaviour
{
    [Header("Pop SFX")]
    [SerializeField]
    AudioClip[] m_PopClips;

    AudioSource m_AudioSource;

    void Awake()
    {
        m_AudioSource = GetComponent<AudioSource>();
        if (m_PopClips.Length > 0)
        {
            AudioClip clip = m_PopClips[Random.Range(0, m_PopClips.Length)];
            m_AudioSource.PlayOneShot(clip);
        }
    }

    public void DestroyMe()
    {
        Destroy(gameObject);
    }
}
