using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Media;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace jogo_da_velha
{
    public partial class MainWindow : Window
    {
        //Uma variavel que armazenasse o jogador da vez.
        string simboloJogadorDaVez = "X";
        //Uma variavel que diz qual jogador venceu.
        string jogadorVencedor = "";
        //Variavel que armazena o número de jogadas
        int numeroDeJogadas = 0;
        //Nome do jogador X informado na tela FrmMenu
        string nomeJogadorX;
        //Nome do jogador O informado na tela FrmMenu
        string nomeJogadorO;
        //Variavel que armazena a cor da vitória
        SolidColorBrush corDaVitoria = new SolidColorBrush(Color.FromArgb(255, 0, 255, 0));
        //Variavel que armazena a cor da derrota
        SolidColorBrush corDaVelha = new SolidColorBrush(Color.FromArgb(255, 255, 0, 0));
        //Variavel que armazena a cor de inicio dos botões
        SolidColorBrush corReset = new SolidColorBrush(Color.FromArgb(255, 115, 154, 183));

        public MainWindow(string nomeJogadorX, string nomeJogadorO)
        {
            //Metódo para construir dos elementos na tela
            InitializeComponent();

            //Deixa vazio o campo vencedor
            txtResultadoVencedor.Text = "";

            //Atribui valor da variavel temporária a variavel global
            this.nomeJogadorX = nomeJogadorX;

            //Atribui valor da variavel temporária a variavel global
            this.nomeJogadorO = nomeJogadorO;

            //Método para dizer o jogador da vez
            InformaJogadorDaVez(simboloJogadorDaVez);

            //Ocultando o botão de novo jogo
            btnNovoJogo.Visibility = Visibility.Hidden;
        }

        private void InformaJogadorDaVez(string simboloJogador)
        {
            //Operção ternária que retorna o nome do jogador.
            string nomeDoJogadorDaVez = simboloJogador == "X" ? nomeJogadorX : nomeJogadorO;
            //Informa o nome do jogador no text block
            txtInformaVezJogador.Text = $"É a vez do jogador(a) {nomeDoJogadorDaVez}";
        }

        private void IniciarNovoJogo(object sender, RoutedEventArgs e)
        {
            numeroDeJogadas = 0;
            btnA1.Content = "";
            btnA2.Content = "";
            btnA3.Content = "";
            btnB1.Content = "";
            btnB2.Content = "";
            btnB3.Content = "";
            btnC1.Content = "";
            btnC2.Content = "";
            btnC3.Content = "";
            PintarBotoesReset();
            txtResultadoVencedor.Text = "";
            if (jogadorVencedor != "")
            {
                simboloJogadorDaVez = jogadorVencedor;
                jogadorVencedor = "";
            }
            InformaJogadorDaVez(simboloJogadorDaVez);
            //Oculta o botão
            btnNovoJogo.Visibility = Visibility.Hidden;
        }

        private void TrocarJogadorDaVez()
        {
            //Verifico qual jogador atual
            if (simboloJogadorDaVez == "X")
            {
                //Informo na variavel que o jogador agora será outro
                simboloJogadorDaVez = "O";
            }
            //Se não X o jogador, será O logo deve mudar o jogador da vez para O
            else
            {
                simboloJogadorDaVez = "X";
            }
            //Informo o jogador da vez na tela
            InformaJogadorDaVez(simboloJogadorDaVez);
        }

        private void SelecionarBtnA1(object sender, RoutedEventArgs e)
        {
            PreencheBotaoDaVezDoJogador(btnA1);
        }

        private void SelecionarBtnA2(object sender, RoutedEventArgs e)
        {
            PreencheBotaoDaVezDoJogador(btnA2);
        }

        private void SelecionarBtnA3(object sender, RoutedEventArgs e)
        {
            PreencheBotaoDaVezDoJogador(btnA3);
        }

        private void SelecionarBtnB1(object sender, RoutedEventArgs e)
        {
            PreencheBotaoDaVezDoJogador(btnB1);
        }

        private void SelecionarBtnB2(object sender, RoutedEventArgs e)
        {
            PreencheBotaoDaVezDoJogador(btnB2);
        }

        private void SelecionarBtnB3(object sender, RoutedEventArgs e)
        {
            PreencheBotaoDaVezDoJogador(btnB3);
        }

        private void SelecionarBtnC1(object sender, RoutedEventArgs e)
        {
            PreencheBotaoDaVezDoJogador(btnC1);
        }

        private void SelecionarBtnC2(object sender, RoutedEventArgs e)
        {
            PreencheBotaoDaVezDoJogador(btnC2);
        }

        private void SelecionarBtnC3(object sender, RoutedEventArgs e)
        {
            PreencheBotaoDaVezDoJogador(btnC3);
        }

        private void PreencheBotaoDaVezDoJogador(Button botaoApertado)
        {
            //Verifico se houve um vencedor e se houve menos que 9 jogadas 
            if (jogadorVencedor == "" && numeroDeJogadas < 9)
            {
                //Verifico se o texto do botão está vazio
                if (botaoApertado.Content.ToString() == "")
                {
                    //Preencho o texto do botão com o jogador da vez
                    botaoApertado.Content = simboloJogadorDaVez;

                    //Computo uma jogada.
                    numeroDeJogadas++;

                    // Verifica se houve jogador após a jogada
                    VerificaVencedor();

                    // Verifico se ainda não houve vencedor
                    if (jogadorVencedor == "")
                    {
                        //Troco a vez do jogador
                        TrocarJogadorDaVez();
                    }
                }
            }
        }

        private void VerificaVencedor()
        {
            // Verifico se a coluna esquerda houve vencedor
            if (btnA1.Content.ToString() == simboloJogadorDaVez && btnA2.Content.ToString() == simboloJogadorDaVez && btnA3.Content.ToString() == simboloJogadorDaVez)
            {
                //Chama o método para pintar os botões
                PintarBotoesVencedor(btnA1, btnA2, btnA3);
                // Atribuo o jogador da vez a variavel que informa o vencedor
                jogadorVencedor = simboloJogadorDaVez;
            }
            // Verifico se a coluna do meio houve vencedor
            else if (btnB1.Content.ToString() == simboloJogadorDaVez && btnB2.Content.ToString() == simboloJogadorDaVez && btnB3.Content.ToString() == simboloJogadorDaVez)
            {
                PintarBotoesVencedor(btnB1, btnB2, btnB3);
                jogadorVencedor = simboloJogadorDaVez;
            }
            // Verifico se a coluna direita houve vencedor
            else if (btnC1.Content.ToString() == simboloJogadorDaVez && btnC2.Content.ToString() == simboloJogadorDaVez && btnC3.Content.ToString() == simboloJogadorDaVez)
            {
                PintarBotoesVencedor(btnC1, btnC2, btnC3);
                jogadorVencedor = simboloJogadorDaVez;
            }
            // Verifico se a linha superior houve vencedor
            else if (btnA1.Content.ToString() == simboloJogadorDaVez && btnB1.Content.ToString() == simboloJogadorDaVez && btnC1.Content.ToString() == simboloJogadorDaVez)
            {
                PintarBotoesVencedor(btnA1, btnB1, btnC1);
                jogadorVencedor = simboloJogadorDaVez;
            }
            // Verifico se a linha do meio houve vencedor
            else if (btnA2.Content.ToString() == simboloJogadorDaVez && btnB2.Content.ToString() == simboloJogadorDaVez && btnC2.Content.ToString() == simboloJogadorDaVez)
            {
                PintarBotoesVencedor(btnA2, btnB2, btnC2);
                jogadorVencedor = simboloJogadorDaVez;
            }
            // Verifico se a linha inferior houve vencedor
            else if (btnA3.Content.ToString() == simboloJogadorDaVez && btnB3.Content.ToString() == simboloJogadorDaVez && btnC3.Content.ToString() == simboloJogadorDaVez)
            {
                PintarBotoesVencedor(btnA3, btnB3, btnC3);
                jogadorVencedor = simboloJogadorDaVez;
            }
            // Verifico se a diagonal esquerda houve vencedor
            else if (btnA1.Content.ToString() == simboloJogadorDaVez && btnB2.Content.ToString() == simboloJogadorDaVez && btnC3.Content.ToString() == simboloJogadorDaVez)
            {
                PintarBotoesVencedor(btnA1, btnB2, btnC3);
                jogadorVencedor = simboloJogadorDaVez;
            }
            // Verifico se a diagonal direita houve vencedor
            else if (btnC1.Content.ToString() == simboloJogadorDaVez && btnB2.Content.ToString() == simboloJogadorDaVez && btnA3.Content.ToString() == simboloJogadorDaVez)
            {
                PintarBotoesVencedor(btnC1, btnB2, btnA3);
                jogadorVencedor = simboloJogadorDaVez;
            }
            // Se não houver vencedor, será velha.
            else
            {
                //Informo que deu velha o jogo.
                InformaVelha();
            }

            //Informo se houve vencedor
            InformaVencedorSeHouver();
        }

        private void InformaVelha()
        {
            // Verifico se todos os botões foram jogados
            if (numeroDeJogadas == 9)
            {
                // Chama o método para pintar os botões que resultou em velha
                PintarBotoesDeVelha();
                // Informo no campo que houve velha
                txtResultadoVencedor.Text = "Deu velha!";
                //Emite o da Derrota
                SomDaDerrota();
                //Exibe o botão.
                btnNovoJogo.Visibility = Visibility.Visible;
            }
        }

        private void InformaVencedorSeHouver()
        {
            // Verifico se a variavel do vencedor foi preenchida
            if (jogadorVencedor != "")
            {
                // Retorna o nome do jogador vencedor para a variavel
                string nomeJogadorVencedor = jogadorVencedor == "X" ? nomeJogadorX : nomeJogadorO;
                // Informo qual jogador que ganhou
                txtResultadoVencedor.Text = $"Jogador(a) {nomeJogadorVencedor} venceu!";
                //Emite o Som da vitória
                SomDaVitoria(); 
                //Exibe o botão.
                btnNovoJogo.Visibility = Visibility.Visible;
            }
        }

        private void PintarBotoesVencedor(Button btn1, Button btn2, Button btn3)
        {
            //Pinta o background do botão para a cor da vitória
            btn1.Background = corDaVitoria;
            btn2.Background = corDaVitoria;
            btn3.Background = corDaVitoria;
        }
        private void PintarBotoesDeVelha()
        {
            btnA1.Background = corDaVelha;
            btnA2.Background = corDaVelha;
            btnA3.Background = corDaVelha;
            btnB1.Background = corDaVelha;
            btnB2.Background = corDaVelha;
            btnB3.Background = corDaVelha;
            btnC1.Background = corDaVelha;
            btnC2.Background = corDaVelha;
            btnC3.Background = corDaVelha;
        }

        private void PintarBotoesReset()
        {
            btnA1.Background = corReset;
            btnA2.Background = corReset;
            btnA3.Background = corReset;
            btnB1.Background = corReset;
            btnB2.Background = corReset;
            btnB3.Background = corReset;
            btnC1.Background = corReset;
            btnC2.Background = corReset;
            btnC3.Background = corReset;
        }

        private void SomDaVitoria()
        {
            //Instancia o arquivo para ser tocado
            Stream audio = Properties.Resources.Victory;
            //Cria um player para tocar o audio
            SoundPlayer player = new SoundPlayer(audio);
            //Toca o audio
            player.Play();
        }

        private void SomDaDerrota()
        {
            //Instancia o arquivo para ser tocado
            Stream audio = Properties.Resources.GameOver;
            //Cria um player para tocar o audio
            SoundPlayer player = new SoundPlayer(audio);
            //Toca o audio
            player.Play();
        }
    }
}
