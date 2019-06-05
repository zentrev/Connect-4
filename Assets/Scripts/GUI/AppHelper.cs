using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class AppHelper :MonoBehaviour
{
    public void Quit()
    {
        if (EditorApplication.isPlaying)
        {
            UnityEditor.EditorApplication.isPlaying = false;
        }
    }
}
