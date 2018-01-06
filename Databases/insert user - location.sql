insert into THRMEmployee (firstName,lastName,pass) values('Ari','Ari','sato');

select * from THRMEmployee where firstName='Ari'

insert into THRMTrEmpPosition (employeeId,positionId) values(12,5)

insert into THRMTrEmployeeLocation (employeeId,locationId) values(12,2)

select * from THRMEmpPosition

insert into TCILocation 

select * from TCILocation 
join TCITrLocationGroup on TCILocation.locationId=TCITrLocationGroup.locationId
join TCILocationGroup on TCILocationGroup.locationGroupId=TCITrLocationGroup.locationGroupId
select * from TCITrLocationGroup
select * from TCILocationGroup

insert into TCILocation (location,locationAbbv,locationAddress,companyId) values('Brightwash 5 Otista','BW5 Otista','Jl. Otista Raya',1)

select * from TIWProduct
insert into TCITrLocationGroup (locationId,locationGroupId) values(6,2)

select * from THRMEmployee 
join THRMTrEmpPosition on THRMEmployee.employeeId=THRMTrEmpPosition.employeeId 
join THRMEmpPosition on THRMTrEmpPosition.positionId=THRMEmpPosition.positionId 
join THRMTrEmployeeLocation on THRMTrEmployeeLocation.employeeId=THRMEmployee.employeeId 
join TCILocation on TCILocation.locationId=THRMTrEmployeeLocation.locationId 
join TCITrLocationGroup on TCILocation.locationId=TCITrLocationGroup.locationId 
join TCILocationGroup on TCILocationGroup.locationGroupId=TCITrLocationGroup.locationGroupId 
where firstName='Tekhauw'
