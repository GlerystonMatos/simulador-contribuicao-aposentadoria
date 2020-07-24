using AutoMapper;
using SimuladorContribuicaoAposentadoria.Domain.Dto;
using SimuladorContribuicaoAposentadoria.Domain.Entities;

namespace SimuladorContribuicaoAposentadoria.Service.AutoMapper
{
    public class AutoMapping : Profile
    {
        public AutoMapping()
        {
            CreateMap<InformacoesDto, SimulacaoDto>()
                .ForMember(s => s.Usuario, opt => opt.Ignore());

            CreateMap<Simulacao, SimulacaoDto>()
                .ReverseMap();

            CreateMap<Resultado, ResultadoDto>()
                .ReverseMap();

            CreateMap<Usuario, UsuarioDto>()
                .ReverseMap();
        }
    }
}