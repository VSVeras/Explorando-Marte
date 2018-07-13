# ** EXPLORANDO MARTE **

Um conjunto de sondas foi enviado pela NASA à Marte e irá pousar num planalto. Esse
planalto, que curiosamente é retangular, deve ser explorado pelas sondas para que suas
câmeras embutidas consigam ter uma visão completa da área e enviar as imagens de
volta para a Terra.

A posição e direção de uma sonda são representadas por uma combinação de
coordenadas x-y e uma letra representando a direção cardinal para qual a sonda aponta,
seguindo a rosa dos ventos em inglês.

O planalto é divido numa malha para simplificar a navegação. Um exemplo de posição
seria (0, 0, N), que indica que a sonda está no canto inferior esquerdo e apontando para
o Norte.
Para controlar as sondas, a NASA envia uma simples sequência de letras. As letras
possíveis são "L", "R" e "M". Destas, "L" e "R" fazem a sonda virar 90 graus para a
esquerda ou direita, respectivamente, sem mover a sonda. "M" faz com que a sonda
mova-se para a frente um ponto da malha, mantendo a mesma direção.

Nesta malha o ponto ao norte de (x, y) é sempre (x, y+1).

Você deve fazer um programa que prom ponto da malha, mantendo a mesma direção.

Nesta malha o ponto ao norte de (x, y) é sempre (x, y+1).

Você deve fazer um programa que processe uma série de instruções enviadas para as
sondas que estão explorando este planalto. O formato da entrada e saída deste programa
segue abaixo

A forma de entrada e saída dos dados fica à sua escolha.

## ENTRADA

A primeira linha da entrada de dados é a coordenada do ponto superior-direito da malha
do planalto. Lembrando que a inferior esquerda sempre será (0,0).

O resto da entrada será informação das sondas que foram implantadas. Cada sonda é
representada por duas linhas. A primeira indica sua posição inicial e a segunda uma
série de instruções indicando para a sonda como ela deverá explorar o planalto.

A posição é representada por dois inteiros e uma letra separados por espaços,
correspondendo à coordenada X-Y e à direção da sonda. Cada sonda será controlada
sequencialmente, o que quer dizer que a segunda sonda só irá se movimentar após que a
primeira tenha terminado suas instruções.

## SAÍDA

A saída deverá conter uma linha para cada sonda, na mesma ordem de entrada,
indicando sua coordenada final e direção.

### Exemplos de Entrada e Saída:

#### Entrada:

5 5<br/>
1 2 N<br/>
LMLMLMLMM<br/>
3 3 E<br/>
MMRMMRMRRM<br/>

#### Saída esperada:

1 3 N<br/>
5 1 E<br/>

