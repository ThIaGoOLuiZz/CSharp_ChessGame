<h1 align="center">Chess Game</h1>

# About the Project
This project was developed during the C# course by @acenelio. The main goal was to apply our knowledge of C# to create a Chess game that runs in the console. At first glance, it may seem like a simple project, but it's far more complex than you might think.

# About the Board
### Printing the Board in the Console
Printing the board was the easiest part of this project. I used a for loop to create "-" with a space between them in an 8x8 size, totaling sixty-four squares (the standard size of a Chessboard).

##### Code for the ImprimirTabuleiro method:
```cs
public static void ImprimirTabuleiro(Tabuleiro tab)
{
            for (int i = 0; i < tab.linhas; i++)
            {
                ConsoleColor aux1 = Console.ForegroundColor;
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.Write(8 - i + " ");
                Console.ForegroundColor = aux1;

                for (int j = 0; j < tab.colunas; j++)
                { 
                        ImprimirPeca(tab.peca(i, j));
                }
                Console.WriteLine();
            }
            ConsoleColor aux = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("  A B C D E F G H");
            Console.ForegroundColor = aux;
}
```
##### Final Output:
<p align="center">
   <img width="142" height="165" src="https://user-images.githubusercontent.com/66191563/88415065-3e39bc80-cdb4-11ea-9c31-ece24c2dda8d.PNG">
</p>
    
  ### The TabuleiroException Exception
  It was necessary to create an exception for the board, which occurs when an invalid square is selected.
  ##### Examples of exceptions:
  ```cs
  throw new TabuleiroException("Não existe peça na posição de origem escolhida!"); 
  ```
  ```cs
  throw new TabuleiroException("A peça na posição de origem escolhida não é sua!");          
  ```
  ```cs
  throw new TabuleiroException("Não há movimentos possiveis para a peça de origem escolhida!");          
  ```
  ```cs
  throw new TabuleiroException("Posição de destino invalida!");
  ```        

# About the Pieces
 ### Pieces on the Board:
 - King `(x1 White & x1 Black)`, represented by the letter `R`;
 - Queen `(x1 White & x1 Black)`, represented by the letter `D`;
 - Bishop `(x2 White & x2 Black)`, represented by the letter `B`;
 - Knight `(x1 White & x1 Black)`, represented by the letter `C`;
 - Rook `(x1 White & x1 Black)`, represented by the letter `T`;
 - Pawn `(x8 White & x8 Black)`, represented by the letter `P`.
 
 ##### Board with Pieces:
 <p align="center">
   <img width="141" height="164" src="https://user-images.githubusercontent.com/66191563/88418677-53b1e500-cdba-11ea-981f-723ef7543664.PNG">
</p>
 
 ### Method to Place Pieces on the Board
 I implemented a method to place pieces in specific positions. Each piece has its corresponding letter, which is defined by the following method:
 ##### Code:
 ```cs
 public override string ToString()
 {
            return "R";
 }
 ```
 > *In this case, the letter "R" is returned because this part of the code belongs to the King.*
 
 To place this letter on the board, I created two methods in the *PartidaXadrez* class:
 
 ##### Method ColocarNovaPeca:
 ```cs
 public void ColocarNovaPeca(char coluna, int linha, Peca peca)
 {
            tab.colocarPeca(new PosicaoXadrez(coluna, linha).toPosicao(), peca);
            pecas.Add(peca);
 }
 ```
 
 > This method receives a column as a char and a number, then the toPosicao() method converts these values into a valid matrix position.
 
 ##### Method colocarPecas:
 Using this method, I placed pieces on the board like this:

 
 ```cs
 ColocarNovaPeca('a', 1, new Torre(tab, Cor.Branco));
 ```

# How Movement Restrictions Were Implemented for Each Piece
In the *PartidaXadrez* class, two methods were created: *validarPosicaoDeOrigem* and *validarPosicaoDeDestino*.

## Validating the Origin Position:
This method receives a position entered by the user (the piece's coordinate they want to move). It is divided into three *if* statements..

#### 1. Null Position
   To check if a position is null, I used the following condition:

   ```cs
if ( tab.peca(pos) == null)
{
       throw new TabuleiroException(" Não existe peça na posição de origem escolhida!");
}
   
   ```

#### 2. Checking If the Selected Piece Belongs to the Player:
   I had to check if the selected piece was the same color as the current player. This was done with:

   ```cs
 if (jogadorAtual != tab.peca(pos).cor)
 {
        throw new TabuleiroException("Uma peça de origem escolhida não é sua!");
 }
   ```
   
#### 3. Checking for Possible Moves
 If the selected piece had no possible moves, an exception was thrown.

 ```cs
 if ( !tab.peca(pos).existeMovimentosPossiveis())
 {
          throw new TabuleiroException("Não há movimentos possíveis para uma peça de origem escolhida!");
 }

 ```

## Validating the Destination Position
This method receives a position entered by the user (the target coordinates). It contains a single *if* statement.

```cs
if ( !tab.peca(origem).podeMoverPara(destino))
{
           throw new TabuleiroException ("Posição de destino invalida!");
}
```

# Special Moves
 This section describes the special moves implemented in the game.

 #### 1. Kingside Castling
 Kingside castling occurs when the King and a Rook have not moved, and there are two empty squares between them.

 ```cs
 // #jogadaespecial roque pequeno
 if (p is Rei && destino.Coluna == origem.Coluna + 2)
 {
        Posicao origemT = new Posicao(origem.Linha, origem.Coluna + 3);
        Posicao destinoT = new Posicao(origem.Linha, origem.Coluna + 1);
        Peca T = tab.retirarPeca(origemT);
        T.incrementarMovimentos();
        tab.colocarPeca(destinoT, T);
 }

 ```
 
 #### 2. Queenside Castling
 Queenside castling occurs when the King and a Rook have not moved, and there are four empty squares between them.

 ```cs
// #jogadaespecial roque grande
if (p is Rei && destino.Coluna == origem.Coluna - 2)
{
        Posicao origemT = new Posicao(origem.Linha, origem.Coluna - 4);
        Posicao destinoT = new Posicao(origem.Linha, origem.Coluna - 1);
        Peca T = tab.retirarPeca(origemT);
        T.incrementarMovimentos();
        tab.colocarPeca(destinoT, T);
}
 ```
 
 #### 3. En Passant
  En Passant occurs when an opponent's pawn moves two squares forward on its first move to avoid capture, and an adjacent pawn can capture it as if it had moved only one square.

  
##### Illustrative Image:
<p align="center">
   <img  src="https://qph.fs.quoracdn.net/main-qimg-945c1c2845899be55fad08abe4c010f9">
</p>

> *[site](https://www.quora.com/How-would-you-explain-en-passant-to-a-beginner)*

##### Code:
  ```cs
// #jogadaespecial en passant
if (p is Peao)
{
       if (origem.Coluna != destino.Coluna && pecaCapturada == null)
       {
             Posicao posP;
             if (p.cor == Cor.Branco)
             {
                        posP = new Posicao(destino.Linha + 1, destino.Coluna);
             }
             else
             {
                        posP = new Posicao(destino.Linha - 1, destino.Coluna);
             }
             pecaCapturada = tab.retirarPeca(posP);
             capturadas.Add(pecaCapturada);
       }
}
```
  
 #### 4. Promotion
Pawn promotion occurs when a pawn reaches the opponent's last rank. The pawn can be promoted to a Queen, Bishop, Knight, or Rook.

 ##### Example of Promotion to a Knight:
```cs
case 'C':
     Peca cavalo = new Cavalo(tab, p.cor);
     tab.colocarPeca(destino, cavalo);
     pecas.Add(cavalo);
     break;
case 'c':
     Peca cavalo1 = new Cavalo(tab, p.cor);
     tab.colocarPeca(destino, cavalo1);
     pecas.Add(cavalo1);
     break;
```
