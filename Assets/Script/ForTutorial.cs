using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ForTutorial : MonoBehaviour
{
    public bool doubleClick = false; //проверка на нажатие кнопки два раза
    public GameObject[] text;

    [Header("Магазин")]
    public Animation anim_1; //анимация для магазина
    public GameObject shopPan; //панель магазина

    [Header("Настройки")]
    public Animation anim_2; //анимация для панели настроек
    public GameObject setPan; //панель настроек

    public void SetPanShow() //скрытие и показ панели настроек
    {
        if (doubleClick == false)
        {
            anim_2.Play("InSetting");
            setPan.SetActive(true);
            doubleClick = true;
            for(int i = 0; i < text.Length; i++)
            {
                text[i].SetActive(false);
            }
        }
        else
        {
            anim_2.Play("OutSetting");
            doubleClick = false;
            for (int i = 0; i < text.Length; i++)
            {
                text[i].SetActive(true);
            }
            Invoke("HidePan", 1);
        }
    }

    public void ShopPanShow() //скрытие и показ магазина
    {
        if (doubleClick == false)
        {
            anim_1.Play("InShop");
            shopPan.SetActive(true);
            doubleClick = true;
            for (int i = 0; i < text.Length; i++)
            {
                text[i].SetActive(false);
            }
        }
        else
        {
            anim_1.Play("OutShop");
            doubleClick = false;
            for (int i = 0; i < text.Length; i++)
            {
                text[i].SetActive(true);
            }
            Invoke("HidePan", 1);
        }
    }
    public void HidePan() //скрытие магазина и панели настроек
    {
        setPan.SetActive(false);
        shopPan.SetActive(false);
    }

    public void PreviousScene()
    {
        WaitTime();
        SceneManager.LoadScene(0);
    }

    private IEnumerator WaitTime()
    {
        yield return new WaitForSeconds(10);
    }
}
