using ApplicationCore.Enums;
using ApplicationCore.Models;
using ApplicationCore.Interfaces;

namespace ApplicationCore.Services
{
    public class MembershipService : IMembershipService
    {
        public readonly IStudentRepository _studentRepository;
        public readonly IMembershipRepository _membershipRepository;
        public readonly IMembershipHistoryRepository _membershipHistoryRepository;

        public MembershipService(
            IStudentRepository studentRepository,
            IMembershipRepository membershipRepository,
            IMembershipHistoryRepository membershipHistoryRepository)
        {
            _studentRepository = studentRepository;
            _membershipRepository = membershipRepository;
            _membershipHistoryRepository = membershipHistoryRepository;
        }

        public async Task<CreateMembershipOutcome> CreateMembership(
            int studentId,
            DateTime startDate,
            DateTime endDate,
            double subscriptionFee)
        {
            var student = await _studentRepository
                .GetStudentAndMembershipByStudentId(studentId);
            if (student == null)
                return CreateMembershipOutcome.StudentNotFound;
            if (student.Membership != null)
                return CreateMembershipOutcome.StudentMembershipAlreadyExists;

            Membership membership;
            try
            {
                membership = await _membershipRepository.AddAsync(new Membership
                {
                    StudentId = studentId,
                    StartDate = startDate.ToLocalTime(),
                    EndDate = endDate.ToLocalTime(),
                    SubscriptionFee = subscriptionFee
                });
            }
            catch (Exception)
            {
                return CreateMembershipOutcome.InsertFailed;
            }

            try
            {
                await _membershipHistoryRepository.AddAsync(new MembershipHistory
                {
                    MembershipId = membership.Id,
                    StartDate = membership.StartDate,
                    EndDate = membership.EndDate,
                    SubscriptionFee = membership.SubscriptionFee
                });
            }
            catch (Exception) { }

            return CreateMembershipOutcome.Success;
        }

        public async Task<UpdateMembershipOutcome> UpdateMembership(
            int membershipId,
            DateTime startDate,
            DateTime endDate,
            double subscriptionFee)
        {
            var membership = await _membershipRepository.GetByIdAsync(membershipId);
            if (membership == null)
                return UpdateMembershipOutcome.MembershipNotFound;

            membership.StartDate = startDate.ToLocalTime();
            membership.EndDate = endDate.ToLocalTime();
            membership.SubscriptionFee = subscriptionFee;

            try
            {
                await _membershipRepository.UpdateAsync(membership);
            }
            catch (Exception)
            {
                return UpdateMembershipOutcome.UpdateFailed;
            }

            try
            {
                await _membershipHistoryRepository.AddAsync(new MembershipHistory
                {
                    MembershipId = membership.Id,
                    StartDate = membership.StartDate,
                    EndDate = membership.EndDate,
                    SubscriptionFee = membership.SubscriptionFee
                });
            }
            catch (Exception) { }

            return UpdateMembershipOutcome.Success;
        }
    }
}
