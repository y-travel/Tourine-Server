using ServiceStack.DataAnnotations;

namespace Tourine.ServiceInterfaces.Models
{
    public class Currency
    {
        [AutoIncrement]
        public int Id { get; set; }
        public string Name { get; set; }
        public float Factor { get; set; }
    }
}
