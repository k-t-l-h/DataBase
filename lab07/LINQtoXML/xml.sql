	USE myDb
GO

SELECT 1 as Tag
	, NULL as Parent
	, CourierTable.Id as [Courier!1!Id!ID,element]
	, LTRIM(RTRIM(CourierTable.Name)) as [Courier!1!Name!element]
	, CourierTable.CurrentDelivery as [Courier!1!CurrentDelivery!element]
FROM CourierTable
ORDER BY [Courier!1!Id!ID,element]
FOR XML EXPLICIT, ROOT('Couriers')
GO