// Ignore Spelling: SDM

namespace Skyline.DataMiner.SDM.Ticketing.Storage
{
    using System;
    using System.Collections.Generic;

    using Skyline.DataMiner.Net.Messages.SLDataGateway;

    public interface IStorageProvider<T>
    {
        T Create(T oToCreate);

        IEnumerable<T> Read(FilterElement<T> filter);

        T Update(T oToUpdate);

        T Delete(T oToDelete);
    }
}
