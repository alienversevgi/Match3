using DG.Tweening;
using UnityEngine;

namespace _Game.Scripts.Board.Entity
{
    public class MatchEntity : BaseEntity
    {
        public override void Merge()
        {
            this.transform.DORotate(new Vector3(0, 0, 15f), .2f);
            this.transform.DOScale(Vector3.zero, .2f).OnComplete(() => Destroy(gameObject));
        }
    }
}