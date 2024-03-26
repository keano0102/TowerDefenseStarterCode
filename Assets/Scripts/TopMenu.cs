using UnityEngine;
using UnityEngine.UIElements;

public class TopMenu : MonoBehaviour
{
    public Label wavelabel;
    public Label creditslabel;
    public Label healthlabel;
    public Button startWaveButton;

    public void UpdateTopMenuLabels(int credits, int health, int currentWave)
    {
        Debug.Log("Updating top menu labels: Credits: " + credits + ", Health: " + health + ", Wave: " + currentWave);
        creditslabel.text = "Credits: " + credits;
        healthlabel.text = "Health: " + health;
        wavelabel.text = "Wave: " + currentWave;
    }
    private void Awake()
    {
        var root = GetComponent<UIDocument>().rootVisualElement;
        startWaveButton = root.Q<Button>("start-button");
        wavelabel = root.Q<Label>("wave");
        creditslabel = root.Q<Label>("credits");
        healthlabel = root.Q<Label>("health");
    }


    public void SetWaveLabel(string text)
    {
        wavelabel.text = text;
    }
    public void SetCreditsLabel(string text)
    {
        creditslabel.text = text;
    }
    public void SetHealthLabel(string text)
    {
        healthlabel.text = text;
    }

}
