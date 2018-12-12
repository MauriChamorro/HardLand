using UnityEngine;

public class Mine : MonoBehaviour
{
    private GameController gameController;
    private SphereCollider sphereCollider;
    private ParticleSystem mineExplotion;

    private float timeToExploit;

    public MineStates statesMine { get; private set; }

    private void Awake()
    {
        sphereCollider = GetComponent<SphereCollider>();
        mineExplotion = GetComponent<ParticleSystem>();
    }

    void Start()
    {
        statesMine = MineStates.Reposo;
    }

    void Update()
    {
        if (statesMine == MineStates.Contando)
        {
            if (timeToExploit <= 0)
            {
                statesMine = MineStates.Detonacion;
            }
        }

        DoActionState();
    }

    public void Initializer(GameController pGameController, float MineExploitTime)
    {
        this.gameController = pGameController;
        timeToExploit = MineExploitTime;
    }

    public void RestartMine(float pMineExploitTime)
    {
        statesMine = MineStates.Reposo;
        timeToExploit = pMineExploitTime;
    }

    private void DoActionState()
    {
        switch (statesMine)
        {
            case MineStates.Contando:
                {
                    timeToExploit -= Time.deltaTime;
                }
                break;
            case MineStates.Detonacion:
                {
                    // lanzar particulas
                    // sonido explocion
                    mineExplotion.Play();
                    
                    gameController.MineExploision(this.gameObject, mineExplotion.main.duration);
                    statesMine = MineStates.Detonada;
                    //Destroy(gameObject);
                }
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (statesMine == MineStates.Reposo)
        {
            if (other.gameObject.tag == "Player")
            {
                gameController.DetectedMine();
                statesMine = MineStates.Contando;
                sphereCollider.radius = 5f;
            }
        }
    }

    public enum MineStates
    {
        Reposo,
        Contando,
        Detonacion,
        Detonada
    }
}
