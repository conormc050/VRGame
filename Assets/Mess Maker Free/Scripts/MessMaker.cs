using System.Collections.Generic;
using UnityEngine;

public class MessMaker : MonoBehaviour
{
    public MessMakerPoole messMakerPoole;
    public int Number = 10;
    public int Range = 20;

    List<GameObject> Objects = new List<GameObject>();
    bool active = false;

    private void Start()
    {
        
        Debug.Log("[MessMaker] Start() called.");
        if (!active)
        {
            GenerateMess();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log("[MessMaker] Trigger entered by: " + other.name);
        if (other.gameObject == messMakerPoole.Player && !active)
        {
            GenerateMess();
        }
    }

    // private void OnTriggerExit(Collider other)
    // {
    //     if (other.gameObject == messMakerPoole.Player && active)
    //     {
    //         Debug.Log("[MessMaker] Player left trigger, clearing objects.");
    //         active = false;

    //         foreach (GameObject thing in Objects)
    //         {
    //             thing.transform.position = Vector3.zero;
    //             thing.SetActive(false);
    //         }
    //     }
    // }

    private void GenerateMess()
    {
        if (messMakerPoole == null)
        {
            Debug.LogError("[MessMaker] No MessMakerPoole assigned!");
            return;
        }

        active = true;
        Objects = new List<GameObject>();
        Debug.Log("[MessMaker] Generating " + Number + " objects...");

        for (int i = 0; i < Number; i++)
        {
            int which = UnityEngine.Random.Range(0, messMakerPoole.PooledObjects.Count);

            float x = transform.position.x + UnityEngine.Random.Range(-Range, Range + 1);
            float z = transform.position.z + UnityEngine.Random.Range(-Range, Range + 1);
            Vector3 spawnPos = new Vector3(x, transform.position.y, z);

            GameObject obj = messMakerPoole.TakeFromPoole(which, spawnPos);
            if (obj == null)
            {
                Debug.LogWarning("[MessMaker] Failed to take object from pool!");
                continue;
            }

            Quaternion rotation = Quaternion.identity;
            if (messMakerPoole.RandomiseXRotation)
                rotation *= Quaternion.Euler(UnityEngine.Random.Range(0, 360), 0, 0);
            if (messMakerPoole.RandomiseYRotation)
                rotation *= Quaternion.Euler(0, UnityEngine.Random.Range(0, 360), 0);
            if (messMakerPoole.RandomiseZRotation)
                rotation *= Quaternion.Euler(0, 0, UnityEngine.Random.Range(0, 360));

            obj.transform.rotation = rotation;

            if (messMakerPoole.RandomiseScale)
            {
                float scale = UnityEngine.Random.Range(messMakerPoole.MinimumScale, messMakerPoole.MaximumScale);
                obj.transform.localScale = new Vector3(scale, scale, scale);
            }

            Objects.Add(obj);
            Debug.Log($"[MessMaker] Spawned object #{i} at {spawnPos}");
        }

        Debug.Log("[MessMaker] Generation complete.");
    }
}
