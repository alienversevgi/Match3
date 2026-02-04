using _Game.Scripts.Data;
using _Game.Scripts.View.Entity;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace _Game.Scripts.Board.Entity
{
    public class MatchEntity : BaseEntity<EntityData, MatchEntityView>
    {
        public override async UniTask Merge() 
        {
            this.transform.DORotate(new Vector3(0, 0, 15f), .2f);
            this.transform.DOScale(Vector3.zero, .2f);
            await UniTask.CompletedTask;
        }
    }
}