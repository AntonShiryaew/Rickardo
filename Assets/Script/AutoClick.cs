using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoClick : MonoBehaviour
{
    public ForClickScore elem = new ForClickScore();

    void Start()
    {
        StartCoroutine(BonusPerSec());
    }

    public void HireFirstWorker(int index) //нанятие рабочих
    {
        if (elem.score >= elem.shopCost[index])
        {
            elem.firstWorker_ScorePerSec++;
            elem.score -= elem.shopCost[index];
            elem.shopCost[index] *= 3;
            elem.bonusText[index].text = "HIT OR MISS\n" + elem.shopCost[index] + "$";
        }
        else
        {
            Debug.Log("You need more Flex$ to buy it");
        }
    }

    IEnumerator BonusPerSec() //добавляем к очкам заработанные деньги рабочих
    {
        while (true)
        {
            elem.score += elem.firstWorker_ScorePerSec;
            yield return new WaitForSeconds(1);
        }
    }
}
