select empCode,pass,position from THRMEmp 
join THRMTrEmpPosition on THRMEmp.empId=THRMTrEmpPosition.empId
join THRMEmpPosition on THRMTrEmpPosition.positionId=THRMEmpPosition.positionId
where empCode='ST2' 

create view viewProductList
as
select productCode,product,prodcat,prodcatgroup,remark from TIWProduct 
left join TIWTrProductCategoryGroup on TIWProduct.trProdCatGroupId=TIWTrProductCategoryGroup.trProdCatGroupId
left join TIWProductCategoryGroup on TIWProductCategoryGroup.prodCatGroupId=TIWTrProductCategoryGroup.prodCatGroupId 
left join TIWProductCategory on TIWProductCategory.prodCatId=TIWTrProductCategoryGroup.prodCatid
left join TIWTrProdDiscount on TIWTrProdDiscount.productId=TIWProduct.productId
left join TIWProductDiscount on TIWProductDiscount.prodDiscId=TIWTrProdDiscount.prodDiscId

select * from TIWTrProductDiscount
select * from TIWProduct
delete from TIWProduct
insert into TIWProduct (product,price) values('Robotic Washing',42500)