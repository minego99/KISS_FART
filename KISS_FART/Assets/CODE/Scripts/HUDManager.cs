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
    public Image m_hudTimeText;
    public Image m_hudEggText;

   

    // Update is called once per frame
    void Update()
    {
        if (GameManager.s_instance.m_currentTime == GameManager.ETimeState.Day)
        {
            m_hudTimeText.sprite = m_friendlyChickenText;
            if (GameManager.s_instance.m_hasLayedAnEgg)
            {
                m_hudEggText.enabled = true;
                m_hudEggText.sprite = m_takeEggText;
            }
            else
            {
                m_hudEggText.sprite = null;
                m_hudEggText.enabled = false;
            }
            if (DinoMovement.s_instance.m_hasEgg)
            {
                m_hudEggText.sprite = m_takenEggText;
            }
        }
        else
        {
            m_hudTimeText.sprite = m_unfriendlyChickenText;
        }

        
    }
}
