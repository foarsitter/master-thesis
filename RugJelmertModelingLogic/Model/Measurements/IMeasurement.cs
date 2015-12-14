using RugJelmertModelingLogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RugJelmertModelingLogic.Model.Measurements
{
    public interface IMeasurement
    {
        List<double> getResult();
        double calculate(Agent[] agents);

        double getItem(int index);
    }
}
