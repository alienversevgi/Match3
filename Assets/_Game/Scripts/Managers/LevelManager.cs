using System.Collections.Generic;
using _Game.Scripts.Components;
using _Game.Scripts.Data;
using _Game.Scripts.Factories;
using UnityEngine;
using Zenject;

namespace _Game.Scripts.Managers
{
    public class LevelManager : GameBehaviour
    {
        [SerializeField] private List<LevelData> levels;

        [Inject] private EntityFactory _entityFactory;
        
        public LevelData CurrentLevel { get; private set; }
        
        public int CurrentLevelIndex
        {
            get => PlayerPrefs.GetInt("CurrentLevel", 0);
            set
            {
                PlayerPrefs.SetInt("CurrentLevel", value);
                PlayerPrefs.Save();
            }
        }

        public void Initialize()
        {
            CurrentLevel = levels[CurrentLevelIndex];
            var entities = CurrentLevel.Board.Entities;
            for (int i = 0; i < entities.Count; i++)
            {
                _entityFactory.Bind(entities[i].Prefab, entities[i].Prefab.PoolID);
            }
        }
    }
}