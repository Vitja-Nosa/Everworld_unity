using UnityEngine;

public class AuthManager : MonoBehaviour
{
    public static AuthManager Instance { get; private set; }

    public string ApiToken { get; private set; }

    private void Awake()
    {
       if (Instance == null) {
            Instance = this;
            DontDestroyOnLoad(gameObject);
       }
       else
       {
            Destroy(gameObject);
       }
    }

    public void SetToken(string token)
    {
        ApiToken = token;
    }

    public void ClearToken()
    {
        ApiToken = null;
    }
}
