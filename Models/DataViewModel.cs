using System.Collections.Generic;
using System.Data;
using System.Linq;
using MVCEngineeringSystemApplication.DTO;
using MVCEngineeringSystemApplication.Interfaces;

namespace MVCEngineeringSystemApplication.Models
{
    public class DataViewModel
    {
        private IMockDataService DataService { get; set; }
        private int TotalRows { get; set; }
        public List<DataMock> ModelData { get; set; }
        public readonly DataSet ModelDataSet;
        public int PageSizes { get; set; }
        public int PageId { get; set; }
        public int TotalPages { get; set; }
        public int CurrentLevel { get; set; }
        public const int PageSize = 15;

        public DataViewModel(IMockDataService service, int pageId, int level)
        {
            DataService = service;
            GenerateMockData();
            ModelDataSet = service.GenerateStrongDataSet(); 
            PageId = pageId;
            CurrentLevel = level;

            if (ModelDataSet != null)
            {
                TotalRows = ModelDataSet.Tables[0].Rows.Count;
                TotalPages = CalculateTotalPages();
            }
        }

        public void GenerateMockData()
        {
            if (DataService != null)
            {
                ModelData = DataService.CreateStubData();
            }
        }

        private int CalculateTotalPages()
        {
            return TotalRows / PageSize;
        }

        public List<DataMock> GenerateLowerLevelGroup(int level)
        {
            int lowerLevel = level + 1;
            List<DataMock> results = new List<DataMock>();

            foreach (var mock in ModelData.Where(md => md.Level == lowerLevel))
            {
                results.Add(mock);
            }

            return results;
        }

        /// <summary>
        /// Used for the relational mock service
        /// </summary>
        /// <param name="level"></param>
        /// <returns></returns>
        public List<DataMock> GenerateLowerRelationalLevelGroup(int level)
        {
            int lowerLevel = level + 1;
            List<DataMock> results = new List<DataMock>();
            var referencedResults = ModelData.Where(r => r.ReferencedItems.Any(ri => ri.Level == lowerLevel)).ToList();

            foreach (var mock in ModelData.Where(md => md.Level == level))
            {
                if (mock.ReferencedItems != null && mock.ReferencedItems.Any(r => r.Level == lowerLevel))
                {
                    foreach (var childItem in mock.ReferencedItems.Where(r => r.Level == lowerLevel))
                    {
                        results.Add(childItem);
                    }
                }
            }

            if (!results.Any() && referencedResults.Any())
            {
                foreach (var items in referencedResults)
                {
                    results.Add(items);
                }

            }

            return results;
        }
    }
}