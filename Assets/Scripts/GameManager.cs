using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject TowerMenu;
    private TowerMenu towerMenu;
    // Start is called before the first frame update 
    void Start()
    {
        towerMenu = TowerMenu.GetComponent<TowerMenu>();
    }
}
