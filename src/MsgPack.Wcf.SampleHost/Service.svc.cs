using System;
using System.Collections.Generic;

namespace MsgPack.Wcf.SampleHost
{
    public class Service : IService
    {
        public List<CompositeType> GetCollectionUsingDataContract(CompositeType composite)
        {
            var list = new List<CompositeType>();
            for (int i = 0; i < 10; i++)
            {
                list.Add(composite);
            }

            return list;
        }

        public string GetData(int value)
            => $"You entered: {value}";

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException(nameof(composite));
            }

            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }

            return composite;
        }
    }
}
