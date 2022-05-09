namespace Rabbb.Functional
{
    public class POI<T , F>
    {
        public T? Resolve { get; set; }

        public F? Reject { get; set; }
    }
}