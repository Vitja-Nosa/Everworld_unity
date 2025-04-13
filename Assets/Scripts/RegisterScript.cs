using System;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RegisterScript : MonoBehaviour
{
    public TMP_InputField username;
    public TMP_InputField password;
    public TMP_InputField confirm_password;

    public UserApiClient userApiClient;

    [ContextMenu("User/Register")]

    public bool DoPasswordsMatch()
    {
        if (password.text == confirm_password.text)
        {
            return true;
        }
        return false;
    }
    public async void AttemptRegister(User user)
    {
        IWebRequestReponse webRequestResponse = await userApiClient.Register(user);

        switch (webRequestResponse)
        {
            case WebRequestData<string> dataResponse:
                Debug.Log("Register succes!");
                GoToLogin();
                break;
            case WebRequestError errorResponse:
                string errorMessage = errorResponse.ErrorMessage;
                Debug.Log("Register error: " + errorMessage);
                // TODO: Handle error scenario. Show the errormessage to the user.
                break;
            default:
                throw new NotImplementedException("No implementation for webRequestResponse of class: " + webRequestResponse.GetType());
        }
    }

    public void Register()
    {
        User user = new User(username.text, password.text);
        if (DoPasswordsMatch()) {
            AttemptRegister(user);
        }
        else
        {
            Debug.Log("Passwords do not match");
        }

    }

    public void GoToLogin()
    {
        SceneManager.LoadScene("LoginScene");
    }
}
