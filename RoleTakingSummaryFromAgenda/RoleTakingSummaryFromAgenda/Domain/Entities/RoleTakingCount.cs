﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoleTakingSummaryFromAgenda.Domain.Entities
{
    public class RoleTakingCount
    {
        public string Role_Taker_Name { get; set; }

        public string Role_Name { get; set; }

        public int Count { get; set; }
    }
}
