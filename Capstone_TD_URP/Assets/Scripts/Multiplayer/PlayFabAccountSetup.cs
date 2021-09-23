using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PlayFab;
using PlayFab.ClientModels;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class PlayFabAccountSetup : MonoBehaviour
{
    private string userEmail;
    private string userPassword;
    private string userName;

    [SerializeField]
    private InputField userEmailInput;
    [SerializeField]
    private InputField userEmailpwd;


    public GameObject loginPanel;
    public GameObject RegisternPanel;
    //public GameObject loginPanel;

    //public GameObject addLoginPanel;

    //public GameObject recoverButton;

    void Start()
    {
        //Title Id should already be set in Editor Extensions already, but this is for redundency.
        if (string.IsNullOrEmpty(PlayFabSettings.TitleId))
        {
            PlayFabSettings.TitleId = "9FC97";
        }


        if (PlayerPrefs.HasKey("EMAIL"))
        {

            Debug.Log("PlayerPref Email: " + PlayerPrefs.GetString("EMAIL"));
            Debug.Log("PlayerPref pwd: " + PlayerPrefs.GetString("PASSWORD"));
            userEmailInput.text = PlayerPrefs.GetString("EMAIL");
            userEmailpwd.text = PlayerPrefs.GetString("PASSWORD");

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


    public static string returnMobileID()
    {
        string deviceID = SystemInfo.deviceUniqueIdentifier;
        return deviceID;
    }

    public void GetUserEmail(string emailIn)
    {
        userEmail = emailIn;
    }

    public void GetUserPassword(string passwordIn)
    {
        userPassword = passwordIn;
    }

    public void GetUserName(string userNameIn)
    {
        userName = userNameIn;
    }


    //Attempt to Login without credentials given
    public void OnClickLogin()
    {
        Debug.Log("Login Clicked.");
        var request = new LoginWithEmailAddressRequest { Email = userEmail, Password = userPassword };
        PlayFabClientAPI.LoginWithEmailAddress(request, OnLoginSuccess, OnLoginFailure);
    }

    private void OnLoginSuccess(LoginResult result)
    {
        Debug.Log("Non-Mobile Login Success!");
        PlayerPrefs.SetString("EMAIL", userEmail);
        //PlayerPrefs.SetString("USERNAME", userName);
        PlayerPrefs.SetString("PASSWORD", userPassword);
        //loginPanel.SetActive(false);
        //recoverButton.SetActive(false);

        Debug.Log("Loading TestConnect Scene!");
        SceneManager.LoadScene("TestConnect");
        

    }

    //If Login Fails then try to Register the User. 
    private void OnLoginFailure(PlayFabError error)
    {
        Debug.Log("Non-Mobile Login Failure");
        Debug.LogError("Here's some debug information:" + error.GenerateErrorReport());
        //var registerRequest = new RegisterPlayFabUserRequest { Email = userEmail, Password = userPassword, Username = userName };
        //PlayFabClientAPI.RegisterPlayFabUser(registerRequest, OnRegisterSucess, OnRegisterFailure);
    }

    private void OnLoginMobileSuccess(LoginResult result)
    {
        Debug.Log("Mobile Login Success!");
        loginPanel.SetActive(false);
    }

    private void OnLoginMobileFailure(PlayFabError error)
    {
        Debug.Log("Mobile Login Failure");
        Debug.Log(error.GenerateErrorReport());
    }

    public void OnClickRegister()
    {
        Debug.Log("OnClickRegister Called.");
        var registerRequest = new RegisterPlayFabUserRequest { Email = userEmail, Password = userPassword, Username = userName };
        //Try to register the User, if success call OnRegisterSuccess, otherwise call OnRegisterFailure
        PlayFabClientAPI.RegisterPlayFabUser(registerRequest, OnRegisterSucess, OnRegisterFailure);

    }

    private void OnRegisterSucess(RegisterPlayFabUserResult result)
    {
        Debug.Log("Account is now Registered!");
        //on success save to playerprefs so user doesnt have to login each time.
        PlayerPrefs.SetString("EMAIL", userEmail);
        //PlayerPrefs.SetString("USERNAME", userName);
        PlayerPrefs.SetString("PASSWORD", userPassword);
        //loginPanel.SetActive(false);
        SceneManager.LoadScene("TestConnect");
    }

    private void OnRegisterFailure(PlayFabError error)
    {
        Debug.Log("Register Failure");
        Debug.LogError(error.GenerateErrorReport());
    }





}
