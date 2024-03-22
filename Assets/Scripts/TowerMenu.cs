using UnityEngine;
using UnityEngine.UIElements;
public class TowerMenu : MonoBehaviour
{
    private ConstructionSite selectedSite;
    private Button archerButton;
    private Button swordButton;
    private Button wizardButton;
    private Button updateButton;
    private Button destroyButton;
    private VisualElement root;
    public void EvaluateMenu()
    {
        if (selectedSite == null)
        {
            // If selectedSite is null, return without enabling any buttons
            return;
        }

        // Access the site level property of selectedSite
        int siteLevel = (int)selectedSite.Level;

        // Disable all buttons initially
        archerButton.SetEnabled(false);
        wizardButton.SetEnabled(false);
        swordButton.SetEnabled(false);
        updateButton.SetEnabled(false);
        destroyButton.SetEnabled(false);

        // Enable buttons based on site level using a switch statement
        switch (siteLevel)
        {
            case 0:
                // For site level 0, enable archer, wizard, and sword buttons
                archerButton.SetEnabled(true);
                wizardButton.SetEnabled(true);
                swordButton.SetEnabled(true);
                break;
            case 1:
            case 2:
                // For site levels 1 and 2, enable update and destroy buttons
                updateButton.SetEnabled(true);
                destroyButton.SetEnabled(true);
                break;
            case 3:
                // For site level 3, only enable the destroy button
                destroyButton.SetEnabled(true);
                break;
            default:
                // Handle any other site levels if necessary
                break;
        }
    }
    public void SetSite(ConstructionSite site)
    {
        // Assign the site to the selectedSite variable
        selectedSite = site;

        if (selectedSite == null)
        {
            // If the selected site is null, hide the menu and return
            root.visible = false;
            return;
        }
        else
        {
            // If the selected site is not null, make sure the menu is visible
            root.visible = true;

            // Call the EvaluateMenu method to update button visibility
            EvaluateMenu();
        }
    }
    void Start()
    {
        root = GetComponent<UIDocument>().rootVisualElement;

        archerButton = root.Q<Button>("archer-tower");
        swordButton = root.Q<Button>("sword-tower");
        wizardButton = root.Q<Button>("wizard-tower");
        updateButton = root.Q<Button>("button-upgrade");
        destroyButton = root.Q<Button>("button-destroy");

        if (archerButton != null)
        {
            archerButton.clicked += OnArcherButtonClicked;
        }

        if (swordButton != null)
        {
            swordButton.clicked += OnSwordButtonClicked;
        }

        if (wizardButton != null)
        {
            wizardButton.clicked += OnWizardButtonClicked;
        }

        if (updateButton != null)
        {
            updateButton.clicked += OnUpdateButtonClicked;
        }

        if (destroyButton != null)
        {
            destroyButton.clicked += OnDestroyButtonClicked;
        }

        root.visible = false;
    }

    public void ToggleVisibility()
    {
        root.visible = !root.visible;
    }

    private void OnArcherButtonClicked()
    {
        GameManager.Instance.Build(Enums.TowerType.Archer, Enums.SiteLevel.Onbebouwd);
    }
    private void OnSwordButtonClicked()
    {
        GameManager.Instance.Build(Enums.TowerType.Sword, Enums.SiteLevel.Onbebouwd);
    }
    private void OnWizardButtonClicked()
    {
        GameManager.Instance.Build(Enums.TowerType.Wizard, Enums.SiteLevel.Onbebouwd);
    }
    private void OnUpdateButtonClicked()
    {
        if (selectedSite == null)
            return;
        if (selectedSite.TowerType == null)
        {
            Debug.LogWarning("Cannot upgrade site because no tower has been build ");
            return;
        }
        Enums.SiteLevel newLevel = selectedSite.Level + 1;
        GameManager.Instance.Build(selectedSite.TowerType.Value, newLevel);
        EvaluateMenu();
    }
    private void OnDestroyButtonClicked()
    {
        if(selectedSite == null) 
        return;
        selectedSite.SetTower(null, Enums.SiteLevel.Onbebouwd, Enums.TowerType.None);
        EvaluateMenu() ;
    }


    private void OnDestroy()
    {
        if (archerButton != null)
        {
            archerButton.clicked -= OnArcherButtonClicked;
        }
        if (swordButton != null)
        {
            swordButton.clicked -= OnSwordButtonClicked;
        }
        if (wizardButton != null)
        {
            wizardButton.clicked -= OnWizardButtonClicked;
        }
        if (updateButton != null)
        {
            updateButton.clicked -= OnUpdateButtonClicked;
        }
        if (destroyButton != null)
        {
            destroyButton.clicked -= OnArcherButtonClicked;
        }
    }
}
