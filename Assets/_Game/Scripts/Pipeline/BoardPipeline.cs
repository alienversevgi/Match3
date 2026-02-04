using System.Collections.Generic;
using System.Diagnostics;
using _Game.Scripts.Board;
using Cysharp.Threading.Tasks;
using Debug = UnityEngine.Debug;

namespace _Game.Scripts.Pipeline
{
    public sealed class BoardPipeline
    {
        private readonly List<IBoardStep> _steps = new();

        public BoardPipeline AddStep(IBoardStep step)
        {
            _steps.Add(step);
            return this;
        }

        public async UniTask Run(BoardContext context)
        {
            for (int i = 0; i < _steps.Count; i++)
            {
                await _steps[i].Execute(context);
            }
        }
    }
}