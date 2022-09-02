using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlatformSpawner))]
public class PlatformPool : MonoBehaviour
{
    [Tooltip("The more they are, the greater chance to spawn they have.")]
    [SerializeField]
    private List<GameObject> prefabs;

    [SerializeField] private int _poolSize;
    public int PoolSize
    {
        get { return _poolSize; }
    }


    private Queue<GameObject> availablePoolObjects = new Queue<GameObject>();

    void Awake()
    {
        GrowPoolBy(PoolSize);
    }

    private void GrowPoolBy(int platformCount)
    {
        for (int count = 0; count < platformCount; count++)
        {

            GameObject randomInstanceToAdd = Instantiate(GetRandomPrefab());
            randomInstanceToAdd.transform.SetParent(transform);

            AddToPool(randomInstanceToAdd);
        }
    }

    private GameObject GetRandomPrefab()
    {
        int randomPrefabIndex = Random.Range(0, prefabs.Count);

        return prefabs[randomPrefabIndex];
    }

    public void AddToPool(GameObject instance)
    {
        HideObject(instance);

        // If this is a platform with a gem on the top, hide the gem too.
        // A platform with a gem has more than one childs for some visual effects.
        if (instance.transform.childCount > 0)
        {

            HideObject(GetGemFromPlatform(instance));
        }


        availablePoolObjects.Enqueue(instance);
    }

    private void HideObject(GameObject instance)
    {
        instance.SetActive(false);
    }

    private GameObject GetGemFromPlatform(GameObject instance)
    {
        return instance.transform.GetChild(0).gameObject;
    }

    public GameObject GetFromPoolOrNull()
    {
        // This statement makes sure pool stays in the same size.
        if (availablePoolObjects.Count == 0)
        {

            return null;
        }


        GameObject instance = availablePoolObjects.Dequeue();
        ShowObject(instance);

        if (instance.transform.childCount > 0)
        {

            ShowObject(GetGemFromPlatform(instance));
        }

        return instance;
    }

    private void ShowObject(GameObject instance)
    {
        instance.SetActive(true);
    }
}
