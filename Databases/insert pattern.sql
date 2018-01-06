insert into tdmdocpattern (tablename,fieldName,pattern) values('THRMEmp','empCode','ST');
insert into tdmdocpattern (tablename,fieldName,pattern) values('THRMEmpPosition','positionCode','STP');
insert into tdmdocpattern (tablename,fieldName,pattern) values('TCCCustomerCategory','custCatCode','CCU');
insert into tdmdocpattern (tablename,fieldName,pattern) values('TCCCustomer','customerCode','CCO');
insert into tdmdocpattern (tablename,fieldName,pattern) values('TCCCustomerDetail','custdDetailCode','CDC');
insert into tdmdocpattern (tablename,fieldName,pattern) values('TIWProductCategory','prodCatCode','PCC');
insert into tdmdocpattern (tablename,fieldName,pattern) values('TIWProduct','productCode','STP');
insert into tdmdocpattern (tablename,fieldName,pattern) values('TIWProdDiscount','prodDiscCode','PDC');
insert into tdmdocpattern (tablename,fieldName,pattern) values('TACCSOHeader','soNumber','SSO');
insert into tdmdocpattern (tablename,fieldName,pattern) values('TACCInvoiceHeader','invoiceNumber','SNV');

create trigger trigTHRMEmp on THRMEmp after insert
as
declare @id int,@tablename varchar(50),@pattern varchar(50)
set @tablename = 'THRMEmp'
select top 1 @id=empId from THRMEmp order by empId desc
select @pattern=pattern from TDMDocPattern where tablename=@tablename
update THRMEmp set empCode=@pattern+cast(@id as varchar(50)) where empId=@id	

drop trigger trigTHRMEmp
drop trigger trigTHRMEmpPosition
drop trigger trigTCCCustomerCategory
drop trigger trigTCCCustomerCategoryGroup
drop trigger trigTCCCustomer
drop trigger trigTIWProductCategoryGroup
drop trigger trigTCCCustomerDetail
drop trigger trigTIWProductCategory
drop trigger trigTIWProduct
drop trigger trigTIWProductDiscount
drop trigger trigTACCSOHeader
drop trigger trigTACCInvoiceHeader

create trigger trigTHRMEmpPosition on THRMEmpPosition after insert
as
declare @id int,@tablename varchar(50),@pattern varchar(50)
set @tablename = 'THRMEmpPosition'
select top 1 @id=positionId from THRMEmpPosition order by positionId desc
select @pattern=pattern from TDMDocPattern where tablename=@tablename
update THRMEmpPosition set positionCode=@pattern+cast(@id as varchar(50)) where positionId=@id	

create trigger trigTCCCustomerCategory on TCCCustomerCategory after insert
as
declare @id int,@tablename varchar(50),@pattern varchar(50)
set @tablename = 'TCCCustomerCategory'
select top 1 @id=custCatId from TCCCustomerCategory order by custCatId desc
select @pattern=pattern from TDMDocPattern where tablename=@tablename
update TCCCustomerCategory set custCatCode=@pattern+cast(@id as varchar(50))	

create trigger trigTCCCustomerCategoryGroup on TCCCustomerCategoryGroup after insert
as
declare @id int,@tablename varchar(50),@pattern varchar(50)
set @tablename = 'TCCCustomerCategoryGroup'
select top 1 @id=custCatGroupId from TCCCustomerCategoryGroup order by custCatGroupId desc
select @pattern=pattern from TDMDocPattern where tablename=@tablename
update TCCCustomerCategoryGroup set custCatGroupCode=@pattern+cast(@id as varchar(50)) where custCatGroupId=@id		

create trigger trigTCCCustomer on TCCCustomer after insert
as
declare @id int,@tablename varchar(50),@pattern varchar(50)
set @tablename = 'TCCCustomer'
select top 1 @id=customerId from TCCCustomer order by customerId desc
select @pattern=pattern from TDMDocPattern where tablename=@tablename
update TCCCustomer set customerCode=@pattern+cast(@id as varchar(50)) where customerId=@id		

create trigger trigTCCCustomerDetail on TCCCustomerDetail after insert
as
declare @id int,@tablename varchar(50),@pattern varchar(50)
set @tablename = 'TCCCustomerDetail'
select top 1 @id=custDetailId from TCCCustomerDetail order by custDetailId desc
select @pattern=pattern from TDMDocPattern where tablename=@tablename
update TCCCustomerDetail set custDetailCode=@pattern+cast(@id as varchar(50)) where custDetailId=@id		


create trigger trigTIWProductCategoryGroup on TIWProductCategoryGroup after insert
as
declare @id int,@tablename varchar(50),@pattern varchar(50)
set @tablename = 'TIWProductCategoryGroup'
select top 1 @id=prodCatGroupId from TIWProductCategoryGroup order by prodCatGroupId desc
select @pattern=pattern from TDMDocPattern where tablename=@tablename
update TIWProductCategoryGroup set prodCatGroupCode=@pattern+cast(@id as varchar(50)) where prodCatGroupId=@id		

create trigger trigTIWProductCategory on TIWProductCategory after insert
as
declare @id int,@tablename varchar(50),@pattern varchar(50)
set @tablename = 'TIWProductCategory'
select top 1 @id=prodCatId from TIWProductCategory order by prodCatId desc
select @pattern=pattern from TDMDocPattern where tablename=@tablename
update TIWProductCategory set prodCatCode=@pattern+cast(@id as varchar(50))	where prodCatId=@id	

create trigger trigTIWProduct on TIWProduct after insert
as
declare @id int,@tablename varchar(50),@pattern varchar(50)
set @tablename = 'TIWProduct'
select top 1 @id=productId from TIWProduct order by productId desc
select @pattern=pattern from TDMDocPattern where tablename=@tablename
update TIWProduct set productCode=@pattern+cast(@id as varchar(50))	where productId=@id

create trigger trigTIWProductDiscount on TIWProductDiscount after insert
as
declare @id int,@tablename varchar(50),@pattern varchar(50)
set @tablename = 'TIWProductDiscount'
select top 1 @id=prodDiscId from TIWProductDiscount order by prodDiscId desc
select @pattern=pattern from TDMDocPattern where tablename=@tablename
update TIWProductDiscount set prodDiscCode=@pattern+cast(@id as varchar(50)) where prodDiscId=@id	

create trigger trigTACCSOHeader on TACCSOHeader after insert
as
declare @id int,@tablename varchar(50),@pattern varchar(50)
set @tablename = 'TACCSOHeader'
select top 1 @id=soId from TACCSOHeader order by soId desc
select @pattern=pattern from TDMDocPattern where tablename=@tablename
update TACCSOHeader set soNumber=@pattern+cast(@id as varchar(50)) where soId=@id	

create trigger trigTACCInvoiceHeader on TACCInvoiceHeader after insert
as
declare @id int,@tablename varchar(50),@pattern varchar(50)
set @tablename = 'TACCInvoiceHeader'
select top 1 @id=invoiceId from TACCInvoiceHeader order by invoiceId desc
select @pattern=pattern from TDMDocPattern where tablename=@tablename
update TACCInvoiceHeader set invoiceNumber=@pattern+cast(@id as varchar(50)) where invoiceId=@id		



insert into THRMEmp (firstname,lastname,pass) values('Ryan','Prihantono','asdf')
insert into THRMEmp (firstname,lastname,pass) values('Fei','Li','asdf')
select * from thrmemp
select * from THRMEmpPosition
insert into THRMTrEmpPosition (empid,positionid) values(2,18)
insert into THRMTrEmpPosition (empid,positionid) values(3,19)
select * from THRMTrEmpPosition
delete from THRMEmpPosition
insert into THRMEmpPosition (position) values('Supervisor')
insert into THRMEmpPosition (position) values('Cashier')
insert into THRMEmpPosition (position) values('Operator')

select * from TDMDocPattern where tablename='THRMEmpPosition'