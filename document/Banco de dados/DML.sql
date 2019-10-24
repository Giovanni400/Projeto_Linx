USE XepaDigital;

INSERT INTO Endereco (Endereco, Numero, CEP, Cidade, Bairro, Estado) 
VALUES
('Av. 23 Maio','152','53427-003','São Paulo','Brooklin','SP'),
('Rua Barão de Limeira','539','06453-004','São Paulo','Campos Elíseos','SP'),
('Av Teruo Asaeda','137','07181-230','São Paulo','Cid.Jd Cumbica','SP'),
('R.Gen.Júlio Marcondes Salgado','100','54367-007','São Paulo','São Bento Novo','SP');


INSERT INTO Usuario (NomeUsuario, EmailUsuario, SenhaUsuario, ReceberNotif, TipoUsuario, Telefone1, Telefone2, IdEndereco) 
VALUES
('Administrador','adm@admxepadigital.com','admps591',1,'Administrador','(00) 0000-0000','00 0000-0000',1),
('Ayana','Ayanadasilva@gmail.com','Ayana543',1,'Colaborador','','',2),
('Joselito','Joselito.mendes@hotmail.com','21051998',1,'Colaborador','','',3),
('Fernando','fernando.lol@gmail.com','lol123',0,'Cliente','(00) 0000-0000','',4);

INSERT INTO Colaborador (ImgPerfil, RazaoSocial, DocumentoColab, FazEntrega, SobreColab, IdUsuario) 
VALUES
('Url/100/colaporador1.jpg','Zunduri & CIA','111222333-00',1,'Nascida no Brasil mas Ayana 45 anos, é filha de imigrantes, negra casada com 2 filhas, acorda todos os dias às 5:00 AM para cuidar de suas plantações junto ao seu marido. Mora num humilde terreno em Rio Pequeno, SP onde plantam seus produtos orgânicos. Ayana tem ensino médio e técnico em agricultura completo.',2),
('Url/101/colaborador2.jpg','Fazenda Vass','444555666-00',1,'Nascido no Paraná Joselito 63 anos, Branco veio para São Paulo aos 14 anos de idade. Casado sem filhos, sem estudo, não tem nenhuma experiência com tecnologia. Todos dias trabalha com criação de animais e suas plantações, mora em São Pedro do Turvo.',3);


INSERT INTO Receita (NomeReceita, ImgReceita, DescricaoIngrediente, DescricaoPreparo, IdUsuario) 
VALUES
('Abóbora cabotiá com curry de legumes','Url/159/abobora.jpg','1 abóbora cabotiá; 1 couve flor; 1 abobrinha; 150g de cogumelo Paris; 1 colher de sobremesa de curry tailandês; 100 ml de vinho branco; 1 dente de alho; Leite de coco a gosto; Alecrim, gengibre, sal e pimenta a gosto; Salsinha e cebolinha finamente picadas', '1. Corte uma abóbora cabotiá em gomos de aproximadamente 2cm de largura. Tempere com sal e pimenta do reino moída na hora. Acrescente o alecrim.; 2. Em uma assadeira forrada com papel alumínio, leve ao forno a 220 graus por 40 minutos, virando os gomos na metade do tempo. Para se certificar, espete a abóbora com um palito e verifique se está macia. Reserve.; 3. Separe a couve flor em pequenos buquês e corte a abobrinha em cubos médios. Corte os cogumelos em quatro.; 4. Branqueie a couve e a abobrinha separadamente, da seguinte forma: ferva os vegetais para que fiquem cozidos mas ainda firmes e, em seguida, mergulhe-os em água gelada, de preferência com pedras de gelo. Reserve.; 5. Em uma frigideira funda e quente, salteie os cogumelos com alho e gengibre picadinhos. Acrescente o curry tailandês vermelho, o vinho branco e o leite de coco a gosto.',1),
('Berinjela recheada com lentilha','Url/160/benrinjela.jpg','3 berinjelas; 500g de lentilha turca; 2 cebolas picadas; 3 limões; 2 colheres de sopa de raspas de laranja; 5 ramos de hortelã picada; sal e pimenta a gosto','1. Cortar as berinjelas ao meio e separar o miolo da casca.; 2. Refogar as cebolas picadas e os miolos das berinjelas com sal e pimenta a gosto. Deixe esfriar em um recipiente.; 3. Coloque a lentilha para ferver em pouca água adicionando mais líquido à medida que necessário. Acrescente também o suco do limão, sal, pimenta e no final as raspas de laranja e a hortelã. A lentilha deve ficar no ponto al dente.; 4. Recheie as berinjelas primeiro com o miolo resfriado. Em cima coloque a lentilhas cozida e salpique com castanhas trituradas.; 5. Colocar no forno a 180 graus por15 minutos, para finalizar. Sirva em seguida.',4),
('Hambúrguer de cogumelos com cevada','Url/161/hamburguer.jpg','1 batata pequena sem casca cortada em pedaços de 1 cm;  6 cogumelos portobello médios; 12 champignons graúdos; 10 cogumelos shitake médios; 3 colheres (sopa) de azeite de oliva; ½ colher (chá) de tomilho seco; 2 colheres (sopa) de vinagre balsâmico; 1 xícara de cevada cozida; ½ colher (chá) de sal; ¼ de colher (chá) de pimenta-do-reino moída na hora','1. Cozinhe a batata até ficar macia e amasse a seguir.; 2. Corte os talos dos portobello, limpe os chapéus com uma escova macia ou papel-toalha e corte em lâminas de 1 cm. Corte os champignons e os shitake em lâminas finas.; 3. Pré-aqueça o forno a 190 °C.; 4. Em uma frigideira com fundo grosso, aqueça 1 colher (sopa) de azeite em fogo médio. Refogue o portobello e o tomilho por 6-8 minutos ou até soltar líquido. Junte os outros cogumelos e cozinhe por 10 minutos ou até que o líquido tenha secado. Acrescente o vinagre balsâmico e raspe o fundo da frigideira com uma colher de pau.; 5. Transfira para o processador e pulse até obter uma massa granulada. Se preferir, pique muito bem à mão. Junte a batata, a cevada, o sal e a pimenta-do-reino, pulse, somente para homogeneizar a massa, e modele os hambúrgueres.; 6. Em uma frigideira grande que possa ir ao forno, aqueça o restante do azeite em fogo médio-alto. Frite os hambúrgueres por 6-10 minutos ao todo ou até tostar dos dois lados. Leve a frigideira ao forno e asse por 12-15 minutos ou até o ponto desejado.',2);


INSERT INTO SobreProduto (DescricaoProduto, Disponibilidade, Organico, Preco, Validade) 
VALUES
('A cenoura é uma raiz de cor alaranjada. Este legume é riquíssimo em betacaroteno, um elemento importante para a visão.',12,1,'R$ 7,90 por Kg','10/12/2019');


INSERT INTO Produto (NomeProduto, ImgProduto, IdSobreProduto) VALUES
('Cenoura','url/123/cenoura.jpg',1);

INSERT INTO RegistroProduto (IdProduto, IdColaborador) VALUES
(1,2);

INSERT INTO ReservaProduto (Quantidade, IdProduto, IdUsuario) VALUES
(10.2,1,4);