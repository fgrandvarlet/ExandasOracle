using System.Collections.Generic;

using ExandasOracle.Domain;

namespace ExandasOracle.Dao
{
    public class ParameterData
    {
        public List<ConnectionParams> ConnectionParamsList { get; set; }
        public List<ComparisonSet> ComparisonSetList { get; set; }

    }
}
