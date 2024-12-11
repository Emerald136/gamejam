using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    [Header("Настройки карты")]
    public int width = 10; // Ширина карты
    public int height = 10; // Высота карты

    [Header("Префабы")]
    public GameObject[] floorTiles; // Массив префабов пола
    public GameObject[] decorationTiles; // Массив префабов декораций
    public GameObject capsule; // Префаб капсулы

    [Header("Шансы спавна")]
    [Range(0, 1)] public float decorationSpawnChance = 0.2f; // Шанс появления декорации
    [Range(0, 1)] public float capsuleSpawnChance = 0.1f; // Шанс появления капсулы

    void Start()
    {
        GenerateMap();
    }

    void GenerateMap()
    {
        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // Создаем пол, выбирая случайную плитку из массива
                GameObject floor = floorTiles[Random.Range(0, floorTiles.Length)];
                Instantiate(floor, new Vector3(x, y, 0), Quaternion.identity);

                // Рандомно спавним декорации
                if (Random.value < decorationSpawnChance)
                {
                    GameObject decoration = decorationTiles[Random.Range(0, decorationTiles.Length)];
                    Instantiate(decoration, new Vector3(x, y, 0), Quaternion.identity);
                }

                // Рандомно спавним капсулы
                if (Random.value < capsuleSpawnChance)
                {
                    Instantiate(capsule, new Vector3(x, y, 0), Quaternion.identity);
                }
            }
        }
    }
}
