using Newtonsoft.Json;

namespace Nft.App.Models
{
    [JsonArray]
    public class TransactionsList : List<Transaction>
    {
    }
}
