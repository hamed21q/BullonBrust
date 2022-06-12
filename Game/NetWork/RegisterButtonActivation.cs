using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RegisterButtonActivation : MonoBehaviour
{
    private void Start()
    {
        CheckIfAuthenticated();
        GameManager.instance.OnUserSignedIn += CheckIfAuthenticated;
        GameManager.instance.OnUserLogedOut += CheckIfAuthenticated;
    }
    public void CheckIfAuthenticated()
    {
        bool val = !GameManager.instance.isAuthenticated();
        this.gameObject.SetActive(val);
    }
}