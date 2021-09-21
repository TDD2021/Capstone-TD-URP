using PlayFab;
using PlayFab.ClientModels;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class PlayFabLogin : MonoBehaviour
{
    private string userEmail;
    private string userPassword;
    private string userName;

    public GameObject loginPanel;
    public GameObject addLoginPanel;
    public GameObject recoverButton;


    public void Start()
    {
        //Setting title Id is set in Editor Extensions already.
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
        {
            PlayFabSettings.TitleId = "9FC97"; //Change to value of Title in Playfab Game Manager for redundancy
        }
        //var request = new LoginWithCustomIDRequest { CustomId = "GettingStartedGuide", CreateAccount = true };
        //PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);

        //var request = new LoginWithCustomIDRequest { CustomId = "GettingStartedGuide", CreateAccount = true };
        //PlayFabClientAPI.LoginWithCustomID(request, OnLoginSuccess, OnLoginFailure);
        Debug.Log("Start Called.");

        if (PlayerPrefs.HasKey("EMAIL"))
        {
            userEmail = PlayerPrefs.GetString("EMAIL");
            userPassword = PlayerPrefs.GetString("PASSWORD");

            //var request = new LoginWithEmailAddressRequest { Email = userEmail, Password = userPassword };
            //PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
        }
        
        else
        {
#if UNITY_ANDROID
            var requestAndroid = new LoginWithAndroidDeviceIDRequest { AndroidDeviceId = returnMobileID(), CreateAccount = true };
            PlayFabClientAPI.LoginWithAndroidDeviceID(requestAndroid, OnLoginMobileSuccess, OnLoginMobileFailure);
#endif

#if UNITY_IOS
            var requestIOS = new LoginWithIOSDeviceIDRequest { DeviceId = returnMobileID(), CreateAccount = true };
            PlayFabClientAPI.LoginWithIOSDeviceID(requestIOS, OnLoginMobileSuccess, OnLoginMobileFailure);
#endif
        }

        

    }
    
    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Non-Mobile Login Success!");
        PlayerPrefs.SetString("EMAIL", userEmail);
        PlayerPrefs.SetString("PASSWORD", userPassword);
        loginPanel.SetActive(false);
        recoverButton.SetActive(false);

        Debug.Log("Loading TestConnect Scene!");
        SceneManager.LoadScene("TestConnect");
    }

    private void OnLoginMobileSuccess(LoginResult result)
    {
        Debug.Log("Mobile Login Success!");
        loginPanel.SetActive(false);
    }

    private void OnRegisterSucess(RegisterPlayFabUserResult result)
    {
        Debug.Log("Account is now Registered!");
        //on success save to playerprefs so user doesnt have to login each time.
        PlayerPrefs.SetString("EMAIL", userEmail);
        PlayerPrefs.SetString("PASSWORD", userPassword);
        loginPanel.SetActive(false);
    }

    private void OnLoginFailure(PlayFabError error)
    {
        Debug.Log("Non-Mobile Login Failure");
        Debug.LogError("Here's some debug information:");
        Debug.LogError(error.GenerateErrorReport());
        var registerRequest = new RegisterPlayFabUserRequest { Email = userEmail, Password = userPassword, Username = userName };
        PlayFabClientAPI.RegisterPlayFabUser(registerRequest, OnRegisterSucess, OnRegisterFailure);
    }

    private void OnLoginMobileFailure(PlayFabError error)
    {
        Debug.Log("Mobile Login Failure");
        Debug.Log(error.GenerateErrorReport());
    }
    private void OnRegisterFailure(PlayFabError error)
    {
        Debug.Log("Register Failure");
        Debug.LogError(error.GenerateErrorReport());
    }

    public void GetUserEmail(string emailIn)
    {
        //Debug.Log(emailIn);
        userEmail = emailIn;
    }

    public void GetUserPassword(string passwordIn)
    {
        //Debug.Log(passwordIn);
        userPassword = passwordIn;
    }

    public void GetUserName(string userNameIn)
    {
        //Debug.Log(userNameIn);
        userName = userNameIn;
    }

    public void OnClickLogin()
    {
        var request = new LoginWithEmailAddressRequest { Email = userEmail, Password = userPassword };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
    }

    public static string returnMobileID()
    {
        string deviceID = SystemInfo.deviceUniqueIdentifier;
        return deviceID;
    }

    public void OpenAddLogin()
    {
        addLoginPanel.SetActive(true);
    }

    public void OnClickAddLogin()
    {
        var addLoginRequest = new AddUsernamePasswordRequest { Email = userEmail, Password = userPassword, Username = userName };
        PlayFabClientAPI.AddUsernamePassword(addLoginRequest, OnAddLoginSucess, OnRegisterFailure);
    }

    private void OnAddLoginSucess(AddUsernamePasswordResult result)
    {
        Debug.Log("Account credentials saved!");
        //on success save to playerprefs so user doesnt have to login each time.
        PlayerPrefs.SetString("EMAIL", userEmail);
        PlayerPrefs.SetString("PASSWORD", userPassword);
       // loginPanel.SetActive(false);
        addLoginPanel.SetActive(false);
    }

}