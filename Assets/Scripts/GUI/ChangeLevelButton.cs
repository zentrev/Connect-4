using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class ChangeLevelButton : MonoBehaviour
{
    [SerializeField] int m_nextLevel = 0;
    AudioSource MainMenu_Audio = null;

    private Button m_button = null;


    void Start()
    {
        m_button = GetComponent<Button>();
        m_button.onClick.AddListener(TaskOnClick);
        MainMenu_Audio = GetComponent<AudioSource>();
    }

   public void TaskOnClick()
    {
        if (MainMenu_Audio.isPlaying) MainMenu_Audio.Stop();
        GameManager.Instance.LoadLevel(m_nextLevel);
    }
}
