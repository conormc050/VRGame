using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class deletetrash : MonoBehaviour
{
    private XRSocketInteractor socket;

    void Start()
    {
        socket = GetComponent<XRSocketInteractor>();
        socket.selectEntered.AddListener(OnItemPlaced);
    }

    private void OnItemPlaced(SelectEnterEventArgs args)
    {
        GameObject placedObject = args.interactableObject.transform.gameObject;

        Destroy(placedObject);
    }
}
