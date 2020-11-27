using System;
using System.Collections.Generic;
using System.Text;

namespace ElasticSearch.Helpers
{
    public enum Index
    {

        [Value("bonus-system-data")]
        Data,
        [Value("bonus-system-service")]
        Service,
        [Value("bonus-system-core")]
        Core
    }
}
