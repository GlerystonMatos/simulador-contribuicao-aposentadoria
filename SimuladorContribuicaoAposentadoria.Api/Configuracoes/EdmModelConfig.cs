using Microsoft.AspNet.OData.Builder;
using Microsoft.OData.Edm;
using SimuladorContribuicaoAposentadoria.Domain.Dto;

namespace SimuladorContribuicaoAposentadoria.Api.Configuracoes
{
    public class EdmModelConfig
    {
        public static IEdmModel GetEdmModel()
        {
            ODataConventionModelBuilder odataBuilder = new ODataConventionModelBuilder();

            odataBuilder.EntitySet<UsuarioDto>("Usuario");
            odataBuilder.EntitySet<SimulacaoDto>("Simulacao");            

            return odataBuilder.GetEdmModel();
        }
    }
}