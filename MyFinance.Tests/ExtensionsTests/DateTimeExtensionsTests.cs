using System;
using System.Collections.Generic;
using System.Text;
using Xunit;
using MyFinance.Extensions;

namespace MyFinance.Tests.ExtensionsTests
{
    
    public class DateTimeExtensionsTests
    {
        [Theory]
        [InlineData("19.05.2018", "24.02.1997", "14.06.1997", "01.01.2000")]
        //[InlineData("24.02.1997")]
        //[InlineData("14.06.1997")]
        //[InlineData("01.01.2000")]

        public void GenerateFromString_ShouldWork(string str1,string str2,string str3,string str4)
        {
            //Arange

            //Act
            var expected1 = new DateTime(2018, 5, 19);
            var expected2 = new DateTime(1997, 2, 24);
            var expected3= new DateTime(1997, 6, 14);
            var expected4 = new DateTime(2000, 1, 1);

            var actual1 = new DateTime().GenerateFromString(str1);
            var actual2 = new DateTime().GenerateFromString(str2);
            var actual3 = new DateTime().GenerateFromString(str3);
            var actual4 = new DateTime().GenerateFromString(str4);
            //Assert
            Assert.Equal(expected1,actual1);
            Assert.Equal(expected2,actual2);
            Assert.Equal(expected3,actual3);
            Assert.Equal(expected4,actual4);
        }
        [Fact]
        public void GenerateFromString_ShouldThrowArgumentNullException()
        {
            //Arange

            //Act
            Action action1 = () => new DateTime().GenerateFromString("");
            Action action2 = () => new DateTime().GenerateFromString(null);
            //Assert
            Assert.Throws<ArgumentNullException>(action1);
            Assert.Throws<ArgumentNullException>(action2);
        }
        [Fact]
        public void GenerateFromString_ShouldThrowArgumentException()
        {
            //Arange

            //Act
            Action action1 = () => new DateTime().GenerateFromString("waddwdwa");
            Action action2 = () => new DateTime().GenerateFromString("22.01");
            Action action3 = () => new DateTime().GenerateFromString("lalala.w");
            Action action4 = () => new DateTime().GenerateFromString("D.D.D.D.D");
            //Assert
            Assert.Throws<ArgumentException>(action1);
            Assert.Throws<ArgumentException>(action2);
            Assert.Throws<ArgumentException>(action3);
            Assert.Throws<ArgumentException>(action4);
        }

        [Fact]
        public void GenerateFromString_ShouldThrowFormatException()
        {
            //Arange

            //Act
            Action action1 = () => new DateTime().GenerateFromString("aa.aa.aa");
            Action action2 = () => new DateTime().GenerateFromString("05.a.2018");
            Action action3 = () => new DateTime().GenerateFromString("c.3.2");
            Action action4 = () => new DateTime().GenerateFromString("24.03.D");
            //Assert
            Assert.Throws<FormatException>(action1);
            Assert.Throws<FormatException>(action2);
            Assert.Throws<FormatException>(action3);
            Assert.Throws<FormatException>(action4);
        }

        [Fact]
        public void GenerateFromString_ShouldThrowArgumentOutOfRangeException()
        {
            //Arange

            //Act
            Action action1 = () => new DateTime().GenerateFromString("33.01.2018");
            Action action2 = () => new DateTime().GenerateFromString("31.02.2000");
            Action action3 = () => new DateTime().GenerateFromString("01.13.2006");
            Action action4 = () => new DateTime().GenerateFromString("0.01.2000");
            Action action5 = () => new DateTime().GenerateFromString("01.0.2000");
            Action action6 = () => new DateTime().GenerateFromString("01.01.0");
            //Assert
            Assert.Throws<ArgumentOutOfRangeException>(action1);
            Assert.Throws<ArgumentOutOfRangeException>(action2);
            Assert.Throws<ArgumentOutOfRangeException>(action3);
            Assert.Throws<ArgumentOutOfRangeException>(action4);
            Assert.Throws<ArgumentOutOfRangeException>(action5);
            Assert.Throws<ArgumentOutOfRangeException>(action6);
        }
    }
}
