using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace LudoTest.Eksamen
{
    public class DataDrivenTests
    {


        // Using InlineData to provide data for the test method
        [Theory(Skip = "reason")]
        [InlineData(1, 2, 3)]
        [InlineData(2, 3, 5)]
        [InlineData(5, 5, 10)]
        public void Add_ReturnsCorrectSum(int a, int b, int expected)
        {
            var result = a + b;
            Assert.Equal(expected, result);
        }



        // Using MemberData to provide data for the test method
        public static IEnumerable<object[]> AddData =>
            new List<object[]>
            {
                new object[] { 1, 2, 3 },
                new object[] { 2, 3, 5 },
                new object[] { 5, 5, 10 }
            };

        [Theory(Skip = "reason")]
        [MemberData(nameof(AddData))]
        public void Add_WithMemberData(int a, int b, int expected)
        {
            var result = a + b;
            Assert.Equal(expected, result);
        }



        // Using ClassData to provide test data
        public class AddDataClass : IEnumerable<object[]>
        {
            public IEnumerator<object[]> GetEnumerator()
            {
                yield return new object[] { 1, 2, 3 };
                yield return new object[] { 4, 5, 9 };
            }

            IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
        }

        [Theory(Skip = "reason")]
        [ClassData(typeof(AddDataClass))]
        public void Add_WithClassData(int a, int b, int expected)
        {
            Assert.Equal(expected, a + b);
        }



        // Using a custom DataAttribute to provide test data
        public class CsvDataAttribute : DataAttribute
        {
            public override IEnumerable<object[]> GetData(MethodInfo testMethod)
            {
                return new List<object[]>
                {
                    new object[] { 1, 2, 3 },
                    new object[] { 2, 2, 4 }
                };
            }
        }

        [Theory(Skip = "reason")]
        [CsvData]
        public void Add_WithCustomAttribute(int a, int b, int expected)
        {
            Assert.Equal(expected, a + b);
        }
    }
}
