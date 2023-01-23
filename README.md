Sistema de Alerta de Prazos

Este sistema é fruto de um Trabalho de Conclusão de Curso para o curso de Graduação em Sistemas de Informação da Faculdade de Computação (FACOM) que faz parte da Universidade Federal de Uberlândia (UFU).



Objetivo:
Este trabalho tem como objetivo criar um sistema que verifique a situação dos discentes da FACOM e dispare e-mails para os mesmos com informações para se atentarem aos prazos e limites previstos em normas.
Essa etratégia preza por diminuir as demandas de reconsideração que chegam a coordenação por conta de discentes que perderam aproveitamento de algum componente curricular por não se atentarem aos limites previstos nas normas de graduação.



Desenvolvimento:
O sistema deste projeto foi desenvolvido em .Net Core e, foi sua constinuição é composta por 4 aplicaçÕes distintas e 2 bancos de dados SQL Server.

As aplicações são:

-Web App com web site gerenciamento.
Sua função é prover uma interface entre o um usuário da coordenação para configurar os partâmetros de execução da rotina de disparo de alertas.

-Web Api do Sistema de alerta.
Sua função é servir de interface para acesso ao banco de dados por meio das outras aplicações que buscam informações no banco de dados, sendo essas aplicações a automação (Console App) e o Web site de gerenciamento (Web App).

-Web Api representando o sistema de graduação.
O propósito do desenvolvimento dessa api é proporcionar um acesso aos dados da graduação realístico o suficiente para apoiar no desenvolvimento do sistema de alerta de prazos. Essa api vai gerar e fornecer acesso ao banco de dados que representa a base de dados de graduação.

-Console App como automação.
Essa aplicação vai consumir as 2 Apis para acessar os dados da graduação e os dados que se encontram no banco de dados do sistema de alerta de prazos definindo os parâmetros de execução.
Essa automação foi projetada para ser agendada no agendador de tarefas do windows para executar diáriamente buscando dados dos discente e identificando situações que impliquem atenção dos discentes para evitar contratempos e perda de aproveitamento, para assim poder alerta-los disparando um e-mail por meio de um smtpclient na automação que irá disparar as mensagens de alerta por e-mail para todos os discentes matriculados na facom que atenderem as condições de alerta.



Implantação:
O web site e as web apis foram projetadas para serem hospedadas em um servidor que suporte aplicações .Net Core, como por exemplo o Servidor IIS dos sistemas operacionais Windows mais modernos.
Enquanto a aplicação de console foi projetada para ser implantada em uma máquina com SO Windows para ser agendada como rotina por meio do agendador de tarefas.

Uma solução de implantação mais moderna seria implantar o web site e as web apis como serviço utilizando as aplicações como serviço da Azure. E para a automação de console app um serviço de máquina virtual com sistema operacional windows para agenda-lo.
