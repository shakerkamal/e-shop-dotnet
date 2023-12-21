﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Entities.Exceptions
{
    public sealed class UnAuthorizedException : Exception
    {
        public UnAuthorizedException(string? message) : base(message)
        {
        }
    }
}
