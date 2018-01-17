using ServiceStack.DataAnnotations;

namespace Tourine.ServiceInterfaces
{
    public class Currency
    {
        [AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public int Factor { get; set; }
    }
}
