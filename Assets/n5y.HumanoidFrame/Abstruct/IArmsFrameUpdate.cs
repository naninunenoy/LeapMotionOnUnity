using Cysharp.Threading.Tasks;
using n5y.HumanoidFrame.Dto;


namespace n5y.HumanoidFrame.Abstruct
{
    public interface IArmsFrameUpdate
    {
        IUniTaskAsyncEnumerable<Arms> ArmsUpdateAsync();
    }
}
