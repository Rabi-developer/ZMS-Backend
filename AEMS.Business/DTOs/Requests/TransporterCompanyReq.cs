﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZMS.Business.DTOs.Requests
{
    public class TransporterCompanyReq
    {
        public Guid? Id { get; set; }
        public string? Descriptions { get; set; }
        public string? Segment { get; set; }
    }
}
