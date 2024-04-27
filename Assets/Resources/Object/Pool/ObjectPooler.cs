using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ObjectPooler : MonoBehaviour
{
    [System.Serializable]
    public class Pool {
        public string tag;
        public int size;
        public GameObject prefab;
    }
    public static ObjectPooler Instance;
    private void Awake() {
        Instance = this;
    }
    public List<Pool> pools;
    public Dictionary<string, Queue<GameObject>> poolDictionary;

    private void Start() {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (var pool in pools) {
            Queue<GameObject> objectPool = new Queue<GameObject>();
            for(int i=0; i < pool.size; i++){
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }
            poolDictionary.Add(pool.tag, objectPool);
        }  
    }

    public void SpawnFromPool(string tag, Vector3 position, Quaternion rotation){
        GameObject obj = poolDictionary[tag].Dequeue();
        obj.transform.position = position;
        obj.transform.rotation = rotation;
        obj.SetActive(true);
        poolDictionary[tag].Enqueue(obj);
    }
}
