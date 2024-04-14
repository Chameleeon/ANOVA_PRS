
    namespace PerformanceTest;
        public class DataItem
        {
            public string Name { get; set; }
            public double Value1 { get; set; }
            public double? Value2 { get; set; }
            public double? Value3 { get; set; }

            public DataItem(string name, double value1, double? value2 = null, double? value3 = null)
            {
                Name = name;
                Value1 = value1;
                Value2 = value2;
                Value3 = value3;
            }
        }