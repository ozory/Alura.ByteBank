using Alura.ByteBank.Dados.Repositorio;
using Alura.ByteBank.Dominio.Entidades;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Alura.ByteBank.Infraestrutura.Testes
{
    public class AgenciaRepositorioTestes
    {
        private AgenciaRepositorio _repositorio;

        [Fact]
        public void TestaObterTodasAgencias( )
        {
            //Arrange
            _repositorio = new AgenciaRepositorio();

            //Act
            List<Agencia> lista = _repositorio.ObterTodos();

            //Assert
            Assert.NotNull(lista);
        }

        [Fact]
        public void TestaObterAgenciaPorId()
        {
            //Arrange
            _repositorio = new AgenciaRepositorio();

            //Act
            var agencia = _repositorio.ObterPorId(1);

            //Assert
            Assert.NotNull(agencia);

        }

        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        public void TestaObterAgenciasPorVariosId(int id)
        {
            //Arrange
            _repositorio = new AgenciaRepositorio();

            //Act
            var agencia = _repositorio.ObterPorId(id);

            //Assert
            Assert.NotNull(agencia);

        }
        [Fact]
        public void TesteInsereUmaNovaAgenciaNaBaseDeDados()
        {
            //Arrange            
            string nome = "Agencia Guarapari";
            int numero = 125982;
            Guid identificador = Guid.NewGuid();
            string endereco = "Rua: 7 de Setembro - Centro";
            _repositorio = new AgenciaRepositorio();

            var agencia = new Agencia()
            {
                Nome = nome,
                Identificador = identificador,            
                Endereco = endereco,
                Numero = numero
            };

            //Act
            var retorno = _repositorio.Adicionar(agencia);

            //Assert
            Assert.True(retorno);
        }

        [Fact]
        public void TestaAtualizacaoInformacaoDeterminadaAgencia()
        {
            //Arrange
            _repositorio = new AgenciaRepositorio();
            var agencia = _repositorio.ObterPorId(2);
            var nomeNovo = "Agencia Nova";
            agencia.Nome = nomeNovo;

            //Act
            var atualizado = _repositorio.Atualizar(2, agencia);

            //Assert
            Assert.True(atualizado);
        }

        [Fact]
        public void TestaRemoverInformacaoDeterminadaAgencia()
        {
            //Arrange
            _repositorio = new AgenciaRepositorio();
    

            //Act
            var atualizado = _repositorio.Excluir(3);

            //Assert
            Assert.True(atualizado);
        }

        //Exceções
        [Fact]
        public void TestaExcecaoConsultaPorAgenciaPorId()
        {

            //Act
            _repositorio = new AgenciaRepositorio();
            //Assert
            Assert.Throws<FormatException>(
                () => _repositorio.ObterPorId(33)
             );

        }


        // Testes com Mock
        [Fact]
        public void TestaObterAgenciasMock()
        {
            //Arange
            var bytebankRepositorioMock = new Mock<IByteBankRepositorio>();
            var mock = bytebankRepositorioMock.Object;

            //Act
            var lista = mock.BuscarAgencias();

            //Assert
            bytebankRepositorioMock.Verify(b => b.BuscarAgencias());
        }

        [Fact]
        public void TestaAdiconarAgenciaMock()
        {
            // Arrange
            var agencia = new Agencia()
            {
                Nome = "Agência Amaral",
                Identificador = Guid.NewGuid(),
                Id = 4,
                Endereco = "Rua Arthur Costa",
                Numero = 6497
            };

            var repositorioMock = new ByteBankRepositorio();

            //Act
            var adicionado = repositorioMock.AdicionarAgencia(agencia);

            //Assert
            Assert.True(adicionado);
        }
    }
}
