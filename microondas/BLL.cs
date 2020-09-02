using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace microondas
{
    public class BLL
    {
        public string tempo;
        public int potencia = 10;
        public int programacao = 0;
        public string minutos = "00";
        public string segundos = "00";

        int TimerM = 0;
        int TimerS = 0;

        frmMicroondas microondas;        
        
        #region criação da lista com os programas de aquecimento pré definidos
        /// <summary>
        /// contem uma lista de 5 itens com a programação já pre definida  para diferentes tipo de alimentos/preparos
        /// </summary>
        List<Programas> listaProgramas = new List<Programas>()
        {
            new Programas(){Nome = "Pipoca", instrucao="Coloque a embalagem aberta e aperte iniciar", minutos = 1, segundos = 0, potencia = 4},
            new Programas(){Nome ="Carne", instrucao="Coloque a carne dentro do micro e aperte iniciar", minutos = 2, segundos = 0, potencia = 6},
            new Programas(){Nome = "Frango", instrucao="Colocque o frango dentro do ambiente e aperte iniciar", minutos = 1, segundos = 30, potencia = 6},
            new Programas(){Nome = "Descongelar", instrucao="Colocque a ser descongelado dentro de um recipiente e aperte iniciar", minutos = 2, segundos = 0, potencia = 3},
            new Programas(){Nome=  "Massas", instrucao = "Coloque a massas dentro e aperte  iniciar", minutos = 1, segundos = 30, potencia = 5}
        };
        #endregion

        #region controla o tempo digitado pelo usuario 
        /// <summary>
        /// faz o controle  do tempo digitado, pegas os valores e vai atribuindo o mesmo da direita para esquerda, assumindo que o usuario começa digitando 
        /// valores pelos segundos
        /// </summary>
        /// <param name="Valor"> valor que o usuario digitou no display </param>
        /// <returns> retorna os valores digitado agrupando o mesmo com outro valores digitado anteriormente</returns>
        public string tempoAdd(int Valor)
        {
            tempo = tempo + Valor.ToString();
            var teste = tempo.PadLeft(4, '0');

            segundos = teste.Substring((teste.Length - 2), 2);
            minutos = teste.Substring(0, 2);

             return exibirTempo(minutos, segundos);
        }
        #endregion

        #region exibe o valor do visor
        /// <summary>
        /// metodo utilizado para formatar o tempo
        /// </summary>
        /// <param name="minuto"> deve ser informado o valor  do tempo em minutos</param>
        /// <param name="segundo">deve ser informado o valor  do tempo em segundos</param>
        /// <returns> retorna uma string  contendo o tempo já formatado com  ":" </returns>
        public string exibirTempo(string minuto, string segundo)
        {
            return string.Format("{0}:{1}", minuto, segundo);
        }
        #endregion

        #region método para exibir a mensagem  enviado de outras chamadas
        /// <summary>
        /// metodo para exibir mensagens, separando a mesma
        /// </summary>
        /// <param name="Mensagem"> paramentro utilizado para informar qual a mensasem será usada</param>
        /// <returns>faz um controle de qual mensagem será retornada, porem se for alguma diferente de aquecida será exibida
        /// e depois reornar ao tempo setado no display</returns>
        public string exibirMensagem(string Mensagem)
        {
            microondas = new frmMicroondas();            
            
            if (!Mensagem.Equals("Aquecida"))
            {
                return Mensagem;
            }
            else
            {
                return Mensagem;
                exibirTempo(minutos, segundos);
                Thread.Sleep(2000);
            }
        }
        #endregion

        #region metodo para  verificar se no segundo click o timer estiver parado e zerar todos os paramentros
        /// <summary>
        /// método que auxilia na  no controle de tempo pausado fazendo com que o usuario possa dar continuidade no tempo em
        /// andamento ou  cancelar todo o tempo restante
        /// </summary>
        /// <param name="cancel"> paramento que é pego do timer do formulario, se mo mesmo estiver ativo(rodando) não é zerado os campos
        /// já se o mesmo estiver parado, ele irar zerar todos os valores</param>
        /// <returns> retorna uma falso para o timer, idenpedente se é para pausar ou cancelar </returns>
        public bool cancelarEvento(bool cancel)
        {
            if (!cancel)
            {
                tempo = "";
                minutos = "00";
                segundos = "00";                
            }
            return false;
        }
        #endregion

        #region metodo para poder montar a potencia do micoondas para ser exibida
        /// <summary>
        /// metodo que pega a potencia  em valor numero e transforma a mesma para texto a ser exibida no display
        /// </summary>
        /// <param name="potencia"> valor referente a potencia a ser aplicada no display, sendo 1 o valor minumo e 10 o maximo</param>
        /// <returns> retorna uma sring  contendo (.) ponto, com a quantidade de pontos faz referencia a potencia aplicada</returns>
        public string ExibirPotencia(int potencia)
        {
            string Potencia = string.Empty;
            for (int i = 0; i < potencia; i++)
            {
                Potencia = Potencia + " .";
            }
            return Potencia;
        }
        #endregion

        #region metodo para startar a contagem de tempo
        /// <summary>
        /// pega o tempo da variavel minutos e segundo e atribua a mesma para outras variaveis que farão o controle do tempo
        /// </summary>
        /// <returns>
        /// esse metodo tbm retorna um booleano verdadeiro para que seja feita a inicialização do timer  que está no formulario
        /// </returns>
        public bool IniciarContagem()
        {
            TimerM = int.Parse(minutos);
            TimerS = int.Parse(segundos);

            return true;            
        }
        #endregion

        #region metodo que pega o programa de aquecimento pre estabelicido 
        /// <summary>
        /// metodo que pegas as listas de programação de aquecimento pre existente e separa os valores  correspondente a cada campo
        /// para definir qual o item da lista o metodo utilizará  o valor de uma variavel programacao
        /// </summary>
        /// <returns>retornar um objeto da classe Programas  contendo os atributos que  foram retirados da lista</returns>
        public Programas exibirProgramas()
        {
            Programas p = new Programas();
            if (programacao >= (listaProgramas.Count()))
            {
                programacao = 0;
            }
            p.Nome = listaProgramas[programacao].Nome;
            p.potencia = listaProgramas[programacao].potencia;
            p.instrucao = listaProgramas[programacao].instrucao;
            p.minutos = listaProgramas[programacao].minutos;
            p.segundos = listaProgramas[programacao].segundos;
            programacao++;
            return p;
        }
        #endregion

        #region comando para dar o sinal sonoro das teclas
        /// <summary>
        /// metodos que são utilizado para fazer o sinal sonoro quando as teclas do painel são precionadas,
        /// o beepDiff, tem dois paramentros devido ao seu som que pode ser perssonalizado
        /// </summary>
        public void Beep()
        {
            Console.Beep();
        }
        public void BeepDiff(int a, int b)
        {
            Console.Beep(a, b);
        }
        #endregion

        #region metodo para fazer o controle de tempo regrecivo
        /// <summary>
        /// esse metodo está vinculado ao timer do formulario, onde ele faz o controle de minutos e segundo  fazendo que todas vez
        /// que acabe um minuto os segundos recebam  59 segundo para continuar o timer, caso ele zere o cronometro irar retornar uma string
        /// informando a mensagem de aquecida
        /// </summary>
        /// <returns> deve retornar uma string  contendo o tempo restante para o timer zerar </returns>
        public string timer()
        {
            string retorno;
            if (TimerS <= 0 && TimerM >= 1)
            {
                TimerM--;
                TimerS = 59;
            }

            minutos = TimerM.ToString().PadLeft(2, '0');
            segundos = TimerS.ToString().PadLeft(2, '0');
            if (TimerM == 0 && TimerS == 0)
            {
                Console.Beep(1000, 1000);
                retorno = exibirMensagem("Aquecida");
            }
            else
            {
                retorno = exibirTempo(TimerM.ToString().PadLeft(2, '0'), TimerS.ToString().PadLeft(2, '0'));
            }
            TimerS--;
            return retorno;

        }
        #endregion

        #region  metodo para o controlar e gerenciar a potencia do microondas
        ///<sumary>
        ///essa clase irar receber a chamada sem nenhum parametro para fazer o controle do nivel de potecia que o microondas irar usar
        ///caso o mesmo for ultrapassar uma valor maior que 10 ele faz o controle para o mesmo retornar para 1
        ///</sumary>
        public string Potencia()
        {
            if (potencia == 10)
            {
                potencia = 1;
            }
            else
            {
                potencia += 1;
            }

            return ExibirPotencia(potencia);
        }
        #endregion

        #region metodo para inicializar a contagem e com o inicio rapido de 30 segundo
        /// <summary>
        /// esse metodo é utilizado para inicializar a contagem regesiva do tempo, caso o usuario precione iniciar sem o tempo ele irar
        /// adotar  a potencia 8 para aquecer e 30 segundo de tempo maximo
        /// </summary>
        /// <param name="timer"> o timer irar pegar o valor que está no display </param>
        /// 
        /// <returns> ira retornar  o tempo definido em uma string para que seja exibido na tela</returns>
        public string inicializar(string timer)
        {
            int tempo;
            int MinutoAdd = Convert.ToInt32((timer.ToString()).Substring(0,2));
            minutos = timer.ToString().Substring(0, 2);
            segundos = timer.ToString().Substring(3, 2);
            string temp = timer;
            string retorno = string.Empty;

            if (timer.Equals("00:00"))
            {
                segundos = "30";
            }

            if (Convert.ToInt32(segundos) >= 60 || Convert.ToInt32(minutos) >= 2)
            {
                MinutoAdd += (Convert.ToInt32(segundos) / 60);
                segundos = (Convert.ToInt32(segundos) % 60).ToString();
                if (MinutoAdd >= 2)
                {
                    retorno ="Tempo Invalido";
                }
                else
                {
                    minutos = MinutoAdd.ToString().PadLeft(2, '0');
                    retorno = exibirTempo(MinutoAdd.ToString().PadLeft(2, '0'), segundos);
                }
            }
            else
            {
                minutos = MinutoAdd.ToString().PadLeft(2, '0');
                retorno =  exibirTempo(MinutoAdd.ToString().PadLeft(2, '0'), segundos);
            }
            programacao = 0;
            return retorno;
        }
        #endregion

    }
}
