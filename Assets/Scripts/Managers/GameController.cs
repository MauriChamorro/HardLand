using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameController : MonoBehaviour
{
    public CanvasManager canvasManager;
    public SoundManager soundManager;

    public GameObject MenuPanel;
    public Text[] textButtons;
    public GameObject iselect;

    public PlayerMovement playerMovement;
    public PlayerStats playerStats;

    public GameObject mineParent;
    public GameObject minePrefab;
    public GameObject chipPrefab;
    public GameObject mineZone;

    public Bola bola;
    public MineDetector mineDetector;

    public Level currentLevel;
    public Text gameOverText;
    public Text levelText;

    List<GameObject> chips;
    List<GameObject> mines;

    #region MonoBehaviour

    private void Awake()
    {
        currentLevel = new Level();

        GamePreInitializer();
    }

    private void Start()
    {
        soundManager = SoundManager.Instance;

        canvasManager.Initilizer(currentLevel);

        playerStats.Initializaer(this);

        GameInitializer();

        StartGame();
    }

    void Update()
    {
        if (GeneralGameValues.Playing)
        {
            if (Input.GetButtonDown("Cancel"))
            {
                PauseGame();
            }

            if (!playerStats.IsAlive())
            {
                GameOver(false);
                return;
            }

            if (currentLevel.LevelWon())
            {
                soundManager.PlaySFXClipName("level-up");
                LevelUp();
            }
        }
    }

    #endregion

    #region Own Methods

    public void StartGame()
    {
        GeneralGameValues.Playing = true;
        bola.Resume(true);
        CheckLevelState();
    }

    public void PauseGame()
    {
        GeneralGameValues.Paused = GeneralGameValues.Paused ? false : true;
        soundManager.PlaySFXClipName("back-menu");

        MenuPanel.SetActive(MenuPanel.gameObject.activeSelf ? false : true);
        iselect.SetActive(false);
    }

    public void RestartGame()
    {
        GeneralGameValues.Playing = false;
        GeneralGameValues.Paused = false;

        GameInitializer();

        currentLevel = new Level();

        ActiveChipsPool(currentLevel.CantChipSpawn);

        ActiveMinesPool(currentLevel.CantMinesSpawn);

        canvasManager.Initilizer(currentLevel);

        StartGame();

        GeneralGameValues.Paused = GeneralGameValues.Paused ? false : true;
        PauseGame();

        playerStats.RestartStats();
    }

    public void LevelUp()
    {
        currentLevel.LevelUp();

        if (currentLevel.GameWon())
        {
            GameOver(true);
            return;
        }

        CheckLevelState();

        GamePreInitializer();

        GameInitializer();

        StartGame();
    }

    private void CheckLevelState()
    {
        string auxText = "";

        switch (currentLevel.NumLevel)
        {
            case 1:
                auxText = "NIVEL 1";
                break;
            case 2:
                auxText = "NIVEL 2";
                break;
            case 3:
                auxText = "NIVEL 3";
                break;
            default:
                auxText = "Error en CheckLevelState";
                break;
        }

        //levelText.text = "<color=#DDF33BFF>" + auxText + "</color>";
        levelText.text = auxText;

        canvasManager.ActivateTriggerAnimator("LevelUp");
    }

    private void GamePreInitializer()
    {
        GeneralGameValues.Playing = false;
        GeneralGameValues.Paused = false;

        GenerateChips();
        ActiveChipsPool(currentLevel.CantChipSpawn);

        GenerateMines();
        ActiveMinesPool(currentLevel.CantMinesSpawn);
    }

    private void GameInitializer()
    {
        bola.Resume(false);

        playerMovement.Revive();

        Color outColor;
        ColorUtility.TryParseHtmlString("#38302D8F", out outColor);
        levelText.GetComponentInParent<Image>().color = outColor;

    }
    #endregion

    #region Events
    public void PointerEnterText(string pNameText)
    {
        soundManager.PlaySFXClipName("PointerEnterText");

        Text auxText = textButtons.FirstOrDefault(t => t.name == pNameText);
        iselect.SetActive(true);

        iselect.transform.position =
            new Vector3(auxText.transform.position.x - auxText.transform.position.x * 0.1f,
            auxText.transform.position.y,
            auxText.transform.position.z);
    }

    public void PointerExitText(string pNameText)
    {
        iselect.SetActive(false);
    }

    #endregion

    #region Operations

    private void GenerateChips()
    {
        chips = new List<GameObject>();

        GameObject chipsParent = new GameObject()
        {
            name = "ChipsParent"
        };

        for (int i = 0; i < currentLevel.CantMaxChipSpawn; i++)
        {
            GameObject chipAux = Instantiate(chipPrefab, chipsParent.transform);
            chipAux.SetActive(false);
            chipAux.name += i;
            chips.Add(chipAux);
        }

    }

    private void ActiveChipsPool(int pCantSpawm)
    {
        Bounds rectZone = mineZone.GetComponent<MeshCollider>().bounds;

        for (int i = 0; i < pCantSpawm; i++)
        {
            chips[i].gameObject.SetActive(true);

            chips[i].transform.position = new Vector3(
                    Random.Range(rectZone.min.x, rectZone.max.x),
                    chips[i].transform.position.y,
                    Random.Range(rectZone.min.z, rectZone.max.z));
        }
    }

    private void GenerateMines()
    {
        mines = new List<GameObject>();

        mineParent = new GameObject()
        {
            name = "MinesParent"
        };

        for (int i = 0; i < currentLevel.CantMaxMinesSpawn; i++)
        {
            GameObject mineAux = Instantiate(minePrefab, mineParent.transform);
            mineAux.SetActive(false);
            mineAux.name += i;
            mineAux.GetComponent<Mine>().Initializer(this, currentLevel.MineExplotionTime);

            mines.Add(mineAux);
        }
    }

    private void ActiveMinesPool(int pCantSpawm)
    {
        Bounds rectZone = mineZone.GetComponent<MeshCollider>().bounds;

        for (int i = 0; i < pCantSpawm; i++)
        {
            mines[i].SetActive(true);

            mines[i].transform.position =
                new Vector3(
                    Random.Range(rectZone.min.x, rectZone.max.x),
                    rectZone.max.y,
                    Random.Range(rectZone.min.z, rectZone.max.z));
        }
    }

    #region Ddetected Objects

    public void DetectedMine()
    {
        soundManager.PlaySFXClipName("clock-ticking");
    }

    public void MineExploision(GameObject pMine, float pParticlesTime)
    {
        soundManager.PlaySFXClipName("mine-explotion");
        StartCoroutine(DesactiveMine(pMine, pParticlesTime));
    }

    IEnumerator DesactiveMine(GameObject pMine, float pWaitTime)
    {
        yield return new WaitForSeconds(1f);
        pMine.SetActive(false);
    }

    public void MineExploit(Collider other)
    {
        if (other.gameObject.GetComponent<Mine>().statesMine == Mine.MineStates.Detonada)
        {
            playerStats.LoseLifes(1, canvasManager);
        }
    }

    public void PlayerCollisionWall()
    {
        playerStats.LoseLifes(1, canvasManager);
    }

    public void PlayerCollisionBall()
    {
        playerStats.LoseLifes(2, canvasManager);
    }

    public void PickUpChip(GameObject pChip)
    {
        soundManager.PlaySFXClipName("chip-pickedup");
        canvasManager.ChipPickedUp();
        pChip.SetActive(false);
    }

    #endregion

    public void BackToMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void GameOver(bool pWon)
    {
        GeneralGameValues.Playing = false;

        PauseGame();

        Color colorOut = Color.white;
        if (pWon)
        {
            soundManager.PlaySFXClipName("win-sound");

            ColorUtility.TryParseHtmlString("#37FF118F", out colorOut);

            levelText.GetComponentInParent<Image>().color = colorOut;

            levelText.text = "Escapaste";
        }
        else
        {
            soundManager.PlaySFXClipName("lose-sound");
            levelText.text = "Atrapado";

            ColorUtility.TryParseHtmlString("#FF3D118F", out colorOut);

            levelText.GetComponentInParent<Image>().color = colorOut;
        }

        canvasManager.ActivateTriggerAnimator("LevelUp");
        //gameOverText.gameObject.SetActive(true);
        playerMovement.Death();
    }

    #endregion
}
