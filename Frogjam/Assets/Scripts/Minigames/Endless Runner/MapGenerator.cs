using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GeneralJTUtils;
using Random = UnityEngine.Random;

public class MapGenerator : MonoBehaviour
{
    private enum Difficulty{EASY,MEDIUM,HARD,EXTREME}
    private Difficulty Level;
    
    private enum StateOfGame{Waiting,Playing}
    private StateOfGame _stateOfGame;
    
    private readonly uint[] _seed = new uint[2500];
    public int StartSequenceLenght = 10;
    public string SeedSequence;
    
    public GameObject Background;
    public Transform BackgroundSpawnPoint;
    public float BackgroundSpawnCooldown;
    private float _lastBackgroundSpawnTime;
    
    public List<GameObject> Obstacles = new List<GameObject>();
    public Transform ObstacleSpawnPoint;
    public float ObstacleSpawnCooldown;
    private float _lastObstacleSpawnTime;

    public float DifficultyTimeIncreaseCooldown;
    private float _lastDifficultyIncreaseTime;

    private List<GameObject> _easyObstacles;
    private List<GameObject> _mediumObstacles;
    private List<GameObject> _hardObstacles;
    private List<GameObject> _extremeObstacles;


    private void Start()
    {
        GenerateMap();
        _lastBackgroundSpawnTime = Time.time;
        _lastDifficultyIncreaseTime = DifficultyTimeIncreaseCooldown;
        SetObstacleLevel();
        _stateOfGame = StateOfGame.Waiting;
    }
    
    private void GenerateMap()
    {
        for (int i = 0; i < StartSequenceLenght; i++)
        {
            SeedSequence += "0";
        }
        
        for (int i = 0; i < _seed.Length; i++)
        {
            _seed[i] = (uint)Random.Range(0,10); //0 to 9
            SeedSequence += _seed[i].ToString();
        }    
    }

    private void SetObstacleLevel()
    {
        foreach (var o in Obstacles)
        {
            var t = o.GetComponent<ObstacleObject>();
            if (t != null)
            {
                switch (t.ObstacleDifficulty)
                {
                    case ObjectDifficulty.Easy:
                        _easyObstacles.Add(o);
                        break;
                    case ObjectDifficulty.Medium:
                        _easyObstacles.Add(o);
                        break;
                    case ObjectDifficulty.Hard:
                        _easyObstacles.Add(o);
                        break;
                    case ObjectDifficulty.Extreme:
                        _easyObstacles.Add(o);
                        break;
                }
            }
        }
    }

    private void Update()
    {
        SpawnConsecutiveBackgrounds();
        
        if(_stateOfGame == StateOfGame.Waiting) return;
        IncreaseDifficultyLevelAfterTimeAmount();
        SpawnConsecutiveObstacles();
    }


    private void SpawnConsecutiveObstacles()
    {
        if(JTUtils.CheckForCooldownTime(_lastObstacleSpawnTime,ObstacleSpawnCooldown))
        {
            FindObjectDifficulty();
            _lastBackgroundSpawnTime = Time.time;
        }
    }

    private int index;
    private void FindObjectDifficulty()
    {
        switch (SeedSequence[index])
        {
            case '0':
                if(Level == Difficulty.EASY) return;
                if(Level == Difficulty.MEDIUM) return;
                if (Level == Difficulty.HARD) GenerateObstacle(ObjectDifficulty.Easy);
                if(Level == Difficulty.EXTREME) GenerateObstacle(ObjectDifficulty.Medium);
                break;
            case '1':
                if(Level == Difficulty.EASY) return;
                if(Level == Difficulty.MEDIUM) return;
                if(Level == Difficulty.HARD) GenerateObstacle(ObjectDifficulty.Easy);
                if(Level == Difficulty.EXTREME) GenerateObstacle(ObjectDifficulty.Medium);
                break;
            case '2':
                if(Level == Difficulty.EASY) return;
                if(Level == Difficulty.MEDIUM) GenerateObstacle(ObjectDifficulty.Easy);
                if(Level == Difficulty.HARD) GenerateObstacle(ObjectDifficulty.Easy);
                if(Level == Difficulty.EXTREME) GenerateObstacle(ObjectDifficulty.Hard);
                break;
            case '3':
                if(Level == Difficulty.EASY) return;
                if(Level == Difficulty.MEDIUM) GenerateObstacle(ObjectDifficulty.Easy);
                if(Level == Difficulty.HARD) GenerateObstacle(ObjectDifficulty.Medium);
                if(Level == Difficulty.EXTREME) GenerateObstacle(ObjectDifficulty.Hard);
                break;
            case '4':
                if(Level == Difficulty.EASY) GenerateObstacle(ObjectDifficulty.Easy);
                if(Level == Difficulty.MEDIUM) GenerateObstacle(ObjectDifficulty.Easy);
                if(Level == Difficulty.HARD) GenerateObstacle(ObjectDifficulty.Medium);
                if(Level == Difficulty.EXTREME) GenerateObstacle(ObjectDifficulty.Hard);
                break;
            case '5':
                if(Level == Difficulty.EASY) GenerateObstacle(ObjectDifficulty.Easy);
                if(Level == Difficulty.MEDIUM) GenerateObstacle(ObjectDifficulty.Easy);
                if(Level == Difficulty.HARD) GenerateObstacle(ObjectDifficulty.Medium);
                if(Level == Difficulty.EXTREME) GenerateObstacle(ObjectDifficulty.Hard);
                break;
            case '6':
                if(Level == Difficulty.EASY) GenerateObstacle(ObjectDifficulty.Easy);
                if(Level == Difficulty.MEDIUM) GenerateObstacle(ObjectDifficulty.Medium);
                if(Level == Difficulty.HARD) GenerateObstacle(ObjectDifficulty.Medium);
                if(Level == Difficulty.EXTREME) GenerateObstacle(ObjectDifficulty.Extreme);
                break;
            case '7':
                if(Level == Difficulty.EASY) GenerateObstacle(ObjectDifficulty.Easy);
                if(Level == Difficulty.MEDIUM) GenerateObstacle(ObjectDifficulty.Medium);
                if(Level == Difficulty.HARD) GenerateObstacle(ObjectDifficulty.Hard);
                if(Level == Difficulty.EXTREME) GenerateObstacle(ObjectDifficulty.Extreme);
                break;
            case '8':
                if(Level == Difficulty.EASY) GenerateObstacle(ObjectDifficulty.Easy);
                if(Level == Difficulty.MEDIUM) GenerateObstacle(ObjectDifficulty.Medium);
                if(Level == Difficulty.HARD) GenerateObstacle(ObjectDifficulty.Hard);
                if(Level == Difficulty.EXTREME) GenerateObstacle(ObjectDifficulty.Extreme);
                break;
            case '9':
                if(Level == Difficulty.EASY) GenerateObstacle(ObjectDifficulty.Medium);
                if(Level == Difficulty.MEDIUM) GenerateObstacle(ObjectDifficulty.Hard);
                if(Level == Difficulty.HARD) GenerateObstacle(ObjectDifficulty.Hard);
                if(Level == Difficulty.EXTREME) GenerateObstacle(ObjectDifficulty.Extreme);
                break;
        }
        index++;
    }
    
    private void GenerateObstacle(ObjectDifficulty d)
    {
        var random = new System.Random();
        int index = 0;
        switch (d)
        {
            case ObjectDifficulty.Easy:
                index = random.Next(_easyObstacles.Count);
                Instantiate(_easyObstacles[index]);
                break;
            case ObjectDifficulty.Medium:
                index = random.Next(_mediumObstacles.Count);
                Instantiate(_mediumObstacles[index]);
                break;
            case ObjectDifficulty.Hard:
                index = random.Next(_hardObstacles.Count);
                Instantiate(_hardObstacles[index]);
                break;
            case ObjectDifficulty.Extreme:
                index = random.Next(_extremeObstacles.Count);
                Instantiate(_extremeObstacles[index]);
                break;
        }
    }

    private void IncreaseDifficultyLevelAfterTimeAmount()
    {
        if (JTUtils.CheckForCooldownTime(_lastDifficultyIncreaseTime, DifficultyTimeIncreaseCooldown))
        {
            Level = Level switch
            {
                Difficulty.EASY => Difficulty.MEDIUM,
                Difficulty.MEDIUM => Difficulty.HARD,
                Difficulty.HARD => Difficulty.EXTREME,
                _ => Level
            };
            _lastDifficultyIncreaseTime += DifficultyTimeIncreaseCooldown;
            OnLevelCompleted();
        }
    }

    public void OnLevelCompleted()
    {
        _stateOfGame = StateOfGame.Waiting;
    }

    public void StartNextLevel()
    {
        _stateOfGame = StateOfGame.Playing;
    }

    private void SpawnConsecutiveBackgrounds()
    {
        if(JTUtils.CheckForCooldownTime(_lastBackgroundSpawnTime,BackgroundSpawnCooldown))
        {
            Instantiate(Background);
            _lastBackgroundSpawnTime = Time.time;
        }
    }
}
