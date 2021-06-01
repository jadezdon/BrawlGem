using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    public static ObjectPooler current;
    public GameObject prefab;
    public int poolSize;
    public bool isExpandable;

    List<GameObject> objectList;

    void Awake()
    {
        current = this;
        objectList = new List<GameObject>();
    }

    // Start is called before the first frame update
    void Start()
    {
        for (int i = 0; i < poolSize; i++)
        {
            GameObject obj = Instantiate(prefab);
            obj.transform.parent = transform;
            obj.SetActive(false);
            objectList.Add(obj);
        }
    }

    public GameObject GetPooledObject()
    {
        for (int i = 0; i < objectList.Count; i++)
        {
            if (!objectList[i].activeInHierarchy) return objectList[i];
        }

        if (isExpandable)
        {
            GameObject obj = Instantiate(prefab);
            objectList.Add(obj);
        }

        return null;
    }
}
