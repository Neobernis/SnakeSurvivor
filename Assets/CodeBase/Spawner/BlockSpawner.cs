using System;
using UnityEngine;
using CodeBase.Blocks;
using CodeBase.Bonuses;
using CodeBase.Walls;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace CodeBase.Spawner
{
    public class BlockSpawner : MonoBehaviour
    {
        [Header("Options")] 
        [SerializeField] private Transform _container;
        [SerializeField] private int _repeatCount;
        [SerializeField] private int _distanceBetweenFullLine;
        [SerializeField] private int _distanceBetweenRandomLine;

        [Header("Blocks")] 
        [SerializeField] private Block _blockPrefab;
        [SerializeField] private int _blockSpawnChance;

        private BlockSpawnPoint[] _blockSpawnPoints;

        [Header("Wall")] 
        [SerializeField] private Wall _wallPrefab;
        [SerializeField] private int _wallSpawnChance;

        private WallSpawnPoint[] _wallSpawnPoints;

        [Header("Bonus")] 
        [SerializeField] private Bonus _bonusPrefab;
        [SerializeField] private int _bonusSpawnChance;

        private BonusSpawnPoint[] _bonusSpawnPoints;

        private Object _finish;

        private void Awake()
        {
            _blockSpawnPoints = GetComponentsInChildren<BlockSpawnPoint>();
            _wallSpawnPoints = GetComponentsInChildren<WallSpawnPoint>();
            _bonusSpawnPoints = GetComponentsInChildren<BonusSpawnPoint>();
            _finish = Resources.Load("Prefabs/Finish");
        }

        private void Start()
        {
            for (int i = 0; i < _repeatCount; i++)
            {
                MoveSpawner(_distanceBetweenFullLine);
                GenerateRandomElements(_wallSpawnPoints, _wallPrefab.gameObject, _wallSpawnChance, 3);
                GenerateFullLine(_blockSpawnPoints, _blockPrefab.gameObject);
                MoveSpawner(_distanceBetweenRandomLine);
                GenerateRandomElements(_blockSpawnPoints, _blockPrefab.gameObject, _blockSpawnChance);
                GenerateRandomElements(_bonusSpawnPoints, _bonusPrefab.gameObject, _bonusSpawnChance, 0.3f);
            }

            MoveSpawner(_distanceBetweenRandomLine);
            Instantiate(_finish, transform.position, Quaternion.identity);
        }

        private void GenerateFullLine(SpawnPoint[] spawnPoints, GameObject generatedElement)
        {
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                GenerateElement(spawnPoints[i].transform.position, generatedElement);
            }
        }

        private void GenerateRandomElements(SpawnPoint[] spawnPoints, GameObject generatedElement, int spawnChance,
            float scaleY = 1.1f)
        {
            for (int i = 0; i < spawnPoints.Length; i++)
            {
                if (Random.Range(0, 100) < spawnChance)
                {
                    GameObject element =
                        GenerateElement(spawnPoints[i].transform.position, generatedElement, scaleY / 2);
                    element.transform.localScale = new Vector3(element.transform.localScale.x, scaleY,
                        element.transform.localScale.z);
                }
            }
        }

        private GameObject GenerateElement(Vector3 spawnPoint, GameObject generatedElement, float offsetY = 0)
        {
            spawnPoint.y -= offsetY;
            return Instantiate(generatedElement, spawnPoint, Quaternion.identity, _container);
        }

        private void MoveSpawner(int distanceY)
        {
            transform.position = new Vector2(transform.position.x, transform.position.y + distanceY);
        }
    }
}