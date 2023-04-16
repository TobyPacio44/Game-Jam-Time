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


    void OnTriggerStay(Collider other)
    {
        Debug.Log("Trigger");
        if (Input.GetKeyDown(KeyCode.E))
        {
            var item = other.transform.GetComponent<Item>();
            if (item)
            {
                inventory.AddItem(item.item, 1);
                Destroy(other.transform.gameObject);
                Instantiate(item.item.prefab, hand.transform.position, hand.transform.rotation, hand.transform);
            }
        }
    }
    
}
