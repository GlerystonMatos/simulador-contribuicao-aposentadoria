using NUnit.Framework;
using SimuladorContribuicaoAposentadoria.Data.Repositories;
using SimuladorContribuicaoAposentadoria.Domain.Entities;
using SimuladorContribuicaoAposentadoria.Domain.Interfaces.Data;
using SimuladorContribuicaoAposentadoriaNUnitTest.Comum;

namespace SimuladorContribuicaoAposentadoriaNUnitTest.Repositories
{
    public class UsuarioRepositoryTest
    {
        private IUsuarioRepository _usuarioRepository;

        public UsuarioRepositoryTest()
            => _usuarioRepository = new UsuarioRepository(UtilitariosTest.GetContext(), UtilitariosTest.GetMapper());

        [Test]
        public void CreateTest()
        {
            Usuario usuario = new Usuario("Usuario Teste Create", "123");
            _usuarioRepository.Create(usuario);

            Assert.IsNotNull(_usuarioRepository.FindById(usuario.Id));
        }

        [Test]
        public void UpdateTest()
        {
            Usuario usuario = new Usuario("Usuario Teste Update", "123");
            _usuarioRepository.Create(usuario);

            usuario.Nome = "Usuario Atualizado";
            _usuarioRepository.Update(usuario);

            Assert.IsNotNull(_usuarioRepository.FindById(usuario.Id).Nome.Equals("Usuario Atualizado"));
        }

        [Test]
        public void RemoveTest()
        {
            Usuario usuario = new Usuario("Usuario Teste Remove", "123");
            _usuarioRepository.Create(usuario);

            _usuarioRepository.Remove(usuario);
            Assert.IsNull(_usuarioRepository.FindById(usuario.Id));
        }

        [Test]
        public void GetAllTest()
            => Assert.IsNotNull(_usuarioRepository.GetAll());

        [Test]
        public void FindByIdTest()
        {
            Usuario usuario = new Usuario("Usuario Teste FindById", "123");
            _usuarioRepository.Create(usuario);

            Assert.IsNotNull(_usuarioRepository.FindById(usuario.Id));
        }

        [Test]
        public void FindByNomeSenhaTest()
        {
            Usuario usuario = new Usuario("Usuario Teste FindByNomeSenhaTest", "123");
            _usuarioRepository.Create(usuario);

            Assert.IsNotNull(_usuarioRepository.FindByNomeSenha(usuario.Nome, usuario.Senha));
        }

        [Test]
        public void FindByNomeTest()
        {
            Usuario usuario = new Usuario("Usuario Teste FindByNomeTest", "123");
            _usuarioRepository.Create(usuario);

            Assert.IsNotNull(_usuarioRepository.FindByNome(usuario.Nome));
        }
    }
}