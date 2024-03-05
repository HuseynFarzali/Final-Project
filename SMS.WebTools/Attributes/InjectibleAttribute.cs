﻿using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SMS.WebTools.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class InjectibleAttribute : Attribute
    {
        public ServiceLifetime Lifetime { get; set; }
        public Type? Implements { get; set; }
    }
}
