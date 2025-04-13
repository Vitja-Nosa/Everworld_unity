using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public class NavigateHandler : MonoBehaviour
{
    public TMP_InputField username; 
    public TMP_InputField password;

    public GameObject CreateOverlay;
    public GameObject ErrorMessage;

    public UserApiClient userApiClient;

    [ContextMenu("User/Login")]
    public async void AttemptLogin(User user)
    {
        IWebRequestReponse webRequestResponse = await userApiClient.Login(user);

        switch (webRequestResponse)
        {
            case WebRequestData<string> dataResponse:
                Debug.Log("Login succes!");
                SceneManager.LoadScene("WorldSelector");
                break;
            case WebRequestError errorResponse:
                string errorMessage = errorResponse.ErrorMessage;
                Debug.Log("Login error: " + errorMessage);
                showError();
                // TODO: Handle error scenario. Show the errormessage to the user.
                break;
            default:
                throw new NotImplementedException("No implementation for webRequestResponse of class: " + webRequestResponse.GetType());
        }

    }

    public void Login()
    {
        User user = new User(username.text, password.text);
        AttemptLogin(user);
    }

    public void GoToRegister()
    {
        SceneManager.LoadScene("RegisterScene");
    }

    public void showError()
    {
        ErrorMessage.SetActive(true);
    }
}
