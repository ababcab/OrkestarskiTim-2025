using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameLogic : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField]
    private Plata plata;

    [Header("UI")]
    [SerializeField]
    private TMPro.TextMeshProUGUI timer;
    [SerializeField]
    private TMPro.TextMeshProUGUI sredstva;

    [Header("Caci")]
    [SerializeField]
    private Transform parentOfCaci;
    [SerializeField]
    private ObjectPool caciPool;
    [SerializeField]
    private List<Caci> caci;
    [SerializeField]
    private List<CaciScrObj> caciScrObjs;

    [Header("Studenti")]
    [SerializeField]
    private int studenti = 0;
    [Header("Round params")]
    [SerializeField]
    private float tickRate = 0.1f;
    [SerializeField]
    private float preparationTime = 10;
    [SerializeField]
    private float protestTime = 20;
    [SerializeField]
    private float timeLeft = 0;
    private float timeLeftRound;



    private Coroutine currentCoroutine = null;

    private void Start()
    {
        caci = new List<Caci>();
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
        plata.AddMoney(100);

        IncreaseCaci(1);


        yield return new WaitForEndOfFrame();
        timeLeft = preparationTime;
        while (timeLeft > 0)
        {
            yield return new WaitForSeconds(tickRate);
            RoundTime();
            if (timeLeftRound <= 5)
                TimerColor();
            timer.SetText("Priprema: " + timeLeftRound);
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

        studenti = Random.Range(0, 20);

        yield return new WaitForSeconds(tickRate);
        timeLeft = protestTime;
        while(timeLeft > 0)
        {
            StudentTick();
            CaciDestinationTick();

            if (caci.Count != 0)
                yield return new WaitForSeconds(tickRate);
            else
            {
                GameOver();
                yield break;

            }
            
            RoundTime();
            timer.SetText("Protest: " + timeLeftRound);
        }

        studentTickCount = 0;
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

    int studentTickCount = 0;
    int studentTickPass = 25;
    private void StudentTick()
    {
        if(studentTickCount != studentTickPass)
        {
            studentTickCount++;
            return;
        }
        studentTickCount = 0;
        int before = caci.Count;
        int n = caci.Count;
        Caci item = null;
        for (int i=0;i<n;i++)
        {
            //Debug.Log($"{caci.Count} {n}");
            item = caci[i];
            if(item.Affect(studenti))
            {
                caci.RemoveAt(i);
                caciPool.ReturnObject(item.gameObject);
                n--;
                i--;
            }
        }

        Debug.Log($"Student Tick: Deleted Caci {-caci.Count+before}");
    }

    private void CaciDestinationTick()
    {
        int n = caci.Count;
        Caci item = null;
        for (int i = 0; i < n; i++)
        {
            item = caci[i];
            item.GoToDestination(tickRate);
        }
    }

    private void DetermineNewEvent()
    {


        Debug.Log($"Survived protest");

        StartCoroutine(Preparation());
    }


    private void IncreaseCaci(int amount)
    {
        for(int i=0;i<amount;i++)
        {
            //Caci @new = new Caci();
            /*Caci @new = caciPool.GetObject().GetComponent<Caci>();
            @new.SetSO(caciScrObjs[0]);
            @new.SetUp(parentOfCaci);
            caci.Add(@new);*/
        }
    }


    public void HandleMouseClick()
    {
        //IncreaseCaci(20);
    }

    private void RoundTime()
    {
        timeLeft -= tickRate;
        timeLeftRound = timeLeft;
        timeLeftRound = Mathf.Round(timeLeftRound);
    }

    private void TimerColor()
    {
        if (timeLeftRound <= timeLeft)
            timer.color = new Color32(255, 0, 0, 255);
        else
            timer.color = new Color32(255, 255, 255, 255);
    }
}