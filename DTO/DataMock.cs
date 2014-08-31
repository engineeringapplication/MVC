using System.Collections.Generic;

namespace MVCEngineeringSystemApplication.DTO
{
    public class DataMock
    {
        public int Id { get; set; }
        public int Level { get; set; }
        public string Description { get; set; }
        public int RelatedItemCount { get; set; }
        public List<DataMock> ReferencedItems { get; set; }

        public DataMock()
        {
            ReferencedItems = new List<DataMock>();
        }
    }
}