using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactor : MonoBehaviour
{
    Transform actorCamera;
    LayerMask layerMask;

    [SerializeField]
    private float maxDistanceFromCamera = 10f;

    [SerializeField]
    private float maxInteractableDistance = 3f;
    private float distanceFromActor;

    void Start()
    {
        layerMask = ~LayerMask.GetMask(LayerMask.LayerToName(gameObject.layer));
    }

    // Update is called once per frame
    void Update()
    {
        Interact();
    }

    public void Interact()
    {
        if (Input.GetKeyDown(KeyCode.F))
        {
            actorCamera = Camera.main.transform;
            Debug.Log("Live Camera: " + actorCamera.name);

            Ray ray = new Ray(actorCamera.position, actorCamera.forward);

            if (Physics.Raycast(ray, out RaycastHit raycastHit, maxDistanceFromCamera, layerMask))
            {
                if (raycastHit.transform != null)
                {
                    distanceFromActor = Vector3.Distance(transform.position, raycastHit.transform.position);
                    if (distanceFromActor <= maxInteractableDistance)
                    {
                        Debug.Log("In range: " + raycastHit.transform.name + " (" + distanceFromActor.ToString("0.00") + " units)");
                        Item item = raycastHit.transform.GetComponent<Item>();
                        if (item != null)
                        {
                            item.Interact();
                        }
                    }

                }
                //raycastHit
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, maxInteractableDistance);
    }
}
