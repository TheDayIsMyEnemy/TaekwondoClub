using Data.Models;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace WebApi.Services
{
    public class StudentMembershipService : IStudentMembershipService
    {
        public readonly IStudentRepository _studentRepository;
        public readonly IStudentMembershipRepository _studentMembershipRepository;

        public StudentMembershipService(
            IStudentRepository studentRepository,
            IStudentMembershipRepository studentMembershipRepository)
        {
            _studentRepository = studentRepository;
            _studentMembershipRepository = studentMembershipRepository;
        }

        public async Task<bool> CreateStudentMembership(int studentId, int days)
        {
            var student = await _studentRepository
                .GetStudentByIdAndStudentMembership(studentId);

            if (student == null)
                return false;

            if (student.Membership != null)
                return false;

            var dateNow = DateTime.Now;

            student.Membership = new StudentMembership
            {
                StartDate = dateNow,
                EndDate = dateNow.AddDays(days)
            };

            await _studentRepository.UpdateAsync(student);
            return true;
            
        }

        //public async Task<bool> UpdateMembeshipService(int studentId, int days)
        //{
        //    var student = await _dbContext
        //        .Students
        //        .FirstOrDefaultAsync(s => s.Id == studentId);

        //    if (student == null)
        //        return false;

        //    var dateNow = DateTime.Now;

        //    if (student.Membership.IsExpired)
        //    {
        //        student.Membership.StartDate = dateNow;
        //        student.Membership.EndDate = dateNow.AddDays(days);
        //    }
        //    else
        //    {
        //        int daysLeft = (student.Membership.EndDate - student.Membership.StartDate).Value.Days;
        //        int totalMembershipDays = daysLeft + days;
        //        student.Membership.StartDate = dateNow;
        //        student.Membership.EndDate = dateNow.AddDays(daysLeft);
        //    }
        //    return true;
        //}
    }
}
