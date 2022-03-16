using System.Collections.Generic;

namespace RestWithASPNETUdemy.Hypermedia.Abstract
{
    public interface ISupportsHyperMedia
    {
        //List<Hypermedia> Links { get; set }
        List<HyperMediaLink> Links { get; set; }
    }
}
