using System;
using System.Collections.Generic;
using System.Text;

namespace ElasticSearch.Helpers
{
    public enum TypeEvent
    {
        [Value("Error")]
        Error,
        [Value("Warning")]
        Warning,
        [Value("Information")]
        Information,
    }
}
