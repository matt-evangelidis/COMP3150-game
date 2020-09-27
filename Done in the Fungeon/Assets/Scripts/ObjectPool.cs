using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectPool : MonoBehaviour
{
    public GameObject prefab;
    
    static public Queue<GameObject> pool = new Queue<GameObject>();
    
    private void CreateObject()
    {
        GameObject o = Instantiate(prefab);
        o.gameObject.SetActive(false);
        pool.Enqueue(o);
    }
    
    public GameObject GetFromPool()
    {
        if(pool.Count == 0)
        {
            CreateObject();
        }
        
        GameObject o = pool.Dequeue();
        o.gameObject.SetActive(true);
        return o;
    }
    
    static public void ReturnToPool(GameObject o)
    {
        o.SetActive(false);
        pool.Enqueue(o);
    }
}
