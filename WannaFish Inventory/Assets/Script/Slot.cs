using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using System;

public class Slot : MonoBehaviour, IDropHandler
{
    public int id;
    private Inventory inv; //llamamos al script inventory

    private void Start()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>(); //cargamos el inventario y nos traemos todos los datos
    }

    public void OnDrop(PointerEventData eventData)
    {
        ItemData droppedItem = eventData.pointerDrag.GetComponent<ItemData>(); //al soltar un objeto en una casilla traerse como parametro el itemData del objeto

        if (inv.items[id].ID == -1) //si el id es -1 significa que el slot esta vacio
        {
            inv.items[droppedItem.slot] = new Item(); // limpiamos el slot donde estaba el objeto
            inv.items[id] = droppedItem.item;
            droppedItem.slot = id; //el objeto tomara el id del slot nuevo para ser asignado ahi.
        }
        else if(droppedItem.slot != id) // si el slot no esta vacío
        {
            Transform item = this.transform.GetChild(0); //llamamos al unico hijo del slot actual
            item.GetComponent<ItemData>().slot = droppedItem.slot;  
            item.transform.SetParent(inv.slots[droppedItem.slot].transform); // asignamos el slot antiguo como padre del objeto
            item.transform.position = inv.slots[droppedItem.slot].transform.position; //asignamos el objeto en la posicion del padre

            droppedItem.slot = id; //asignamos al objeto que estabamos arrastrando al slot actual
            droppedItem.transform.SetParent(this.transform);
            droppedItem.transform.position = this.transform.position;

            inv.items[droppedItem.slot] = item.GetComponent<ItemData>().item;
            inv.items[id] = droppedItem.item;
        }

    }
}
