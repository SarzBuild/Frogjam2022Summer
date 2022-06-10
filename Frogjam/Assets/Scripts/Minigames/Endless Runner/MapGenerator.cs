using System;
using System.Collections;
using System.Collections.Generic;
using Events;
using UnityEngine;
using GeneralJTUtils;
using Random = UnityEngine.Random;

public class MapGenerator : MonoBehaviour
{
    private static MapGenerator _instance;
    public static MapGenerator Instance {
        get
        {
            if (_instance != null) return _instance;
            
            var singleton = FindObjectOfType<MapGenerator>();
            if (singleton != null) return _instance;
            
            var go = new GameObject();
            _instance = go.AddComponent<MapGenerator>();
            return _instance;
        }
    }
    
    
    
    
    private enum Difficulty{EASY,MEDIUM,HARD}
    private Difficulty Level;
    
    public enum StateOfGame{Waiting,Playing,Dead}
    public StateOfGame StateGame;
    
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

    //private List<GameObject> _easyObstacles = new List<GameObject>();
    //private List<GameObject> _mediumObstacles = new List<GameObject>();
    //private List<GameObject> _hardObstacles = new List<GameObject>();


    public GameEventData OnLevelComplete;
    public GameEventData OnNewLevelStart;
    public GameEventData OnChallengeFailed;

    public GameObject Button;


    private void Awake()
    {
        Singleton();
    }

    private void Singleton()
    {
        if (_instance != null && _instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            _instance = this; 
        }
    }

    private void Start()
    {
        GenerateMap();
        _lastBackgroundSpawnTime = Time.time;
        _lastDifficultyIncreaseTime = DifficultyTimeIncreaseCooldown;
        //SetObstacleLevel();
        StateGame = StateOfGame.Waiting;
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

    /*private void SetObstacleLevel()
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
                }
            }
        }
    }*/

    private void Update()
    {
        if (StateGame == StateOfGame.Waiting)
        {
            JTUtils.ToggleObjects(new List<GameObject>(){Button}, true);
        }
        else
        {
            JTUtils.ToggleObjects(new List<GameObject>(){Button}, false);
        }
        
        
        if(StateGame == StateOfGame.Dead) return;
        //SpawnConsecutiveBackgrounds();
        
        if(StateGame == StateOfGame.Waiting) return;
        IncreaseDifficultyLevelAfterTimeAmount();
        SpawnConsecutiveObstacles();
    }

    private void FixedUpdate()
    {
        if(StateGame == StateOfGame.Dead) return;
        SpawnConsecutiveBackgrounds();
    }

    private float times;
    private void SpawnConsecutiveBackgrounds()
    {
        if(JTUtils.CheckForCooldownTime(_lastBackgroundSpawnTime,BackgroundSpawnCooldown))
        {
            Instantiate(Background, BackgroundSpawnPoint.position,Quaternion.identity);
            _lastBackgroundSpawnTime = JTUtils.GetTime();
        }
    }

    private void SpawnConsecutiveObstacles()
    {
        if(JTUtils.CheckForCooldownTime(_lastObstacleSpawnTime,ObstacleSpawnCooldown))
        {
            //FindObjectDifficulty();
            GenerateRandomItem();
            _lastObstacleSpawnTime = JTUtils.GetTime();
        }
    }

    private void GenerateRandomItem()
    {
        var random = new System.Random();
        int index = 0;
        index = random.Next(Obstacles.Count);
        Instantiate(Obstacles[index],ObstacleSpawnPoint.position,Quaternion.identity);
    }

    private int index;
    /*private void FindObjectDifficulty()
    {
        index++;
        Debug.Log(index);
        switch (SeedSequence[index])
        {
            
            case '0':
                if(Level == Difficulty.EASY) return;
                if(Level == Difficulty.MEDIUM) return;
                if (Level == Difficulty.HARD) GenerateObstacle(ObjectDifficulty.Easy);
                break;
            case '1':
                if(Level == Difficulty.EASY) return;
                if(Level == Difficulty.MEDIUM) return;
                if(Level == Difficulty.HARD) GenerateObstacle(ObjectDifficulty.Easy);
                break;
            case '2':
                if(Level == Difficulty.EASY) return;
                if(Level == Difficulty.MEDIUM) GenerateObstacle(ObjectDifficulty.Easy);
                if(Level == Difficulty.HARD) GenerateObstacle(ObjectDifficulty.Easy);
                break;
            case '3':
                if(Level == Difficulty.EASY) return;
                if(Level == Difficulty.MEDIUM) GenerateObstacle(ObjectDifficulty.Easy);
                if(Level == Difficulty.HARD) GenerateObstacle(ObjectDifficulty.Medium);
                break;
            case '4':
                if(Level == Difficulty.EASY) GenerateObstacle(ObjectDifficulty.Easy);
                if(Level == Difficulty.MEDIUM) GenerateObstacle(ObjectDifficulty.Easy);
                if(Level == Difficulty.HARD) GenerateObstacle(ObjectDifficulty.Medium);
                break;
            case '5':
                if(Level == Difficulty.EASY) GenerateObstacle(ObjectDifficulty.Easy);
                if(Level == Difficulty.MEDIUM) GenerateObstacle(ObjectDifficulty.Easy);
                if(Level == Difficulty.HARD) GenerateObstacle(ObjectDifficulty.Medium);
                break;
            case '6':
                if(Level == Difficulty.EASY) GenerateObstacle(ObjectDifficulty.Easy);
                if(Level == Difficulty.MEDIUM) GenerateObstacle(ObjectDifficulty.Medium);
                if(Level == Difficulty.HARD) GenerateObstacle(ObjectDifficulty.Medium);
                break;
            case '7':
                if(Level == Difficulty.EASY) GenerateObstacle(ObjectDifficulty.Easy);
                if(Level == Difficulty.MEDIUM) GenerateObstacle(ObjectDifficulty.Medium);
                if(Level == Difficulty.HARD) GenerateObstacle(ObjectDifficulty.Hard);
                break;
            case '8':
                if(Level == Difficulty.EASY) GenerateObstacle(ObjectDifficulty.Easy);
                if(Level == Difficulty.MEDIUM) GenerateObstacle(ObjectDifficulty.Medium);
                if(Level == Difficulty.HARD) GenerateObstacle(ObjectDifficulty.Hard);
                break;
            case '9':
                if(Level == Difficulty.EASY) GenerateObstacle(ObjectDifficulty.Medium);
                if(Level == Difficulty.MEDIUM) GenerateObstacle(ObjectDifficulty.Hard);
                if(Level == Difficulty.HARD) GenerateObstacle(ObjectDifficulty.Hard);
                break;
        }
    }*/
    
    /*private void GenerateObstacle(ObjectDifficulty d)
    {
        var random = new System.Random();
        int index = 0;
        switch (d)
        {
            case ObjectDifficulty.Easy:
                index = random.Next(_easyObstacles.Count);
                Instantiate(_easyObstacles[index],ObstacleSpawnPoint.position,Quaternion.identity);
                break;
            case ObjectDifficulty.Medium:
                index = random.Next(_mediumObstacles.Count);
                Instantiate(_mediumObstacles[index],ObstacleSpawnPoint.position,Quaternion.identity);
                break;
            case ObjectDifficulty.Hard:
                index = random.Next(_hardObstacles.Count);
                Instantiate(_hardObstacles[index],ObstacleSpawnPoint.position,Quaternion.identity);
                break;
        }
    }*/

    private void IncreaseDifficultyLevelAfterTimeAmount()
    {
        if (JTUtils.CheckForCooldownTime(_lastDifficultyIncreaseTime, DifficultyTimeIncreaseCooldown))
        {
            Level = Level switch
            {
                Difficulty.EASY => Difficulty.MEDIUM,
                Difficulty.MEDIUM => Difficulty.HARD,
                _ => Level
            };
            _lastDifficultyIncreaseTime += DifficultyTimeIncreaseCooldown;
            OnLevelCompleted();
        }
    }

    public void OnLevelCompleted()
    {
        StateGame = StateOfGame.Waiting;
        OnLevelComplete.Raise();
    }

    public void StartNextLevel()
    {
        StateGame = StateOfGame.Playing;
        OnNewLevelStart.Raise();
    }

    public void OnDeath()
    {
        StateGame = StateOfGame.Dead;
        OnChallengeFailed.Raise();
    }

    
}
