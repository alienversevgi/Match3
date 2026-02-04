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
            this.transform.DORotate(new Vector3(0, 0, 15f), .1f);
            this.transform.DOScale(Vector3.zero, .1f);
            await UniTask.CompletedTask;
        }

        public override void Dispose()
        {
            base.Dispose();
            this.transform.DOKill();
            this.transform.eulerAngles = Vector3.zero;
            this.transform.localScale = Vector3.one;
        }
    }
}