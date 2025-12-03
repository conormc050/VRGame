using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MessMakerPoole : MonoBehaviour
{
    public GameObject Player;
    public int DrawDistance = 500;
    public LayerMask MessMakerLayer;
    public int StartSize = 10;
    public List<GameObject> PooledObjects = new List<GameObject>();

    List<List<GameObject>> Poole = new List<List<GameObject>>();

    public bool RandomiseScale;
    public float MinimumScale = 1f;
    public float MaximumScale = 1f;

    public bool RandomiseXRotation;
    public bool RandomiseYRotation;
    public bool RandomiseZRotation;

public GameObject TakeFromPoole(int ObjNumber, Vector3 Pos)
{
    // Safety checks
    if (ObjNumber < 0 || ObjNumber >= Poole.Count)
    {
        Debug.LogError($"[MessMakerPoole] Invalid pool index: {ObjNumber} (Poole.Count={Poole.Count})");
        return null;
    }

    // Ensure pool root is active so children can be activeInHierarchy
    if (!this.gameObject.activeSelf)
    {
        Debug.LogWarning("[MessMakerPoole] Pool GameObject was inactive — forcing active.");
        this.gameObject.SetActive(true);
    }

    var relevantPool = Poole[ObjNumber];
    foreach (var gameObject in relevantPool)
    {
        if (!gameObject.activeSelf)
        {
            gameObject.transform.position = Pos;
            gameObject.SetActive(true);
            Debug.Log($"[MessMakerPoole] Reused pooled object '{gameObject.name}' (index {ObjNumber}) at {Pos}. activeSelf={gameObject.activeSelf}, activeInHierarchy={gameObject.activeInHierarchy}");
            return gameObject;
        }
    }

    // none available, instantiate a new one
    if (PooledObjects == null || ObjNumber >= PooledObjects.Count)
    {
        Debug.LogError($"[MessMakerPoole] Cannot instantiate: PooledObjects missing or index out of range. ObjNumber={ObjNumber}, PooledObjects.Count={(PooledObjects!=null?PooledObjects.Count:0)}");
        return null;
    }

    var newInstance = Instantiate(PooledObjects[ObjNumber], this.transform);
    relevantPool.Add(newInstance);

    int layer = LayerMaskToLayer(MessMakerLayer);
    newInstance.layer = layer;
    foreach (Transform child in newInstance.transform)
        child.gameObject.layer = layer;

    newInstance.transform.position = Pos;

    // Force active and log
    newInstance.SetActive(true);
    Debug.Log($"[MessMakerPoole] Instantiated new '{newInstance.name}' (index {ObjNumber}) at {Pos}. activeSelf={newInstance.activeSelf}, activeInHierarchy={newInstance.activeInHierarchy}");

    return newInstance;
}


    private int LayerMaskToLayer(LayerMask mask)
    {
        if (mask == 0) return 0;
        int layer = 0;
        int maskValue = mask.value;
        while (maskValue > 1)
        {
            maskValue >>= 1;
            layer++;
        }
        return layer;
    }

    void Start()
    {
        int layer = LayerMaskToLayer(MessMakerLayer);

        Camera mainCamera = Camera.main;
        if (mainCamera != null)
        {
            float[] distances = mainCamera.layerCullDistances;
            if (distances.Length < 32) distances = new float[32];
            distances[layer] = DrawDistance;
            mainCamera.layerCullDistances = distances;
        }

        // Initialize pools safely
        for (int i = 0; i < PooledObjects.Count; i++)
        {
            Poole.Add(new List<GameObject>());
            for (int j = 0; j < StartSize; j++)
            {
                var newInstance = Instantiate(PooledObjects[i], this.transform);
                newInstance.layer = layer;

                foreach (Transform child in newInstance.transform)
                    child.gameObject.layer = layer;

                newInstance.SetActive(false);
                Poole[i].Add(newInstance);
            }
        }

        Debug.Log($"[MessMakerPoole] Initialized {Poole.Count} object pools.");
    }
    
}
