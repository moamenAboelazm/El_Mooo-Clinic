using AutoMapper;
using El_Mooo_Clinic.DTOs;
using El_Mooo_Clinic.Models;
using El_Mooo_Clinic.Services.Interfaces;
using EL_Mooo_Clinic.Repositories.Interfaces;

namespace El_Mooo_Clinic.Services.Implementations
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public DepartmentService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<DtoDepartment.ReadDTO>> GetAllAsync()
        {
            var departments = await _unitOfWork.Departments.GetAllAsync();
            return _mapper.Map<IEnumerable<DtoDepartment.ReadDTO>>(departments);
        }

        public async Task<DtoDepartment.ReadDTO> GetByIdAsync(int id)
        {
            var dept = await _unitOfWork.Departments.GetByIdAsync(id);
            if (dept == null) return null;
            return _mapper.Map<DtoDepartment.ReadDTO>(dept);
        }

        public async Task<DtoDepartment.ReadDTO> CreateAsync(DtoDepartment.CreateDTO dto)
        {
            var dept = _mapper.Map<ClsDepartment>(dto);
            await _unitOfWork.Departments.AddAsync(dept);
            await _unitOfWork.CompleteAsync();

            return _mapper.Map<DtoDepartment.ReadDTO>(dept);
        }

        public async Task<bool> UpdateAsync(DtoDepartment.UpdateDTO dto)
        {
            var dept = await _unitOfWork.Departments.GetByIdAsync(dto.ID);
            if (dept == null) return false;

            _mapper.Map(dto, dept);

            _unitOfWork.Departments.Update(dept);
            await _unitOfWork.CompleteAsync();
            return true;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var dept = await _unitOfWork.Departments.GetByIdAsync(id);
            if (dept == null) return false;

            _unitOfWork.Departments.Delete(dept);
            await _unitOfWork.CompleteAsync();
            return true;
        }
    }
}