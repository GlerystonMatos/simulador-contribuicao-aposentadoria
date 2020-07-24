using AutoMapper;
using Moq;
using NUnit.Framework;
using SimuladorContribuicaoAposentadoria.Domain.Dto;
using SimuladorContribuicaoAposentadoria.Domain.Entities;
using SimuladorContribuicaoAposentadoria.Domain.Exception;
using SimuladorContribuicaoAposentadoria.Domain.Interfaces.Data;
using SimuladorContribuicaoAposentadoria.Domain.Interfaces.Services;
using SimuladorContribuicaoAposentadoria.Service.Services;
using SimuladorContribuicaoAposentadoriaNUnitTest.Comum;
using System.Collections.Generic;
using System.Linq;

namespace SimuladorContribuicaoAposentadoriaNUnitTest.Services
{
    public class UsuarioServiceTest
    {
        private Mock<IUsuarioRepository> _mockUsuarioRepository;
        private IUsuarioService _usuarioService;
        private IMapper _mapper;

        public UsuarioServiceTest()
        {
            _mapper = UtilitariosTest.GetMapper();
            _mockUsuarioRepository = new Mock<IUsuarioRepository>();
            _usuarioService = new UsuarioService(_mapper, _mockUsuarioRepository.Object);
        }

        [Test]
        public void CreateTest()
            => Assert.DoesNotThrow(() => _usuarioService.Create(new UsuarioDto("Usuario Teste Create", "123")));

        [Test]
        public void CreateTestException()
        {
            UsuarioDto usuarioDto = new UsuarioDto("Usuario Teste", "123");
            _mockUsuarioRepository.Setup(r => r.FindByNome(usuarioDto.Nome)).Returns(usuarioDto);
            Assert.Throws<SimuladorContribuicaoAposentadoriaException>(() => _usuarioService.Create(new UsuarioDto("Usuario Teste", "123")), "Usuário já cadastrado");
        }

        [Test]
        public void UpdateTest()
            => Assert.DoesNotThrow(() => _usuarioService.Update(new UsuarioDto("Usuario Teste Update", "132")));

        [Test]
        public void RemoveTest()
            => Assert.DoesNotThrow(() => _usuarioService.Remove(new UsuarioDto("Usuario Teste Remove", "123")));

        [Test]
        public void GetAllTest()
        {
            Usuario usuario = new Usuario("Usuario Teste", "123");
            IList<Usuario> usuariosList = new List<Usuario>();
            usuariosList.Add(usuario);

            _mockUsuarioRepository.Setup(r => r.GetAll()).Returns(usuariosList.AsQueryable());
            Assert.IsNotNull(_usuarioService.GetAll());
        }

        [Test]
        public void FindByIdTest()
        {
            UsuarioDto usuarioDto = new UsuarioDto("Usuario Teste", "123");
            _mockUsuarioRepository.Setup(r => r.FindById(usuarioDto.Id)).Returns(usuarioDto);
            Assert.IsNotNull(_usuarioService.FindById(usuarioDto.Id));
        }

        [Test]
        public void FindByNomeSenhaTest()
        {
            UsuarioDto usuarioDto = new UsuarioDto("Usuario Teste", "123");
            _mockUsuarioRepository.Setup(r => r.FindByNomeSenha(usuarioDto.Nome, usuarioDto.Senha)).Returns(usuarioDto);
            Assert.IsNotNull(_usuarioService.FindByNomeSenha(usuarioDto.Nome, usuarioDto.Senha));
        }

        [Test]
        public void FindByNomeTest()
        {
            UsuarioDto usuarioDto = new UsuarioDto("Usuario Teste", "123");
            _mockUsuarioRepository.Setup(r => r.FindByNome(usuarioDto.Nome)).Returns(usuarioDto);
            Assert.IsNotNull(_usuarioService.FindByNome(usuarioDto.Nome));
        }
    }
}