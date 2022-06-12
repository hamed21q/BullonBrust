using System.Collections;
using System.Collections.Generic;
using System.Dynamic;
using UnityEngine;

public class Panel : MonoBehaviour
{
    [Header("Panel")]
    public string title = "Panel";
    public bool dialog = false;

    public PanelManager panelManager { get; private set; }

    public bool isActive { get; private set; } = false;

    public virtual void Awake()
    {

    }
    public virtual void Start()
    {

    }
    public virtual void Update()
    {

    }
    public virtual void FixedUpdate()
    {

    }

    public virtual void Setup(PanelManager panelManager)
    {
        this.panelManager = panelManager;
    }
    public virtual void SetActive(bool value)
    {
        isActive = value;
        gameObject.SetActive(value);
    }
    public void SetPanel()
    {
        panelManager.SetPanel(this);
    }
    public virtual void Action(params object[] obj)
    {

    }
}
