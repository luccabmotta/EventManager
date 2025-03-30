namespace EventManagementAPI.Models
{
    public class ResourceWithLinks<T>
    {
        public T Data { get; set; }
        public List<Link> Links { get; set; } = new();

        public ResourceWithLinks(T data)
        {
            Data = data;
        }
    }

    public class Link
    {
        public string Href { get; set; }
        public string Rel { get; set; }
        public string Method { get; set; }

        public Link(string href, string rel, string method)
        {
            Href = href;
            Rel = rel;
            Method = method;
        }
    }

}
