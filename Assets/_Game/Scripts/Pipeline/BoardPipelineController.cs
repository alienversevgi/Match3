using _Game.Scripts.Board;
using _Game.Scripts.Pipeline.Processors;
using Cysharp.Threading.Tasks;

namespace _Game.Scripts.Pipeline
{
    public class BoardPipelineController
    {
        private BoardContext Context;
        private BoardPipeline MatchPipeline;

        public void Initialize(BoardContext context)
        {
            Context = context;
            MatchPipeline = new BoardPipeline()
                .AddStep(new MatchStep());
        }

        public async UniTask RunMatch()
        {
            Context.IsRunning = true;
            
            await MatchPipeline.Run(Context);
        }
    }
}