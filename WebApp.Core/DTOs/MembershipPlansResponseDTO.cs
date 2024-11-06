﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebApp.Core.DTOs
{
    public class MembershipPlansResponseDTO
    {
        public string Name { get; set; } = string.Empty;
        public int Duration     { get; set; }
        public double Price { get; set; }
    }
}
