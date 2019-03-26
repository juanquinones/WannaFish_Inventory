using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    //creamos un game object para cada uno de los objetos que forman el inventario
    GameObject inventoryPanel;
    GameObject slotPanel;
    ItemDatabase database; //referencia del database Fetch
    public GameObject inventorySlot;
    public GameObject inventoryItem;

    int slotAmount; //variable que asignaremos para el número de slots a crear en el inventario
    public List<Item> items = new List<Item>(); //creamos la lista de items
    public List<GameObject> slots = new List<GameObject>();

    private void Start()
    {
        database = GetComponent<ItemDatabase>(); //hacemos un llamado alscript ItemDatabase que como estan en el mismo objeto se pueden llamar con getcomponent

        slotAmount = 16;
        inventoryPanel = GameObject.Find("Inventory Panel"); //cargamos en el start el objeto inventory panel
        slotPanel = inventoryPanel.transform.Find("Slot Panel").gameObject; //buscamos el hijo de inventory panel y lo cargamos
        for(int i = 0; i < slotAmount; i++) //hacemos un loop para crear los slots en el inventario
        {
            items.Add(new Item()); //añadimos items a la lista pero con id -1 quiere decir que habara un item pero vacio que hay que llenar
            slots.Add(Instantiate(inventorySlot)); //añadimos los slots en el inventario
            slots[i].GetComponent<Slot>().id = i; //llamamos al script Slot para poder asignar al objeto en otro Slot
            slots[i].transform.SetParent(slotPanel.transform); //establecemos los parametros del padre para que se creen los slots bajo su transform.
        }


        AddItem(0);
        AddItem(0);
        AddItem(1);
        AddItem(1);
        AddItem(1);
        AddItem(1);
        AddItem(1);
        AddItem(1);
        AddItem(1);
        AddItem(2);
        AddItem(2);
        AddItem(2);


        Debug.Log(items[1].Title);

    }

    public void AddItem(int id) //metodo para llenar el item
    {
        Item itemToAdd = database.FetchItemByID(id); //llamamos el metodo fetch para agarrar los items por id
        if (itemToAdd.Stackable && CheckItemInventory(itemToAdd))
        {
            for (int i = 0; i < items.Count; i++)
            {
                if (items[i].ID == id)
                {
                    ItemData data = slots[i].transform.GetChild(0).GetComponent<ItemData>();
                    data.amount++;
                    data.transform.GetChild(0).GetComponent<Text>().text = data.amount.ToString();

                    break;
                }
            }
        }
        else
        {
            for (int i = 0; i < items.Count; i++) //debemos buscar un slot vacio para colocar el item
            {
                if (items[i].ID == -1) //si hay un item con id en -1 es que el slot esta vacio entonces procedemos a ingresar un item en el
                {
                    items[i] = itemToAdd;
                    GameObject itemObj = Instantiate(inventoryItem); // creamos un objeto
                    itemObj.GetComponent<ItemData>().item = itemToAdd; //hacemos que el item en itemdata sea igual a itemToAdd
                    itemObj.GetComponent<ItemData>().amount = 1;
                    itemObj.GetComponent<ItemData>().slot = i; //alamacenamos el numero i de slot en el que esta el objeto
                    itemObj.transform.SetParent(slots[i].transform); //lo asignamos al slot i 
                    itemObj.transform.position = Vector2.zero; // centramos la posición del item al centro del slot
                    itemObj.GetComponent<Image>().sprite = itemToAdd.Sprite; //le agregamos la imagen que corresponde al item
                    itemObj.name = itemToAdd.Title; //mostramos el nombre del item en la geraquia de unity

                    break;
                }
            }
        }
    }   

    bool CheckItemInventory(Item item) //metodo que revisara los slots para saber si hay items en el inventario
    {
        for (int i = 0; i < items.Count; i++)
        {
            if (items[i].ID == item.ID)
                return true;

        }
        return false;
    }

}
