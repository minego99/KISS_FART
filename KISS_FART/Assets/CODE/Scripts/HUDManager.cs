using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUDManager : MonoBehaviour
{
    public Sprite m_friendlyChickenText;
    public Sprite m_unfriendlyChickenText;
    public Sprite m_takeEggText;
    public Sprite m_takenEggText;
    public Image m_hudText;

   

    // Update is called once per frame
    void Update()
    {
        if (GameManager.s_instance.m_currentTime == GameManager.ETimeState.Day)
        {
            
        }
    }
}
