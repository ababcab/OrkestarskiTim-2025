using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class GameLogic : MonoBehaviour
{
    [Header("Dependencies")]
    [SerializeField]
    private Plata plata;
    [SerializeField]
    private TMP_Dropdown dropdown;
    [SerializeField]
    private GameObject endScreen;

    [Header("UI")]
    [SerializeField]
    private TMPro.TextMeshProUGUI timer;

    [Header("Caci")]
    [SerializeField]
    private Transform parentOfCaci;
    [SerializeField]
    private int newCaciAfterProtest;
    [SerializeField]
    private ObjectPool caciPool;
    [SerializeField]
    private List<Caci> caci;
    [SerializeField]
    private List<CaciScrObj> caciScrObjs;

    [Header("Studenti")]
    [SerializeField]
    private GameObject parentOfStudenti;
    private StudentSpawner studentSpawner;
    [SerializeField]
    private int studenti = 10;
    [SerializeField]
    private int studentsInNextProtest = 0;


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
        dropdown = GameObject.Find("Dropdown").GetComponent<TMP_Dropdown>();
        studentSpawner = parentOfStudenti.GetComponent<StudentSpawner>();
        plata.AddMoney(40);
        StartPreparation();
    }

    private void StartPreparation()
    {
        if (currentCoroutine == null)
        {
            currentCoroutine = StartCoroutine(Preparation());
        }
    }

    public bool gameStarted = false;

    IEnumerator Preparation()
    {
        yield return new WaitUntil(() => { return gameStarted; });

        plata.AddMoney(10);


        IncreaseCaci(num_sator* caci_in_sator - caci.Count);


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
        studenti = studentsInNextProtest;
        studentSpawner.StudentSpawnerCoroutine(studenti, protestTime);

        yield return new WaitForSeconds(tickRate);
        timeLeft = protestTime;
        while(timeLeft > 0)
        {
            StudentTick();
            AnimationAndDestinationTick();

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

        studentSpawner.EndProtest();
        studentsInNextProtest += 10;
        studentSpawner.DespawnStudents();
        studentTickCount = 0;
        timeLeft = 0;
        currentCoroutine = null;
        DetermineNewEvent();
    }

    #region Sator
    private int num_sator = 0;
    private int caci_in_sator = 5;
    public void ChangeSator(int change)
    {
        lock(this)
        {
            if(gameStarted == false)
            {
                gameStarted = true;
                dropdown = GameObject.Find("Dropdown").GetComponent<TMP_Dropdown>();
                //Debug.Log($"{dropdown} {GameObject.Find("Dropdown")}");
                dropdown.interactable = true;
            }
            num_sator+= change;
        }
    }
    #endregion

    private void GameOver()
    {
        timeLeft = 0;
        currentCoroutine = null;
        Debug.Log("Game end");

        endScreen.SetActive(true);
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
        int n = caci.Count;
        Caci item = null;
        for (int i=0;i<n;i++)
        {
            //Debug.Log($"{caci.Count} {n}");
            item = caci[i];
            if(item.Affect(studenti))
            {
                //caci.RemoveAt(i);
                //caciPool.ReturnObject(item.gameObject);
                //n--;
                //i--;
            }
        }
;
    }

    private void AnimationAndDestinationTick()
    {
        int before = caci.Count;
        int n = caci.Count;
        Caci item = null;
        for (int i = 0; i < n; i++)
        {
            item = caci[i];
            if(item.AnimationWithRegardsToVelocityUpdate())
            {
                caci.RemoveAt(i);
                //caciPool.ReturnObject(item.gameObject);
                n--;
                i--;
            }
        }
        //throw new System.Exception("Need to update student animator in code!!!!!11");
        

        
        Debug.Log($"Student Tick: Deleted Caci {-caci.Count + before}");
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
            Caci @new = caciPool.GetObject().GetComponent<Caci>();
            @new.SetSO(caciScrObjs[0]);
            @new.SetUp(parentOfCaci);
            caci.Add(@new);
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