using System;
using System.Collections.Generic;
using System.Data;
using MVCEngineeringSystemApplication.DTO;
using MVCEngineeringSystemApplication.Interfaces;

namespace MVCEngineeringSystemApplication.Services.Data
{
    namespace MVCEngineeringApplication.Services
    {
        public class ADODataService : IMockDataService
        {

            private readonly DataSet _dataSet = new DataSet("EngineeringData");
            private DataTable _dataTable = new DataTable("EngineeringDataTable");

            private enum ColumnType
            {
                Text = 0,
                Money,
                Decimal,
                Note,
                Other
            }

            public List<DataMock> CreateStubData()
            {
                return new List<DataMock>();
            }

            public DataSet GenerateStrongDataSet()
            {
                DataSet ds = new DataSet();
                ds.Tables.Add("StrongDataSet");

                ds.Tables[0].Columns.Add("Row");
                ds.Tables[0].Columns.Add("Amount (£)");
                ds.Tables[0].Columns.Add("Precision");
                ds.Tables[0].Columns.Add("Notes");
                ds.Tables[0].Columns.Add("Other");
                ds.Tables[0].Columns.Add("Row Information");

                for (int i = 0; i < 500; i++)
                {
                    DataRow row = ds.Tables[0].Rows.Add();
                    row[0] = string.Format("Row {0} Column 1", i);
                    row[1] = RetrieveColumnValue((ColumnType)1); //string.Format("Row {0} Column 2", i);
                    row[2] = RetrieveColumnValue((ColumnType)2);  //string.Format("Row {0} Column 3", i);
                    row[3] = "Row " + i + " " + RetrieveColumnValue((ColumnType)3);  //string.Format("Row {0} Column 4", i);
                    row[4] = RetrieveColumnValue((ColumnType)4);  // string.Format("Row {0} Column 5", i);
                    row[5] = string.Format("Row {0} Column 6", i);
                }

                return ds;
            }

            private string RetrieveColumnValue(ColumnType column)
            {
                switch (column)
                {
                    case ColumnType.Text:
                        return "Column text value";

                    case ColumnType.Note:
                        return "This is an example of some long text which would represent notes within the system. This is an example of some long text which would represent notes within the system. ";

                    case ColumnType.Money:
                        int randomMonteary = new Random().Next(1, 100);
                        return string.Format("{0:C}", randomMonteary);

                    case ColumnType.Decimal:
                        int ticks = new Random().Next(1, 100);
                        return string.Format("{0:N}", ticks);

                    case ColumnType.Other:
                        return "Another arbitary column text value";
                }

                return string.Empty;
            }

            public DataSet RetrieveData()
            {
                if (_dataSet.Tables.Count == 0 || _dataSet.Tables[0].Columns.Count == 0)
                {
                    RandomizeColumns();
                }

                return _dataSet;
            }

            public void RandomizeColumns()
            {
                int numberOfColumns = RandomNumber();
                DataRow row = _dataTable.Rows.Add();

                for (int i = 0; i < numberOfColumns; i++)
                {
                    _dataTable.Columns.Add("Column " + i);
                }

                // Number of rows to generate (Columns * 10)
                for (int j = 0; j < numberOfColumns * 10; j++)
                {
                    InsertRows(numberOfColumns);
                }

                _dataSet.Tables.Add(_dataTable);
            }

            public void InsertRows(int count)
            {
                DataRow row = _dataTable.Rows.Add();
                for (int j = 0; j < count; j++)
                {
                    row[j] = "Column " + j + " data " + DateTime.Now.Ticks;
                }
            }

            public int RandomNumber()
            {
                int seed = (int)DateTime.Now.Ticks;
                Random random = new Random(seed);
                return random.Next(15);
            }

            public string RandomColumnName()
            {
                return string.Empty;
            }
        }
    }
}