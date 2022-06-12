using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PanelManager : Panel
{
    public int startPanel = 0;
    public List<Panel> panels = new List<Panel>();

    private Panel _currentPanel;

    public static PanelManager Instance
    {
        get
        {
            return FindObjectOfType<PanelManager>();
        }
    }

    public Canvas canvas
    {
        get
        {
            return GetComponent<Canvas>();
        }
    }

    public int currentPanel
    {
        get
        {
            return panels.IndexOf(_currentPanel);
        }
    }
    public string currentPanelTitle
    {
        get
        {
            return panels[currentPanel].title;
        }
    }

    public override void Awake()
    {
        base.Awake();
        foreach (var panel in panels)
            panel.Setup(this);
        if (startPanel >= 0)
            SetPanel(startPanel);
        Application.targetFrameRate = 60;
    }

    public override void SetActive(bool value)
    {
        base.SetActive(value);
        if (!value)
            SetPanel(-1);
        else
            SetPanel(currentPanel);
    }

    public virtual void SetPanel(int index)
    {
        if (index >= 0 && panels.Count > index && panels[index].dialog)
        {
            panels[index].SetActive(true);
            panels[index].transform.SetAsLastSibling();
        }
        else
        {
            for (int i = 0; i < panels.Count; i++)
            {
                if (i == index)
                {
                    panels[i].transform.SetAsFirstSibling();
                    _currentPanel = panels[i];
                }
                panels[i].SetActive(i == index);
            }
        }
    }
    public void SetPanel(string title)
    {
        SetPanel(IndexOf(GetPanel(title)));
    }
    public void SetPanel(Panel p)
    {
        SetPanel(IndexOf(p));
    }
    public void ChangePanel(int change)
    {
        SetPanel((int)Mathf.Repeat(currentPanel + change, panels.Count));
    }

    public Panel GetPanel(string title)
    {
        for (int i = 0; i < panels.Count; i++)
            if (panels[i].title == title)
                return panels[i];
        return null;
    }
    public int IndexOf(Panel p)
    {
        return panels.IndexOf(p);
    }

    public void LoadScene(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void NextPanel()
    {
        SetPanel((int)Mathf.Repeat(currentPanel + 1, panels.Count));
    }
}
