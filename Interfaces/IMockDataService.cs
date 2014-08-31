using System.Collections.Generic;
using System.Data;
using MVCEngineeringSystemApplication.DTO;

namespace MVCEngineeringSystemApplication.Interfaces
{
    public interface IMockDataService
    {
        List<DataMock> CreateStubData();
        DataSet RetrieveData();
        DataSet GenerateStrongDataSet();
    }
}
