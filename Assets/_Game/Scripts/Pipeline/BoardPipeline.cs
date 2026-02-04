using System.Collections.Generic;
using _Game.Scripts.Board;
using Cysharp.Threading.Tasks;

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
            foreach (var step in _steps)
            {
                await step.Execute(context);
            }
        }
    }
}