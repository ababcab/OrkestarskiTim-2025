using System.Collections;
using System.Collections.Generic;
using NUnit.Framework;
using UnityEngine;


public class StudentSpawner : MonoBehaviour
{
    public List<Transform> spawnPos;

    public GameObject studentPrefab;

    public List<GameObject> students;

    public void SpawnStudents(int numberOfStudents, float protestTime)
    {
        StartCoroutine(SpawnSomeStudents(numberOfStudents, protestTime));
    }

    IEnumerator SpawnSomeStudents(int numberOfStudents, float protestTime)
    {
        for (int i = 0; i < 10; i++)
        {
            for (int j = 0; j < numberOfStudents / 10; j++)
            {
                Vector3 pos = spawnPos[Random.Range(0, spawnPos.Count)].position;
                GameObject newStudent = Instantiate(studentPrefab, pos, Quaternion.identity);
                students.Add(newStudent);
            }
            yield return new WaitForSeconds(protestTime / 10);
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
}
