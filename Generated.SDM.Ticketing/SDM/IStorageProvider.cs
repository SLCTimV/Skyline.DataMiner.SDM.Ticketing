namespace Skyline.DataMiner.SDM
{
    using System;
    using System.Collections.Generic;

    using Skyline.DataMiner.Net.Messages.SLDataGateway;
    using Skyline.DataMiner.SDM;

    public interface IStorageProvider<T>
    {
        T Create(T oToCreate);
        IEnumerable<T> Read(FilterElement<T> filter);
        T Update(T oToUpdate);
        T Delete(T oToDelete);
    }
}