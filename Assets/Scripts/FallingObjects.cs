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

    IEnumerator Start()
    {
        while(enabled)
        {
            GameObject ballonGO = Instantiate(m_Prefabs[0], new Vector3(Random.Range(-7, 7), 4.0f, 0.0f), Quaternion.identity);
            GameObject uiGO = Instantiate(m_PrefabsUI[0], new Vector3(Random.Range(-7, 7), 4.0f, 0.0f), Quaternion.identity);
            HitsUI hitsUI = ballonGO.GetComponent<HitsUI>();
            Contacts contacts = hitsUI.GetComponent<Contacts>();
            contacts.onContact += () =>
            {
                if (contacts.hits >= contacts.maxHits)
                {
                    GameObject popGO = Instantiate(m_PrefabsPop[0], ballonGO.transform.position, Quaternion.identity);
                    Destroy(ballonGO);
                    Destroy(uiGO);
                }
            };
            hitsUI.HitsCanvas = uiGO.GetComponent<Canvas>();
            yield return new WaitForSeconds(m_DropInterval);

        }
    }
}
