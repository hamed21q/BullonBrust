using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Submit : MonoBehaviour
{
    protected GameManager game;
    public PanelManager panelManager;
    public List<InputField> fields;
    protected const string ERROR_MESSAGE = " field is required";
    protected const string WELLCOME_MESSAGE = "Welcome to Brust Bullon ";
    protected ResponseParser parser;
    public TextHelper resultMessage;
    protected virtual void Start()
    {
        game = GameManager.instance;
        parser = new ResponseParser(new JsonSerialization());
    }

    public virtual void SubmitForm()
    {
        ClearFilledInputsErrorMessage();
        if (ExistEmptyField())
        {
            ShowEmptyErrorMessage();
            return;
        }
    }
    
    protected void ClearFilledInputsErrorMessage()
    {
        List<InputField> filledInputs = filled();
        foreach (InputField input in filledInputs)
        {
            input.error.text = "";
        }
    }
    public bool ExistEmptyField() => emptyFeilds().Count > 0;

    protected void ShowEmptyErrorMessage()
    {

        List<InputField> empty = emptyFeilds();
        foreach (InputField input in empty)
        {
            input.error.text = "The " + input.name + ERROR_MESSAGE;
        }
    }
    public List<InputField> emptyFeilds()
    {
        List<InputField> empty = fields.FindAll(f => string.IsNullOrEmpty(f.input.text));
        return empty;
    }
    public List<InputField> filled()
    {
        List<InputField> filled = fields.FindAll(f => !string.IsNullOrEmpty(f.input.text));
        return filled;
    }
    protected InputField Find(string name)
    {
        InputField field = fields.Find(f => f.name == name);
        return field;
    }
    protected virtual WWWForm GenerateForm()
    {
        return null;
    }
    protected virtual void ShowServerErrors(ErrorFormat errorFormat)
    {
        
    }
    protected string linkErrors(string[] errors)
    {
        if (errors == null) return "";
        string error = "";
        foreach (string item in errors)
        {
            error += item + "\n";
        }
        return error;
    }
    protected void ClearInputFields()
    {
        foreach (InputField item in fields)
        {
            item.input.text = "";
        }
    }
}
