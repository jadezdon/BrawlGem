using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private static LevelManager instance;

    public static LevelManager Instance { get { return instance; } }

    [SerializeField] Transform playerSpawnPoint;
    [SerializeField] float spawnDelay = 2f;
    [SerializeField] Transform spawnParticlesPrefab;
    [SerializeField] LevelComplete levelComplete;

    GameObject playerPrefab;

    void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this.gameObject);
        else
            instance = GameObject.FindGameObjectWithTag("LM").GetComponent<LevelManager>();

        playerPrefab = GameController.instance.currentCharacter.Prefab;
    }

    void Start()
    {
        if (SceneManager.GetActiveScene().buildIndex > 0) instance.SpawnPlayer(playerPrefab);
    }

    public void KillPlayer(Player player)
    {
        player.gameObject.SetActive(false);
        instance.StartCoroutine(instance.RespawnPlayer(player));
    }

    public IEnumerator RespawnPlayer(Player player)
    {
        // TODO add spawn sound

        yield return new WaitForSeconds(spawnDelay);

        player.gameObject.transform.position = playerSpawnPoint.position;
        player.gameObject.SetActive(true);

        // play particles system
        Transform clone = Instantiate(spawnParticlesPrefab, playerSpawnPoint.position, playerSpawnPoint.rotation);
        Destroy(clone.gameObject, 3f);
    }

    public void SpawnPlayer(GameObject _playerPrefab)
    {
        Instantiate(_playerPrefab, playerSpawnPoint.position, playerSpawnPoint.rotation);

        // play particles system
        Transform clone = Instantiate(spawnParticlesPrefab, playerSpawnPoint.position, playerSpawnPoint.rotation);
        Destroy(clone.gameObject, 3f);
    }

    public void CompleteLevel()
    {
        levelComplete.ShowLevelCompletePanel();
        GameController.instance.gameData.TotalGemCount += GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().stats.GemCount;
    }
}
