using System;
using System.Collections;
using UnityEngine;

public class GameLogic : MonoBehaviour
{
    [Header("Caci")]
    [SerializeField]
    private int caci_count = 0;
    [SerializeField]
    private float preparationTime = 10;
    [SerializeField]
    private float protestTime = 20;
    [SerializeField]
    private float timeLeft = 0;



    private Coroutine currentCoroutine = null;

    private void Awake()
    {
        StartPreparation();
    }

    
    private void StartPreparation()
    {
        if (currentCoroutine == null)
        {
            currentCoroutine = StartCoroutine(Preparation());
        }
    }

    IEnumerator Preparation()
    {
        yield return new WaitForEndOfFrame();
        timeLeft = preparationTime;
        while (timeLeft > 0)
        {
            yield return new WaitForSeconds(0.1f);
            timeLeft -= 0.1f;
        }
        timeLeft = 0;
        currentCoroutine = null;
        StartProtest();
    }




    private void StartProtest()
    {
        if(currentCoroutine == null)
        {
            currentCoroutine = StartCoroutine(Protest());
        }
    }

    IEnumerator Protest()
    {
        //Init system logic

        caci_count += 100;
        yield return new WaitForSeconds(0.1f);
        timeLeft = protestTime;
        while(timeLeft > 0)
        {
            //StudentTick();
            caci_count--;
            if (caci_count != 0)
                yield return new WaitForSeconds(0.1f);
            else
            {
                GameOver();
                yield break;
                Debug.Log($"Shouldnt happen");
            }
            
            timeLeft -= 0.1f;
        }

        timeLeft = 0;
        currentCoroutine = null;
        DetermineNewEvent();
    }

    private void GameOver()
    {
        timeLeft = 0;
        currentCoroutine = null;
        Debug.Log("Game end");
    }

    /*
    void StudentTick()
    {
        foreach (Caci item in caci)
        {

            item.affected(currentStudentScrObj.loyaltyImpact);
        }
    }*/


    private void DetermineNewEvent()
    {


        Debug.Log($"Survived protest");

        StartCoroutine(Preparation());
    }


    private void IncreaseCaci(int amount)
    {
        caci_count += amount;
    }


    public void HandleMouseClick()
    {
        IncreaseCaci(20);
    }


}
