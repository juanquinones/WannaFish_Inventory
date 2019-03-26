using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Tooltip : MonoBehaviour
{
    private Item item;
    private string data;
    private GameObject tooltip;

    private void Start()
    {
        tooltip = GameObject.Find("Tooltip"); //cargamos  el tooltip
        tooltip.SetActive(false);//desactivamos el tootltip despues de enocntrarlo
    }

    private void Update()
    {
        if (tooltip.activeSelf)
        {
            tooltip.transform.position = Input.mousePosition; //el tooltip sigue el mouse
        }
    }

    public void Activate(Item item) //metodo que activa el tooltip al hacer hover el pez
    {
        this.item = item; 
        ConstructDataString();
        tooltip.SetActive(true);
    }

    public void Deactivate() //metodo que desactiva el tooltip cuando no hacemos hover
    {
        tooltip.SetActive(false);
    }

    public void ConstructDataString() // metodo que llama los datos dentro del tooltip
    {
        data = "<color=#0473f0><b>" + item.Title + "</b></color>\n\n" + item.Description + "\n\nPeso: " + item.Weight + "\nValor: " + item.Value; //llamamos la informacion del objeto y la guardamos en una variable
        tooltip.transform.GetChild(0).GetComponent<Text>().text = data; //llamamos al componente texto del item y lo igualamos a data
    }
}
