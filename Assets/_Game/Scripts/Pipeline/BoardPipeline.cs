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
            var sw = new Stopwatch();

            foreach (var step in _steps)
            {
                sw.Start();
                Debug.Log(step.GetType().Name + "Started");
                await step.Execute(context);
                Debug.Log($"{step.GetType().Name} Ended Geçen süre: {sw.ElapsedMilliseconds} ms");
                sw.Restart();
            }

            sw.Stop();
        }
    }
}