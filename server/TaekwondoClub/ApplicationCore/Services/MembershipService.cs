using ApplicationCore.Interfaces;
using ApplicationCore.Models;

namespace ApplicationCore.Services
{
    public class MembershipService : IMembershipService
    {
        public readonly IMembershipRepository _membershipRepository;
        public readonly IStudentRepository _studentRepository;

        public MembershipService(
            IMembershipRepository membershipRepository,
            IStudentRepository studentRepository)
        {
            _membershipRepository = membershipRepository;
            _studentRepository = studentRepository;
        }

        public async Task<bool> CreateNewMembership(int studentId, DateTime startDate, DateTime endDate)
        {
            var student = await _studentRepository
                .GetStudentAndMembershipByStudentId(studentId);

            if (student == null)
                return false;
            if (student.Membership != null)
                return false;
            if (!ValidateMembershipPeriod(startDate, endDate))
                return false;

            var clubMembership = new Membership
            {
                StudentId = studentId,
                StartDate = startDate,
                EndDate = endDate,
                CreatedDate = DateTime.Now
            };

            try
            {
                await _membershipRepository.AddAsync(clubMembership);
            }
            catch (Exception)
            {
                return false;
            }
    
            return true;
        }

        public async Task<bool> UpdateMembership(int clubMembershipId, DateTime startDate, DateTime endDate)
        {
            var membership = await _membershipRepository.GetByIdAsync(clubMembershipId);

            if (membership == null)
                return false;
            if (!ValidateMembershipPeriod(startDate, endDate))
                return false;

            membership.StartDate = startDate;
            membership.EndDate = endDate;

            try
            {
                await _membershipRepository.UpdateAsync(membership);
            }
            catch (Exception)
            {
                return false;
            }
            
            return true;
        }

        private bool ValidateMembershipPeriod(DateTime startDate, DateTime endDate)
        {
            if (startDate < DateTime.Now || startDate >= endDate)
                return false;

            return true;
        }
    }
}
