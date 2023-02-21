using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEditor;
using UnityEngine;
[RequireComponent(typeof(Collider))]
public class Traps : MonoBehaviour
{
    [SerializeField]
    GameObject itemPrefab;
    [SerializeField]
    Sprite icon;

    [SerializeField]
    float rawDamage = 10f;

    [SerializeField]
    string itemName;
    [SerializeField]
    [TextArea(4, 16)]
    string description;

    [SerializeField]
    float weight = 0;
    [SerializeField]
    int quantity = 1;
    [SerializeField]
    int maxStackableQuantity = 1; // for bundles of items, such as arrows or coins

    [SerializeField]
    bool isStorable = false; // if false, item will be used on pickup
    [SerializeField]
    bool isConsumable = true; // if true, item will be destroyed (or quantity reduced) when used

    [SerializeField]
    bool isPickupOnCollision = false;

    private void Start()
    {
        if (isPickupOnCollision)
        {
            gameObject.GetComponent<Collider>().isTrigger = true;
        }
    }

    private void OnTriggerEnter(Collider collider)
    {
        if (isPickupOnCollision)
        {
            if (collider.tag == "Player")
            {
                Interact(collider.gameObject);
            }
        }
    }

    public void Interact(GameObject playerObject)
    {
        Debug.Log("Hit by " + transform.name);

        HealthManager manager = playerObject.GetComponent<HealthManager>();
        if (manager != null)
        {
            manager.SendMessageUpwards("Hit ", 10, SendMessageOptions.DontRequireReceiver);
            
        }
    }

   

    void Use(GameObject playerObject)
    {
        Debug.Log("Using " + transform.name);
        if (isConsumable)
        {
            quantity--;
            if (quantity <= 0)
            {
                Destroy(gameObject);
            }
        }

        HealthManager manager = playerObject.GetComponent<HealthManager>();
        if (manager != null)
        {
            manager.SendMessageUpwards("Hit ", rawDamage, SendMessageOptions.DontRequireReceiver);
        }

    }
}
