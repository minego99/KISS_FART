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
    public Sprite m_0EggScore;
    public Sprite m_1EggScore;
    public Sprite m_2EggScore;
    public Sprite m_3EggScore;
    public Sprite m_4EggScore;
    public Sprite m_5EggScore;
    public Image m_hudTimeText;
    public Image m_hudEggText;
    public Image m_hudScoreEggText;


    // Update is called once per frame
    void Update()
    {

        if (GameManager.s_poulerScore == 0)
        {
            m_hudEggText.sprite = m_0EggScore;
        }
        else if (GameManager.s_poulerScore == 1)
        {
            m_hudEggText.sprite = m_1EggScore;
        }
        else if(GameManager.s_poulerScore == 2)
        {
            m_hudEggText.sprite = m_2EggScore;
        }
        else if(GameManager.s_poulerScore == 3)
        {
            m_hudEggText.sprite = m_3EggScore;
        }
        else if(GameManager.s_poulerScore == 4)
        {
            m_hudEggText.sprite = m_4EggScore;
        }
        else if(GameManager.s_poulerScore == 5)
        {
            m_hudEggText.sprite = m_5EggScore;
        }


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
