using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace microondas
{
    public partial class frmMicroondas : Form
    {

        string texto = string.Empty;

        BLL bll = new BLL();    

        public frmMicroondas()
        {
            InitializeComponent();
        }
        #region metodo load do formulario
        /// <summary>
        /// load do formulario quando o mesmo iniciado carregas as primeiras instruções
        /// </summary>
        private void frmMicroondas_Load(object sender, EventArgs e)
        {
            txtPotencia.Text = bll.ExibirPotencia(10); 
            txtTimer.Text = bll.exibirTempo("00", "00");
        }
        #endregion

        #region metodo para poder tratar o digito
        /// <summary>
        /// criado esse evento para pode facilitar a manipulação de dados onde ele 
        /// recebe qual tecla o usuario teclou e a manda para a classe de negocios
        /// </summary>
        /// <param name="i"></param>
        private void tempo(int i)
        {
            txtTimer.Text = bll.tempoAdd(i);
        }
        #endregion

        #region evento click dos botoes
        /// <summary>
        /// classe dos eventos do botoes numericos
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            tempo(1);
            bll.Beep();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            tempo(2);
            bll.Beep();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            tempo(3);
            bll.Beep();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            tempo(4);
            bll.Beep();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            tempo(5);
            bll.Beep();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            tempo(6);
            bll.Beep();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            tempo(7);
            bll.Beep();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            tempo(8);
            bll.Beep();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            tempo(9);
            bll.Beep();
        }

        private void button11_Click(object sender, EventArgs e)
        {
            tempo(0);
            bll.Beep();
        }
        #endregion

        #region botoes que possuem funçoes
       
        /// <summary>
        /// cotão cancelar e pausar
        ///  esse botão tem  dupla função de pausar o tempo/programação e de cancelar a mesma, quando o botão é precionado uma 
        ///  unica vez, o mesmo pausa o timer podendo continuar de onde parou apertando somente o iniciar, caso o botão de pausar /cancelar
        ///  seja precionado mais uma vez o mesmo irar cancelar toda a programação não podendo  dar continuidade da mesma como anterior
        /// </summary>
        private void button12_Click(object sender, EventArgs e)
        {
            bll.BeepDiff(2003, 700);
            txtTimer.Text = !timer1.Enabled ? "00:00" : texto;
            timer1.Enabled = bll.cancelarEvento(timer1.Enabled);
            bll.ExibirPotencia(10);            
        }        

        /// <summary>
        /// botão de inicializar e inicio rapido
        /// esse botão tem  por função  inicializar o timer do microondas que está seado no display, caso o usuario preciome o mesmo
        /// sem nehuma programação  ele iara definir automaticamente o tempo de 30 segundos e a potencia de numero 8
        /// </summary>
        private void btnInicio30Seg_Click(object sender, EventArgs e)
        {
            bll.BeepDiff(3000, 200);
            if (txtTimer.Text.Equals("00:00"))
            {                
                txtPotencia.Clear();
                txtPotencia.SelectedText = bll.ExibirPotencia(8);
            }
            if ("Tempo Invalido"== bll.inicializar(txtTimer.Text))
            {
                timer1.Enabled = false;
                txtTimer.Text = bll.inicializar(txtTimer.Text);
            }
            else
            {
                txtTimer.Text = bll.inicializar(txtTimer.Text);
                timer1.Enabled = bll.IniciarContagem();
            }
        }
        #endregion

        #region botão que faz o controle da potencia
        /// <summary>
        /// botão de potencia, esse botão cada vez que ele e´clicado acumula uma valor variando de 1 por 1
        /// fazendo com que o atinja o valor maximo de 10 e no prximo click retorne para o 1
        /// </summary>
        private void button14_Click(object sender, EventArgs e)
        {
            bll.BeepDiff(500,300);
            txtPotencia.Text =  bll.Potencia();
        }
        #endregion

        #region timer do relogio que está  contando de um segundo para a contagem regreciva 

        /// <summary>
        /// timer do relogio, esse metodo está no formulario para que o mesmo possa fazer o controle da contagem regreciva 
        /// do tempo que foi definido no display do relogio  e fazendo um aviso sonoro quando o mesmo se esgotar 
        /// </summary>

        private void timer1_Tick(object sender, EventArgs e)
        {            
            texto = bll.timer();
            txtTimer.Clear();
            if (texto == "Aquecida")
            {
                timer1.Enabled = bll.cancelarEvento(false);
                txtTimer.SelectedText = texto;
                Thread.Sleep(1500);
                txtTimer.Text = "00:00";
                bll.ExibirPotencia(10);
            }
            else
            {
                txtTimer.SelectedText = texto;
            }             
        }
        #endregion

        #region botão que controla a programação pre existente

        /// <summary>
        /// botão que faz a rolagem  sobre a programação pre existente
        /// esse botão  pega a lista e faz uma rolagem  de todas os itens cadastrados, separando a mesma para serem exibidas nos display
        /// exibirar qual o tipo de alimento e logo apos isso ele irar rolar as instruloes  de preparo do mesmo
        /// depois de ter rolado as instruçoes  exibirar o tempo de preparo recomendado e a potencia no display de baixo
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button15_Click(object sender, EventArgs e)
        {
            int inicio = 0;
            int final = 12;
            Programas p = new Programas();
            p = bll.exibirProgramas();

            txtTimer.Clear();
            Thread.Sleep(100);
            txtTimer.SelectedText = p.Nome;
            txtPotencia.Clear();
            Thread.Sleep(100);
            txtPotencia.SelectedText = bll.ExibirPotencia(p.potencia);
            Thread.Sleep(1500);
            for (int i = 0; i < (p.instrucao.Length - 12); i++)
            {
                if ((p.instrucao.Length - (i + 12)) < 0)
                {
                    final--;
                }
                else
                {
                    final = ((p.instrucao.Length - 12) >= 0) ? final : final--;
                }
                txtTimer.Clear();
                txtTimer.SelectedText = p.instrucao.Substring(inicio, final);
                inicio++;
                Thread.Sleep(900);
            }            
            txtTimer.Text = bll.exibirTempo(p.minutos.ToString().PadLeft(2, '0'), p.segundos.ToString().PadLeft(2, '0'));
        }
        #endregion
    }
}
