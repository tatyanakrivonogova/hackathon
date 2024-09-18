using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Nsu.HackathonProblem.Contracts
{
    public record Team(Employee TeamLead, Employee Junior)
    {
        public void Deconstruct(out Employee teamLead, out Employee junior)
        {
            teamLead = this.TeamLead;
            junior = this.Junior;
        }
    }
}
