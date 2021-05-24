﻿using Agents.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Agents.ViewModels.Home
{
    public class HomeViewModel
    {
        public string Message { get; set; }

        public List<Agent> Agents { get; set; }
    }
}