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
}