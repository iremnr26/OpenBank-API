﻿using System;
using OpenBankAPI.Domain.Entities.Common;

namespace Domain.Entities 
{
	public class Product : BaseEntity 
    {
        public string Name { get; set; }
        public int Stock { get; set; }
        public long Price { get; set; }

    }
}

  