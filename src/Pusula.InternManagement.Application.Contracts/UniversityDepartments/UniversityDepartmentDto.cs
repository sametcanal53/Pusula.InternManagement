﻿using System;
using System.Collections.Generic;
using System.Text;
using Volo.Abp.Application.Dtos;

#nullable disable
namespace Pusula.InternManagement.UniversityDepartments
{
    public class UniversityDepartmentDto : FullAuditedEntityDto<Guid>
    {
        public string Name { get; set; }

    }
}
