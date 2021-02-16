/* Exercicio 1. Com base no modelo acima, escreva um comando SQL que liste a 
 * quantidade de processos por Status com sua descrição. 
 */
SELECT p.idProcesso,s.dsStatus FROM tb_Status as s
inner join tb_Processo as p on p.idStatus = s.idStatus


/* Exercicio 2. Com base no modelo acima, construa um comando SQL que liste a maior data 
 * de andamento por número de processo, com processos encerrados no ano de 2013.
 */

SELECT MAX(a.dtAndamento) FROM tb_Processo as p
inner join tb_Andamento as a on a.idProcesso = p.idProcesso
WHERE p.DtEncerramento = 2013

/* Exercicio 3. Com base no modelo acima, construa um comando SQL que liste a quantidade de 
 * Data de Encerramento agrupada por ela mesma onde a quantidade da contagem seja maior que 5. 
 */

SELECT COUNT(DtEncerramento) AS qntDataEncerramento  FROM tb_Processo 
GROUP BY DtEncerramento 
HAVING qntDataEncerramento > 5

/* Exercicio 4. Possuímos um número de identificação do processo, onde o mesmo contém 12 
 * caracteres com zero à esquerda, contudo nosso modelo e dados ele é apresentado como bigint. 
 * Como fazer para apresenta-lo com 12 caracteres considerando os zeros a esquerda? 
 */

SELECT SUBSTRING(nroProcesso,0,12) FROM tb_Processo 