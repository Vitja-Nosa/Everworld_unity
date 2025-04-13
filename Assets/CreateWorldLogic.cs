using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;
using NUnit.Framework;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;
using System;
using UnityEngine.SceneManagement;

public class CreateWorldLogic : MonoBehaviour
{
    public GameObject CreateOverlay;
    public Environment2DApiClient EnvironmentApi;
    public List<Environment2D> Environments;
    public List<GameObject> MenuItems;

    public TMP_InputField WorldName;
    public TMP_InputField WorldHeight;
    public TMP_InputField WorldLength;

    public async void Start()
    {
        await ShowEnvironments();
    }

    public async Task ShowEnvironments()
    {
        IWebRequestReponse response = await EnvironmentApi.ReadEnvironment2Ds();

        if (response is WebRequestData<List<Environment2D>> listResponse)
        {
            Environments = listResponse.Data;

            if (Environments != null && Environments.Count > 0)
            {
                int i = 0;
                foreach (var environment in Environments)
                {
                    MenuItems[i].SetActive(true);
                    TMP_Text titleText = MenuItems[i].GetComponentInChildren<TMP_Text>();
                    titleText.text = environment.name;
                    
                    i++;
                }
            }
            else
            {
                Debug.LogWarning("No environments found.");
            }
        }
        
    }
    
    public void ShowCreateOverlay(bool state)
    {
        CreateOverlay.SetActive(state);
    }

    public async void CreateNewEnvironment()
    {
        Environment2D env = new Environment2D
        {
            name = WorldName.text,
            maxHeight = int.Parse(WorldHeight.text),
            maxLength = int.Parse(WorldLength.text)
        };
        IWebRequestReponse res = await EnvironmentApi.CreateEnvironment(env);
        if (res is WebRequestError errorResponse)
        {
            Debug.Log("Something went wrong");
            return;
        }
        ShowCreateOverlay(false);
        ResetMenuItems();
        await ShowEnvironments();
    }

    public void ResetMenuItems()
    {
        foreach(GameObject obj in MenuItems)
        {
            obj.SetActive(false);
        }
    }

    public async void DeleteEnvironment(int index)
    {
        Debug.Log("clicked");
        Environment2D env = Environments[index];
        await EnvironmentApi.DeleteEnvironment(env.id);
        ResetMenuItems();
        await ShowEnvironments();
    }

    public async void EnterEnvironment(int index)
    {
        Environment2D env = Environments[index];
        CurrentEnvironment.id = env.id;
        SceneManager.LoadScene("MainGameScene");
    }
}

public static class CurrentEnvironment
{

    public static string id;
}
