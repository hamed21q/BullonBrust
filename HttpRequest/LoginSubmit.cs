using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoginSubmit : Submit
{
    public override async void SubmitForm()
    {
        base.SubmitForm();

        Response response = await game.LoginUser(GenerateForm());
        if (response.responseCode == 201)
        {
            UserFormat userFormat = parser.parse<UserFormat>(response.json);
            game.SaveNewUser(userFormat);
            resultMessage.text = WELLCOME_MESSAGE + userFormat.user.username;
            ClearInputFields();
            game.OnUserSignedIn();
            panelManager.SetPanel("MainMenu");
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
        form.AddField("email", Find("Email").input.text);
        form.AddField("password", Find("Password").input.text);
        return form;
    }
    protected override void ShowServerErrors(ErrorFormat errorFormat)
    {
        resultMessage.text = errorFormat.message;
    }
}
