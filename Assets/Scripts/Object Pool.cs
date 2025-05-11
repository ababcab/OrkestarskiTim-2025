using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.Users;

public class ObjectPool : MonoBehaviour
{
    [Header("Object Pool Prefab")]
    [SerializeField]
    public Transform parentPool;
    [SerializeField]
    public GameObject prefab;
    [SerializeField]
    public int count;
    public int head = 0;
    public int inUse = 0;


    public List<GameObject> pool;


    private void Awake()
    {
        GameObject @object;
        for(int i=0;i<count; i++)
        {
            @object = Instantiate(prefab, parentPool);
            @object.SetActive(false);
            pool.Add(@object);
        }
    }


    public GameObject GetObject(bool setActive = true)
    {
        if(inUse == count)
        {
            throw new System.Exception("BRUH TOO LITTLE POOL SIZE");
        }
        else
        {
            GameObject gO = pool[head];
            head++;
            head %= count;
            inUse++;

            if (setActive)
                gO.SetActive(true);
            return gO;
        }
    }
    public void ReturnObject(GameObject gO)
    {
        if (inUse == 0)
            throw new System.Exception("You didnt take anything");
        gO.transform.parent = parentPool;
        gO.SetActive(false);
        inUse--;
    }


}
