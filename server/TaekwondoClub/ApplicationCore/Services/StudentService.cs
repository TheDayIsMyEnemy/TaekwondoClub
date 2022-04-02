using ApplicationCore.Interfaces;
using ApplicationCore.Models;
using System.Text.RegularExpressions;

namespace ApplicationCore.Services
{
    public class StudentService : IStudentService
    {
        private readonly IStudentRepository _studentRepository;

        public StudentService(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }

        public async Task<bool> CreateNewStudentWithMembership(
            string firstName,
            string lastName,
            string gender,
            DateTime? birthDate,
            string? phoneNumber,
            DateTime[]? membershipPeriod)
        {
            if (!ValidateStudent(firstName, lastName, gender, birthDate, phoneNumber, membershipPeriod))
                return false;

            var student = new Student(firstName, lastName, gender, birthDate, phoneNumber);

            if (membershipPeriod != null)
            {
                student.Membership = new Membership
                {
                    CreatedDate = DateTime.Now,
                    StartDate = membershipPeriod[0],
                    EndDate = membershipPeriod[1]
                };
            }

            try
            {
                await _studentRepository.AddAsync(student);
            }
            catch (Exception)
            {
                return false;
            }

            return true;
        }

        private bool ValidateStudent(
            string firstName,
            string lastName,
            string gender,
            DateTime? birthDate,
            string? phoneNumber,
            DateTime[]? membershipPeriod)
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
            if (membershipPeriod != null &&
                (membershipPeriod.Length != 2 || membershipPeriod[0] < DateTime.Now || membershipPeriod[0] >= membershipPeriod[1]))
                return false;

            return true;
        }
    }
}
