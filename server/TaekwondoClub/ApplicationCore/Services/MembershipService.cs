using ApplicationCore.Enums;
using ApplicationCore.Models;
using ApplicationCore.Interfaces;

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

        public async Task<CreateMembershipOutcome> CreateMembership(
            int studentId,
            DateTime startDate,
            DateTime endDate)
        {
            var student = await _studentRepository
                .GetStudentAndMembershipByStudentId(studentId);

            if (student == null)
                return CreateMembershipOutcome.StudentNotFound;
            if (student.Membership != null)
                return CreateMembershipOutcome.StudentMembershipAlreadyExists;
            if (!ValidateMembershipPeriod(startDate, endDate))
                return CreateMembershipOutcome.InvalidMembershipPeriod;

            try
            {
                await _membershipRepository.AddAsync(new Membership
                {
                    StudentId = studentId,
                    StartDate = startDate,
                    EndDate = endDate,
                    CreatedDate = DateTime.Now
                });
            }
            catch (Exception)
            {
                return CreateMembershipOutcome.InsertFailed;
            }

            return CreateMembershipOutcome.Success;
        }

        public async Task<UpdateMembershipOutcome> UpdateMembership(
            int membershipId,
            DateTime startDate,
            DateTime endDate)
        {
            var membership = await _membershipRepository.GetByIdAsync(membershipId);

            if (membership == null)
                return UpdateMembershipOutcome.MembershipNotFound;
            if (!ValidateMembershipPeriod(startDate, endDate))
                return UpdateMembershipOutcome.InvalidMembershipPeriod;

            membership.StartDate = startDate;
            membership.EndDate = endDate;

            try
            {
                await _membershipRepository.UpdateAsync(membership);
            }
            catch (Exception)
            {
                return UpdateMembershipOutcome.UpdateFailed;
            }

            return UpdateMembershipOutcome.Success;
        }

        private bool ValidateMembershipPeriod(DateTime startDate, DateTime endDate)
        {
            if (startDate.Date < DateTime.Now.Date || startDate.Date >= endDate.Date)
                return false;

            return true;
        }
    }
}
