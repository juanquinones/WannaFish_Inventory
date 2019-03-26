using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using LitJson; //usaremos la libreria LitJson para crear la bd de items
using System.IO; // nos da acceso al sistema de archivos

public class ItemDatabase : MonoBehaviour
{
    private List<Item> database = new List<Item>(); //lista de todos los items del inventario
    private JsonData itemData; //variable json que permitira extraer los objetos del archivo json

    private void Start()
    {
        itemData = JsonMapper.ToObject(File.ReadAllText(Application.dataPath + "/StreamingAssets/Items.json")); //manda a llamar al archivo json
        ConstructItemDatabase(); //llama al metodo en el start para cargar los items.

    }

    public Item FetchItemByID(int id) //metodo que obtiene un item por ID
    {
        for (int i = 0; i < database.Count; i++) //conteja los items en la BD
        {
            if (database[i].ID == id) //si el id coincide entonces
                return database[i]; // me retorna un item
        }
        return null; // de lo contrario no muestra nada
    }

    void ConstructItemDatabase() //diccionario que coteja los items y sus valores en el json
    {
        for (int i = 0; i < itemData.Count; i++)
        {
            database.Add(new Item((int)itemData[i]["id"], itemData[i]["title"].ToString(), (int)itemData[i]["weight"],
                (int)itemData[i]["value"], itemData[i]["description"].ToString(), itemData[i]["slug"].ToString(), 
                (bool)itemData[i]["stackable"])); //se añade a la lista los datos de los atributos del item
        }
    }
}

public class Item  //contentra todas las propiedades de los items
{
    public int ID { get; set; }
    public string Title { get; set; }
    public int Weight { get; set; }
    public int Value { get; set; }
    public string Description { get; set; }   
    public string Slug { get; set; }
    public bool Stackable { get; set; }
    public Sprite Sprite { get; set; } // creamos una propiedad sprite
    
       
    public Item(int id, string title, int weight, int value, string description, string slug, bool stackable) //constructor que define los atributos de los items
    {
        this.ID = id;
        this.Title = title;
        this.Weight = weight;
        this.Value = value;
        this.Description = description;
        this.Slug = slug;
        this.Stackable = stackable;
        this.Sprite = Resources.Load<Sprite>("Sprites/Items/" + slug); //asignamos que la propiedad sprite sea igual al slug del item
        
    }

    public Item() //constructor que al eliminar un item de una casilla la vacia.
    {
        this.ID = -1;
    }
}