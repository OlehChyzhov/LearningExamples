SELECT OrderID, Sales,
CASE -- Start of the logic
	WHEN Sales > 50 THEN 'HIGH'
	WHEN Sales > 20 THEN 'MEDIUM'
	ELSE 'LOW'
END -- End of the logic
FROM Sales.Orders;


SELECT EmployeeID, FirstName, LastName,
CASE
	WHEN Gender = 'M' THEN 'Male'
	WHEN Gender = 'F' THEN 'Female'
END AS Gender,

-- Quick Format
CASE Gender
	WHEN 'M' THEN 'Male'
	WHEN 'F' THEN 'Female'
END AS Gender
FROM Sales.Employees;