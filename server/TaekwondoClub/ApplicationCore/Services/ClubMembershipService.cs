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
                EndDate = endDate,
                CreatedDate = DateTime.Now
            };

            try
            {
                await _clubMembershipRepository.AddAsync(clubMembership);
            }
            catch (Exception)
            {
                return false;
            }

            
            return true;
        }

        public async Task<bool> UpdateClubMembership(int clubMembershipId, DateTime startDate, DateTime endDate)
        {
            var clubMembership = await _clubMembershipRepository.GetByIdAsync(clubMembershipId);

            if (clubMembership == null)
                return false;

            clubMembership.StartDate = startDate;
            clubMembership.EndDate = endDate;

            try
            {
                await _clubMembershipRepository.UpdateAsync(clubMembership);
            }
            catch (Exception)
            {
                return false;
            }
            
            return true;
        }
    }
}
