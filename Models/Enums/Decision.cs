﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace E_Shopper.Models.Enums
{
    public enum Decision
    {
        [Display(Name = "ACCEPT")]
        Accept = 0,

        [Display(Name = "REJECT")]
        Reject = 1
    }
}
