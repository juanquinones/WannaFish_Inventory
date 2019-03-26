using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ItemData : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler, IPointerEnterHandler, IPointerExitHandler
{
    public Item item; //objeto que se guardara en el slot
    public int amount; //variable que guardara el numero de objetos del mismo tipo
    public int slot; //variable que guardara el numero de slot del objeto

    private Inventory inv;
    private Tooltip tooltip;
    private Vector2 offset;

    private void Start()
    {
        inv = GameObject.Find("Inventory").GetComponent<Inventory>();
        tooltip = inv.GetComponent<Tooltip>();
    }

    public void OnBeginDrag(PointerEventData eventData) //metodo que arrastra un objeto por frame
    {
        if(item != null) //si existe un objeto
        {
            offset = eventData.position - new Vector2(this.transform.position.x, this.transform.position.y);
            
            this.transform.SetParent(this.transform.parent.parent); //para sacarlo del slot en el que actualmente esta
            this.transform.position = eventData.position - offset; //la posisción del objeto será igual a la del evento (osea al arrastrarlo)
            GetComponent<CanvasGroup>().blocksRaycasts = false; //cuando arrastramos blockeamos el raycast del canvas
        }
    }

    public void OnDrag(PointerEventData eventData) //metodo que arrastra un objeto continuamente
    {
        if (item != null) //si existe un objeto
        {

            this.transform.position = eventData.position - offset;
        }
    }

    public void OnEndDrag(PointerEventData eventData) //meotod que se ejecuta al terminar de arrastrar un objeto
    {
        this.transform.SetParent(inv.slots[slot].transform); //cuando lo soltemos volvera a ser hijo de su padre orginal
        this.transform.position = inv.slots[slot].transform.position; //que vuelva a la posición de su padre cuando soltemos
        GetComponent<CanvasGroup>().blocksRaycasts = true; //cuando soltamos volvemos activar el raycast
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        tooltip.Activate(item);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        tooltip.Deactivate();
    }
}
