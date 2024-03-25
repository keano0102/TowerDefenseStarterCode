using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    private ConstructionSite selectedSite;
    public GameObject TowerMenu;
    private TowerMenu towerMenu;
    public List<GameObject> Archers = new List<GameObject>();
    public List<GameObject> Swords = new List<GameObject>();
    public List<GameObject> Wizards = new List<GameObject>();
    private int health;
    private int credits;
    private int wave;
    private int currentWave;
    public TopMenu topMenu;
    public bool waveActive = false;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject obj = new GameObject("GameManager");
                    instance = obj.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }
    private Dictionary<Enums.TowerType, List<int>> towerPrefabCosts = new Dictionary<Enums.TowerType, List<int>>()
    {
        { Enums.TowerType.Archer, new List<int> { 50, 100, 150} },
        {Enums.TowerType.Sword, new List<int> { 75,125,175} },
        {Enums.TowerType.Wizard, new List<int> {100, 150, 200} }
    };
    public void SelectSite(ConstructionSite site)
    {
        // Onthoud de geselecteerde site
        this.selectedSite = site;
        // Geef de geselecteerde site door aan het towerMenu door SetSite aan te roepen
        towerMenu.SetSite(site);
    }
    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        towerMenu = TowerMenu.GetComponent<TowerMenu>();
    }
    public void Build(Enums.TowerType type, Enums.SiteLevel level)
    {
        // Je kunt niet bouwen als er geen site is geselecteerd
        if (selectedSite == null)
        {
            return;
        }

        // Selecteer de juiste lijst op basis van het torentype
        List<GameObject> towerList = null;
        switch (type)
        {
            case Enums.TowerType.Archer:
                towerList = Archers;
                break;
            case Enums.TowerType.Sword:
                towerList = Swords;
                break;
            case Enums.TowerType.Wizard:
                towerList = Wizards;
                break;
        }

        // Gebruik een switch met het niveau om een GameObject-toren te maken
        GameObject towerPrefab = towerList[(int)level];

        // Haal de positie van de ConstructionSite op
        Vector3 buildPosition = selectedSite.BuildPosition();

        GameObject towerInstance = Instantiate(towerPrefab, buildPosition, Quaternion.identity);
       int towerCost = GetCost(type, level);
        AddCredits(-towerCost);
        // Configureer de geselecteerde site om de toren in te stellen
        selectedSite.SetTower(towerInstance, level, type); // Voeg level en type toe als
        towerMenu.SetSite(null);
    }
    
    public void StartGame()
    {
        credits = 200; 
        health = 10;
        currentWave= 0;
        topMenu.UpdateTopMenuLabels(credits, health, currentWave + 1);
    }
    public void AttackGate(Enums.Path path)
    {
        if(path== Enums.Path.Path1 || path== Enums.Path.Path2)
        {
            health--;
            
        }
        else
        {
            Debug.LogWarning("unknown path: " + path);
        }
    }
    public void AddCredits(int amount)
    {
        credits+= amount;
        topMenu.SetCreditsLabel("Credits: " + credits);
    }
    public void RemoveCredits(int amount)
    {
        credits -= amount;
        topMenu.SetCreditsLabel("Credits: " + credits);
    }
    public int GetCredits()
    {
        return credits;
    }
    public int GetCost(Enums.TowerType type, Enums.SiteLevel level, bool selling= false)
    {
        int cost = 0;
        if(selling)
        {
            cost = towerPrefabCosts[type][(int)level] /2;
        }
        else
        {
            cost = towerPrefabCosts[type][(int)level];
        }
        return cost;
    }
    
}