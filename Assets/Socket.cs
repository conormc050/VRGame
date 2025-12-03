using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Socket : MonoBehaviour
{
        public XRSocketInteractor socket;

    void Awake()
    {
        socket = GetComponent<XRSocketInteractor>();
    }

    void OnEnable()
    {
        socket.selectEntered.AddListener(OnPlaced);
        socket.selectExited.AddListener(OnRemoved);
    }

    void OnDisable()
    {
        socket.selectEntered.RemoveListener(OnPlaced);
        socket.selectExited.RemoveListener(OnRemoved);
    }

    void OnPlaced(SelectEnterEventArgs args)
    {
        args.interactableObject.transform.localRotation = Quaternion.identity;
        args.interactableObject.transform.localPosition = Vector3.zero;
    }

    void OnRemoved(SelectExitEventArgs args)
    {
        
    }
}