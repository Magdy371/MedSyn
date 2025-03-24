using OutpatientClinic.Business.Services.Interfaces;
using OutpatientClinic.Core.UnitOfWorks;
using OutpatientClinic.DataAccess.Entities;

public class StaffService : IStaffService
{
    private readonly IUnitOfWork _unitOfWork;
    public StaffService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Staff>> GetAllStaffAsync() =>
        await _unitOfWork.Repository<Staff>().GetAllAsync();

    public async Task<Staff> GetStaffByIdAsync(int id) =>
        await _unitOfWork.Repository<Staff>().GetByIdAsync(id);

    public async Task<Staff> CreateStaffAsync(Staff staff)
    {
        await _unitOfWork.Repository<Staff>().AddAsync(staff);
        await _unitOfWork.CompleteAsync();
        return staff;
    }

    public async Task<bool> UpdateStaffAsync(Staff staff)
    {
        _unitOfWork.Repository<Staff>().Update(staff);
        return await _unitOfWork.CompleteAsync() > 0;
    }

    public async Task<bool> DeleteStaffAsync(int id)
    {
        var staff = await _unitOfWork.Repository<Staff>().GetByIdAsync(id);
        if (staff == null)
            return false;
        _unitOfWork.Repository<Staff>().Delete(staff);
        return await _unitOfWork.CompleteAsync() > 0;
    }

    public async Task<IEnumerable<Staff>> FindStaffAsync(string name) =>
        await _unitOfWork.Repository<Staff>().FindAsync(s => s.FirstName.Contains(name) || s.LastName.Contains(name));

    public async Task<int> CountStaffAsync() =>
        await _unitOfWork.Repository<Staff>().CountAsync();
}
