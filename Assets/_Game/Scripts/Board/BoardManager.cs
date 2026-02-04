using System;
using System.Linq;
using _Game.Scripts.Board.Entity;
using _Game.Scripts.Components;
using _Game.Scripts.Data;
using _Game.Scripts.Factories;
using UnityEngine;
using Zenject;

namespace _Game.Scripts.Board
{
    public class BoardManager : GameBehaviour
    {
        [SerializeField] private Grid grid;
        [SerializeField] private Transform container;

        [Inject] private EntityFactory _entityFactory;

        private Cell[,] _cells = new Cell[8, 10];

        public int Width => Data.Width;
        public int Height => Data.Height;

        public BoardContext Context { get; private set; }
        public BoardData Data { get; private set; }

        public void Initialize(BoardData data)
        {
            Context = DiContainer.Instantiate<BoardContext>();
         
            Data = data;
            Setup();
        }

        private void Setup()
        {
            _cells = new Cell[Width, Height];
            for (int y = Height - 1; y >= 0; y--)
            {
                for (int x = 0; x < Width; x++)
                {
                    InitializeCell(x, y);
                }
            }
        }

        private void InitializeCell(int x, int y)
        {
            var cell = new Cell();
            var position = new Vector2Int(x, y);
            var worldPosition = grid.GetCellCenterWorld(new Vector3Int(x, y, 0));

            cell.Initialize(position);

            var cellData = Data.Grid[x, Height - y - 1];
            var entityData = GetEntityById(cellData.Entity.ID);
            var entity = SpawnEntity(entityData, worldPosition);

            // entity.Initialize(entityData, position);
            cell.SetEntity(entity);

            _cells[x, y] = cell;
        }

        public EntityData GetRandomEntityData()
        {
            return Data.Entities[UnityEngine.Random.Range(0, Data.Entities.Count)];
        }

        public EntityData GetEntityById(Guid id)
        {
            return Data.Entities.First(entity => entity.ID.Equals(id));
        }

        public BaseEntity SpawnRandomEntity(Vector2Int position)
        {
            return SpawnEntity(GetRandomEntityData(), CellPositionToWorld(position));
        }
        
        public BaseEntity SpawnEntity(EntityData data, Vector3 position)
        {
            var entity = _entityFactory.Spawn(data.Prefab, data.Prefab.PoolID, position,
                Quaternion.identity, container);
            entity.Initialize(data, WorldToCell(position));
            return entity;
        }

        public Vector3 CellToWorld(Cell cell) => CellPositionToWorld(cell.Position);

        public Vector3 CellPositionToWorld(Vector2Int position)
        {
            return grid.GetCellCenterWorld(new Vector3Int(position.x, position.y, 0));
        }

        public Vector2Int WorldToCell(Vector3 worldPosition)
        {
            var pos = grid.WorldToCell(worldPosition);
            return new Vector2Int(pos.x, pos.y);
        }

        public bool IsInBounds(Vector2Int position)
        {
            return position.x >= 0 && position.x < Width && position.y >= 0 && position.y < Height;
        }

        public Cell GetCell(Vector3 worldPosition)
        {
            var position = WorldToCell(worldPosition);

            return GetCell(position.x, position.y);
        }

        public Cell GetCell(Vector2Int position) => GetCell(position.x, position.y);
        public Cell GetCell(int x, int y) => _cells[x, y];

        public BaseEntity GetEntity(Vector3 worldPosition)
        {
            var cell = GetCell(worldPosition);

            return cell.Entity;
        }

        public BaseEntity GetEntity(Vector2Int position) => GetEntity(position.x, position.y);

        public BaseEntity GetEntity(int x, int y) => _cells[x, y].Entity;
    }
}