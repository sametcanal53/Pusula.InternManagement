﻿using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

#nullable disable
namespace Pusula.InternManagement.UniversityDepartments
{
    public class UniversityDepartmentLookupDto : EntityDto<Guid>
    {
        public string Name { get; set; }

    }
}
