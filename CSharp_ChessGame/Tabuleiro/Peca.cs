
namespace TabuleiroGame
{
    internal class Peca
    {
        public Posicao Posicao { get; set; }
        public Cor Cor { get; protected set; }
        public int QtdMovimentos { get; protected set; }
        public Tabuleiro Tabuleiro { get; protected set; }

        public Peca(Posicao posicao, Tabuleiro tabuleiro, Cor cor)
        {
            Posicao = posicao;
            Cor = cor;
            Tabuleiro = tabuleiro;
            QtdMovimentos = 0;
        }
    }
}
