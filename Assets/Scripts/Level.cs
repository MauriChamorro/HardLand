public class Level
{
    public int NumLevel { get; private set; }
    public int MaxLevel { get; private set; }
    public int CantMaxMinesSpawn { get; private set; }
    public int CantMinesSpawn { get; private set; }
    public int CantMaxChipSpawn { get; private set; }
    public int CantChipSpawn { get; private set; }
    public int CurrentChipCant { get; private set; }
    public int Lifes { get; private set; }
    public float MineExplotionTime { get; private set; }
    public float ClockTime { get; private set; }

    public Level()
    {
        NumLevel = 1;
        MaxLevel = 3;
        CantMaxMinesSpawn = GeneralGameValues.CantMaxMines;
        CantMinesSpawn = GeneralGameValues.CantMines;
        CantMaxChipSpawn = GeneralGameValues.CantMaxChip;
        CantChipSpawn = GeneralGameValues.CantChip;
        CurrentChipCant = CantChipSpawn;
        MineExplotionTime = GeneralGameValues.TimeToExploitMine;
        Lifes = GeneralGameValues.CantLifes;
        ClockTime = 0;
    }

    public void LevelUp()
    {
        NumLevel++;
        MineExplotionTime -= MineExplotionTime * 5 / 100;
        CantMinesSpawn += CantMinesSpawn * 50 / 100;
        CantChipSpawn += CantChipSpawn * 50 / 100;
        CurrentChipCant = CantChipSpawn;
        ClockTime = 0;
    }

    public bool GameWon()
    {
        return NumLevel > MaxLevel;
    }

    public void ChipPickedUp()
    {
        CurrentChipCant--;
    }

    public bool LevelWon()
    {
        return CurrentChipCant == 0;
    }
}
