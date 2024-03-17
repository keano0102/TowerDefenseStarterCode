using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConstructionSite
{
    public Vector3Int TilePosition { get; private set; }
    public Vector3 WorldPosition { get; private set; }
    public Enums.SiteLevel Level { get; private set; }
    public Enums.TowerType TowerType { get; private set; }

    private GameObject tower;

    public ConstructionSite(Vector3Int tilePosition, Vector3 worldPosition, Enums.SiteLevel level, Enums.TowerType towerType)
    {
        TilePosition = tilePosition;
        // Pas de y-waarde van de wereldpositie aan
        WorldPosition = new Vector3(worldPosition.x, worldPosition.y + 0.5f, worldPosition.z);
        Level = level;
        TowerType = towerType;
        tower = null;
    }
    public void SetTower(GameObject newTower, Enums.SiteLevel newLevel, Enums.TowerType newType)
    {
        // Controleer eerst of er al een bestaande toren op de site staat
        if (tower != null)
        {
            // Verwijder de huidige toren voordat je de nieuwe toren toewijst
            DestroyTower();
        }

        // Wijs de nieuwe toren toe
        tower = newTower;
        Level = newLevel;
        TowerType = newType;
    }
    private void DestroyTower()
    {
        // Controleer eerst of er een toren is om te vernietigen
        if (tower != null)
        {
            // Vernietig het gameobject van de toren
            GameObject.Destroy(tower);
            // Stel de toren in op null, aangezien er nu geen toren meer is
            tower = null;
        }
    }
}
