using _Game.Scripts.Board;
using _Game.Scripts.Managers;
using _Game.Scripts.Pipeline.Processors;
using Cysharp.Threading.Tasks;
using UnityEngine;
using Zenject;

namespace _Game.Scripts.Pipeline
{
    public class BoardPipelineController
    {
        [Inject] private InputManager _inputManager;
        
        private BoardContext Context;
        private BoardPipeline MatchPipeline;
        
        public void Initialize(BoardContext context)
        {
            Context = context;
            MatchPipeline = new BoardPipeline()
                .AddStep(new MatchStep())
                .AddStep(new RemoveStep())
                .AddStep(new FallStep())
                .AddStep(new FillStep())
                .AddStep(new CheckStep())
                ;
        }

        public async UniTask RunMatch()
        {
            Context.IsRunning = true;
            _inputManager.Disable();
            Debug.Log("Started");
            
            while (Context.IsRunning)
            {
                await MatchPipeline.Run(Context);
            }
            
            _inputManager.Enable();
            Debug.Log("Finished");
        }
    }
}