using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using TestModel;

namespace TestModel_Tests
{
    [TestClass]
    public class When_an_order_is_to_be_written_to_disk
    {
        private Order order;
        private Mock<IFileWriter> mockFileWriter;
        private OrderWriter orderWriter;

        [TestMethod]
        public void TestWriting()
        {
            order = new Order();
            order.OrderId = 1001;
            order.OrderTotal = 10.53M;

            mockFileWriter = new Mock<IFileWriter>();
            orderWriter = new OrderWriter(mockFileWriter.Object);

            orderWriter.WriteOrder(order);

            mockFileWriter.Verify(fw => fw.WriteLine("1001,10.53"), Times.Exactly(1));
        }
    }
}
