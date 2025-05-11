using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.AI;


public class StudentSpawner : MonoBehaviour
{
    public List<Transform> spawnPos;

    public GameObject studentPrefab;

    public List<GameObject> students;
    [Header("Bounds")]
    [SerializeField]
    private Transform boundBotLeft;
    [SerializeField]
    private Transform boundBotRight;
    [SerializeField]
    private Transform boundTopLeft;
    [SerializeField]
    private Transform boundTopRight;

    private void Update()
    {
        int count = students.Count;
        for(int i=0;i<count;i++)
        {

        }
    }

    public void StudentSpawnerCoroutine(int numberOfStudents, float protestTime)
    {
        StartCoroutine(SpawnSomeStudents(numberOfStudents, protestTime));
    }

    IEnumerator SpawnSomeStudents(int numberOfStudents, float protestTime)
    {
        float spawnTime = protestTime * 0.1f, waitTillLeave=protestTime*0.99f, remaining = protestTime  - waitTillLeave;
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < numberOfStudents / 10; j++)
            {
                Vector3 pos = spawnPos[Random.Range(0, spawnPos.Count)].position;
                GameObject newStudent = Instantiate(studentPrefab, pos, Quaternion.identity);
                newStudent.GetComponent<NavMeshAgent>().SetDestination(GetRandomDestination());
                students.Add(newStudent);
            }
            yield return new WaitForSeconds(spawnTime / 10);
        }

        yield return new WaitForSeconds(waitTillLeave);
        int count = students.Count;
        
        for (int i=0;i<count;i++)
        {
            try
            {

                students[i].GetComponent<NavMeshAgent>().destination = spawnPos[Random.Range(0, spawnPos.Count)].position;
            }
            catch
            {

            }
            yield return new WaitForSeconds(remaining / count);
        }
    }

    public void DespawnStudents()
    {
        foreach(GameObject student in students)
        {
            Destroy(student);
        }
        students.Clear();
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
