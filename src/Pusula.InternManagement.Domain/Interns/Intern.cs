using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Volo.Abp.Domain.Entities.Auditing;
using Volo.Abp;
using Pusula.InternManagement.Exceptions;

#nullable disable
namespace Pusula.InternManagement.Interns
{
    public class Intern : FullAuditedAggregateRoot<Guid>
    {
        public Guid DepartmentId { get; set; }
        public string Name { get; private set; }
        public string PhoneNumber { get; private set; }
        public string Email { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime EndDate { get; private set; }

        public Intern() { }

        public Intern(Guid id, Guid departmentId, string name, string phoneNumber, string email, DateTime startDate, DateTime endDate) : base(id)
        {
            DepartmentId = departmentId;
            SetName(name);
            SetPhoneNumber(phoneNumber);
            SetEmail(email);
            SetStartDate(startDate);
            SetEndDate(endDate);
        }

        public void SetName(string name)
        {
            Name = Check.NotNullOrWhiteSpace(name, nameof(name), InternConsts.MaxNameLength);
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

    }
}
