using UnityEngine;

public class PlayerStats : MonoBehaviour
{
    private GameController gameController;

    private float timeToLoseLife;

    private int lifes;
    private bool losedLife;

    private void Awake()
    {
        losedLife = false;
    }

    private void Update()
    {
        if (losedLife)
        {
            timeToLoseLife -= Time.deltaTime;

            if (timeToLoseLife <= 0)
            {
                losedLife = false;
            }
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Wall")
        {
            gameController.PlayerCollisionWall();
        }

        if (collision.gameObject.tag == "Ball")
        {
            gameController.PlayerCollisionBall();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Chip")
        {
            gameController.PickUpChip(other.gameObject);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Mine")
        {
            gameController.MineExploit(other);
        }
    }

    public void Initializaer(GameController pGameController)
    {
        gameController = pGameController;
        lifes = pGameController.currentLevel.Lifes;
    }

    public void LoseLifes(int pCant, CanvasManager pCanvasManager)
    {
        if (!losedLife)
        {
            if (lifes != 1)
                gameController.soundManager.PlaySFXClipName("hit");
            else
                gameController.soundManager.PlaySFXClipName("death");


            pCanvasManager.LoseLifes(lifes, pCant);

            lifes -= pCant;

            timeToLoseLife = 1.02f;

            losedLife = true;
        }
    }

    public bool IsAlive()
    {
        return lifes > 0;
    }
}
