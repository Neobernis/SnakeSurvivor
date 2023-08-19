using System;
using CodeBase.Snake;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace CodeBase
{
    public class FinishBlock : MonoBehaviour
    {
        private int _currentLevel;
        private int _nextLevel;
        
        private void Awake()
        {
            _currentLevel = SceneManager.GetActiveScene().buildIndex;
            _nextLevel = _currentLevel + 1;
        }

        private void OnTriggerEnter2D(Collider2D collider)
        {
            if (collider.gameObject.GetComponent<SnakeMovement>())
            {
                if (SceneManager.sceneCountInBuildSettings > _nextLevel)
                    SceneManager.LoadScene(_nextLevel);
            }
        }
    }
}