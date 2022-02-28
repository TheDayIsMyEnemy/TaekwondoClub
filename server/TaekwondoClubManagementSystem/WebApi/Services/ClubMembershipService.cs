using Infrastructure.Data.Models;
using Infrastructure.Data.Repositories.Interfaces;

namespace WebApi.Services
{
    public class ClubMembershipService : IClubMembershipService
    {
        public readonly IStudentRepository _studentRepository;

        public ClubMembershipService(
            IStudentRepository studentRepository)
            
        {
            _studentRepository = studentRepository;
        }

        public async Task<bool> CreateClubMembership(int studentId, int days)
        {
            var student = await _studentRepository
                .GetStudentAndClubMembershipByStudentId(studentId);

            if (student == null)
                return false;

            if (student.ClubMembershipId.HasValue)
                return false;

            var dateNow = DateTime.Now;

            student.ClubMembership = new ClubMembership
            {
                StartDate = dateNow,
                EndDate = dateNow.AddDays(days)
            };

            await _studentRepository.UpdateAsync(student);
            return true;
            
        }

        //public async Task<bool> UpdateMembeshipService(int studentId, int days)
        //{
        //    var student = await _context
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
