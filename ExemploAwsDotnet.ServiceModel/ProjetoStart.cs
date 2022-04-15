using ServiceStack;

namespace ExemploAwsDotnet.ServiceModel
{
    public class ProjetoStart : IReturn<ProjetoStartResponse>
    {
        public string Name { get; set; }
    }

    public class ProjetoStartResponse
    {
        public string Result { get; set; }
        public ResponseStatus ResponseStatus { get; set; }
    }
}
