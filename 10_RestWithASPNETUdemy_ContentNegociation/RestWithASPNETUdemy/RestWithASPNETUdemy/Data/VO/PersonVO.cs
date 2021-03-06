using System.Text.Json.Serialization;

namespace RestWithASPNETUdemy.Data.VO
{
    public class PersonVO
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
    }
}
