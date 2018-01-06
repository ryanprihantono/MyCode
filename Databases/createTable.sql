create database SatoISDB

use SatoISDB
begin tran
create table THRMEmployee(
	employeeId int identity primary key,
	employeeCode varchar(50) unique,
	firstName varchar(50),
	lastName varchar(50),
	pass varchar(50)
)
create table THRMEmpPosition(
	positionId int identity primary key,
	positionCode varchar(50) unique,
	position varchar(50)
)
create table THRMTrEmpPosition(
	trPositionId int identity primary key,
	employeeId int constraint fk_employeeId foreign key (employeeId) references THRMEmployee(employeeId) on update cascade on delete cascade,
	positionId int constraint fk_positionId foreign key (positionId) references THRMEmpPosition(positionId) on update cascade on delete cascade,
	remark varchar(255)
)

create table TCCCustomerCategory(
	custCatId int identity primary key,
	custCatCode varchar(50) unique,
	custCat varchar(50)
)


create table TCCCustomer(
	customerId int identity primary key,
	customerCode varchar(50) unique,
	firstName varchar(50),
	lastName varchar(50),
	remark varchar(255)
)
create table TCCTrCustomerCategory(
	trCustCatId int identity primary key,
	custCatId int constraint fk_custCatId foreign key (custCatId) references TCCCustomerCategory(custCatId) on update cascade on delete cascade,
	customerId int constraint fk_customerId foreign key (customerId) references TCCCustomer(customerId) on update cascade on delete cascade,
	remark varchar(255)
)

create table TCCCustomerDetail(
	custDetailId int identity primary key,
	custDetailCode varchar(50),
	detailName varchar(50)
)
create table TCCTrCustomerDetail(
	trCustDetailId int identity primary key,
	customerId int constraint fk_customerIdDetail foreign key (customerId) references TCCCustomer(customerId) on update cascade on delete cascade,
	custDetailId int constraint fk_custDetailId foreign key (custDetailId) references TCCCustomerDetail(custDetailId) on update cascade on delete cascade,
	remark varchar(255)
)


create table TIWProductCategory(
	prodCatId int identity primary key,
	prodCatCode varchar(50),
	prodCat varchar(50)
)


create table TIWProduct(
	productId int identity primary key,
	productCode varchar(50) unique,
	product varchar(50),
	price int
)
create table TIWTrProductCategory(
	trProdCatId int identity primary key,
	productId int constraint fk_productIdCat foreign key (productId) references TIWProduct(productId) on update cascade on delete cascade,
	prodCatId int constraint fk_prodCatId foreign key (prodCatId) references TIWProductCategory(prodCatId) on update cascade on delete cascade,
	remark varchar(255)
)
create table TIWProductDiscount(
	prodDiscId int identity primary key,
	prodDiscCode varchar(50),
	discName varchar(50),
	discVal int
)

create table TIWTrProdDiscount(
	trProdDiscId int identity primary key,
	prodDiscId int constraint fk_prodDiscId foreign key (prodDiscId) references TIWProductDiscount(prodDiscId) on update cascade on delete cascade,
	productId int constraint fk_productIdDisc foreign key (productId) references TIWProduct(productId) on update cascade on delete cascade,
	remark varchar(255)
)
create table TACCCurrency(
	currencyId int identity primary key,
	currency varchar(50),
	currencySymbol varchar(5)
)
create table TACCCurrencyConverter(
	converterId int identity primary key,
	currencyId1 int constraint fk_currencyId1 foreign key (currencyId1) references TACCCurrency(currencyId) on update cascade on delete cascade,
	currencyId2 int constraint fk_currencyId2 foreign key (currencyId2) references TACCCurrency(currencyId) on update cascade on delete cascade,
	dateUpdated datetime,
	rate float
)
create table TACCSOHeader(
	soId int identity primary key,
	soNumber varchar(50),
	soDate datetime,
	empId int,
	customerId int,
	currencyId int,
	amount int,
	isVoid int,
	isInvoice int
)
create table TACCSODetail(
	soDetailId int identity primary key,
	soId int,
	productId int,
	soPriceItem int,
	qty int,
	itemPrice int
)
create table TACCInvoiceHeader(
	invoiceId int identity primary key,
	invoiceNumber varchar(50),
	soId int,
	amount int,
	currencyId int,
	invoiceDate datetime,
	isVoid int,
	isPaid int
)
create table TACCInvoiceDetail(
	invoiceDetailId int identity primary key,
	invoiceid int,
	productId int,
	itemPrice int,
	qty int
)
create table TACCJournalCategory(
	journalCatId int identity primary key,
	journalCat int
)
create table TACCJournalHeader(
	journalId int identity primary key,
	journalCode varchar(50),
	journalDate datetime,
	amount int,
	journalCatId int,
	currencyId int
)
create table TACCJournalDetail(
	journalDetailId int identity primary key,
	journalId int
)

create table TDMDocPattern(
	patternId int identity primary key,
	pattern varchar(50),
	fieldName varchar(50),
	tableName varchar(50)
)
