tabela local======
id: guid
nome: string
endereço: string
createdAt: datetime
updatedAt: datetime
deletedAt: datetime

tabela sala========
id: guid
fk_local_id: guid
vagas:  1-999
tipo: privado/compartilhado
createdAt: datetime
updatedAt: datetime
deletedAt: datetime

tabela locaçoes========
id: guid
fk_sala_id: guid
fk_user_id: guid
dia_alocado: datetime
periodo_alocado: manhã/tarde/full
codigo: A523E <- codigo alphanumerico de 5 digitos
createdAt: datetime
updatedAt: datetime
deletedAt: datetime

ao clicar em reserve agora:
	1. abrir modal
	2. pedir endereço (combobox)
	3. pedir tipo de sala (4 pessoas, 5 pessoas, 10 pessoas, compartilhado)
	4. tela de transação, vai exibir tipo de pagamento em checkbox:
		dinheiro real
		pix
		cartao de credito/debito
	5. confirma agendamento, entregando um codigo gerado de acesso
		