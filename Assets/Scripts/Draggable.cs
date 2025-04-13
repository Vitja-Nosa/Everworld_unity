using System;
using System.Collections.Generic;
using NUnit.Framework;
using Unity.VisualScripting;
using UnityEngine;

/*
* The GameObject also needs a collider otherwise OnMouseUpAsButton() can not be detected.
*/
public class Draggable: MonoBehaviour
{
    public Transform trans;
    public Vector2 localPoint;
    private Canvas canvas;

    private MainLogic mainLogic;

    public bool isDragging = false;
    private bool locked = false;

    public void Start()
    {
        GameObject obj = GameObject.Find("GameLogic");
        mainLogic = obj.GetComponent<MainLogic>();
    }
    public void StartDragging()
    {
        isDragging = false;
    }

    public void Update()
    {
        if (isDragging)
            trans.position = GetMousePosition();
    }

    private void OnMouseUpAsButton()
    {
        if (!locked)
        {
            locked = true;
            isDragging = !isDragging;

            if (!isDragging)
            {

                Object2D obj = new Object2D
                {
                    id = 0,
                    prefabId = int.Parse(trans.name[0].ToString()),
                    positionX = this.transform.localPosition.x,
                    positionY = this.transform.localPosition.y,
                    environmentId = int.Parse(CurrentEnvironment.id),
                    scaleY = 0,
                    scaleX = 0,
                    rotationZ = 0,
                    sortingLayer = 0
                };
                mainLogic.SaveObj(obj);

            }
        }
    }

    private Vector3 GetMousePosition()
    {
        Vector3 positionInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        positionInWorld.z = 0;
        return positionInWorld;
    }

}
