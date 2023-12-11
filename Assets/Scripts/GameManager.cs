using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public int world { get; private set; } // получить мир
    public int stage { get; private set; } // получить уровень
    public int lives { get; private set; } // получить жизни

    public int coins { get; private set; } // получить монеты

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }
    //Этот код представляет собой простую реализацию шаблона "Singleton" для класса GameManager в Unity. 
    //Шаблон "Singleton" обеспечивает, что у класса существует только один экземпляр, и предоставляет глобальную точку доступа к этому экземпляру.

    //Давайте разберемся с кодом пошагово:

    //public static GameManager Instance { get; private set; }: Это статическое свойство, 
    //которое предоставляет доступ к единственному экземпляру GameManager.
    //Свойство доступно для чтения извне класса, но доступ к установке значения имеет только сам класс(private set).

    //private void Awake(): Этот метод вызывается Unity при первом активировании объекта.Здесь реализована проверка, 
    //существует ли уже экземпляр GameManager.Если экземпляр существует, 
    //то текущий объект уничтожается (DestroyImmediate(gameObject)), чтобы не было возможности создать второй экземпляр.
    //Если экземпляр не существует, то текущий объект становится экземпляром GameManager(Instance = this;), 
    //и его сохранение между сценами обеспечивается с помощью DontDestroyOnLoad(gameObject).

    //private void OnDestroy(): Этот метод вызывается Unity при уничтожении объекта.Здесь проверяется, 
    //уничтожается ли текущий объект(Instance == this). Если да, 
    //то свойство Instance устанавливается в null, что позволяет создать новый экземпляр GameManager в будущем.

    //Таким образом, благодаря этому коду, вы создаете глобальную точку доступа к объекту GameManager, 
    //который существует в единственном экземпляре и сохраняется между сценами.

    private void Start()
    {
        Application.targetFrameRate = 60;
        NewGame();
    }

    private void NewGame()
    {
        lives = 3;
        coins = 0;
        LoadLevel(1, 1);
    }

    public void GameOver()
    {
        NewGame();
    }
    private void LoadLevel(int world, int stage)
    {
        this.world = world;
        this.stage = stage;

        SceneManager.LoadScene($"{world}-{stage}");
    }

    private void NextLevel()
    {
        if (stage >= 3)
        {
            LoadLevel(world + 1, 1);
        }
        else
        {
            LoadLevel(world, stage + 1);
        }

    }

    public void ResetLevel()
    {
        lives--;
        if (lives > 0)
        {
            LoadLevel(world, stage);
        }
        else
        {
            GameOver();
        }
    }

    public void ResetLevel(float delay)
    { // добавлена перегрузка, теперь есть задержка
        Invoke(nameof(ResetLevel), delay);
    }

    public void AddCoin()
    {
        coins++;
        if (coins == 100)
        {
            Addlife();
            coins = 0;
        }
    }

    public void Addlife()
    {
        lives++;
    }


}
