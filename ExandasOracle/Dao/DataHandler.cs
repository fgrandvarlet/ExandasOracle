using System.IO;
using System.Text.Json;

namespace ExandasOracle.Dao
{
    public static class DataHandler
    {
        private static ParameterData GetParameterData()
        {
            var parameterData = new ParameterData();

            var connectionParamsDao = DaoFactory.Instance.GetConnectionParamsDao();
            parameterData.ConnectionParamsList = connectionParamsDao.GetList();

            var comparisonSetDao = DaoFactory.Instance.GetComparisonSetDao();
            parameterData.ComparisonSetList = comparisonSetDao.GetList();

            return parameterData;
        }

        public static void SerializeConnectionsComparisonSets(string fileName)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(GetParameterData(), options);
            File.WriteAllText(fileName, jsonString);
        }

        public static void DeserializeConnectionsComparisonSets(string fileName)
        {
            string jsonString = File.ReadAllText(fileName);
            ParameterData parameterData = JsonSerializer.Deserialize<ParameterData>(jsonString);
            var parameterDataDao = DaoFactory.Instance.GetParameterDataDao();
            parameterDataDao.Load(parameterData);
        }

    }
}
