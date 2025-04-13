using UnityEngine;
using UnityEngine.UI;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine.SceneManagement;

public class MainLogic : MonoBehaviour
{
    public List<Object2D> Objects;
    public Object2DApiClient objectApi;
    public GameObject testPrefab;
    public List<GameObject> allPrefabs;
    public GameObject myCanvas;

    public async void Start()
    {

        //CurrentEnvironment.id = "40"; //TODO remove this
        IWebRequestReponse response = await objectApi.ReadObject2Ds(Int32.Parse(CurrentEnvironment.id));

        if (response is WebRequestData<List<Object2D>> listResponse)
        {
            Objects = listResponse.Data;

            if (Objects != null && Objects.Count > 0)
            {
                foreach (var obj in Objects)
                {
                    Vector3 position = new Vector3(obj.positionX, obj.positionY, 0);
                    Instantiate(allPrefabs[obj.prefabId-1], position, Quaternion.identity);
                }
            }
            else
            {
            }
        }

    }
    public void SpawnObj(GameObject prefab)
    {
        GameObject obj = Instantiate(prefab, GetMousePosition(), Quaternion.identity);
        Draggable scr = obj.GetComponent<Draggable>();
        scr.isDragging = true;
    }

    public async void SaveObj(Object2D obj)
    {
        await objectApi.CreateObject2D(obj);
    }

    private Vector3 GetMousePosition()
    {
        Vector3 positionInWorld = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        positionInWorld.z = 0;
        return positionInWorld;
    }

    public void GoBack()
    {
        SceneManager.LoadScene("WorldSelector");
    }
}
