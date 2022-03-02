using RestWithASPNETUdemy.Hypermedia;
using RestWithASPNETUdemy.Hypermedia.Abstract;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace RestWithASPNETUdemy.Data.VO
{
    public class PersonVO: ISupportsHyperMedia
    {   
        //[JsonPropertyName("code")]
        public  long Id { get; set; }
        //[JsonPropertyName("Nome")]
        public string FirstName { get; set; }
        //[JsonPropertyName("Sobrenome")]
        public string LastName { get; set; }
        //[JsonPropertyName("Endereco")]
        public string Address { get; set; }
        //[JsonIgnore] //EXCLUI DA VISUALIZACAO
        public string Gender { get; set; }
        public List<HyperMediaLink> Links { get; set; } = new List<HyperMediaLink>();
    }
}
