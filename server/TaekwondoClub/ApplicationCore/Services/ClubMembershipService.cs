using ApplicationCore.Interfaces;
using ApplicationCore.Models;

namespace ApplicationCore.Services
{
    public class ClubMembershipService : IClubMembershipService
    {
        public readonly IClubMembershipRepository _clubMembershipRepository;
        public readonly IStudentRepository _studentRepository;

        public ClubMembershipService(
            IClubMembershipRepository clubMembershipRepository,
            IStudentRepository studentRepository)
        {
            _clubMembershipRepository = clubMembershipRepository;
            _studentRepository = studentRepository;
        }

        public async Task<bool> CreateNewClubMembership(int studentId, DateTime startDate, DateTime endDate)
        {
            var student = await _studentRepository
                .GetStudentAndClubMembershipByStudentId(studentId);

            if (student == null)
                return false;

            if (student.ClubMembership != null)
                return false;

            var clubMembership = new ClubMembership
            {
                StudentId = studentId,
                StartDate = startDate,
                EndDate = endDate
            };

            await _clubMembershipRepository.AddAsync(clubMembership);
            return true;
        }

        //public async Task<bool> UpdateClubMembership(int clubMembershipId, int days)
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
