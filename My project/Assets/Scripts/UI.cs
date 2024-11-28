using UnityEngine;
using UnityEngine.UIElements;

public class UI : MonoBehaviour
{
    private HealthComponent playerHealth;
    public CombatManager combatManager;
    private Label healthLabel;
    private Label pointLabel;
    private Label waveLabel;
    private Label enemiesLabel;

    void Start()
    {
        Debug.Log("UI Start");

        playerHealth = Player.Instance.GetComponent<HealthComponent>();

        if (playerHealth == null)
        {
            Debug.LogError("Player health not found");
        }

        Debug.Log("hi");

        VisualElement root = GetComponent<UIDocument>().rootVisualElement;

        healthLabel = root.Q<Label>("Health");
        pointLabel = root.Q<Label>("Point");
        waveLabel = root.Q<Label>("Wave");
        enemiesLabel = root.Q<Label>("Enemies");

        Debug.Log("current health label text: " + healthLabel.text);
    }

    void Update()
    {
        if (healthLabel != null && playerHealth != null)
        {
            healthLabel.text = $"Health: {playerHealth.getHealth()}";
        }

        Debug.Log(combatManager.points);
        pointLabel.text = $"Points: {combatManager.points}";
        waveLabel.text = $"Wave: {combatManager.waveNumber}";
        enemiesLabel.text = $"Enemies: {combatManager.totalEnemies}";
    }
}