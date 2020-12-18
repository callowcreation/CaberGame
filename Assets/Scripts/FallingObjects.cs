using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FallingObjects : MonoBehaviour
{
    [SerializeField]
    GameObject[] m_Prefabs = null;
    [SerializeField]
    GameObject[] m_PrefabsUI = null;
    [SerializeField]
    GameObject[] m_PrefabsPop = null;

    [SerializeField]
    float m_DropInterval = 5.0f;

    [SerializeField]
    float m_MaxAngularVelocity = 10.0f;

    [Header("Sound FX")]
    [SerializeField]
    AudioClip[] m_GoundClips;
    [SerializeField]
    AudioClip[] m_BodyClips;
    [SerializeField]
    AudioClip[] m_HeadClips;
    [SerializeField]
    AudioClip[] m_BalloonClips;

    IEnumerator Start()
    {
        while(enabled)
        {
            GameObject balloonGO = Instantiate(m_Prefabs[0], new Vector3(Random.Range(-7, 7), 6.0f, 0.0f), Quaternion.identity);
            GameObject uiGO = Instantiate(m_PrefabsUI[0], new Vector3(Random.Range(-7, 7), 6.0f, 0.0f), Quaternion.identity);
            HitsUI hitsUI = balloonGO.GetComponent<HitsUI>();
            Contacts contacts = hitsUI.GetComponent<Contacts>();
            balloonGO.GetComponent<Rigidbody2D>().angularVelocity = Random.Range(-m_MaxAngularVelocity, m_MaxAngularVelocity);

            balloonGO.transform.localScale = Vector2.one * Random.Range(0.5f, 1.5f);
            contacts.onContact += () =>
            {
                if (contacts.hits >= contacts.maxHits)
                {
                    GameObject popGO = Instantiate(m_PrefabsPop[0], balloonGO.transform.position, Quaternion.identity);
                    Destroy(balloonGO);
                    Destroy(uiGO);
                }
            };
            contacts.onContactSurface += (surface, audioSource) =>
            {
                AudioClip clip = null;
                switch (surface)
                {
                    case Contacts.Surface.Ground:
                        if(m_GoundClips.Length > 0)
                        {
                            clip = m_GoundClips[Random.Range(0, m_GoundClips.Length)];
                        }
                        break;
                    case Contacts.Surface.Body:
                        if (m_BodyClips.Length > 0)
                        {
                            clip = m_BodyClips[Random.Range(0, m_BodyClips.Length)];
                        }
                        break;
                    case Contacts.Surface.Head:
                        if (m_HeadClips.Length > 0)
                        {
                            clip = m_HeadClips[Random.Range(0, m_HeadClips.Length)];
                        }
                        break;
                    case Contacts.Surface.Balloon:
                        if (m_BalloonClips.Length > 0)
                        {
                            clip = m_BalloonClips[Random.Range(0, m_BalloonClips.Length)];
                        }
                        break;
                    default:
                        break;
                }
                if(clip != null)
                {
                    audioSource.PlayOneShot(clip);
                }
            };
            hitsUI.HitsCanvas = uiGO.GetComponent<Canvas>();
            yield return new WaitForSeconds(m_DropInterval);

        }
    }

    /*[SerializeField]
    float m_Scale = 0.8f;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            m_Scale += 0.1f;
        }
        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            m_Scale -= 0.1f;
        }

        m_Scale = Mathf.Clamp(m_Scale, 0.4f, 1.5f);
    }*/
}
