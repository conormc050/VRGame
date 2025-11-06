using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class ScoreBin : MonoBehaviour
{
    public string TrashTag;
    public GameManager gm;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(TrashTag))
        {
            if (gm != null)
            {
                gm.AddScore(1);
            }
            else
            {
                Debug.LogWarning("Nope" + gameObject.name);
            }
        }
    }
}
