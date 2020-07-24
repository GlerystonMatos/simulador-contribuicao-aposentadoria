using System.ComponentModel;

namespace SimuladorContribuicaoAposentadoria.Domain.Exception
{
    [DisplayName("Mensagem")]
    public class ExceptionMessage
    {
        public ExceptionMessage(string mensagem)
        {
            Mensagem = mensagem;
        }

        public string Mensagem { get; set; }
    }
}