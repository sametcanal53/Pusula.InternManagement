using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp;
using Pusula.InternManagement.Exceptions;
using Volo.Abp.Identity;
using Azure.Storage.Blobs.Models;
using JetBrains.Annotations;
using Volo.Abp.Users;
using Microsoft.AspNetCore.Identity;

#nullable disable
namespace Pusula.InternManagement.Interns
{
    public class Intern : IdentityUser //FullAuditedAggregateRoot<Guid>
    {
        public Guid DepartmentId { get; set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public Intern() { }

        public Intern(Guid id, Guid departmentId, string name, string surname, string phoneNumber, string email, string password, DateTime startDate, DateTime endDate) : base(id, email.Split('@')[0], email)
        {
            DepartmentId = departmentId;
            SetName(name); 
            SetSurname(surname);
            SetPhoneNumber(phoneNumber);
            SetEmail(email);
            SetPassword(password);
            SetStartDate(startDate);
            SetEndDate(endDate);
            LockoutEnabled = true;
        }

        public void SetName(string name)
        {
            Name = Check.NotNullOrWhiteSpace(name, nameof(name), InternConsts.MaxNameLength);
        }
        public void SetSurname(string surname)
        {
            Surname = Check.NotNullOrWhiteSpace(surname, nameof(surname), InternConsts.MaxSurnameLength);
        }
        public void SetPhoneNumber(string phoneNumber)
        {
            PhoneNumber = phoneNumber;
        }
        public void SetEmail(string email)
        {
            Email = email;
        }
        public void SetStartDate(DateTime startDate)
        {
            StartDate = startDate;
        }
        public void SetEndDate(DateTime endDate)
        {
            if (endDate < StartDate)
                throw new DateInputException();
            EndDate = endDate;
        }
        public void SetPassword(string password)
        {
            PasswordHash = PasswordHelper.HashPassword(password);
        }

    }
}
