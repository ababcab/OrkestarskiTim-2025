using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;


public class StudentSpawner : MonoBehaviour
{
    [Header("Object pool")]
    [SerializeField]
    private ObjectPool pool;
    public List<Transform> spawnPos;


    private List<Student> students;
    [Header("Bounds")]
    [SerializeField]
    private Transform boundBotLeft;
    [SerializeField]
    private Transform boundBotRight;
    [SerializeField]
    private Transform boundTopLeft;
    [SerializeField]
    private Transform boundTopRight;

    private void Start()
    {
        students = new List<Student>();

        speedInWhichIdle_Squared = speedInWhichIdle * speedInWhichIdle;
    }

    private bool protestOn = false;
    private int numberOfStudents,spawnedStudents,leavingStudents;
    private float protestTime, spawnTime, waitTillLeave, remaining, passedTime = 0;
    private void Update()
    {

        int studentiCount = students.Count;
        for (int i = 0; i < studentiCount; i++)
        {
            AnimationWithRegardsToVelocityUpdate(i);
        }






        if (protestOn)
        {
            if(passedTime < spawnTime)
            {
                for (int j = spawnedStudents; j < numberOfStudents; j++)
                {
                    Student newStudent = pool.GetObject().GetComponent<Student>();

                    Vector3 pos = spawnPos[Random.Range(0, spawnPos.Count)].position;
                    newStudent.transform.position = pos;



                    newStudent.agent.SetDestination(GetRandomDestination());

                    students.Add(newStudent);
                    spawnedStudents++;
                }
                passedTime += Time.deltaTime;
                return;
            }
            else if(spawnedStudents != numberOfStudents)
            {
                for (int j = spawnedStudents; j < numberOfStudents; j++)
                {
                    Student newStudent = pool.GetObject().GetComponent<Student>();

                    Vector3 pos = spawnPos[Random.Range(0, spawnPos.Count)].position;
                    newStudent.transform.position = pos;



                    newStudent.agent.SetDestination(GetRandomDestination());

                    students.Add(newStudent);
                    spawnedStudents++;
                }

                passedTime += Time.deltaTime;
                return;
            }

            if(passedTime >= waitTillLeave)
            {
                for (int i = leavingStudents; i < numberOfStudents; i++)
                {
                    try
                    {
                        students[i].agent.destination = spawnPos[Random.Range(0, spawnPos.Count)].position;
                        leavingStudents++;
                    }
                    catch (System.Exception e)
                    {
                        Debug.LogError($"Student NavMeshAgents {numberOfStudents} {i} {e.Message}");
                    }
                }
            }

            passedTime += Time.deltaTime;
            return;

        }

    }

    public void EndProtest()
    {
        if (protestOn == false)
            throw new System.Exception("Protest ended before getting the message to end protest");
        protestTime = spawnTime = waitTillLeave = remaining = passedTime = 0;
        protestOn = false;
    }
    public void StudentSpawnerCoroutine(int numberOfStudents, float protestTime)
    {

        protestOn = true;
        this.protestTime = protestTime;
        spawnTime = protestTime * 0.1f;
        waitTillLeave = protestTime - 5f;
        if (waitTillLeave <= 0)
            throw new System.Exception($"Protest time is too short {protestTime} {waitTillLeave}");
        passedTime = 0;
        spawnedStudents = 0;
        leavingStudents = 0;
        this.numberOfStudents = numberOfStudents;

    }

    IEnumerator SpawnSomeStudents(int numberOfStudents, float protestTime)
    {
        float spawnTime = protestTime * 0.1f, waitTillLeave=protestTime*0.99f, remaining = protestTime  - waitTillLeave;
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < numberOfStudents / 10; j++)
            {
                Student newStudent = pool.GetObject().GetComponent<Student>();

                Vector3 pos = spawnPos[Random.Range(0, spawnPos.Count)].position;
                newStudent.transform.position = pos;



                newStudent.agent.SetDestination(GetRandomDestination());

                students.Add(newStudent);
            }
            yield return new WaitForSeconds(spawnTime / 10);
        }

        yield return new WaitForSeconds(waitTillLeave);
        int count = students.Count;
        Debug.LogError($"Students leaving");
        for (int i=0;i<count;i++)
        {
            try
            {
                students[i].agent.destination = spawnPos[Random.Range(0, spawnPos.Count)].position;
            }
            catch (System.Exception e)
            {
                Debug.LogError($"Student NavMeshAgents {count} {i} {e.Message}");
            }
            yield return new WaitForSeconds(remaining / count);
        }
    }

    public void DespawnStudents()
    {
        int n = students.Count;
        for (int i=0;i<n;i++)
        {
            pool.ReturnObject(students[i].gameObject);
        }
        students.Clear();
    }
    private float speedInWhichIdle = 1, speedInWhichIdle_Squared;
    public bool AnimationWithRegardsToVelocityUpdate(int studentIndex)
    {
        if(students[studentIndex].agent.velocity.sqrMagnitude >= speedInWhichIdle_Squared 
            && students[studentIndex].animator.GetBool("Walk") == false)
        {
            Debug.Log($"student set animator Walk to true");
            students[studentIndex].animator.SetBool("Walk",true);
            students[studentIndex].animator.SetBool("Jump",false);
        }
        else if (students[studentIndex].agent.velocity.sqrMagnitude < speedInWhichIdle_Squared
            && students[studentIndex].animator.GetBool("Jump") == false)
        {
            Debug.Log($"student set animator Jump to true");

            students[studentIndex].animator.SetBool("Jump", true);
            students[studentIndex].animator.SetBool("Walk", false);
        }



        return false;
    }


    public Vector3 GetRandomDestination()
    {
        float randomLeftRight = Random.Range(0, 1f);
        float randomBotTop = Random.Range(0, 1f);
        Vector3 dest = boundBotLeft.position
            + (boundTopRight.position - boundTopLeft.position) * randomBotTop
            + (boundTopLeft.position - boundBotLeft.position) * randomLeftRight;



        return dest;
    }

}
