using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldManager
{
    // #region Singleton

    // private static WorldManager _instance;

    // public static WorldManager Instance
    // {
    //     get
    //     {
    //         return _instance;
    //     }
    // }

    // private void Awake()
    // {
    //     DontDestroyOnLoad(this);

    //     if (_instance == null)
    //     {
    //         _instance = this;
    //     }
    //     else
    //     {
    //         Destroy(gameObject);
    //     }
    // }

    // #endregion

    private const string EasyType = "_0_";
    private const string DifficultType = "_1_";
    private const string BossType = "_2_";
    private const float EasyProbability = 0.5f;

    // [SerializeField] private static List<World> _worlds;

    private static int _worldIndex;
    public static int _worldStages;
    private static GameFlowController _gameFlowController;

    private static List<string> _usedStages;

    [SerializeField] private static int _maxBosses;
    [SerializeField] private static int _maxEasyStages;
    [SerializeField] private static int _maxDifficultStages;

    [SerializeField] private static string _angelStage;

    private static int _currentStageNumber;

    public static void AssignWorld(World world)
    {
        // Asignamos cosas del mundo
        _currentStageNumber = -1; // Esto puede que no haga falta.
        _worldIndex = world.index;
        _worldStages = world.stages;

        if (SaveDataController.CurrentWorld != _worldIndex)
        {
            SaveDataController.CurrentWorld = _worldIndex;
            // SaveDataController.HighestStageReached = 0;
        }

        // ? Esto se podría hacer de otra forma?
        // * Leer las escenas solo para sacar el número de escenas o algo así.
        _maxBosses = world.maxBosses;
        _maxEasyStages = world.maxEasyStages;
        _maxDifficultStages = world.maxDifficultStages;
        _angelStage = _worldIndex.ToString() + "_3_" + "0";
    }

    // public static void AssignWorld(int index) => AssignWorld(_worlds[index]);

    public static void Play()
    {
        if (_usedStages == null)
        {
            _usedStages = new List<string>();
        }
        else
        {
            _usedStages.Clear();
        }

        _currentStageNumber = -1;
        AdvanceStage();
    }

    public static void AdvanceStage()
    {
        _gameFlowController = GameObject.FindObjectOfType<GameFlowController>();
        if (_gameFlowController == null)
        {
            Debug.LogError("Error: GameFlowController no encontrado.");
            return;
        }

        if (_currentStageNumber >= _worldStages - 1)
        {
            // Llegamos al final del mundo
            Debug.Log("Se ha llegado al final del mundo.");
            _gameFlowController.LoadScene("MainMenu");
        }
        else
        {
            ++_currentStageNumber;
            if (_currentStageNumber > SaveDataController.HighestStageReached[_worldIndex])
            {
                Debug.Log("Se actualiza el highest stage: (" + _worldIndex + ", " + _currentStageNumber + ")");
                SaveDataController.HighestStageReached[_worldIndex] = _currentStageNumber;
            }

            var stageUnit = _currentStageNumber % 10;
            string stageToLoad;
            if (stageUnit == 4)
            {
                // Escena Ángel (solo debería ser una)
                stageToLoad = _angelStage;
            }
            else if (stageUnit == 9)
            {
                // Jefe (habrá varios jefes, habría ver si es en orden o aleatorio)
                stageToLoad = pickBoss();
            }
            else
            {
                // Escena normal
                stageToLoad = pickStage();
            }

            if (string.IsNullOrEmpty(stageToLoad))
            {
                Debug.LogError("Error: No hay stage que cargar.");
                return;
            }

            _usedStages.Add(stageToLoad);
            _gameFlowController.LoadScene(stageToLoad);
        }
    }

    private static string pickBoss()
    {
        bool bossRemain = _usedStages.FindAll(stage => stage.Contains(BossType)).Count < _maxBosses;
        if (bossRemain)
        {
            return GenerateRandomStage(BossType, _maxBosses);
        }
        else
        {
            Debug.LogError("Error (pickBoss - random): No hay más jefes a los que acceder.");
            return null;
        }
    }

    private static string pickStage()
    {
        bool easyRemain = _usedStages.FindAll(stage => stage.Contains(EasyType)).Count < _maxEasyStages;
        bool diffRemain = _usedStages.FindAll(stage => stage.Contains(DifficultType)).Count < _maxDifficultStages;

        if (easyRemain && diffRemain)
        {
            float prob = Random.Range(0f, 1f);
            if (prob <= EasyProbability)
            {
                return GenerateRandomStage(EasyType, _maxEasyStages);
            }
            else
            {
                return GenerateRandomStage(DifficultType, _maxDifficultStages);
            }
        }
        else if (easyRemain && !diffRemain)
        {
            Debug.LogWarning("Warning (pickStage - random): No quedan más stages difíciles, se escogerá uno fácil.");
            return GenerateRandomStage(EasyType, _maxEasyStages);
        }
        else if (!easyRemain && diffRemain)
        {
            Debug.LogWarning("Warning (pickStage - random): No quedan más stages fáciles, se escogerá uno difícil.");
            return GenerateRandomStage(DifficultType, _maxDifficultStages);
        }
        else
        {
            Debug.LogError("Error (pickStage - random): No quedan más stages que seleccionar.");
            return null;
        }
    }

    private static string GenerateRandomStage(string type, int maxNumber)
    {
        int index;
        string level;
        do
        {
            index = Random.Range(0, maxNumber);
            level = _worldIndex.ToString() + type + index.ToString();
        } while (_usedStages.Contains(level));

        Debug.Log("Stage:" + _currentStageNumber + " - Se genera el nivel: " + level);
        return level;
    }

    private void worldCompleted()
    {
        // Aquí podríamos resetear las cosas static, y se podría usar de nuevo para otro mundo.
        // Tiene más sentido que se haga al entrar al mundo.

        /*
        Dos opciones:
        1.- Haces cosas static: Implica que tienes que resetear cosas y tal, y no sé cómo funciona 
        con cosas asignadas en el inspector, es decir, si lo reseteo pero en otra escena tengo cosas 
        puestas, eso funciona?
        2.- Haces un DontDestroyOnLoad(): Implica eliminar el objeto, que es más fácil y permitiría ponerlo
        una vez y hala. Lo malo: dónde es la primera vez? - Imposible saberlo creo, pero asegurarse. Para eliminarse
        no sé si se podría hacer desde el mismo objeto o si se gestionaría desde otro lado.
        */
    }

    // private static void getScenes()
    // {
    //     for (int i = 0; i < SceneManager.sceneCountInBuildSettings; ++i)
    //     {
    //         string path = SceneUtility.GetScenePathByBuildIndex(i);
    //         if(string.IsNullOrEmpty(path))
    //         {
    //             TEXTOPRUEBA.text = "HAY ERROR ATONTAO";
    //         }
    //         string sceneName = path.Substring(0, path.Length - 6).Substring(path.LastIndexOf('/') + 1);

    //         Debug.Log("Se mira: " + sceneName);
    //         string[] nameSplit = sceneName.Split('_');

    //         int world;
    //         if (!int.TryParse(nameSplit[0], out world))
    //         {
    //             // No es un número, así que pasamos de escena.
    //             continue;
    //         }

    //         if(world != _worldInfo.index)
    //         {
    //             // No es el mundo que estamos gestionando, así que pasamos de escena
    //             continue;
    //         }

    //         int stageType;
    //         if (!int.TryParse(nameSplit[1], out stageType))
    //         {
    //             // No es un número, así que pasamos de escena.
    //             Debug.LogError("Error: Nombre de escena no válido.");
    //             continue;
    //         }

    //         int number;
    //         if (!int.TryParse(nameSplit[2], out number))
    //         {
    //             // No es un número, así que pasamos de escena.
    //             Debug.LogError("Error: Nombre de escena no válido.");
    //             continue;
    //         }

    //         switch(stageType)
    //         {
    //             case 0:
    //             _easyStages.Insert(number, sceneName);
    //             break;
    //             case 1:
    //             _difficultStages.Insert(number, sceneName);
    //             break;
    //             case 2:
    //             _bossStages.Insert(number, sceneName);
    //             break;
    //             case 3:
    //             _angelStage = sceneName;
    //             break;
    //             default:
    //             Debug.LogWarning("Warning: Tipo de escena no reconocido.");
    //             break;
    //         }
    //     }
    // }
}
