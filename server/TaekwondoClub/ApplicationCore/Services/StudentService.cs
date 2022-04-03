using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using System.Text.RegularExpressions;

namespace ApplicationCore.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;
        private readonly IMembershipService _membershipService;

        public StudentService(
            IStudentRepository studentRepository,
            IMembershipService membershipService)
        {
            _studentRepository = studentRepository;
            _membershipService = membershipService;
        }

        public async Task<bool> CreateNewStudentWithMembership(
            string firstName,
            string lastName,
            string gender,
            DateTime? birthDate,
            string? phoneNumber,
            DateTime[]? membershipPeriod)
        {
            if (!ValidateStudent(firstName, lastName, gender, birthDate, phoneNumber))
                return false;

            var student = new Student(firstName, lastName, gender, birthDate, phoneNumber);

            try
            {
                student = await _studentRepository.AddAsync(student);
            }
            catch (Exception)
            {
                return false;
            }

            if (membershipPeriod != null && membershipPeriod.Length == 2)
            {
                await _membershipService.CreateMembership(
                    student.Id,
                    membershipPeriod[0],
                    membershipPeriod[1]);
            }

            return true;
        }

        private bool ValidateStudent(
            string firstName,
            string lastName,
            string gender,
            DateTime? birthDate,
            string? phoneNumber)
        {
            if (string.IsNullOrWhiteSpace(firstName))
                return false;
            if (string.IsNullOrWhiteSpace(lastName))
                return false;
            if (!gender.Equals("Male") && !gender.Equals("Female"))
                return false;
            if (birthDate.HasValue && birthDate.Value >= DateTime.Now)
                return false;
            if (phoneNumber != null && !Regex.IsMatch(phoneNumber, @"^0\d{9}$|^359\d{9}$"))
                return false;

            return true;
        }
    }
}
