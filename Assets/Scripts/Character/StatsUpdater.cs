using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class StatsUpdater : MonoBehaviour
{
    [SerializeField] GameManager_BS gameManager;
    [SerializeField] Character_Base StatsOwner;
    [SerializeField] UI_Manager UImanager;

    // Player UI elements - TO DO - Make private later
    public GameObject HealthPanelGO = null;
    public TextMeshProUGUI HealthPanel = null;
    public GameObject ManaPanelGO = null;
    public TextMeshProUGUI ManaPanel = null;

    private void Awake()
    {
        setup();
    }

    private void Start()
    {

    }

    void setup()
    {
        gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager_BS>();
        StatsOwner = GetComponent<Character_Base>();
        UImanager = gameManager.GetComponent<UI_Manager>();
    }

    private void AssignStatsPanel()
    {
        if(StatsOwner._nameCharacter == "Player")
        {
            HealthPanelGO = UImanager.PlayerHealthPanelGO;
            ManaPanelGO = UImanager.PlayerManaPanelGO;
        }
        else
        {
            HealthPanelGO = UImanager.EnemyHealthPanelGO;
            ManaPanelGO = UImanager.EnemyManaPanelGO;
        }
        HealthPanel = HealthPanelGO.GetComponent<TextMeshProUGUI>();
        ManaPanel = ManaPanelGO.GetComponent<TextMeshProUGUI>();
    }

    public void HealthPanelUpdate()
    {
        if(HealthPanel == null) 
            AssignStatsPanel();
        HealthPanel.text = StatsOwner._health.ToString();
    }

    public void ManaPanelUpdate()
    {
        if (ManaPanel == null)
            AssignStatsPanel();
        ManaPanel.text = StatsOwner._mana.ToString();
    }



}
