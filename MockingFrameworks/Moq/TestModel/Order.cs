using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestModel
{
    public class Order
    {
        public int OrderId { get; set; }
        public decimal OrderTotal { get; set; }
    }

    public interface IFileWriter
    {
        void WriteLine(string line);
    }

    public class OrderWriter
    {
        private readonly IFileWriter fileWriter;

        public OrderWriter(IFileWriter fileWriter)
        {
            this.fileWriter = fileWriter;
        }

        public void WriteOrder(Order order)
        {
            fileWriter.WriteLine(String.Format("{0},{1}", order.OrderId, order.OrderTotal));
        }
    }
}
