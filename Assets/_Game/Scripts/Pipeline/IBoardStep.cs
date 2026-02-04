using _Game.Scripts.Board;
using Cysharp.Threading.Tasks;

namespace _Game.Scripts.Pipeline
{
    public interface IBoardStep
    {
        UniTask Execute(BoardContext context);
    }
}