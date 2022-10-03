CREATE DATABASE Aeroporto

USE Aeroporto

CREATE TABLE Restrito(

	CPF varchar(15) Not Null

	CONSTRAINT CPF_Rest PRIMARY KEY (CPF)
);

CREATE TABLE Bloqueado(

	CNPJ varchar(18) Not Null

	CONSTRAINT CNPJ_Bloq PRIMARY KEY (CNPJ)
);

CREATE TABLE Iata(

	Sigla varchar(3) Not Null

	CONSTRAINT Sigla_Iata PRIMARY KEY (Sigla)
);

CREATE TABLE Passageiro(
	CPF varchar(11) Not null,
	Nome varchar(50) Not null,
	Data_Nascimento Date Not Null,
	Sexo Char (1),
	Ultima_Compra Date Not Null,
	Data_Cadastro Date Not Null,
	Situacao char  Not Null

	CONSTRAINT CPF_Passageiro PRIMARY KEY (CPF)
);

CREATE TABLE Companhia(
	CNPJ varchar (18) Not null,
	Razao_Social varchar (50) Not null,
	Data_abertura Date Not null,
	Ultimo_Voo Date Not null,
	Data_cadastro Date Not null,
	Situacao char(1) Not null

	CONSTRAINT CNPJ_Comp PRIMARY KEY (CNPJ),
);

CREATE TABLE Aeronave(
	Inscricao varchar(6) Not null,
	CNPJ varchar(18) Not null,
	Capacidade int Not null,
	Data_Cadastro Date Not null,
	Ultima_Venda Date Not null,
	Situacao char(1) Not null

	CONSTRAINT Ins_Aeronave PRIMARY KEY (Inscricao),
	CONSTRAINT FK_CompAeronave FOREIGN KEY(CNPJ) REFERENCES Companhia(CNPJ)
);

CREATE TABLE Voo(
	Id varchar(5) Not null,
	insAeronave varchar(6) Not null,
	Destino varchar(3) Not null,
	Assentos_Ocupados int Not null,
	Data_Voo Date Not null,
	Data_Cadastro Date Not null,
	Situacao char(1) Not null,
	

	CONSTRAINT Id_Voo PRIMARY KEY (Id),
	CONSTRAINT FK_AeronaveVoo FOREIGN KEY (insAeronave) REFERENCES Aeronave(Inscricao)
);

CREATE TABLE Passagem(
	IdPAssagem varchar(15) Not null,
	IdVoo varchar(5) Not null ,
	InsAeronave varchar(6) Not null,
	Data_Voo varchar(20) Not null, 
	Valor decimal(4,2) Not null,
	Situacao char(1)

	CONSTRAINT Cod_Passagem PRIMARY KEY (IdPassagem),
	CONSTRAINT FK_Cod_VooPassagem FOREIGN KEY (IdVoo) REFERENCES Voo(Id),
	CONSTRAINT FK_AeronavePassagem FOREIGN KEY (InsAeronave)REFERENCES Aeronave(Inscricao)

);

 
CREATE TABLE Venda (
	IdVenda int IDENTITY not null,
	cpfPassageiro varchar(11) Not null,
	Data_venda Date,
	Valor_Total float,

	CONSTRAINT Id_Venda PRIMARY KEY (IdVenda),
	CONSTRAINT FK_PassageiroVenda FOREIGN KEY (cpfPassageiro) REFERENCES Passageiro(CPF)

);

CREATE TABLE ItemVenda(
	IdItem int IDENTITY not null,
	codPassagem varchar(15) Not null,
	codVenda int Not null,
	Valor_Unitario float Not null,

	CONSTRAINT Id_Item PRIMARY KEY(IdItem),
	CONSTRAINT FK_PassagemItem FOREIGN KEY (codPassagem) REFERENCES Passagem(IdPassagem),
    CONSTRAINT FK_vendaFKItem FOREIGN KEY (codvenda) REFERENCES Venda(IdVenda)

);




insert into IATA(Sigla) values ('BSB'),('CGH'),('GIG'),('SSA'),('FLN'),('POA'),('VCP'),('REC'),('CWB'),('BEL'),('VIX'),('SDU'),('CGB'),('CGR'),('FOR'),('MCP'),('MGF'),('GYN'),('NVT'),('MAO'),('NAT'),('BPS'),('MCZ'),('PMW'),('SLZ'),('GRU'),('LDB'),('PVH'),('RBR'),('JOI'),('UDI'),('CXJ'),('IGU'),('THE'),('AJU'),('JPA'),('PNZ'),('CNF'),('BVB'),('CPV'),('STM'),('IOS'),('JDO'),('IMP'),('XAP'),('MAB'),('CZS'),('PPB'),('CFB'),('FEN'),('JTC'),('MOC');

