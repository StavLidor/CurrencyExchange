using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Shard
{
    public class Consts
    {
        public const string NAME_DATA_BASE = "myapp.db";
        public const string CONNECTION_APP_DATA_BASE = $"Data Source=../{Consts.NAME_DATA_BASE}";
    }
}
