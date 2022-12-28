﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Models.ViewModels
{
    public class IndexVm
    {
        public IEnumerable<FavorSolutionVm> FavorSolutionVms { get; set; } = null!;
        public IEnumerable<ThemeVm> ThemeVms { get; set; } = null!;
        public IEnumerable<SubjectVm> SubjectVms { get; set; } = null!;
        public int CountFavorSolutions { get; set; } 
    }
}
