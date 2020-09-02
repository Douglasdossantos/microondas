using System;
using Xunit;
using microondas;

namespace Microondas.Test
{
    
    public class MicroondasTest1
    {
        BLL Test = new BLL();

        [Fact]
        public void testCancelarEvento()
        {
            Assert.Equal(false, Test.cancelarEvento(false));
        }
        [Fact]
        public void testIniciarContagem()
        {
            Assert.Equal(true, Test.IniciarContagem());
        }

        [Fact]
        public void testInicializãcaoRapida()
        {
            Assert.Equal(Test.inicializar("00:00"), "00:30");
        }


        [Theory]
        [InlineData(" . . . . . . . . . .", 10)]
        [InlineData(" . . . . . . . . .", 9)]
        [InlineData(" . . . . . . . .", 8)]
        [InlineData(" . . . . . . .", 7)]
        [InlineData(" . . . . . .", 6)]
        [InlineData(" . . . . .", 5)]
        [InlineData(" . . . .", 4)]
        [InlineData(" . . .", 3)]
        [InlineData(" . .", 2)]
        [InlineData(" .", 1)]
        public void testPotencia(string potencia, int valor)
        {
            Assert.Equal(potencia, Test.ExibirPotencia(valor));
        }

        [Fact]
        public void testExibirMensagem()
        {
            Assert.Equal("Test", Test.exibirMensagem("Test"));
        }
        [Fact]
        public void testExibirTempo()
        {
            Assert.Equal(Test.exibirTempo("01", "00"), "01:00");
        }
    }
}
