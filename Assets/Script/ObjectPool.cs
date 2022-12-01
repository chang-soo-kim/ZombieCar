using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    #region ╫л╠шео
    private static ObjectPool objectPool = null;
    public static ObjectPool Instance
    {
        get
        {
            if (objectPool == null)
            {
                objectPool = FindObjectOfType<ObjectPool>();
                if (objectPool == null)
                {
                    objectPool = new GameObject("ObjectPool").AddComponent<ObjectPool>();
                }
            }

            return objectPool;
        }
    }
    #endregion

    Dictionary<string, List<GameObject>> table = new Dictionary<string, List<GameObject>>();

    public GameObject CreatePrefab(GameObject prefab, Vector3 position, Quaternion rotation, Transform parent)
    {
        List<GameObject> list = null;
        GameObject instance = null;
        bool listCheck = table.TryGetValue(prefab.name, out list);
        if (listCheck == false)
        {
            list = new List<GameObject>();
            table.Add(prefab.name, list);
        }
        if (list.Count == 0)
        {
            instance = GameObject.Instantiate(prefab, position, rotation, parent);
        }
        else if (list.Count > 0)
        {
            instance = list[0];
            instance.transform.position = position;
            instance.transform.rotation = rotation;
            instance.transform.parent = parent;
            list.RemoveAt(0);
        }
        if (instance != null)
        {
            instance.gameObject.SetActive(true);
            return instance;
        }
        else { return null; }

    }
    public void DestroyPrefab(GameObject Prefab)
    {
        List<GameObject> list = null;
        string prefabld = Prefab.name.Replace("(Clone)", "");
        bool listCached = table.TryGetValue(prefabld, out list);
        if (listCached == false)
        {
            Debug.LogError("Not Found" + Prefab.name);
            return;
        }
        Prefab.gameObject.SetActive(false);
        list.Add(Prefab);
    }
}
