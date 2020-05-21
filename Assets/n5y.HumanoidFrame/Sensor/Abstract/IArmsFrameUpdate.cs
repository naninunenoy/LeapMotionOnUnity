using Cysharp.Threading.Tasks;
using n5y.HumanoidFrame.Dto;

namespace n5y.HumanoidFrame.Sensor.Abstract
{
    public interface IArmsFrameUpdate
    {
        IUniTaskAsyncEnumerable<Arms> ArmsUpdateAsync();
    }
}
