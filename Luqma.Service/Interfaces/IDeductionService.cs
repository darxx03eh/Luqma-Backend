namespace Luqma.Service.Interfaces
{
    public interface IDeductionService
    {
        public Task<string> AddDeductionToUserAsync(int userId, double deductionRate);
        public Task<string> RemoveDeductionFromUserAsync(int id);
        public Task<string> UpdateDeductionAsync(int deductionId, double deductionRate);
    }
}
