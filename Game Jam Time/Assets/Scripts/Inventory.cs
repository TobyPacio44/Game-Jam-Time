using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    private InventoryObject inventory;
    [HideInInspector] public Hands hands;
    [HideInInspector] public Transform handHolder;

    public int heldItemIndex = 0;

    public List<InventorySlot> Container = new List<InventorySlot>();
    private void Awake()
    {
        hands = GetComponentInChildren<Hands>();
        inventory = GetComponent<Interaction>().inventory;
        handHolder = GetComponent<Interaction>().handHolder;

        inventory.Container.Clear();
    }

    private void Update()
    {
        Container = inventory.Container;

        if (Input.GetKeyDown(KeyCode.Q))
        {
            if(heldItemIndex == inventory.Container.Count) {heldItemIndex = 0;}
            else { heldItemIndex++; }

            Destroy(hands.heldItem.gameObject);
            hands.heldItem = Instantiate(Container.ElementAt(heldItemIndex).item.prefab, handHolder.transform.position, handHolder.transform.rotation, handHolder.transform);
            hands.weaponType = Container.ElementAt(heldItemIndex).item;
            if (heldItemIndex  == inventory.Container.Count) { hands.heldItem = null; hands.weaponType = null; }
        }
    }
}
