using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI; //библиотека для использования UserInterface (объект Text)
using UnityEngine.SceneManagement;

[RequireComponent(typeof(AudioSource))]
public class ForClickScore : MonoBehaviour
{
    [Header("Анимация для Рикардо")]
    private int i = 0;
    public GameObject[] sprites; //создаём массив для спрайтов

    [Header("Основные переменные")]
    public Text text_score; //берём текстовое поле
    public float score; //создаём счётчик монет
    public float bonus = 1; //бонус от улучшения
    public int firstWorker_ScorePerSec = 0; //очки первого рабочего
    public int secondWorker_ScorePerSec = 0; //очки первого рабочего
    public int thirdWorker_ScorePerSec = 0; //очки третьего рабочего
    public int fourthWorker_ScorePerSec = 0; //очки четвёртого рабочего
    public bool doubleClick = false; //проверка на нажатие кнопки два раза

    [Header("Музыка")]
    public GameObject[] playingButtons;
    public int play = 1;
    private int j = 0;
    public AudioSource music;
    public AudioClip[] otherClip;

    [Header("Магазин")]
    public Animation anim_1; //анимация для магазина
    public int[] shopCost; //стоимость улучшения
    public GameObject shopPan; //панель магазина
    public Text[] bonusText; //вывод на кнопку стоимость и текст

    [Header("Настройки")]
    public Animation anim_2; //анимация для панели настроек
    public GameObject setPan; //панель настроек

    private Save sv = new Save();

    private void Awake()
    {
        if (PlayerPrefs.HasKey("saveFile"))
        {
            sv = JsonUtility.FromJson<Save>(PlayerPrefs.GetString("saveFile"));
            score = sv.score;
            bonus = sv.bonus;
            for (int i = 0; i < 5; i++)
            {
                shopCost[i] = sv.levelOfItem[i];
                bonusText[i].text = sv.levelOfBonusText[i];
            }
            firstWorker_ScorePerSec = sv.firstWorker_Score;
            secondWorker_ScorePerSec = sv.secondWorker_Score;
            thirdWorker_ScorePerSec = sv.thirdWorker_Score;
            fourthWorker_ScorePerSec = sv.fourthWorker_Score;
        }
    }

    private void Start() //вызов функции добавления заработанных денег рабочими
    {
        StartCoroutine(BonusPerSec());
        //j = otherClip.Length;
    }

    private void Update() //обновление очков
    {
        text_score.text = score + "$";
    }

    public void SetPanShow() //скрытие и показ панели настроек
    {
        if (doubleClick == false)
        {
            anim_2.Play("InSetting");
            setPan.SetActive(true);
            doubleClick = true;
        }
        else
        {
            anim_2.Play("OutSetting");
            doubleClick = false;
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
        }
        else 
        {
            anim_1.Play("OutShop");
            doubleClick = false;
            Invoke("HidePan", 1);   
        }
    }

    public void NextMusic()
    {
        while (i < otherClip.Length)
        {  
            music.clip = otherClip[i];
            music.Play();
            i++;
            if (i == otherClip.Length)
            {
                i = 0;
            }
            break;
        }
    }

    /*public void PreviousMusic()
    {
        while (j >= otherClip.Length)
        {
            music.clip = otherClip[j];
            music.Play();
            j--;
            if (j == 0)
            {
                j = otherClip.Length;
            }
        }
    }*/

    public void StopMusic()
    {
        if (play == 1)
        {
            music.mute = true;
            play = 0;
            playingButtons[1].SetActive(true);
            playingButtons[0].SetActive(false);
        }
    }

    public void PlayMusic()
    {
        if (play == 0)
        {
            music.mute = false;
            play = 1;
            playingButtons[1].SetActive(false);
            playingButtons[0].SetActive(true);
        }
    }

    public void HidePan() //скрытие магазина и панели настроек
    {
        setPan.SetActive(false);
        shopPan.SetActive(false);
    }

    public void ShopBtn_AddBonus(int index /*В методе указывает, что будем улучшать*/) //метод отвечающий за бонусы
    {
        if (score >= shopCost[index])
        {
            bonus += 0.2f; //прибавляем к бонусу 
            score -= shopCost[index]; //отнимаем от очков
            shopCost[index] *= 2;
            bonusText[index].text = "Улучшение Flex$a\n" + shopCost[index] + "$";
        }
        else
        {
            Debug.Log("You need more Flex$ to buy it");
        }
    }
    /*Наняте рабочих begin*/
    public void HireFirstWorker(int index) 
    {
        if (score >= shopCost[index])
        {
            firstWorker_ScorePerSec++;
            score -= shopCost[index];
            shopCost[index] *= 3;
            bonusText[index].text = "HIT OR MISS\n" + shopCost[index] + "$";
        }
        else
        {
            Debug.Log("You need more Flex$ to buy it");
        }
    }

    public void HireSecondWorker(int index)
    {
        if (score >= shopCost[index])
        {
            secondWorker_ScorePerSec += 10;
            score -= shopCost[index];
            shopCost[index] *= 4;
            bonusText[index].text = "BONGO CAT\n" + shopCost[index] + "$";
        }
        else
        {
            Debug.Log("You need more Flex$ to buy it");
        }
    }

    public void HireThirdWorker(int index)
    {
        if (score >= shopCost[index])
        {
            thirdWorker_ScorePerSec += 100;
            score -= shopCost[index];
            shopCost[index] *= 6;
            bonusText[index].text = "FUJIWARA\n" + shopCost[index] + "$";
        }
    }

    public void HireFourthWorker(int index)
    {
        if (score >= shopCost[index])
        {
            fourthWorker_ScorePerSec += 1000;
            score -= shopCost[index];
            shopCost[index] *= 8;
            bonusText[index].text = "WAIFU\n" + shopCost[index] + "$";
        }
    }
    /*Наняте рабочих end*/
    IEnumerator BonusPerSec() //добавляем к очкам заработанные деньги рабочих
    {
        while (true)
        {
            score += firstWorker_ScorePerSec + secondWorker_ScorePerSec + thirdWorker_ScorePerSec + fourthWorker_ScorePerSec;
            yield return new WaitForSeconds(1);
        }
    }

    private void OnApplicationQuit() //при выходе из игры сохраняем следующие переменные
    {
        sv.score = score;
        sv.bonus = bonus;

        sv.levelOfItem = new int[5];
        sv.levelOfBonusText = new String[5];

        sv.firstWorker_Score = firstWorker_ScorePerSec;
        sv.secondWorker_Score = secondWorker_ScorePerSec;
        sv.thirdWorker_Score = thirdWorker_ScorePerSec;
        sv.fourthWorker_Score = fourthWorker_ScorePerSec;

        for (int i = 0; i < 5; i++)
        {
            sv.levelOfItem[i] = shopCost[i];
            sv.levelOfBonusText[i] = bonusText[i].text;
        }

        PlayerPrefs.SetString("saveFile", JsonUtility.ToJson(sv));
    }

    public void On_Click() //метод увеличивающий счётчик монет
    {
        score += bonus;
    }

    public void Next_sprite() //анимация 
    {
        while (i < sprites.Length)
        {
            sprites[i].SetActive(false);
            sprites[i+1].SetActive(true);
            i++;
            if (i == sprites.Length - 1)
            {
                sprites[i].SetActive(false);
                sprites[0].SetActive(true);
                i=0;
                score += 10; //Получаем + 10 монет (Full Flex$)
            }
            break;
        }
    }

    public void NextScene()
    {
        SceneManager.LoadScene(1);
    }

}
//класс отвечающий за сохранение
[Serializable]
public class Save
{
    public float score;
    public float bonus;
    public int[] levelOfItem;
    public String[] levelOfBonusText;
    public int firstWorker_Score;
    public int secondWorker_Score;
    public int thirdWorker_Score;
    public int fourthWorker_Score;
    /*public GameObject[] Buttons;
    public int misc;*/
}
