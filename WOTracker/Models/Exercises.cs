using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WOTracker.Models
{
    internal class Exercises
    {
        public string Name { get; set; }
        public string MaleMultipliers { get; set; }
        public string FemaleMultipliers { get; set; }


        internal Exercises()
        {
            Name = "";
            MaleMultipliers = "";
            FemaleMultipliers = "";
        }
    }

    
}
