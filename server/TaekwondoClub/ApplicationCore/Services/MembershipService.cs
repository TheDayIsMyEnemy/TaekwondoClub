using ApplicationCore.Enums;
using ApplicationCore.Models;
using ApplicationCore.Interfaces;

namespace ApplicationCore.Services
{
    public class MembershipService : IMembershipService
    {
        public readonly IMembershipRepository _membershipRepository;
        public readonly IStudentRepository _studentRepository;
        public readonly IMembershipValidationService _membershipValidationService;

        public MembershipService(
            IMembershipRepository membershipRepository,
            IStudentRepository studentRepository,
            IMembershipValidationService membershipValidationService)
        {
            _membershipRepository = membershipRepository;
            _studentRepository = studentRepository;
            _membershipValidationService = membershipValidationService;
        }

        public async Task<CreateMembershipOutcome> CreateMembership(
            int studentId,
            DateTimeOffset startDate,
            DateTimeOffset endDate)
        {
            var student = await _studentRepository
                .GetStudentAndMembershipByStudentId(studentId);

            if (student == null)
                return CreateMembershipOutcome.StudentNotFound;
            if (student.Membership != null)
                return CreateMembershipOutcome.StudentMembershipAlreadyExists;
            if (!_membershipValidationService.Validate(startDate, endDate))
                return CreateMembershipOutcome.InvalidMembershipPeriod;
                
            try
            {
                await _membershipRepository.AddAsync(new Membership
                {
                    StudentId = studentId,
                    StartDate = startDate,
                    EndDate = endDate
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
            DateTimeOffset startDate,
            DateTimeOffset endDate)
        {
            var membership = await _membershipRepository.GetByIdAsync(membershipId);

            if (membership == null)
                return UpdateMembershipOutcome.MembershipNotFound;
            if (!_membershipValidationService.Validate(startDate, endDate))
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
    }
}
