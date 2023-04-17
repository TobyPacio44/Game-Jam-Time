using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Interaction : MonoBehaviour
{ 
    [HideInInspector] public GameObject player;
    [HideInInspector] public Movment movement;
    public InventoryObject inventory;

    //Hands
    [HideInInspector] public Hands hands;
    public Transform handHolder;

    //Interaction UI
    public GameObject PickUpUI;
    private Item CurrentNearItem;

    private void Awake()
    {
        player = this.gameObject;
        movement = GetComponent<Movment>();
        hands = GetComponentInChildren<Hands>();
    }

    private void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E) && CurrentNearItem != null)
        {
                inventory.AddItem(CurrentNearItem.item, 1);
                Destroy(CurrentNearItem.transform.gameObject);

                if (hands.heldItem == null)
                {
                    hands.heldItem = Instantiate(CurrentNearItem.item.prefab, handHolder.transform.position, handHolder.transform.rotation, handHolder.transform);
                }

                Destroy(CurrentNearItem.grabUI);
                CurrentNearItem.GrabEvent = false;
                CurrentNearItem = null;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PickUp")
        {
            CurrentNearItem = other.transform.GetComponent<Item>();

            if (!CurrentNearItem.GrabEvent)
            {
                CurrentNearItem.grabUI = Instantiate(PickUpUI, other.transform.position + new Vector3(0, 1, 0), other.transform.rotation);
                CurrentNearItem.GrabEvent = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "PickUp")
        {
            CurrentNearItem = other.transform.GetComponent<Item>();
            Destroy(CurrentNearItem.grabUI);
            CurrentNearItem.GrabEvent = false;
            CurrentNearItem = null;
        }
    }

}
