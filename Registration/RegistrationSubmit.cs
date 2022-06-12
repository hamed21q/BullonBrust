using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RegistrationSubmit : Submit
{
    protected override void Start()
    {
        base.Start();
    }
    public override async void SubmitForm()
    {
        base.SubmitForm();
        Response response = await game.RegisterUser(GenerateForm());
        if (response.responseCode == 201)
        {
            UserFormat userFormat = parser.parse<UserFormat>(response.json);
            game.SaveNewUser(userFormat);
            resultMessage.text = WELLCOME_MESSAGE + userFormat.user.username;
            ClearInputFields();
            panelManager.SetPanel("MainMenu");
            game.OnUserSignedIn?.Invoke();
        }
        else if (response.responseCode == 422)
        {
            ErrorFormat errors = parser.parse<ErrorFormat>(response.json);
            ShowServerErrors(errors);
        }
        else
        {
            resultMessage.text = "no available internt conecction";
        }
    }
    protected override WWWForm GenerateForm()
    {
        WWWForm form = new WWWForm();
        form.AddField("username", Find("Username").input.text);
        form.AddField("email", Find("Email").input.text);
        form.AddField("password", Find("Password").input.text);
        form.AddField("password_confirmation", Find("Password_confirmation").input.text);
        return form;
    }
    protected override void ShowServerErrors(ErrorFormat errorFormat)
    {
        resultMessage.text = errorFormat.message;
        Find("Username").error.text = linkErrors(errorFormat.errors.Username);
        Find("Email").error.text = linkErrors(errorFormat.errors.Email);
        Find("Password").error.text = linkErrors(errorFormat.errors.Password);
    }
}