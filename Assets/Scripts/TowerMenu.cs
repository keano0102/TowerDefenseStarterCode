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
        int availableCredits = GameManager.Instance.GetCredits();
        // Disable all buttons initially
        archerButton.SetEnabled(false);
        wizardButton.SetEnabled(false);
        swordButton.SetEnabled(false);
        updateButton.SetEnabled(false);
        destroyButton.SetEnabled(false);

        if(selectedSite.Level == Enums.SiteLevel.Onbebouwd) 
        {
            archerButton.SetEnabled(availableCredits >= GameManager.Instance.GetCost(Enums.TowerType.Archer, selectedSite.Level));
            swordButton.SetEnabled(availableCredits >= GameManager.Instance.GetCost(Enums.TowerType.Sword, selectedSite.Level));
            wizardButton.SetEnabled(availableCredits >= GameManager.Instance.GetCost(Enums.TowerType.Wizard, selectedSite.Level));
            updateButton.SetEnabled(false);
            destroyButton.SetEnabled(false);
        }
        else if(selectedSite.Level < Enums.SiteLevel.Level3)
        {
            archerButton.SetEnabled(availableCredits >= GameManager.Instance.GetCost(Enums.TowerType.Archer, selectedSite.Level));
            swordButton.SetEnabled(availableCredits >= GameManager.Instance.GetCost(Enums.TowerType.Sword, selectedSite.Level));
            wizardButton.SetEnabled(availableCredits >= GameManager.Instance.GetCost(Enums.TowerType.Wizard, selectedSite.Level));
            updateButton.SetEnabled(availableCredits >= GameManager.Instance.GetCost(selectedSite.TowerType.Value, selectedSite.Level + 1));
            destroyButton.SetEnabled(true);
        }
        else if(selectedSite.Level == Enums.SiteLevel.Level3)
        {
            archerButton.SetEnabled(false);
            swordButton.SetEnabled(false);
            wizardButton.SetEnabled(false);
            updateButton.SetEnabled(false);
            destroyButton.SetEnabled(true);
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
    private void OnArcherButtonClicked()
    {
        GameManager.Instance.Build(Enums.TowerType.Archer, Enums.SiteLevel.Level1);
    }
    private void OnSwordButtonClicked()
    {
        GameManager.Instance.Build(Enums.TowerType.Sword, Enums.SiteLevel.Level1);
    }
    private void OnWizardButtonClicked()
    {
        GameManager.Instance.Build(Enums.TowerType.Wizard, Enums.SiteLevel.Level1);
    }
    private void OnUpdateButtonClicked()
    {
        if (selectedSite != null)
        {
            Enums.SiteLevel newlevel = selectedSite.Level + 1;
            GameManager.Instance.Build(selectedSite.TowerType.Value, newlevel);
            EvaluateMenu();
        }
    }
    private void OnDestroyButtonClicked()
    {
        if(selectedSite == null) 
        return;
        selectedSite.SetTower(null, Enums.SiteLevel.Onbebouwd, Enums.TowerType.None);
        EvaluateMenu();
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
            destroyButton.clicked -= OnDestroyButtonClicked; // Corrected from OnArcherButtonClicked
        }
    }
}
