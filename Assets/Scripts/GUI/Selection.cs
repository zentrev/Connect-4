using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Selection: MonoBehaviour
{
    TMP_Dropdown m_dropDown;
    public static int selection;

    public void Start()
    {
        m_dropDown = GetComponent<TMP_Dropdown>();
        m_dropDown.onValueChanged.AddListener(delegate
        {
            Onselection(m_dropDown);
        });
    }
    public void Onselection(TMP_Dropdown change)
    {
        selection = change.value;
        
        Debug.Log(selection);
    }


}
