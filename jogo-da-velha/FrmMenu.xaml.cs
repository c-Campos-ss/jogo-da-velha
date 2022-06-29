using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace jogo_da_velha
{
    public partial class FrmMenu : Window
    {
        public FrmMenu()
        {
            InitializeComponent();
        }

        private void IniciarJogo(object sender, MouseButtonEventArgs e)
        {
           //Verificar se os textos de entrada foram preenchidos.
            if (txtNomeJogadorO.Text != "" && txtNomeJogadorX.Text != "")
            {
                // Constrói a janela do jogo da velha na variável janelaJogoDaVelha.
                MainWindow janelaJogoDaVelha = new MainWindow(txtNomeJogadorX.Text, txtNomeJogadorO.Text);
                // Abre a janela do jogo da velha.
                janelaJogoDaVelha.Show();
                // Fecha a janela atual, que é a janela FrmMenu.
                Close();
            }
            else
            {
                MessageBoxResult caixaDeMensagem = MessageBox.Show("Preencha todos os campos!","Atenção!",MessageBoxButton.OK,MessageBoxImage.Warning);
            }
        }
    }
}
