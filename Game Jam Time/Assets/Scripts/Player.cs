using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Player : MonoBehaviour
{ 
    public GameObject player;
    public InventoryObject inventory;
    public Movment movement;
    public Transform hand;
    public GameObject GrabEvent;


    private void OnTriggerEnter(Collider other)
    {
        if(other.tag == "PickUp")
        {
            var item = other.transform.GetComponent<Item>();

            if (!item.GrabEvent)
            {
                item.grabUI = Instantiate(GrabEvent, other.transform.position + new Vector3(0, 1, 0), other.transform.rotation);
                item.GrabEvent = true;
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "PickUp")
        {
            var item = other.transform.GetComponent<Item>();
            Destroy(item.grabUI);
            item.GrabEvent = false;
        }
    }

    void OnTriggerStay(Collider other)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            var item = other.transform.GetComponent<Item>();
            if (item)
            {
                inventory.AddItem(item.item, 1);
                Destroy(other.transform.gameObject);
                Instantiate(item.item.prefab, hand.transform.position, hand.transform.rotation, hand.transform);

                Destroy(item.grabUI);
                item.GrabEvent = false;
            }
        }
    }
    
}
