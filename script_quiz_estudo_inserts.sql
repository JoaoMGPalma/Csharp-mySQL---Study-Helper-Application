use db_quiz_estudo;

-- ////////////////////////// 
-- TEMAS
insert into t_temas (nome_tema, descricao)
values
	('c#-básico', 'Conceitos fundamentais da linguagem de programação c#, incluindo sintaxe, variáveis, tipos de dados, operadores e estruturas de controlo básicas.'),
	('programação-orientada-a-objetos', 'Princípios essenciais de poo, como classes, objetos, herança, polimorfismo, encapsulamento e abstração.'),
	('consultas-sql', 'Conceitos essenciais de sql, incluindo instruções select, filtragem, ordenação, junções, agrupamento e manipulação básica de bases de dados.');
    

-- ////////////////////////// 
-- PERGUNTAS
insert into t_perguntas (id_tema, pergunta, explicacao)
values
--  C# BÁSICO (id_tema = 1)
(1, 'O que é uma variável em C#?', 'Uma variável é um espaço na memória usado para armazenar valores que podem mudar durante a execução do programa.'),
(1, 'Qual é a diferença entre int e double?', 'Int armazena números inteiros, enquanto double armazena números decimais com maior precisão.'),
(1, 'Para que serve a palavra-chave var?', 'Var permite ao compilador inferir automaticamente o tipo da variável com base no valor atribuído.'),
(1, 'O que faz a instrução using System;?', 'Permite aceder às classes do namespace System sem escrever o nome completo.'),
(1, 'O que é um método em C#?', 'Um método é um bloco de código que executa uma tarefa específica e pode receber parâmetros e devolver valores.'),
(1, 'O que é uma string?', 'String é um tipo de dados usado para armazenar texto.'),
(1, 'O que significa static em C#?', 'Static indica que um membro pertence à classe e não a uma instância específica.'),
(1, 'O que é uma exceção?', 'Uma exceção é um erro que ocorre durante a execução do programa e pode ser tratado com try-catch.'),
(1, 'Para que serve o comando if?', 'If permite executar código condicionalmente, dependendo de uma expressão booleana.'),
(1, 'O que faz o ciclo for?', 'For repete um bloco de código um número definido de vezes.'),
(1, 'O que é um array?', 'Um array é uma coleção de elementos do mesmo tipo armazenados de forma sequencial.'),
(1, 'Qual é a função do return?', 'Return devolve um valor de um método e termina a sua execução.'),
(1, 'O que é um namespace?', 'Um namespace organiza classes e outros tipos para evitar conflitos de nomes.'),
(1, 'O que é conversão de tipos?', 'É o processo de transformar um valor de um tipo para outro, como int para double.'),
(1, 'O que é interpolação de strings?', 'É uma forma de inserir valores dentro de uma string usando o formato $"texto {variavel}".'),

-- PROGRAMAÇÃO ORIENTADA A OBJETOS (id_tema = 2)
(2, 'O que é uma classe em POO?', 'Uma classe é um modelo que define propriedades e comportamentos dos objetos.'),
(2, 'O que é um objeto?', 'Um objeto é uma instância concreta de uma classe.'),
(2, 'O que significa encapsulamento?', 'Encapsulamento protege os dados internos de uma classe, permitindo acesso controlado.'),
(2, 'O que é herança?', 'Herança permite que uma classe reutilize e estenda funcionalidades de outra classe.'),
(2, 'O que é polimorfismo?', 'Polimorfismo permite que métodos com o mesmo nome tenham comportamentos diferentes.'),
(2, 'O que é abstração?', 'Abstração consiste em expor apenas o essencial e esconder detalhes internos.'),
(2, 'O que é um construtor?', 'Um construtor é um método especial executado ao criar um objeto.'),
(2, 'O que é sobrecarga de métodos?', 'É a criação de vários métodos com o mesmo nome mas parâmetros diferentes.'),
(2, 'O que é sobreposição de métodos?', 'É a redefinição de um método herdado para alterar o seu comportamento.'),
(2, 'O que é uma interface?', 'Uma interface define contratos que as classes devem implementar.'),
(2, 'O que é uma classe abstrata?', 'É uma classe que não pode ser instanciada e pode conter métodos abstratos.'),
(2, 'O que é composição?', 'Composição é uma relação onde um objeto é formado por outros objetos.'),
(2, 'O que é agregação?', 'Agregação é uma relação onde um objeto usa outro, mas ambos podem existir separadamente.'),
(2, 'O que é um atributo?', 'Um atributo é uma variável declarada dentro de uma classe.'),
(2, 'O que é um método virtual?', 'Um método virtual pode ser sobrescrito por classes derivadas.'),

--  CONSULTAS SQL (id_tema = 3)
(3, 'Para que serve a instrução SELECT?', 'SELECT é usada para consultar dados de uma ou mais tabelas.'),
(3, 'O que faz a cláusula WHERE?', 'WHERE filtra os resultados com base numa condição.'),
(3, 'Qual é a função da cláusula JOIN?', 'JOIN combina linhas de duas tabelas com base numa coluna relacionada.'),
(3, 'Para que serve a cláusula ORDER BY?', 'ORDER BY organiza os resultados em ordem crescente ou decrescente.'),
(3, 'O que faz a cláusula GROUP BY?', 'GROUP BY agrupa linhas com valores iguais em colunas específicas.'),
(3, 'O que é uma primary key?', 'É uma coluna que identifica unicamente cada linha de uma tabela.'),
(3, 'O que é uma foreign key?', 'É uma coluna que cria uma relação entre duas tabelas.'),
(3, 'O que faz a função COUNT()?', 'COUNT() devolve o número de linhas que correspondem a uma condição.'),
(3, 'O que faz a função SUM()?', 'SUM() soma os valores de uma coluna numérica.'),
(3, 'O que faz a cláusula HAVING?', 'HAVING filtra grupos criados pelo GROUP BY.'),
(3, 'O que é um INNER JOIN?', 'INNER JOIN devolve apenas as linhas que têm correspondência nas duas tabelas.'),
(3, 'O que é um LEFT JOIN?', 'LEFT JOIN devolve todas as linhas da tabela da esquerda e as correspondentes da direita.'),
(3, 'O que é um RIGHT JOIN?', 'RIGHT JOIN devolve todas as linhas da tabela da direita e as correspondentes da esquerda.'),
(3, 'O que faz a cláusula LIMIT?', 'LIMIT restringe o número de linhas devolvidas pela consulta.'),
(3, 'O que é uma view?', 'Uma view é uma consulta guardada que se comporta como uma tabela virtual.');    

-- ////////////////////
-- RESPOSTAS

INSERT INTO t_respostas (id_pergunta, resposta, correta)
VALUES
-- TEMA 1 — C# BÁSICO
(1, 'Uma variável é um método que executa código.', FALSE),
(1, 'Uma variável é um espaço na memória usado para armazenar valores.', TRUE),
(1, 'Uma variável é um ficheiro externo.', FALSE),
(1, 'Uma variável é uma classe abstrata.', FALSE),

(2, 'int armazena texto e double armazena booleanos.', FALSE),
(2, 'int é usado apenas em ciclos e double apenas em classes.', FALSE),
(2, 'int armazena inteiros e double armazena números decimais.', TRUE),
(2, 'int representa caracteres e double representa strings.', FALSE),

(3, 'Cria uma variável global.', FALSE),
(3, 'Permite ao compilador inferir automaticamente o tipo da variável.', TRUE),
(3, 'Define uma constante.', FALSE),
(3, 'Indica que a variável será destruída no fim do método.', FALSE),

(4, 'Cria um novo namespace chamado System.', FALSE),
(4, 'Importa todas as bibliotecas instaladas.', FALSE),
(4, 'Define que o programa só corre em Windows.', FALSE),
(4, 'Permite usar classes do namespace System sem escrever o nome completo.', TRUE),

(5, 'Um método é um ficheiro externo.', FALSE),
(5, 'Um método é um bloco de código que executa uma tarefa.', TRUE),
(5, 'Um método é uma variável especial.', FALSE),
(5, 'Um método é um ciclo automático.', FALSE),

(6, 'Uma string é um número inteiro.', FALSE),
(6, 'Uma string é um método de conversão.', FALSE),
(6, 'Uma string é um tipo usado para armazenar texto.', TRUE),
(6, 'Uma string é uma classe usada para cálculos.', FALSE),

(7, 'static indica que o membro pertence à classe e não à instância.', TRUE),
(7, 'static indica que o método só pode ser usado uma vez.', FALSE),
(7, 'static impede alterações ao valor.', FALSE),
(7, 'static só funciona dentro de ciclos.', FALSE),

(8, 'Uma exceção é um aviso de compilação.', FALSE),
(8, 'Uma exceção é uma variável especial.', FALSE),
(8, 'Uma exceção é um erro em tempo de execução tratado com try-catch.', TRUE),
(8, 'Uma exceção impede o programa de terminar.', FALSE),

(9, 'O comando if cria ciclos infinitos.', FALSE),
(9, 'O comando if executa código quando uma condição é verdadeira.', TRUE),
(9, 'O comando if converte números em texto.', FALSE),
(9, 'O comando if declara variáveis globais.', FALSE),

(10, 'O ciclo for executa código apenas quando falso.', FALSE),
(10, 'O ciclo for cria variáveis constantes.', FALSE),
(10, 'O ciclo for define métodos.', FALSE),
(10, 'O ciclo for repete código um número definido de vezes.', TRUE),

(11, 'Um array é um método de ordenação.', FALSE),
(11, 'Um array é uma coleção de elementos do mesmo tipo.', TRUE),
(11, 'Um array guarda apenas booleanos.', FALSE),
(11, 'Um array é um ciclo automático.', FALSE),

(12, 'return devolve um valor e termina o método.', TRUE),
(12, 'return cria uma variável.', FALSE),
(12, 'return repete o método.', FALSE),
(12, 'return converte valores.', FALSE),

(13, 'Um namespace é um método.', FALSE),
(13, 'Um namespace organiza classes e tipos para evitar conflitos.', TRUE),
(13, 'Um namespace é uma variável.', FALSE),
(13, 'Um namespace é um ciclo.', FALSE),

(14, 'Conversão de tipos transforma um valor de um tipo noutro.', TRUE),
(14, 'Conversão de tipos cria classes.', FALSE),
(14, 'Conversão de tipos converte C# em SQL.', FALSE),
(14, 'Conversão de tipos altera nomes de variáveis.', FALSE),

(15, 'Interpolação concatena strings com +.', FALSE),
(15, 'Interpolação converte strings em inteiros.', FALSE),
(15, 'Interpolação insere valores numa string com $"texto {variavel}".', TRUE),
(15, 'Interpolação cria arrays automaticamente.', FALSE),

-- TEMA 2 — POO
(16, 'Uma classe é um ficheiro de configuração.', FALSE),
(16, 'Uma classe é uma variável global.', FALSE),
(16, 'Uma classe é um modelo que define propriedades e comportamentos.', TRUE),
(16, 'Uma classe é um método especial.', FALSE),

(17, 'Um objeto é um método estático.', FALSE),
(17, 'Um objeto é uma instância concreta de uma classe.', TRUE),
(17, 'Um objeto é um ficheiro externo.', FALSE),
(17, 'Um objeto é um ciclo.', FALSE),

(18, 'Encapsulamento cria novas classes automaticamente.', FALSE),
(18, 'Encapsulamento protege dados internos e controla o acesso.', TRUE),
(18, 'Encapsulamento converte tipos.', FALSE),
(18, 'Encapsulamento ordena listas.', FALSE),

(19, 'Herança cria variáveis automaticamente.', FALSE),
(19, 'Herança converte classes em métodos.', FALSE),
(19, 'Herança permite reutilizar e estender funcionalidades.', TRUE),
(19, 'Herança impede a criação de objetos.', FALSE),

(20, 'Polimorfismo permite comportamentos diferentes com o mesmo método.', TRUE),
(20, 'Polimorfismo impede sobrecarga.', FALSE),
(20, 'Polimorfismo cria classes automaticamente.', FALSE),
(20, 'Polimorfismo converte objetos em strings.', FALSE),

(21, 'Abstração cria objetos automaticamente.', FALSE),
(21, 'Abstração converte tipos.', FALSE),
(21, 'Abstração expõe apenas o essencial e oculta detalhes.', TRUE),
(21, 'Abstração ordena listas.', FALSE),

(22, 'Um construtor é um método que destrói objetos.', FALSE),
(22, 'Um construtor é executado ao criar um objeto.', TRUE),
(22, 'Um construtor é uma variável especial.', FALSE),
(22, 'Um construtor é um ciclo.', FALSE),

(23, 'Sobrecarga permite vários métodos com o mesmo nome.', TRUE),
(23, 'Sobrecarga impede herança.', FALSE),
(23, 'Sobrecarga converte tipos.', FALSE),
(23, 'Sobrecarga cria classes.', FALSE),

(24, 'Sobreposição redefine métodos herdados.', TRUE),
(24, 'Sobreposição impede polimorfismo.', FALSE),
(24, 'Sobreposição cria variáveis.', FALSE),
(24, 'Sobreposição converte strings.', FALSE),

(25, 'Uma interface é uma classe abstrata.', FALSE),
(25, 'Uma interface define contratos que classes devem implementar.', TRUE),
(25, 'Uma interface é um método estático.', FALSE),
(25, 'Uma interface é um ficheiro externo.', FALSE),

(26, 'Uma classe abstrata é sempre final.', FALSE),
(26, 'Uma classe abstrata não pode ser instanciada.', TRUE),
(26, 'Uma classe abstrata é igual a uma interface.', FALSE),
(26, 'Uma classe abstrata é um método.', FALSE),

(27, 'Composição forma objetos a partir de outros objetos.', TRUE),
(27, 'Composição impede herança.', FALSE),
(27, 'Composição converte tipos.', FALSE),
(27, 'Composição cria ciclos.', FALSE),

(28, 'Agregação destrói objetos automaticamente.', FALSE),
(28, 'Agregação relaciona objetos que podem existir separadamente.', TRUE),
(28, 'Agregação impede polimorfismo.', FALSE),
(28, 'Agregação cria classes.', FALSE),

(29, 'Um atributo é uma variável dentro de uma classe.', TRUE),
(29, 'Um atributo é um método.', FALSE),
(29, 'Um atributo é um namespace.', FALSE),
(29, 'Um atributo é um ficheiro externo.', FALSE),

(30, 'Um método virtual é sempre estático.', FALSE),
(30, 'Um método virtual não pode ser herdado.', FALSE),
(30, 'Um método virtual pode ser sobrescrito por classes derivadas.', TRUE),
(30, 'Um método virtual é um construtor.', FALSE),

-- TEMA 3 — SQL
(31, 'SELECT apaga dados.', FALSE),
(31, 'SELECT consulta dados de uma ou mais tabelas.', TRUE),
(31, 'SELECT cria tabelas.', FALSE),
(31, 'SELECT altera colunas.', FALSE),

(32, 'WHERE ordena resultados.', FALSE),
(32, 'WHERE filtra resultados com base numa condição.', TRUE),
(32, 'WHERE cria tabelas.', FALSE),
(32, 'WHERE soma valores.', FALSE),

(33, 'JOIN apaga linhas duplicadas.', FALSE),
(33, 'JOIN cria índices.', FALSE),
(33, 'JOIN combina linhas de duas tabelas relacionadas.', TRUE),
(33, 'JOIN converte tipos.', FALSE),

(34, 'ORDER BY ordena resultados.', TRUE),
(34, 'ORDER BY filtra resultados.', FALSE),
(34, 'ORDER BY cria tabelas.', FALSE),
(34, 'ORDER BY soma valores.', FALSE),

(35, 'GROUP BY agrupa linhas com valores iguais.', TRUE),
(35, 'GROUP BY ordena resultados.', FALSE),
(35, 'GROUP BY apaga duplicados.', FALSE),
(35, 'GROUP BY cria colunas.', FALSE),

(36, 'Uma primary key permite valores duplicados.', FALSE),
(36, 'Uma primary key identifica unicamente cada linha.', TRUE),
(36, 'Uma primary key é opcional em todas as tabelas.', FALSE),
(36, 'Uma primary key é usada apenas em views.', FALSE),

(37, 'Uma foreign key impede inserções.', FALSE),
(37, 'Uma foreign key é sempre única.', FALSE),
(37, 'Uma foreign key cria relação entre tabelas.', TRUE),
(37, 'Uma foreign key converte tipos.', FALSE),

(38, 'COUNT() soma valores.', FALSE),
(38, 'COUNT() devolve o número de linhas.', TRUE),
(38, 'COUNT() ordena resultados.', FALSE),
(38, 'COUNT() cria tabelas.', FALSE),

(39, 'SUM() conta linhas.', FALSE),
(39, 'SUM() ordena resultados.', FALSE),
(39, 'SUM() soma valores de uma coluna numérica.', TRUE),
(39, 'SUM() cria índices.', FALSE),

(40, 'HAVING filtra grupos criados pelo GROUP BY.', TRUE),
(40, 'HAVING ordena resultados.', FALSE),
(40, 'HAVING cria tabelas.', FALSE),
(40, 'HAVING soma valores.', FALSE),

(41, 'INNER JOIN devolve todas as linhas da esquerda.', FALSE),
(41, 'INNER JOIN devolve apenas linhas com correspondência.', TRUE),
(41, 'INNER JOIN devolve todas as linhas da direita.', FALSE),
(41, 'INNER JOIN apaga duplicados.', FALSE),

(42, 'LEFT JOIN devolve todas as linhas da esquerda.', TRUE),
(42, 'LEFT JOIN devolve apenas correspondências.', FALSE),
(42, 'LEFT JOIN devolve todas as linhas da direita.', FALSE),
(42, 'LEFT JOIN soma valores.', FALSE),

(43, 'RIGHT JOIN devolve apenas correspondências.', FALSE),
(43, 'RIGHT JOIN devolve todas as linhas da esquerda.', FALSE),
(43, 'RIGHT JOIN devolve todas as linhas da direita.', TRUE),
(43, 'RIGHT JOIN cria índices.', FALSE),

(44, 'LIMIT ordena resultados.', FALSE),
(44, 'LIMIT restringe o número de linhas devolvidas.', TRUE),
(44, 'LIMIT cria tabelas.', FALSE),
(44, 'LIMIT soma valores.', FALSE),

(45, 'Uma view é uma consulta guardada que se comporta como tabela.', TRUE),
(45, 'Uma view é uma tabela física.', FALSE),
(45, 'Uma view é um índice.', FALSE),
(45, 'Uma view é uma foreign key.', FALSE);

-- select * from t_respostas;

-- SELECT id, pergunta, explicacao FROM t_perguntas WHERE id_tema = 1 ORDER BY RAND() LIMIT 10;