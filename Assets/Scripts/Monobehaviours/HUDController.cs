using System.Collections;
using System.Collections.Generic;
using Monobehaviours.Characters;
using TMPro;
using UnityEngine;

public class HUDController : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI currentPlayerHP;
    [SerializeField] private TextMeshProUGUI activeFoodType;
    [SerializeField] private TextMeshProUGUI activeAmmoCount;
    [SerializeField] private TextMeshProUGUI activeWaveCount;
    [SerializeField] private TextMeshProUGUI currentScore;
    
    [SerializeField] private GameObject player;
    [SerializeField] private WaveManager waveManager;
    [SerializeField] private GameObject spawnManager;
    private HP playerHealth;
    private PlayerController playerController;
    private AmmoInventory ammoInventory;
    private Wave wave;
    private SpawnController spawnController;
    void Awake()
    {
        playerHealth = player.GetComponent<HP>();
        playerController = player.GetComponent<PlayerController>();
        ammoInventory = player.GetComponent<AmmoInventory>();
        wave = waveManager.GetComponent<Wave>();
        spawnController = spawnManager.GetComponent<SpawnController>();

    }

    // Update is called once per frame
    void Update()
    {
        currentPlayerHP.text = playerHealth.GetCurrentHP();
        activeFoodType.text = playerController.GetActiveFoodTypeToThrow();
        activeAmmoCount.text = ammoInventory.GetCurrentAmmoCount(activeFoodType.text);
        activeWaveCount.text = wave.GetWaveCount();
        currentScore.text = spawnController.GetScore();
    }
}
