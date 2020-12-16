using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Contacts))]
public class HitsUI : MonoBehaviour
{

    [SerializeField]
    Canvas m_HitsCanvas = null;

    Text m_HitsText = null;

    Contacts m_Contacts = null;

    public Canvas HitsCanvas 
    { 
        set 
        {
            m_HitsCanvas = value;
            m_HitsText = m_HitsCanvas.GetComponentInChildren<Text>();
        } 
    }

    void Awake()
    {
        m_Contacts = GetComponent<Contacts>();
        m_Contacts.onContact += M_Contacts_onContact;
    }

    void M_Contacts_onContact()
    {
        m_HitsText.text = string.Format("{0}", m_Contacts.hits);
    }

    void Update()
    {
        m_HitsCanvas.transform.position = transform.position;
    }
}
