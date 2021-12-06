using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InGameMenuController : MonoBehaviour
{
    private static InGameMenuController instance;
    public static InGameMenuController Instance { get { return instance; } }

    [SerializeField] DetailsPanelScript detailsPanel;
    [SerializeField] public RuneDetailsPanelScript runeDetailsPanel;
    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        detailsPanel.UpdateDetails();
    }
}
